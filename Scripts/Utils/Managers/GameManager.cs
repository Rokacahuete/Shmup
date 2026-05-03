using Godot;
using System;
using System.Collections.Generic;

// Author : Roka
public partial class GameManager : Node {

	// Consts

	// Enums
	public enum GameModes { None, Infinite, Waves };

	// Variables
	public static GameManager instance;

	public static Vector2 scrollLastMove = Vector2.Zero;
	public static RandomNumberGenerator rand = new RandomNumberGenerator();
	public static Vector2 screenSize;

	[Export] public Node2D gameContainer;
	
	private Level _currentLevel;
	private GameModes _gameMode = 0;
	public List<Enemy> LEnemies = new();

	// Delegates
	public delegate void Functions();
	private Functions _FunctionsToCall;
	public Functions OnRestart;

	// Functions
	public override void _Ready() {
		base._Ready();

		instance = this;

		rand.Randomize();
		screenSize = GetViewport().GetVisibleRect().Size;
	}

	public override void _Process(double pDelta) {
		float lDelta = (float)pDelta;

		_FunctionsToCall?.Invoke();

		base._Process(pDelta);
	}

	private void _SwitchGameMode(GameModes pMode) {
		_gameMode = pMode;
		
		switch ((int)_gameMode) {
			case 0: 
				_FunctionsToCall = null;
				break;
			case 1: 
				_FunctionsToCall = _InfiniteMode;
				break;
			case 2:
				_FunctionsToCall = _WavesMode;
				break;
		} 
	}

	public void CreateEnemy(Enemy pEnemy) {
		LEnemies.Add(pEnemy);
		pEnemy.OnDied += _RemoveEnemy;
	}

	private void _InstanciateEnemyGroup(int pGroup) {
		pGroup = pGroup.MinMax(0, _currentLevel.AEnemyGroups.Length - 1);
		Node2D lGroup = _currentLevel.AEnemyGroups[pGroup].Instantiate<Node2D>();
		gameContainer.AddChild(lGroup);

		foreach (Enemy lEnemy in lGroup.GetChildren())
			CreateEnemy(lEnemy);
	}
	
	private void _RemoveEnemy(Entity pEnemy) {
		LEnemies.Remove((Enemy)pEnemy);
	}

	public void StartGame(Level pLevel) {
		_currentLevel = pLevel;
		_SwitchGameMode(pLevel.gameMode);

		Player.instance.SetActive(false);
		MenusManager.Switch();
	}

	public void StopGame(bool pIsWin) {
		if (pIsWin) Datas.score += _currentLevel.score;
		GD.Print($"Fin de partie. Nouveau score : {Datas.score} !");

		MenusManager.Switch(MenusManager.Menus.LevelSelector);
		Player.instance.SetActive(true);
		foreach (Enemy lEnemy in LEnemies.ToArray()) lEnemy.Die();
		_FunctionsToCall = null;
	}

	public void Restart() {
		StopGame(false);
		OnRestart?.Invoke();
	}
	
	// Game modes
	private void _InfiniteMode() {
		if (LEnemies.Count != 0) return;

		_InstanciateEnemyGroup(rand.RandiRange(0, _currentLevel.AEnemyGroups.Length - 1));
    }

	private void _WavesMode() {
		if (LEnemies.Count != 0) return;

		if (++_currentLevel.wave >= _currentLevel.AEnemyGroups.Length) StopGame(true);
		else _InstanciateEnemyGroup(_currentLevel.wave);
	}

	// Events
}

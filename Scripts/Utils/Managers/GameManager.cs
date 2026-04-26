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

	[Export] private PackedScene[] _AEnemyGroupScenes = new PackedScene[0];
	[Export] public Node2D gameContainer;

	[Export(PropertyHint.Enum, "None, Infinite, Waves")] private GameModes _gameMode = 0;

	public List<Enemy> LEnemies = new();
	public int currentWave = -1;

	// Delegates
	public delegate void Functions();
	private Functions _FunctionsToCall;
	public Functions OnRestart;

	// Functions
	public override void _Ready() {
		base._Ready();

		instance = this;

		rand.Randomize();
		if (_AEnemyGroupScenes.Length <= 0) return;
		
		MenusManager.Setup(MenusManager.Menus.None, null);
		_SwitchGameMode(_gameMode);
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
		pGroup = pGroup.MinMax(0, _AEnemyGroupScenes.Length - 1);
		Node2D lGroup = _AEnemyGroupScenes[pGroup].Instantiate<Node2D>();
		gameContainer.AddChild(lGroup);

		foreach (Enemy lEnemy in lGroup.GetChildren())
			CreateEnemy(lEnemy);
	}
	
	private void _RemoveEnemy(Entity pEnemy) {
		LEnemies.Remove((Enemy)pEnemy);
	}

	public void StartGame(GameModes pMode, PackedScene[] pAEnemyGroups) {
		_AEnemyGroupScenes = pAEnemyGroups;
		currentWave = -1;
		_SwitchGameMode(pMode);

		Player.instance.SetActive(false);
		MenusManager.Switch();
	}

	public void StopGame() {
		MenusManager.Switch(MenusManager.Menus.LevelSelector);
		Player.instance.SetActive(true);
		foreach (Enemy lEnemy in LEnemies.ToArray()) lEnemy.Die();
		_FunctionsToCall = null;
	}

	public void Restart() {
		StopGame();
		OnRestart?.Invoke();
	}
	
	// Game modes
	private void _InfiniteMode() {
		if (LEnemies.Count != 0) return;

		_InstanciateEnemyGroup(rand.RandiRange(0, _AEnemyGroupScenes.Length - 1));
    }

	private void _WavesMode() {
		if (LEnemies.Count != 0) return;

		if (++currentWave >= _AEnemyGroupScenes.Length) StopGame();
		else _InstanciateEnemyGroup(currentWave);
	}

	// Events
}

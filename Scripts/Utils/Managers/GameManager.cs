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
	[Export] private int _infiniteModeDefaultScore = 0;
	[Export] private int _infiniteModeIncreaseScore = 0;

	[Export] private PackedScene _xpOrbScene = null, _scoreOrbScene = null;
	
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

		if (LEnemies.Count != 0) return;
		_FunctionsToCall?.Invoke();
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
		Enemy lEnemy = (Enemy)pEnemy;

		LEnemies.Remove(lEnemy);

		if (_xpOrbScene == null) return;
		XPOrb lOrb;
		for (int i = 0; i < lEnemy.xpOnKilled; i++) {
			lOrb = _xpOrbScene.Instantiate<XPOrb>();

			lOrb.GlobalPosition = lEnemy.GlobalPosition;
			lOrb.Rotation = MyMaths.RandomAngle();
			gameContainer.CallDeferred(Node.MethodName.AddChild, lOrb);
		}
	}

	public void StartGame(Level pLevel) {
		_currentLevel = pLevel;
		_SwitchGameMode(pLevel.gameMode);

		Player.instance.SetActive(false);
		MenusManager.Switch(MenusManager.Menus.HUD);
	}

	public void StopGame(bool pIsWin) {
		if (pIsWin && _scoreOrbScene != null) {
			ScoreOrb lOrb;
			for (int i = 0; i < _currentLevel.score; i++) {
				lOrb = _scoreOrbScene.Instantiate<ScoreOrb>();

				lOrb.GlobalPosition = screenSize * .5f;
				lOrb.Rotation = MyMaths.RandomAngle();
				gameContainer.AddChild(lOrb);
			}
		}

		MenusManager.Switch(MenusManager.Menus.LevelSelector);
		Player.instance.SetActive(true);
		foreach (Enemy lEnemy in LEnemies.ToArray()) {
			LEnemies.Remove(lEnemy);
			lEnemy.QueueFree();
		}
		_FunctionsToCall = null;
	}

	public void Restart() {
		StopGame(false);
		OnRestart?.Invoke();
	}
	
	// Game modes
	private void _InfiniteMode() {
		_InstanciateEnemyGroup(rand.RandiRange(0, _currentLevel.AEnemyGroups.Length - 1));

		if (_currentLevel.wave >= 0 && _scoreOrbScene != null) {
			ScoreOrb lOrb;
			int lNOrbs = _infiniteModeDefaultScore + _infiniteModeIncreaseScore * _currentLevel.wave;
			for (int i = 0; i < lNOrbs; i++) {
				lOrb = _scoreOrbScene.Instantiate<ScoreOrb>();

				lOrb.GlobalPosition = screenSize * .5f;
				lOrb.Rotation = MyMaths.RandomAngle();
				gameContainer.AddChild(lOrb);
			}
		}
		++_currentLevel.wave;
    }

	private void _WavesMode() {
		if (++_currentLevel.wave >= _currentLevel.AEnemyGroups.Length) StopGame(true);
		else _InstanciateEnemyGroup(_currentLevel.wave);
	}

	// Events
}

using Godot;
using System;
using System.Collections.Generic;

// Author : Roka
public partial class GameManager : Node {

	// Consts

	// Enums
	public enum GameModes { None, Infinite, Waves };
	private Functions[] _AGameModesFunctions;

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
	private Functions _GameMode;
	public Functions OnWaveEnd, OnRestart, OnGameEnd;

	// Functions
	public override void _Ready() {
		base._Ready();

		rand.Randomize();
		screenSize = GetViewport().GetVisibleRect().Size;

		instance = this;
		_AGameModesFunctions = new Functions[3] { null, _InfiniteMode, _WavesMode };
	}

	private void _SwitchGameMode(GameModes pMode) {
		int lMode = (int)pMode;
		if (lMode < 0 || lMode >= _AGameModesFunctions.Length) lMode = 0;

		_GameMode = _AGameModesFunctions[lMode];
		_GameMode?.Invoke();
	}

	public void CreateEnemy(Enemy pEnemy) {
		LEnemies.Add(pEnemy);
		pEnemy.OnDied += _RemoveEnemy;
	}

	private void _CreateOrbs(PackedScene pScene, int pNOrbs, Vector2 pPosition) {
		if (pScene == null) return;

		Orb lOrb;
		for (int i = 0; i < pNOrbs; i++) {
			lOrb = pScene.Instantiate<Orb>();

			lOrb.GlobalPosition = pPosition;
			lOrb.Rotation = MyMaths.RandomAngle();
			gameContainer.CallDeferred(Node.MethodName.AddChild, lOrb);
		}
	}

	private void _InstanciateEnemyGroup(int pGroup) {
		pGroup = pGroup.MinMax(0, _currentLevel.AEnemyGroups.Length - 1);
		Node2D lGroup = _currentLevel.AEnemyGroups[pGroup].Instantiate<Node2D>();
		gameContainer.CallDeferred(MethodName.AddChild, lGroup);

		foreach (Entity lEntity in lGroup.GetChildren())
			if (lEntity is Enemy lEnemy) CreateEnemy(lEnemy);
	}
	
	private void _RemoveEnemy(Entity pEnemy) {
		Enemy lEnemy = (Enemy)pEnemy;
		LEnemies.Remove(lEnemy);

		_CreateOrbs(_xpOrbScene, lEnemy.xpOnKilled, lEnemy.GlobalPosition);

		if (LEnemies.Count != 0) return;
		_GameMode?.Invoke();
	}

	public void StartGame(Level pLevel) {
		_currentLevel = pLevel;
		_SwitchGameMode(_currentLevel.gameMode);

		Player.instance.SetInactive(false);
		MenusManager.Switch(MenusManager.Menus.HUD);
	}

	public void StopGame(bool pIsWin) {
		OnGameEnd?.Invoke();

		if (pIsWin) _CreateOrbs(_scoreOrbScene, _currentLevel.score, screenSize * .5f);

		MenusManager.Switch(MenusManager.Menus.LevelSelector);
		Player.instance.SetInactive(true);
		foreach (Enemy lEnemy in LEnemies.ToArray()) {
			LEnemies.Remove(lEnemy);
			lEnemy.QueueFree();
		}
		_GameMode = null;
	}

	public void Restart() {
		StopGame(false);
		OnRestart?.Invoke();
	}
	
	// Game modes
	private void _InfiniteMode() {
		OnWaveEnd?.Invoke();
		_InstanciateEnemyGroup(rand.RandiRange(0, _currentLevel.AEnemyGroups.Length - 1));

		int lNOrbs = _infiniteModeDefaultScore + _infiniteModeIncreaseScore * _currentLevel.wave;
		if (_currentLevel.wave >= 0) _CreateOrbs(_scoreOrbScene, lNOrbs, screenSize * .5f);
		++_currentLevel.wave;
    }

	private void _WavesMode() {
		OnWaveEnd?.Invoke();
		if (++_currentLevel.wave >= _currentLevel.AEnemyGroups.Length) StopGame(true);
		else _InstanciateEnemyGroup(_currentLevel.wave);
	}

	// Events
}

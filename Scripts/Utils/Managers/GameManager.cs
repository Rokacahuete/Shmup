using Godot;
using System;
using System.Collections.Generic;

// Author : Roka
public partial class GameManager : Node {

	// Consts

	// Variables
	public static Vector2 scrollLastMove = Vector2.Zero;
	public static RandomNumberGenerator rand = new RandomNumberGenerator();

	[Export] private PackedScene[] _AEnemyGroupScenes = new PackedScene[0];
	[Export] private Node2D _gameContainer;

	public List<Enemy> LEnemies = new();

	// Delegates
	private delegate void Functions();
	private Functions _FunctionsToCall;

	// Functions
	public override void _Ready() {
		base._Ready();

		rand.Randomize();
		if (_AEnemyGroupScenes.Length >= 0) _FunctionsToCall += _SpawnEnemyGroup;
	}

	public override void _Process(double pDelta) {
		float lDelta = (float)pDelta;

		_FunctionsToCall?.Invoke();

		base._Process(pDelta);
	}
	
	private void _SpawnEnemyGroup() {
		if (LEnemies.Count != 0) return;

		Node2D lGroup = _AEnemyGroupScenes[rand.RandiRange(0, _AEnemyGroupScenes.Length - 1)].Instantiate<Node2D>();
		_gameContainer.AddChild(lGroup);

		foreach (Enemy lEnemy in lGroup.GetChildren()) {
            LEnemies.Add(lEnemy);
			lEnemy.OnDied += _RemoveEnemy;
        }
    }
	
	private void _RemoveEnemy(Entity pEnemy) {
		LEnemies.Remove((Enemy)pEnemy);
	}
	
	// Events
}

using Godot;
using System;

// Author : Roka
public partial class ScoreOrb : Entity {

	// Consts

	// Variables
	[Export] private int _scoreAmount = 0;
	[Export] private float _rotationMultiplicator = 0f;

	// Functions
	public override void _Ready() {
		base._Ready();

		OnDied += _Keep;
	}

	public override void _Process(double pDelta) {
		float lDelta = (float)pDelta;
		Rotation += GetAngleTo(Player.instance.GlobalPosition) * _rotationMultiplicator * lDelta;

		base._Process(pDelta);
	}

	private void _Keep(Entity pEntity) {
		Datas.UpdateScore(_scoreAmount);
		OnDied -= _Keep;
	}

	// Events
}
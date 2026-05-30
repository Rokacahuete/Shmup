using Godot;
using System;

// Author : Roka
public partial class Crow : Enemy {
	
	// Consts

	// Variables
	[Export] private float _rotationMultiplicator = 0f;
	[Export] private Vector2 _target = Vector2.Zero;

	// Functions
	public override void _Ready() {
		base._Ready();

		_target *= GameManager.screenSize;
	}

	public override void _Process(double pDelta) {
		float lDelta = (float)pDelta;
		Rotation += GetAngleTo(_target) * _rotationMultiplicator * lDelta;

		base._Process(pDelta);
	}
	
	// Events
}
using Godot;
using System;

// Author : Roka
public partial class Crow : Node2D {
	
	// Consts

	// Variables
	[Export] private float _rotationMultiplicator = 0f;

	// Functions
	public override void _Ready() {
	}

	public override void _Process(double pDelta) {
		float lDelta = (float)pDelta;
		Rotation += GetAngleTo(Player.instance.GlobalPosition) * _rotationMultiplicator * lDelta;
	}
	
	// Events
}
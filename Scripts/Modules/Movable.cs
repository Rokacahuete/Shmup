using Godot;
using System;

// Author : Roka
public partial class Movable : Module {
	
	// Consts

	// Variables
	[Export] public Vector2 direction = Vector2.Zero;
	[Export] public float speed = 0f;

	// Functions
	public override void _Process(double pDelta) {
		float lDelta = (float)pDelta;

        nodeToAffect.Position += direction.Normalized() * speed * lDelta;
	}
	
	// Events
}
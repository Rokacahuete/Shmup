using Godot;
using System;

// Author : Roka
public partial class GameManager : Node {
	
	// Consts

	// Variables
	public static Vector2 scrollLastMove = Vector2.Zero;

	// Functions
	public override void _Ready() {
		base._Ready();
	}

	public override void _Process(double pDelta) {
		float lDelta = (float)pDelta;

		base._Process(pDelta);
	}
	
	// Events
}

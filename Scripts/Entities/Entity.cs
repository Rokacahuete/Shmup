using Godot;
using System;

// Author : Roka
public partial class Entity : Node2D {
	
	// Consts

	// Variables

	// Functions
	public override void _Ready() {
		base._Ready();
	}

	public override void _Process(double pDelta) {
		float lDelta = (float)pDelta;

		base._Process(pDelta);
	}

	public void Hurt() {
		GD.Print(Name + " was hurted");
	}
	
	// Events
}

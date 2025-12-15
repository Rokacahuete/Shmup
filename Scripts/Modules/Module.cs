using Godot;
using System;

// Author : Roka
public partial class Module : Node {
	
	// Consts

	// Variables
	[Export] protected Node2D nodeToAffect;

	// Functions
	public override void _Ready() {
        if (nodeToAffect == null) QueueFree();

		base._Ready();
    }

	public override void _Process(double pDelta) {
		float lDelta = (float)pDelta;

		base._Process(pDelta);
	}
	
	// Events
}
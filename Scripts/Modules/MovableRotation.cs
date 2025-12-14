using Godot;
using System;

// Author : Roka
public partial class MovableRotation : Module {
	
	// Consts

	// Variables
	[Export] public float speed = 0f;

	// Functions
	public override void _Process(double pDelta) {
		float lDelta = (float)pDelta;

        nodeToAffect.Position += nodeToAffect.Rotation.FromAngleToVector() * speed * lDelta;
	}
	
	// Events
}
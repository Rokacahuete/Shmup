using Godot;
using System;

// Author : Roka
public partial class MovableCustom : Module {
	
	// Consts

	// Variables
	[Export] public Vector2 direction = Vector2.Zero;
	[Export] public float speed = 0f;

	public float movingStrenght = 0f;

	// Functions
	public override void _Process(double pDelta) {
		float lDelta = (float)pDelta;

        _Move(lDelta);

		base._Process(pDelta);
	}

	private void _Move(float pDelta) {
		if (IsStopped()) return;
		
		nodeToAffect.Position += direction.Normalized() * movingStrenght * speed * pDelta;
	}
	
	// Events
}
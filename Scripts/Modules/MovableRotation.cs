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

        _Move(lDelta);

		base._Process(pDelta);
	}

	private void _Move(float pDelta) {
		if (IsStopped()) return;
		
		nodeToAffect.Position += nodeToAffect.Rotation.FromAngleToVector() * speed * pDelta;
		_CheckOutOfLimits();
	}

	private void _CheckOutOfLimits() {
		if (nodeToAffect is Entity lEntity && (lEntity.GlobalPosition.Y >= GameManager.screenSize.Y * 1.4f || lEntity.GlobalPosition.Y < -100f)) {
			if (lEntity is Enemy lEnemy) lEnemy.xpOnKilled = 0;
			lEntity.Die();
		}
	}
	
	// Events
}
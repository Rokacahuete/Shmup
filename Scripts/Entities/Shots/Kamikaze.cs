using Godot;
using System;

// Author : Roka
public partial class Kamikaze : Shot {

	// Consts

	// Variables
	[Export] private Shooter _shooterModule = null;

	// Functions
	public override void _Ready() {
		base._Ready();
	}

	public override void _Process(double pDelta) {
		float lDelta = (float)pDelta;

		base._Process(pDelta);
	}

    public override void Die() {
		_shooterModule?.Shoot();

        base.Die();
    }


	// Events
}

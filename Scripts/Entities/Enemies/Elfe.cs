using Godot;
using System;

// Author : Roka
public partial class Elfe : Enemy {

	// Consts

	// Variables
	[Export] private Shooter[] _AShooters = new Shooter[0];
	[Export] private Movable _movableModule = null;

	// Functions
	public override void _Ready() {
		base._Ready();
	}

	public override void _Process(double pDelta) {
		float lDelta = (float)pDelta;

		base._Process(pDelta);
	}

    public override void Die() {
        base.Die();

		Shooter lShooter;
		int lLength = _AShooters.Length;
		for (int i = 0; i < lLength; i++) {
			lShooter = _AShooters[i];
			if (lShooter.Shoot() is Elfe lNext && lNext._movableModule != null)
				lNext._movableModule.distanceTime += lShooter.timeBetweenShots * i * .5f;
		}
    }

    // Events
}

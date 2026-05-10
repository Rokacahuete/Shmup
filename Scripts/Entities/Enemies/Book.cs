using Godot;
using System;

// Author : Roka
public partial class Book : Enemy {
	
	// Consts

	// Variables

	// Functions
	public override void _Ready() {
		base._Ready();

		this.GetModule<Shooter>().OnShoot += _OnShoot;
	}

	public override void _Process(double pDelta) {
		float lDelta = (float)pDelta;

		base._Process(pDelta);
	}

	private void _SettingEnemy(Enemy pEnemy) {
		pEnemy.xpOnKilled = 0;
	}
	
	// Events
	private void _OnShoot(Node2D pEnemy) {
		if (pEnemy is Enemy lEnemy) _SettingEnemy(lEnemy);
	}
}

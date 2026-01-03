using Godot;
using System;

// Author : Roka
public partial class Damager : Module {
	
	// Consts

	// Variables
	[Export] public int damage = 0;

	// Functions
	public override void _Ready() {
		base._Ready();
	}

	public override void _Process(double pDelta) {
		float lDelta = (float)pDelta;

		base._Process(pDelta);
	}

	public void Hurt() {
		if (nodeToAffect is Entity lEntity)
			lEntity.Hurt(new Damager());
	}
	
	// Events
}

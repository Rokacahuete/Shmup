using Godot;
using System;

// Author : Roka
public partial class XPOrb : Orb {

	// Consts

	// Variables
	[Export] private int _xpAmount = 0;

	// Functions
	public override void _Ready() {
		base._Ready();
	}

	public override void _Process(double pDelta) {
		float lDelta = (float)pDelta;

		base._Process(pDelta);
	}

	protected override void Keep(Entity pEntity) {
		Datas.AddXp(_xpAmount);
		base.Keep(pEntity);
	}

	// Events
}

using Godot;
using System;

// Author : Roka
public partial class Shield : Enemy {

	// Consts

	// Variables
	[Export] private Color _startColor = new Color(1f, 1f, 1f, .5f),
		_endColor = new Color(0f, 0f, 0f, .5f);

	// Functions
	public override void _Ready() {
		base._Ready();

		Modulate = _startColor;
	}

	public override void _Process(double pDelta) {
		float lDelta = (float)pDelta;

		base._Process(pDelta);
	}

    public override void Hurt(Damager pDamager) {
		float lRatio = (float)health / maxHealth;
		Modulate = _startColor * lRatio + _endColor * (1f - lRatio);
        base.Hurt(pDamager);
    }

	// Events
}

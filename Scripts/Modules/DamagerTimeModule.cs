using Godot;
using System;

// Author : Roka
public partial class DamagerTimeModule : Damager {

	// Consts

	// Variables
	[Export] private float _timeBetweenDamages = 0f;
	[Export] private float _timer = 0f;
	
	private float _timeBeforeNextDamage;

	// Functions
	public override void _Ready() {
		if (nodeToAffect is not Entity) QueueFree();

		base._Ready();

		_timeBeforeNextDamage = _timer;
	}

	public override void _Process(double pDelta) {
		float lDelta = (float)pDelta;

		_Damage(lDelta);

		base._Process(pDelta);
	}

	private void _Damage(float pDelta) {
		if (IsStopped()) return;

		_timeBeforeNextDamage -= pDelta;
		if (_timeBeforeNextDamage >= 0f) return;

		_timeBeforeNextDamage = _timeBetweenDamages;
		((Entity)nodeToAffect)?.Hurt(this);
	}

	// Events
}

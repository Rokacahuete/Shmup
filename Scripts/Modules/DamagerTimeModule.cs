using Godot;
using System;

// Author : Roka
public partial class DamagerTimeModule : Damager {

	// Consts

	// Variables
	[Export] private float _timeBetweenDamages = 0f;
	[Export] private float _timer = 0f;
	
	private TimeManager.Timeout _currentTimer;

	// Functions
	public override void _Ready() {
		if (nodeToAffect is not Entity) QueueFree();

		base._Ready();

		((Entity)nodeToAffect).OnDied += _Stop;
		TimeManager.SetTimeout(_Damage, _timer);
	}

	public override void _Process(double pDelta) {
		float lDelta = (float)pDelta;

		base._Process(pDelta);
	}

	private void _Damage() {
		if (IsStopped()) return;

		_currentTimer = TimeManager.SetTimeout(_Damage, _timeBetweenDamages);
		((Entity)nodeToAffect)?.Hurt(this);
	}

	private void _Stop(Entity pEntity) {
		((Entity)nodeToAffect).OnDied -= _Stop;
		_currentTimer.Dispose();
	}

	// Events
}

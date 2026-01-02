using Godot;
using System;

// Author : Roka
public partial class Entity : Node2D {
	
	// Consts

	// Variables
	[Export] private int _maxHealth = 0;
	private int _health;

	// Delegates
	public delegate void OnDiedEventHandler(Entity pEntity);
	public OnDiedEventHandler OnDied;

	// Functions
	public override void _Ready() {
		_health = _maxHealth;
		base._Ready();
	}

	public override void _Process(double pDelta) {
		float lDelta = (float)pDelta;

		base._Process(pDelta);
	}

	public void Hurt(Damager pDamager) {
		_health -= pDamager.damage;

		if (_health <= 0) Die();
	}

	public void Die() {
		QueueFree();
		OnDied?.Invoke(this);
	}
	
	// Events
}

using Godot;
using System;

// Author : Roka
public partial class Entity : Node2D {
	
	// Consts

	// Variables
	[Export] protected int maxHealth = 0;
	protected int health;

	// Delegates
	public delegate void OnDiedEventHandler(Entity pEntity);
	public OnDiedEventHandler OnDied, OnHurted;

	// Functions
	public override void _Ready() {
		base._Ready();

		health = maxHealth;
		
		if (Visible) {
			Visible = false;
			TimeManager.SetTimeout(() => SetDeferred(PropertyName.Visible, true));
		}
	}

	public override void _Process(double pDelta) {
		float lDelta = (float)pDelta;

		base._Process(pDelta);
	}

	public virtual void Hurt(Damager pDamager) {
		health -= pDamager.damage;

		if (health <= 0) Die();
		else OnHurted?.Invoke(this);
	}

	public virtual void Die() {
		if (IsInstanceValid(this)) QueueFree();
		OnDied?.Invoke(this);
	}
	
	// Events
}

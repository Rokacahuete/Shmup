using Godot;
using System;

// Author : Roka
public partial class MiniPlayer : Entity {

	// Consts

	// Variables
	[Export] private ProgressBar _lifeBar = null;

	// Functions
	public override void _Ready() {
		base._Ready();

		GameManager.instance.OnGameEnd += Die;
	}

	public override void _Process(double pDelta) {
		float lDelta = (float)pDelta;

		base._Process(pDelta);
		UpdateLifeBar();
	}

	public void UpdateLifeBar() {
		if (_lifeBar == null) return;

		_lifeBar.MaxValue = maxHealth;
		_lifeBar.Value = health;
	}

    public override void Hurt(Damager pDamager) {
        base.Hurt(pDamager);

		UpdateLifeBar();
    }

    public override void Die() {
		GameManager.instance.OnGameEnd -= Die;

        base.Die();
    }

	// Events
}

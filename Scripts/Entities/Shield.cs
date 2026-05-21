using Godot;
using System;
using System.Collections.Generic;

// Author : Roka
public partial class Shield : Entity {

	// Consts

	// Variables
	[Export] private Godot.Collections.Array<Entity> _LLinkedEntities = new();
	[Export] private Color _startColor = new Color(1f, 1f, 1f, .5f),
		_endColor = new Color(0f, 0f, 0f, .5f);

	// Functions
	public override void _Ready() {
		base._Ready();

		Modulate = _startColor;

		GameManager.instance.OnWaveEnd += Die;
		foreach (Entity lEntity in _LLinkedEntities) 
			lEntity.OnDied += _OnEntityDie;
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

    public override void Die() {
		GameManager.instance.OnWaveEnd -= Die;
        base.Die();
    }

	// Events
	private void _OnEntityDie(Entity pEntity) {
		_LLinkedEntities.Remove(pEntity);
		if (_LLinkedEntities.Count == 0) Die();
	}
}

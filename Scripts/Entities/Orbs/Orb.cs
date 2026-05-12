using Godot;
using System;

// Author : Roka
public partial class Orb : Entity {

	// Consts

	// Variables
	[Export] private float _rotationMultiplicator = 0f;
	[Export] private Vector2 _target = GameManager.screenSize * .5f;

	// Functions
	public override void _Ready() {
		base._Ready();

		OnDied += Keep;
	}

	public override void _Process(double pDelta) {
		float lDelta = (float)pDelta;

		Rotation += GetAngleTo(_target) * _rotationMultiplicator * lDelta;
		if (GlobalPosition.Y < _target.Y) Die();
		base._Process(pDelta);
	}

	protected virtual void Keep(Entity pEntity) {
		OnDied -= Keep;
	}

	// Events
}

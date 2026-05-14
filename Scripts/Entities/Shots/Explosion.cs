using Godot;
using System;

// Author : Roka
public partial class Explosion : Enemy {

	// Consts

	// Variables
	[Export] private CollisionShape2D _collisionShape = null;
	[Export(PropertyHint.Range, ".1f,2f,or_greater")] private float _explosionTime = .1f;
	[Export] private float _timeBeforeDisapear = 0f;
	[Export] private float _radius = 1f;

	private float _timer = 0f;

	// Functions
	public override void _Ready() {
		base._Ready();

		Scale = Vector2.Zero;
		_timeBeforeDisapear += _explosionTime;
	}

	public override void _Process(double pDelta) {
		float lDelta = (float)pDelta;

		_timer += lDelta;
		Scale = Vector2.One * _radius * MyMaths.Min(_timer / _explosionTime, 1f);

		if (_timer >= _timeBeforeDisapear) Die();

		base._Process(pDelta);
	}

    public override void Hurt(Damager pDamager) {
		if (_collisionShape != null) _collisionShape.SetDeferred(CollisionShape2D.PropertyName.Disabled, true);
        base.Hurt(pDamager);
    }

	// Events
}

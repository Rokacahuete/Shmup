using Godot;
using System;

// Author : Roka
public partial class Movable : Module {
	
	// Consts

	// Variables
	[Export(PropertyHint.Enum, "Line,Sinusoidal,Circle")] private int _patternType = 0;
	[Export] private Vector2 _direction = Vector2.Zero;
	[Export] private float _speed = 0f;
	[Export] public float distanceTime = 0f;

	public Vector2 initPos;

    // Functions
    public override void _Ready() {
        base._Ready();
		
		initPos = nodeToAffect.Position;
    }

	public override void _Process(double pDelta) {
		float lDelta = (float)pDelta;

		_Move(lDelta);

		base._Process(pDelta);
	}

	private void _Move(float pDelta) {
		if (IsStopped()) return;
		
		distanceTime += pDelta * _speed;
        nodeToAffect.Position = initPos + Patterns.GetPosition(_patternType, distanceTime, _direction);
		_CheckOutOfLimits();
	}

	public void Move(Vector2 pDirection) {
		initPos += pDirection;
	}

	public void Skip(float pTimeToSkip) {
		distanceTime += pTimeToSkip;
	}

	private void _CheckOutOfLimits() {
		if (nodeToAffect is Entity lEntity && (lEntity.GlobalPosition.Y >= GameManager.screenSize.Y * 1.4f || lEntity.GlobalPosition.Y < 0f)) {
			if (lEntity is Enemy lEnemy) lEnemy.xpOnKilled = 0;
			lEntity.Die();
		}
	}
	
	// Events
}
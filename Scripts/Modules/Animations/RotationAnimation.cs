using Godot;
using System;

// Author : Roka
public partial class RotationAnimation : Animation {

	// Consts

	// Variables
	[Export] private float _angle;

	private float _baseRotation;

	// Functions
	public override void _Ready() {
		base._Ready();
	}

	public override void _Process(double pDelta) {
		float lDelta = (float)pDelta;
		
		nodeToAffect.RotationDegrees = _baseRotation * cosRatio + _angle * (1f - cosRatio);

		base._Process(pDelta);
	}

    public override void StartAnimation() {
		if (IsStopped()) return;
        base.StartAnimation();

		_baseRotation = nodeToAffect.RotationDegrees;
    }

    public override void StopAnimation() {
		base.StopAnimation();
    }

	// Events
}

using Godot;
using System;

// Author : Roka
public partial class ScaleAnimation : Animation {

	// Consts

	// Variables
	[Export] public Vector2 rescale = Vector2.Zero;

	private Vector2 _baseScale;

	// Functions
	public override void _Ready() {
		base._Ready();
	}

	public override void _Process(double pDelta) {
		float lDelta = (float)pDelta;
		
		nodeToAffect.Scale = _baseScale * cosRatio + rescale * (1f - cosRatio);

		base._Process(pDelta);
	}

    public override void StartAnimation() {
		if (IsStopped()) return;
        base.StartAnimation();

		_baseScale = nodeToAffect.Scale;
    }

    public override void StopAnimation() {
		base.StopAnimation();
		nodeToAffect.Scale = _baseScale;
    }

	// Events
}

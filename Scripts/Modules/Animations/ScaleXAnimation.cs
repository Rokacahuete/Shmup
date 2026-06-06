using Godot;
using System;

// Author : Roka
public partial class ScaleXAnimation : Animation {
	
	// Consts

	// Variables
	[Export] private float _rescale = 0f;

	private float _baseScaleX;

	// Functions
	public override void _Ready() {
		base._Ready();
	}

	public override void _Process(double pDelta) {
		float lDelta = (float)pDelta;
		
		_ChangeScaleX(_baseScaleX * ratio + _rescale * (1f - ratio));

		base._Process(pDelta);
	}

	private void _ChangeScaleX(float pX) {
		nodeToAffect.Scale = new Vector2(pX, nodeToAffect.Scale.Y);
	}

    public override void StartAnimation() {
		if (IsStopped()) return;
        base.StartAnimation();

		_baseScaleX = nodeToAffect.Scale.X;
    }

    public override void StopAnimation() {
		base.StopAnimation();
		_ChangeScaleX(_baseScaleX);
    }
	
	// Events
}

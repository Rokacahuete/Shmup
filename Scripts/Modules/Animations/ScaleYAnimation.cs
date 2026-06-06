using Godot;
using System;

// Author : Roka
public partial class ScaleYAnimation : Animation {
	
	// Consts

	// Variables
	[Export] private float _rescale = 0f;

	private float _baseScaleY;

	// Functions
	public override void _Ready() {
		base._Ready();
	}

	public override void _Process(double pDelta) {
		float lDelta = (float)pDelta;

		_ChangeScaleY(_baseScaleY * ratio + _rescale * (1f - ratio));

		base._Process(pDelta);
	}

	private void _ChangeScaleY(float pY) {
		nodeToAffect.Scale = new Vector2(nodeToAffect.Scale.X, pY);
	}

    public override void StartAnimation() {
		if (IsStopped()) return;
        base.StartAnimation();

		_baseScaleY = nodeToAffect.Scale.Y;
    }

    public override void StopAnimation() {
		base.StopAnimation();
		_ChangeScaleY(_baseScaleY);
    }
	
	// Events
}

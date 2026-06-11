using Godot;
using System;

// Author : Roka
public partial class Competence : Module {

	// Consts

	// Variables
	[Export] private Animation[] _AAnimations = new Animation[0];
	[Export] public float cooldown = 0f;
	[Export] private float _timer = 0f;

	protected Vector2 position = Vector2.Zero;

	// Functions
	public override void _Ready() {
		base._Ready();
	}

	public override void _Process(double pDelta) {
		float lDelta = (float)pDelta;

		base._Process(pDelta);
	}

	public virtual void Active(Vector2 pPosition) {
		position = pPosition;

		foreach (Animation lAnim in _AAnimations) lAnim.StartAnimation();
		TimeManager.SetTimeout(Action, _timer);
	}
	
	protected virtual void Action() {}

	// Events
}

using Godot;
using System;

// Author : Roka
public partial class Animation : Module {

	// Consts

	// Variables
	[Export] public float duration = 0f;
	
	protected float time = 0f;
	protected float ratio => time / duration;
	protected float cosRatio => Mathf.Abs(Mathf.Cos(ratio * Mathf.Pi));

	// Functions
	public override void _Ready() {
		base._Ready();
		
		if (duration <= 0f) QueueFree();
		
		SetProcess(false);
	}

	public override void _Process(double pDelta) {
		float lDelta = (float)pDelta;

		time += lDelta;
		if (time >= duration) StopAnimation();

		base._Process(pDelta);
	}

	public virtual void StartAnimation() {
		if (stopped) return;

		stopped = true;
		time = 0f;

		SetProcess(true);
	}

	public virtual void StopAnimation() {
		stopped = false;
		
		SetProcess(false);
	}

	// Events
}

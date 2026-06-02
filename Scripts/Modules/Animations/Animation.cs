using Godot;
using System;

// Author : Roka
public partial class Animation : Module {

	// Consts

	// Variables
	[Export] private bool _activeOnStart = false;
	[Export] public float duration = 0f, skipTime = 0f;
	[Export] protected bool cosAnimation = false;
	
	protected float time = 0f;
	protected float ratio => cosAnimation ? _cosRatio : _ratio;
	private float _ratio => time / duration;
	private float _cosRatio => Mathf.Abs(Mathf.Cos(_ratio * Mathf.Pi));

	// Functions
	public override void _Ready() {
		base._Ready();
		
		SetProcess(false);
		if (_activeOnStart) StartAnimation();
	}

	public override void _Process(double pDelta) {
		float lDelta = (float)pDelta;

		if (GameManager.gameStopped) return;
		time += lDelta;
		if (time >= duration) StopAnimation();

		base._Process(pDelta);
	}

	public virtual void StartAnimation() {
		if (IsStopped()) return;

		stopped = true;
		time = skipTime;

		SetProcess(true);
	}

	public virtual void StopAnimation() {
		if (_activeOnStart) QueueFree();
		stopped = false;
		
		SetProcess(false);
	}

	// Events
}

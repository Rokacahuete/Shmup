using Godot;
using System;
using System.Collections.Generic;

// Author : Roka
public partial class TimeManager : Node {
	
	// Consts

	// Classes
	public class Timeout : IDisposable {
		private bool _disposed = false;
		public bool paused = true;
		public float timeLeft = 0f;
		public Action FunctionToCall = null;

		public void Start() {
			if (_disposed || !paused) return;

			paused = false;
			instance._LTimers.Add(this);
		}

		public void Update() {
			timeLeft -= delta;
			if (timeLeft > 0f) return;

			FunctionToCall?.Invoke();
			Dispose();
		}

		public void Stop() {
			if (_disposed || paused) return;

			paused = true;
			instance._LTimers.Remove(this);
		}

		public void Dispose() {
			if (_disposed) return;

			Stop();
			_disposed = true;
			FunctionToCall = null;
		}
	}

	// Variables
	public static TimeManager instance;
	public static float delta;

	private List<Timeout> _LTimers = new();

	// Functions
	public override void _Ready() {
		base._Ready();

		instance = this;
	}

	public override void _Process(double pDelta) {
		delta = (float)pDelta;
		
		if (GameManager.gameStopped) return;
		for (int i = _LTimers.Count - 1; i >= 0; i--) _LTimers[i].Update();

		base._Process(pDelta);
	}

	public static Timeout SetTimeout(Action pFunctionToCall, float pTime = 0f) {
		Timeout lTimer = new() { FunctionToCall = pFunctionToCall, timeLeft = pTime };
		lTimer.Start();
		return lTimer;
	}
	
	// Events
}

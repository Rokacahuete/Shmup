using Godot;
using System;
using System.Collections.Generic;

// Author : Roka
public partial class TimeManager : Node {
	
	// Consts

	// Variables
	public static TimeManager instance;
	public static float delta;
	
	private List<Action> _LTimersFunctions = new();
	private List<float> _LTimers = new();

	// Functions
	public override void _Ready() {
		base._Ready();

		instance = this;
	}

	public override void _Process(double pDelta) {
		delta = (float)pDelta;
		
		for (int i = _LTimers.Count - 1; i >= 0; i--) {
			_LTimers[i] -= delta;
			if (_LTimers[i] > 0f) continue;

			_LTimers.RemoveAt(i);
			_LTimersFunctions[i]?.Invoke();
			_LTimersFunctions.RemoveAt(i);
		}

		base._Process(pDelta);
	}

	public static void SetTimeout(Action pFunctionToCall, float pTime = 0f) {
		instance._LTimersFunctions.Add(pFunctionToCall);
		instance._LTimers.Add(pTime);
	}
	
	// Events
}

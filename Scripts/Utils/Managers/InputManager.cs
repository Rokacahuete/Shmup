using Godot;
using System;

// Author : Roka
public partial class InputManager : Node {

	// Consts
	private const string ACTION = "MouseClick";

	// Variables
	public static InputManager instance;

	[Export] private ulong _doubleClickTime = 0ul;

	private ulong _lastClick = 0ul;

	// Delegates
	public delegate void MouseEvent();
	public static MouseEvent OnClick, OnDoubleClick, OnReleased;

	// Functions
	public override void _Ready() {
		base._Ready();
		
		instance = this;
	}

	public override void _Process(double pDelta) {
		float lDelta = (float)pDelta;

		if (GameManager.gameStopped) return;
		
		if (Input.IsActionJustPressed(ACTION)) {
			ulong lNow = Time.GetTicksMsec();
			if (_lastClick + _doubleClickTime >= lNow) OnDoubleClick?.Invoke();
			else OnClick?.Invoke();

			_lastClick = lNow;
		}
		if (Input.IsActionJustReleased(ACTION)) OnReleased?.Invoke();
		
		base._Process(pDelta);
	}

	// Events
}

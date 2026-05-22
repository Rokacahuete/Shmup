using Godot;
using System;

// Author : Roka
public partial class MouseManager : Node {

	// Consts
	private const string ACTION = "MouseClick";

	// Variables
	public static MouseManager instance;

	// Delegates
	public delegate void MouseEvent();
	public static MouseEvent OnClick, OnReleased;

	// Functions
	public override void _Ready() {
		base._Ready();
		
		instance = this;
	}

	public override void _Process(double pDelta) {
		float lDelta = (float)pDelta;
		
		if (Input.IsActionJustPressed(ACTION)) OnClick?.Invoke();
		if (Input.IsActionJustReleased(ACTION)) OnReleased?.Invoke();
		
		base._Process(pDelta);
	}

	// Events
}

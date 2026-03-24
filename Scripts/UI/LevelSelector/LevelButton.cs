using Godot;
using System;

// Author : Roka
public partial class LevelButton : Button {
	
	// Consts

	// Variables
	[Export] private PackedScene[] _AEnemyGroup = new PackedScene[0];

	// Functions
	public override void _Ready() {
		base._Ready();

		Pressed += _OnButtonPressed;
	}

	public override void _Process(double pDelta) {
		float lDelta = (float)pDelta;

		base._Process(pDelta);
	}
	
	// Events
	private void _OnButtonPressed() {
		LevelSelector.SwitchLevel(_AEnemyGroup);
	}
}

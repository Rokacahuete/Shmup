using Godot;
using System;

// Author : Roka
public partial class Menu : Control {

	// Consts

	// Variables
	protected MenusManager.Menus setup = MenusManager.Menus.None;

	// Functions
	public override void _Ready() {
		MenusManager.Setup(setup, this);
	}

	public override void _Process(double pDelta) {
		float lDelta = (float)pDelta;
	}

	// Events
}

using Godot;
using System;

// Author : Roka
public partial class SwitchMenuButton : Button {

	// Consts

	// Variables
	[Export(PropertyHint.Enum, MenusManager.MENUS)] private MenusManager.Menus _menu = MenusManager.Menus.None;

	// Functions
	public override void _Ready() {
	}

	public override void _Process(double pDelta) {
		float lDelta = (float)pDelta;
	}

	// Events
	private void _OnButtonPressed() {
		MenusManager.Switch(_menu);
	}
}

using Godot;
using System;

// Author : Roka
public partial class LevelSelector : Menu {
	
	// Consts

	// Variables

	// Functions
	public override void _Ready() {
		setup = MenusManager.Menus.LevelSelector;
		base._Ready();
		MenusManager.Switch(MenusManager.Menus.LevelSelector);
	}

	public override void _Process(double pDelta) {
		float lDelta = (float)pDelta;

		base._Process(pDelta);
	}

	public static void SwitchLevel(Level pLevel) {
		GameManager.instance.StartGame(pLevel);
	}
	
	// Events
}

using Godot;
using System;

// Author : Roka
public partial class InventoryMenu : Menu {
	
	// Consts

	// Variables

	// Functions
	public override void _Ready() {
		setup = MenusManager.Menus.Inventory;
		base._Ready();
	}

	public override void _Process(double pDelta) {
		float lDelta = (float)pDelta;

		base._Process(pDelta);
	}
	
	// Events
	private void _OnCompetenceButtonPressed(int pIndex) {
		Player.instance.SwitchCompetence(pIndex);
	}
}

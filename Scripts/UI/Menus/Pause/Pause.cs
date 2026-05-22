using Godot;
using System;

// Author : Roka
public partial class Pause : Menu {

	// Consts

	// Variables
	[Export] private Label _summary = null;

	// Functions
	public override void _Ready() {
		setup = MenusManager.Menus.Pause;
		base._Ready();
	}

	public override void _Process(double pDelta) {
		float lDelta = (float)pDelta;

		base._Process(pDelta);
	}

    // Events
    public override void OnShow() {
        base.OnShow();

		GameManager.gameStopped = true;
    }

    public override void OnHide() {
        base.OnHide();

		GameManager.gameStopped = false;
    }

	private void _OnQuitButtonPressed() {
		GameManager.instance.StopGame(false);
	}
	
	private void _OnSettingsButtonPressed() {}
	
	private void _OnReplayButtonPressed() {
		MenusManager.Switch(MenusManager.Menus.HUD);
	}
}

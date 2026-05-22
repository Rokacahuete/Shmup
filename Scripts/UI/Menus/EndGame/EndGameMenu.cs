using Godot;
using System;

// Author : Roka
public partial class EndGameMenu : Menu {

	// Consts

	// Variables
	public static int score = 0;

	// Functions
	public override void _Ready() {
		setup = MenusManager.Menus.EndGame;
		base._Ready();
	}

	public override void _Process(double pDelta) {
		float lDelta = (float)pDelta;

		base._Process(pDelta);
	}

    // Events
    public override void OnShow() {
        base.OnShow();
		HUD.instance.OnShow();
    }

    public override void OnHide() {
        base.OnHide();
		HUD.instance.OnHide();
    }

	private void _OnContinueButtonPressed() {
		MenusManager.Switch(MenusManager.Menus.LevelSelector);
		GameManager.instance.CreateOrbs(GameManager.instance.scoreOrbScene, score, GameManager.screenSize * .5f);
	}
}

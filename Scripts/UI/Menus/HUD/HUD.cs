using Godot;
using System;

// Author : Roka
public partial class HUD : Menu {

	// Consts

	// Variables
	public static HUD instance;

	[Export] private Label _levelLabel = null;
	[Export] private ProgressBar _levelProgress = null;
	
	[Export] private Label _scoreLabel = null;

	// Functions
	public override void _Ready() {
		setup = MenusManager.Menus.HUD;
		base._Ready();

		instance = this;

		if (_levelLabel != null && _levelProgress != null && _scoreLabel != null)
			Datas.OnDatasChanged += _Update;
	}

	public override void _Process(double pDelta) {
		float lDelta = (float)pDelta;

		base._Process(pDelta);
	}

	private void _Update() {
		_levelLabel.Text = Datas.level.ToString();

		_levelProgress.Value = Datas.xp;
		_levelProgress.MaxValue = Datas.xpToLevelUp;

		_scoreLabel.Text = Datas.score.ToString();
	}

	// Events
	private void _OnPauseButtonPressed() {
		MenusManager.Switch(MenusManager.Menus.Pause);
	}
}

using Godot;
using System;

// Author : Roka
public partial class LevelSelector : Control {
	
	// Consts

	// Variables
	public static LevelSelector instance;

	// Functions
	public override void _Ready() {
		base._Ready();

		instance = this;
	}

	public override void _Process(double pDelta) {
		float lDelta = (float)pDelta;

		base._Process(pDelta);
	}

	public void SwitchLevel(PackedScene[] pAEnemyGroups) {
		GameManager.instance.StartGame(GameManager.GameModes.Waves, pAEnemyGroups);
	}
	
	// Events
}

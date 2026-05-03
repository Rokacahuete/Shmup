using Godot;
using System;

// Author : Roka
public partial class InfiniteModeMenu : Menu {

	// Consts

	// Variables
	[Export] private PackedScene[] _AEnemyGroupScenes = new PackedScene[0];

	// Functions
	public override void _Ready() {
		setup = MenusManager.Menus.Infinite;
		base._Ready();
	}

	public override void _Process(double pDelta) {
		float lDelta = (float)pDelta;

		base._Process(pDelta);
	}

	// Events
	private void _OnStartButtonPressed() {
		GameManager.instance.StartGame(new Level() { 
			gameMode = GameManager.GameModes.Infinite,
			AEnemyGroups = _AEnemyGroupScenes
		});
	}
}

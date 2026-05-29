using Godot;
using System;

// Author : Roka
public partial class ShooterCompetence : Competence {

	// Consts

	// Variables
	[Export] private PackedScene _shotScene = null;

	// Functions
	public override void _Ready() {
		base._Ready();
	}

	public override void _Process(double pDelta) {
		float lDelta = (float)pDelta;

		base._Process(pDelta);
	}

    protected override void Action() {
		base.Action();
		
		Node2D lShot = _shotScene.Instantiate<Node2D>();

		lShot.GlobalPosition = position;
		if (lShot is Enemy lEnemy) GameManager.instance.CreateEnemy(lEnemy);

		GameManager.instance.CallDeferred(MethodName.AddChild, lShot);
    }

	// Events
}

using Godot;
using System;

// Author : Roka
public partial class Shooter : Module {
	
	// Consts

	// Variables
	[Export] public PackedScene shotScene = null;
    [Export] protected float timeBetweenShots = 0f;
	[Export] protected float timer;

	// Functions
	public override void _Ready() {
        if (shotScene == null) SetProcess(false);

		base._Ready();
    }

	public override void _Process(double pDelta) {
		float lDelta = (float)pDelta;

		if (timer <= 0f) Shoot();
		timer -= lDelta;

		base._Process(pDelta);
	}
	
	protected void Shoot() {
        timer += timeBetweenShots;

		Node2D lShot = shotScene.Instantiate<Node2D>();
		GameManager.instance.gameContainer.AddChild(lShot);
		lShot.GlobalPosition = nodeToAffect.GlobalPosition;
		lShot.GlobalRotation = nodeToAffect.GlobalRotation;
		if (lShot is Enemy lEnemy)
			GameManager.instance.CreateEnemy(lEnemy);
    }
}
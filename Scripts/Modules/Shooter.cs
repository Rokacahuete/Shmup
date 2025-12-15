using Godot;
using System;

// Author : Roka
public partial class Shooter : Module {
	
	// Consts

	// Variables
	[Export] protected PackedScene shotScene = null;
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
		nodeToAffect.GetParent().AddChild(lShot);
		lShot.Position = nodeToAffect.Position;
		lShot.Rotation = nodeToAffect.Rotation;
    }
}
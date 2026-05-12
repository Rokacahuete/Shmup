using Godot;
using System;

// Author : Roka
public partial class Shooter : Module {
	
	// Consts

	// Variables
	[Export] public PackedScene shotScene = null;
    [Export] public float timeBetweenShots = 0f;
	[Export] protected float timer;

	// Delegates
	public delegate void OnShootEventHandler(Node2D pShot);
	public OnShootEventHandler OnShoot;

	// Functions
	public override void _Ready() {
        if (shotScene == null) SetProcess(false);

		base._Ready();
    }

	public override void _Process(double pDelta) {
		float lDelta = (float)pDelta;

		Shoot(lDelta);

		base._Process(pDelta);
	}

	public Node2D Shoot() {
		if (shotScene == null) return null;
		
		Node2D lShot = shotScene.Instantiate<Node2D>();
		lShot.GlobalPosition = nodeToAffect.GlobalPosition;
		lShot.GlobalRotation = nodeToAffect.GlobalRotation;
		GameManager.instance.gameContainer.CallDeferred(MethodName.AddChild, lShot);
		
		if (lShot is Enemy lEnemy)
			GameManager.instance.CreateEnemy(lEnemy);

		OnShoot?.Invoke(lShot);
		return lShot;
	}
	
	protected void Shoot(float pDelta) {
		if (stopped) return;

		timer -= pDelta;
		if (timer > 0f) return;

        timer += timeBetweenShots;
		Shoot();
    }
}
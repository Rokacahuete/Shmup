using Godot;
using System;

// Author : Roka
public partial class Book : Enemy {
	
	// Consts

	// Variables
	[Export] private ScaleAnimation _scaleAnimationSpawn = null;

	// Functions
	public override void _Ready() {
		base._Ready();

		this.GetModule<Shooter>().OnShoot += _OnShoot;
	}

	public override void _Process(double pDelta) {
		float lDelta = (float)pDelta;

		base._Process(pDelta);
	}

	private void _SettingEnemy(Enemy pEnemy) {
		pEnemy.xpOnKilled = 0;
	}
	
	// Events
	private void _OnShoot(Node2D pShot) {
		if (pShot is Enemy lEnemy) _SettingEnemy(lEnemy);

		if (_scaleAnimationSpawn == null) return;
		ScaleAnimation lAnimCopy = (ScaleAnimation)_scaleAnimationSpawn.Duplicate();
		lAnimCopy.skipTime = lAnimCopy.duration * .5f;

		pShot.AddChild(lAnimCopy);
		lAnimCopy.nodeToAffect = pShot;
		lAnimCopy.CallDeferred(Animation.MethodName.StartAnimation);
		pShot.SetDeferred(Node2D.PropertyName.Scale, lAnimCopy.rescale);
	}
}

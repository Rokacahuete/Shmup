using Godot;
using System;

// Author : Roka
public partial class Damageable : Module {
	
	// Consts

	// Variables
	[Export] private PackedScene[] _ADamageables = new PackedScene[0];

	// Functions
	public override void _Ready() {
		if (nodeToAffect is not Entity) QueueFree();

		base._Ready();
	}

	public override void _Process(double pDelta) {
		float lDelta = (float)pDelta;

		base._Process(pDelta);
	}
	
	// Events
	private void _OnAreaEntered(Area2D pArea) {
		foreach (PackedScene lScene in _ADamageables) {
			if (pArea.GetParent().SceneFilePath != lScene.ResourcePath) continue;
			
			Damager lDamager = pArea.GetModule<Damager>();
			if (lDamager != null) {
				((Entity)nodeToAffect)?.Hurt(lDamager);
				lDamager.Hurt();
			}
			break;
		}
	}
}

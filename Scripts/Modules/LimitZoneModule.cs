using Godot;
using System;

// Author : Roka
public partial class LimitZoneModule : Module {

	// Consts

	// Variables
	[Export] private Rect2 _movingZone;

	// Functions
	public override void _Ready() {
		base._Ready();
		
		_movingZone.Position *= GameManager.screenSize;
		_movingZone.Size *= GameManager.screenSize;
	}

	public override void _Process(double pDelta) {
		float lDelta = (float)pDelta;
		
		_CheckLimits();

		base._Process(pDelta);
	}

	private void _CheckLimits() {
		if (IsStopped()) return;
		
		nodeToAffect.GlobalPosition = new Vector2(
			MyMaths.MinMax(nodeToAffect.GlobalPosition.X, _movingZone.Position.X, _movingZone.Size.X + _movingZone.Position.X),
			MyMaths.MinMax(nodeToAffect.GlobalPosition.Y, _movingZone.Position.Y, _movingZone.Size.Y + _movingZone.Position.Y)
		);
	}

	// Events
}

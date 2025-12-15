using Godot;
using System;

// Author : Roka
public partial class Player : Entity {
	
	// Consts

	// Variables
	public static Player instance;

	[Export] private Movable movableModule = null;
	[Export] private Rect2 _movingZone;

	// Functions
	public override void _Ready() {
		instance = this;
		
		Vector2 lScreenSize = GetViewport().GetVisibleRect().Size;
		_movingZone.Position *= lScreenSize;
		_movingZone.Size *= lScreenSize;

		base._Ready();
	}

	public override void _Process(double pDelta) {
		float lDelta = (float)pDelta;

		if (Input.IsMouseButtonPressed(MouseButton.Left)) {
			movableModule.direction = GetViewport().GetMousePosition() - Position;
        }
		else movableModule.direction = Vector2.Zero;
		
		Position = new Vector2(
			Mathf.Clamp(Position.X, _movingZone.Position.X, _movingZone.Size.X + _movingZone.Position.X),
			Mathf.Clamp(Position.Y, _movingZone.Position.Y, _movingZone.Size.Y + _movingZone.Position.Y)
		);

		base._Process(pDelta);
	}

    // Events
    public override void _Input(InputEvent @event) {
        if (@event is InputEventScreenTouch lTouch) {
            movableModule.direction = lTouch.Position;
        } else if (@event is InputEventScreenDrag lDrag) {
            movableModule.direction	= lDrag.Position;
        }
    }
}
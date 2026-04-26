using Godot;
using System;

// Author : Roka
public partial class Player : Entity {
	
	// Consts

	// Variables
	public static Player instance;

	[Export] private Module[] _AModules = new Module[0];
	[Export] private MovableCustom _movableModule = null;
	[Export] private Rect2 _movingZone;

	// Functions
	public override void _Ready() {
		instance = this;
		
		_movingZone.Position *= GameManager.screenSize;
		_movingZone.Size *= GameManager.screenSize;

		SetActive(true);

		base._Ready();
	}

	public override void _Process(double pDelta) {
		float lDelta = (float)pDelta;

		if (Input.IsMouseButtonPressed(MouseButton.Left)) {
			_movableModule.direction = GetViewport().GetMousePosition() - Position;
        }
		else _movableModule.direction = Vector2.Zero;
		
		Position = new Vector2(
			Mathf.Clamp(Position.X, _movingZone.Position.X, _movingZone.Size.X + _movingZone.Position.X),
			Mathf.Clamp(Position.Y, _movingZone.Position.Y, _movingZone.Size.Y + _movingZone.Position.Y)
		);

		base._Process(pDelta);
	}

	public void SetActive(bool pStopped) {
		foreach (Module lModule in _AModules) lModule.stopped = pStopped;
		Visible = !pStopped;
	}

    public override void Die() {
		health = maxHealth;
		GameManager.instance.Restart();
    }


    // Events
    public override void _Input(InputEvent @event) {
        if (@event is InputEventScreenTouch lTouch) {
            _movableModule.direction = lTouch.Position;
        } else if (@event is InputEventScreenDrag lDrag) {
            _movableModule.direction = lDrag.Position;
        }
    }
}
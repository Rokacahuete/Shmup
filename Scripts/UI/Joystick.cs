using Godot;
using System;

// Author : Roka
public partial class Joystick : Node2D {

	// Consts

	// Variables
	[Export] private Node2D _movingZone = null;
	[Export] private float _distanceMax = 0f;
	
	private Vector2 _initPos, _startPos;
	private MovableCustom _movableModule;

	// Functions
	public override void _Ready() {
		base._Ready();

		_movableModule = Player.instance.movableModule;
		_initPos = _movingZone.Position;
		
		MouseManager.OnClick += _OnClick;
		MouseManager.OnReleased += _OnReleased;
	}

	public override void _Process(double pDelta) {
		float lDelta = (float)pDelta;

        Vector2 lMousePos = GetViewport().GetMousePosition();
        Vector2 lOffset = lMousePos - _startPos;
        if (lOffset.Length() > _distanceMax)
            lOffset = lOffset.Normalized() * _distanceMax;

        _movingZone.GlobalPosition = _startPos + lOffset;

		_movableModule.movingStrenght = lOffset.Length() / _distanceMax;
		_movableModule.direction = lOffset;

		base._Process(pDelta);
	}

	// Events
	private void _OnClick() {
		GlobalPosition = GetViewport().GetMousePosition();
		_movingZone.Position = _initPos;
		_startPos = _movingZone.GlobalPosition;
		Visible = true;
		SetProcess(true);
	}

	private void _OnReleased() {
		_movableModule.direction = Vector2.Zero;
		Visible = false;
		SetProcess(false);
	}
}

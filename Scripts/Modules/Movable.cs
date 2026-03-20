using Godot;
using System;

// Author : Roka
public partial class Movable : Module {
	
	// Consts

	// Variables
	[Export(PropertyHint.Enum, "Line,Sinusoidal,Circle")] private int _patternType = 0;
	[Export] private Vector2 _direction = Vector2.Zero;
	[Export] private float _speed = 0f;

	private Vector2 _initPos;
	private float _distanceTime = 0f;

    // Functions
    public override void _Ready() {
        base._Ready();
		
		_initPos = nodeToAffect.Position;
    }

	public override void _Process(double pDelta) {
		float lDelta = (float)pDelta;
		
		_distanceTime += lDelta * _speed;
        nodeToAffect.Position = _initPos + Patterns.GetPosition(_patternType, _distanceTime, _direction);

		base._Process(pDelta);
	}
	
	// Events
}
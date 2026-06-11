using Godot;
using System;

// Author : Roka
public partial class ParallaxTool : Parallax2D {

	// Consts
	private const float _ACCELERATION_MULTIPLIER = 5f;

	// Variables
	[Export] private float _speed = 0f;

	private Vector2 _move = Vector2.Zero;
	private float _minSpeed, _maxSpeed;
    private float _elapsedTime = 0f;

	// Delegates
	private delegate void Function();
	private Function _FunctionsDelta;

	// Functions
	public override void _Ready() {
		base._Ready();

        GameManager.instance.OnWaveEnd += _OnWaveEnd;
		_FunctionsDelta += _Move;
		_move.Y = _minSpeed = _speed;
		_maxSpeed = _speed * _ACCELERATION_MULTIPLIER;
	}

	public override void _Process(double pDelta) {
		float lDelta = (float)pDelta;

		if (GameManager.gameStopped) return;
		_FunctionsDelta?.Invoke();

		base._Process(pDelta);
	}

	private void _Move() {
        ScrollOffset += _move * TimeManager.delta;
	}
	
	private void _Acceleration() {
		_elapsedTime += TimeManager.delta;

		float lTimer = GameManager.instance.waveTimer;
		float lRatio = _elapsedTime / lTimer;

		if (_elapsedTime >= lTimer) {
			_elapsedTime = 0f;
			_move.Y = _minSpeed;
			_FunctionsDelta -= _Acceleration;
		} else _move.Y = _minSpeed + (_maxSpeed - _minSpeed) * 4f * lRatio * (1f - lRatio);
	}

	// Events
    private void _OnWaveEnd() {
		if (GameManager.instance.waveTimer > 0f) _FunctionsDelta += _Acceleration;
    }

    public override void _ExitTree() {
        GameManager.instance.OnWaveEnd -= _OnWaveEnd;
        base._ExitTree();
    }
}

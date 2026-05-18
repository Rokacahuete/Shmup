using Godot;
using System;

// Author : Roka
public partial class Witch : Enemy {
	
	// Consts

	// Variables
	[Export] private Movable _movableModule = null;
	[Export] private Damageable _damageableModule = null;
	[Export] private CollisionShape2D _visionShape = null;
	
	[Export] private Animation[] _ATpAnimations = new Animation[0];
	[Export] private Timer _tpTimerMove = null, _tpTimerInvicibility = null;
	[Export(PropertyHint.Range, "0f,1f")] private float _lifePercentageToTp = 0f;
	[Export] private int _nbTp = 0;
	[Export] private Vector2 _gapTp = Vector2.Zero;
	
	private int _healthToTp;
	private int _tpActive = 0;
	private bool _tpCanBeTriggered = true;

	// Functions
	public override void _Ready() {
		base._Ready();

		_healthToTp = (int)(maxHealth * _lifePercentageToTp);
		if (GlobalPosition.X >= GameManager.screenSize.X * .5f) _gapTp.X *= -1f;

		if (_tpTimerMove != null) _tpTimerMove.Timeout += _TPMove;
		if (_tpTimerInvicibility != null) _tpTimerInvicibility.Timeout += _TPInvicibility;
	}

	public override void _Process(double pDelta) {
		float lDelta = (float)pDelta;

		base._Process(pDelta);
	}

    public override void Hurt(Damager pDamager) {
        base.Hurt(pDamager);
		if (_tpCanBeTriggered && _damageableModule != null && health <= _healthToTp) {
			_tpCanBeTriggered = false;
			_tpActive = _nbTp;
		}
    }

	private void _TP() {
		foreach (Animation lAnim in _ATpAnimations) {
			lAnim.StartAnimation();
		}
		_tpActive--;

		_damageableModule.stopped = true;
		// _movableModule.stopped = true;
		if (_visionShape != null) _visionShape.SetDeferred(CollisionShape2D.PropertyName.Disabled, true);

		if (_tpTimerMove != null) _tpTimerMove?.Start();
		else _TPMove();
		if (_tpTimerInvicibility != null) _tpTimerInvicibility?.Start();
		else _TPInvicibility();
	}

	private void _TPMove() {
		_movableModule.Move(_gapTp);
		_gapTp.X *= -1f;
	}

	private void _TPInvicibility() {
		_damageableModule.stopped = false;
		// _movableModule.stopped = false;
		if (_visionShape != null) _visionShape.SetDeferred(CollisionShape2D.PropertyName.Disabled, false);
	}
	
	// Events
	private void _OnDetectionZoneEntered(Area2D pArea) {
		if (_tpActive <= 0 || !_damageableModule.CanBeHurtedBy(pArea)) return;

		_TP();
	}
}

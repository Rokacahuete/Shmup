using Godot;
using System;

// Author : Roka
public partial class HitAnimation : Animation {

	// Consts

	// Variables
	[Export] private Color _onHitColor = new Color(1f, 1f, 1f);

	private Color _baseColor;

	// Functions
	public override void _Ready() {
		base._Ready();
		
		if (nodeToAffect is Entity lEntity) lEntity.OnHurted += _OnHurted;
	}

	public override void _Process(double pDelta) {
		float lDelta = (float)pDelta;

		nodeToAffect.Modulate = _baseColor * ratio + _onHitColor * (1f - ratio);

		base._Process(pDelta);
	}

    public override void StartAnimation() {
		if (IsStopped()) return;
        base.StartAnimation();

		_baseColor = nodeToAffect.Modulate;
    }

    public override void StopAnimation() {
		base.StopAnimation();
		nodeToAffect.Modulate = _baseColor;
    }

	// Events
	private void _OnHurted(Entity lEntity) {
		StartAnimation();
	}
}

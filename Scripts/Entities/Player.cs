using Godot;
using System;

// Author : Roka
public partial class Player : Entity {
	
	// Consts

	// Variables
	public static Player instance;

	[Export] private ProgressBar _lifeBar = null;
	[Export] private Module[] _AModules = new Module[0];
	[Export] public MovableCustom movableModule = null;

	[Export] public Competence competence = null;
	[Export] public float competenceCooldown = 0f;

	private float _inactiveCompetenceTime = 0f;

	// Functions
	public override void _Ready() {
		instance = this;

		SetInactive(true);
		
		InputManager.OnDoubleClick += ActiveCompetence;

		base._Ready();
		UpdateLifeBar();
	}

	public override void _Process(double pDelta) {
		float lDelta = (float)pDelta;

		_inactiveCompetenceTime -= lDelta;

		base._Process(pDelta);
	}

	public void UpdateLifeBar() {
		if (_lifeBar == null) return;

		_lifeBar.MaxValue = maxHealth;
		_lifeBar.Value = health;
	}

	public void SetInactive(bool pStopped) {
		foreach (Module lModule in _AModules) lModule.stopped = pStopped;
		SetProcess(!pStopped);
		Visible = !pStopped;
	}

	public void ActiveCompetence() {
		if (_inactiveCompetenceTime > 0f) return;

		_inactiveCompetenceTime = competenceCooldown;
		competence?.Active(GetViewport().GetMousePosition());
	}

    public override void Hurt(Damager pDamager) {
        base.Hurt(pDamager);

		UpdateLifeBar();
    }

    public override void Die() {
		health = maxHealth;
		GameManager.instance.Restart();
		UpdateLifeBar();
    }

    // Events
}
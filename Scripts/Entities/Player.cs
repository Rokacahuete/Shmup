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

	[Export] public Competence[] _ACompetences = new Competence[0];

	private Competence _competence = null;

	private float _inactiveCompetenceTime = 0f;

	// Functions
	public override void _Ready() {
		instance = this;

		SetActive(true);
		
		GameManager.instance.OnWaveEnd += _InterWave;
		InputManager.OnDoubleClick += ActiveCompetence;

		base._Ready();
		UpdateLifeBar();
	}

	public override void _Process(double pDelta) {
		float lDelta = (float)pDelta;

		_inactiveCompetenceTime -= lDelta;

		base._Process(pDelta);
	}

	public void Start() {
		SetActive(false);
		health = maxHealth;
		UpdateLifeBar();
	}

	public void UpdateLifeBar() {
		if (_lifeBar == null) return;

		_lifeBar.MaxValue = maxHealth;
		_lifeBar.Value = health;
	}

	public void SetActive(bool pStopped) {
		SetActive(pStopped, !pStopped);
	}

	public void SetActive(bool pStopped, bool pVisible) {
		foreach (Module lModule in _AModules) lModule.stopped = pStopped;
		SetProcess(!pStopped);
		Visible = pVisible;
	}

	private void _InterWave() {
		SetActive(true, true);
		TimeManager.SetTimeout(() => SetActive(false, true), GameManager.instance.waveTimer);
	}

	public void SwitchCompetence(int pCompetence) {
		int lLength = _ACompetences.Length;
		if (lLength == 0) return;

		pCompetence = pCompetence.MinMax(0, lLength);
		_competence = _ACompetences[pCompetence];
	}

	public void ActiveCompetence() {
		if (_inactiveCompetenceTime > 0f || _competence == null) return;

		_inactiveCompetenceTime = _competence.cooldown;
		_competence.Active(GetViewport().GetMousePosition());
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
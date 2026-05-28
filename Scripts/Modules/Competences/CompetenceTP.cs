using Godot;
using System;

// Author : Roka
public partial class CompetenceTP : Competence {

	// Consts

	// Variables

	// Functions
	public override void _Ready() {
		base._Ready();
	}

	public override void _Process(double pDelta) {
		float lDelta = (float)pDelta;

		base._Process(pDelta);
	}

    protected override void Action() {
        base.Action();

		nodeToAffect.GlobalPosition = position;
    }

	// Events
}

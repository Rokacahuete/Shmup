using Godot;

// Author : Roka
public static class Datas {
	
	// Consts

	// Variables
	public static int score = 0;

	// Functions
	public static void UpdateScore(int pScoreToAdd = 0) {
		Datas.score += pScoreToAdd;
		GD.Print($"Score modifié. Nouveau score : {Datas.score} !");
	}
	
	// Events
}
using Godot;

// Author : Roka
public static class Datas {
	
	// Consts

	// Variables
	private static int _score = 0;

	private static int _xp = 0;
	private static int _level = 1;

	// Functions
	public static void UpdateScore(int pScoreToAdd = 0) {
		_score += pScoreToAdd;
		GD.Print($"Score modifié. Nouveau score : { _score } !");
	}

	private static int _GetXpToLevelUp() {
		return 200 + _level * 10;
	}

	private static void _LevelUp() {
		int lNeededXp = _GetXpToLevelUp();
		if (_xp >= lNeededXp) {
			_xp -= lNeededXp;
			_level++;
			GD.Print($"Level up !! Niveau { _level }, { lNeededXp } pour level up.");
			_LevelUp();
		}
	}

	public static void AddXp(int pXpToAdd) {
		if (pXpToAdd <= 0) return;

		_xp += pXpToAdd;
		_LevelUp();
	}
	
	// Events
}
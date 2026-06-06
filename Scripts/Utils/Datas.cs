using Godot;

// Author : Roka
public static class Datas {
	
	// Consts

	// Variables
	public static string name = "";

	public static int score = 0;
	
	public static int xpToLevelUp => 200 + level * 10;
	public static int xp = 0;
	public static int level = 1;

	// Delegates
	public delegate void OnDatasChangedEventHandler();
	public static OnDatasChangedEventHandler OnDatasChanged;

	// Functions
	public static void UpdateScore(int pScoreToAdd = 0) {
		score += pScoreToAdd;

		OnDatasChanged?.Invoke();
	}


	private static void _LevelUp() {
		int lNeededXp = xpToLevelUp;
		if (xp >= lNeededXp) {
			xp -= lNeededXp;
			level++;
			_LevelUp();
		}
	}

	public static void AddXp(int pXpToAdd) {
		if (pXpToAdd <= 0) return;

		xp += pXpToAdd;
		_LevelUp();

		OnDatasChanged?.Invoke();
	}
	
	// Events
}
using Godot;

// Author : Roka
public static class Datas {
	
	// Consts

	// Variables
	public static int score { get; private set; } = 0;
	
	public static int xpToLevelUp => 200 + level * 10;
	public static int xp { get; private set; } = 0;
	public static int level { get; private set; } = 1;

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
using Godot;

// Author : Roka
public struct Level {
    public class Datas {
        public int score = 0;
        public int xp = 0;
        public int enemyKilled = 0;
    }
	
	// Consts

	// Variables
    public GameManager.GameModes gameMode = GameManager.GameModes.None;
    public PackedScene[] AEnemyGroups = new PackedScene[0];
	public int score = 0;

    public int wave = -1;
    public Datas datas = new();

    // Constructors
    public Level() {}

	// Functions
	
	// Events
}
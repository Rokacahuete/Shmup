using Godot;

// Author : Roka
public struct Level {
	
	// Consts

	// Variables
    public GameManager.GameModes gameMode = GameManager.GameModes.None;
    public PackedScene[] AEnemyGroups = new PackedScene[0];
	public int score = 0;

    public int wave = -1;

    // Constructors
    public Level() {}

	// Functions
	
	// Events
}
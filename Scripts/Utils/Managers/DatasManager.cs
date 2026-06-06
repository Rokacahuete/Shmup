using Godot;
using System.Text.Json;
using System.IO;
using System;

// Author : Roka
public static class DatasManager {
	
	// Consts
	private const string SAVE_PATH = "user://GameDatas.json";

	// Classes
	public class SaveDatas {
		public string name { get; set; }

		public int score { get; set; }
		public int level { get; set; }
		public int xp { get; set; }

		public SaveDatas() {
			name = Datas.name;
			score = Datas.score;
			level = Datas.level;
			xp = Datas.xp;
		}
	}

	// Variables

	// Functions
    public static void Save() {
        SaveDatas lDatas = new SaveDatas();

        string lJson = JsonSerializer.Serialize(lDatas);
        string lPath = ProjectSettings.GlobalizePath(SAVE_PATH);

        File.WriteAllText(lPath, lJson);
        GD.Print("Sauvegarde OK : " + lPath);
    }

	public static void Load() {
		string lPath = ProjectSettings.GlobalizePath(SAVE_PATH);

		if (!File.Exists(lPath)) {
			GD.Print("Aucune sauvegarde trouvée");
			return;
		}

		string lJson = File.ReadAllText(lPath);
		SaveDatas lDatas = JsonSerializer.Deserialize<SaveDatas>(lJson);
		
		Datas.name = lDatas.name;
		Datas.score = lDatas.score;
		Datas.level = lDatas.level;
		Datas.xp = lDatas.xp;
		Datas.OnDatasChanged?.Invoke();
	}
	
	// Events
}
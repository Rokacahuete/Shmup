using Godot;

// Author : Roka
public static class MyMaths {
    
    // Consts

    // Variables

    // Functions
    public static Vector2 FromAngleToVector(this float tAngle) => new Vector2(Mathf.Cos(tAngle), Mathf.Sin(tAngle));
}
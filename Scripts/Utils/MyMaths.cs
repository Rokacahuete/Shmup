using Godot;

// Author : Roka
public static class MyMaths {
    
    // Consts

    // Variables

    // Functions
    public static int Min(this int tNum, int pMin) => tNum > pMin ? pMin : tNum;
    public static int Max(this int tNum, int pMax) => tNum < pMax ? pMax : tNum;
    public static int MinMax(this int tNum, int pMin, int pMax) => tNum.Min(pMax).Max(pMin);

    public static Vector2 FromAngleToVector(this float tAngle) => new Vector2(Mathf.Cos(tAngle), Mathf.Sin(tAngle));
}
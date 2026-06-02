using System.Numerics;
using Godot;

// Author : Roka
public static class MyMaths {
    
    // Consts
    public const float HALF_PI = Mathf.Pi * .5f;

    // Variables

    // Functions
    public static T Min<T>(this T tNum, T pMin) where T : INumber<T> => tNum > pMin ? pMin : tNum;
    public static T Max<T>(this T tNum, T pMax) where T : INumber<T> => tNum < pMax ? pMax : tNum;
    public static T MinMax<T>(this T tNum, T pMin, T pMax) where T : INumber<T> => tNum.Min(pMax).Max(pMin);

    public static float RandomAngle() => GameManager.rand.Randf() * Mathf.Tau;

    public static Godot.Vector2 FromAngleToVector(this float tAngle) => new Godot.Vector2(Mathf.Cos(tAngle), Mathf.Sin(tAngle));
}
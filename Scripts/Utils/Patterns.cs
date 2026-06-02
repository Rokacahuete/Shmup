using System.Collections.Generic;
using Godot;

public static class Patterns {

    // Consts
    public const string TYPES = "Line,Sinusoidal,Circle";

    // Delegates
    public delegate Vector2 Pattern(float pIndex, Vector2 pPatternVector);

    // Variables
    private static List<Pattern> _LPatterns = new List<Pattern>() {
        Line, Sinusoidal, Circle
    };

    // Functions
    public static Vector2 GetPosition(int pPatternType, float pIndex, Vector2 pVector) {
        int lLength = _LPatterns.Count - 1;
        return _LPatterns[MyMaths.MinMax(pPatternType, 0, lLength)](pIndex, pVector);
    }

    // Patterns
    public static Vector2 Line(float pI, Vector2 pVector) {
        return pVector * pI;
    }

    public static Vector2 Sinusoidal(float pI, Vector2 pVector) {
        return new Vector2(Mathf.Sin(pI), pI) * pVector;
    }

    public static Vector2 Circle(float pI, Vector2 pVector) {
        return pI.FromAngleToVector() * pVector;
    }
}
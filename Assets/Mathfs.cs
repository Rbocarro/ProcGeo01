using UnityEngine;

public static class Mathfs 
{
    public const float TAU = Mathf.PI * 2;
    public static Vector2 GetUnitVector2ByAngle(float angRad)
    {
        return new Vector2(Mathf.Cos(angRad),
                           Mathf.Sin(angRad));
    }
}

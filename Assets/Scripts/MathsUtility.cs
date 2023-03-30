using UnityEngine;

public static class MathsUtility 
{

    public enum Axis
    {
        X, Y, Z
    }


    /// <summary>
    /// Clamps the angle.
    /// </summary>
    /// <param name="lfAngle">The lf angle.</param>
    /// <param name="lfMin">The lf minimum.</param>
    /// <param name="lfMax">The lf maximum.</param>
    /// <returns></returns>
    public static float ClampAngle(float lfAngle, float lfMin, float lfMax)
    {
        if (lfAngle < -360f) lfAngle += 360f;
        if (lfAngle > 360f) lfAngle -= 360f;
        return Mathf.Clamp(lfAngle, lfMin, lfMax);
    }



    /// <summary>
    /// Rotate vector vec angle degrees
    /// </summary>
    /// <param name="vec">Vector to rotate</param>
    /// <param name="angle">Angle in degrees</param>
    /// <returns>Rotation result</returns>
    public static Vector3 RotateVector(Vector3 vec, float angle, Axis axis = Axis.Y)
    {
        float cos = Mathf.Cos(angle * Mathf.Deg2Rad);
        float sin = Mathf.Sin(angle * Mathf.Deg2Rad);

        return axis switch
        {
            Axis.X => new Vector3(vec.x, vec.y * cos - vec.z * sin, vec.y * sin + vec.z * cos),
            Axis.Y => new Vector3(vec.x * cos - vec.z * sin, vec.y, vec.x * sin + vec.z * cos),
            Axis.Z => new Vector3(vec.x * cos - vec.y * sin, vec.x * sin + vec.y * cos, vec.z),
            _ => Vector3.zero,
        };
    }

    public static Vector3 RotateVector(Vector3 vec, float angle, Vector3 axis)
    {
        float cos = Mathf.Cos(angle * Mathf.Deg2Rad);
        float sin = Mathf.Sin(angle * Mathf.Deg2Rad);

        axis.Normalize();

        float x = 
              vec.x * cos + vec.x * axis.x * axis.x * (1 - cos)
            + vec.y * axis.x * axis.y * (1 - cos) - vec.y * axis.z * sin
            + vec.z * axis.x * axis.z * (1 - cos) + vec.z * axis.y * sin;

        float y =
              vec.x * axis.y * axis.x * (1 - cos) + vec.x * axis.z * sin
            + vec.y * cos + vec.y * axis.y * axis.y * (1 - cos)
            + vec.z * axis.y * axis.z * (1 - cos) - vec.z * axis.x * sin;

        float z = 
              vec.x * axis.z * axis.x * (1 - cos) - vec.x * axis.y * sin
            + vec.y * axis.z * axis.y * (1 -cos) + vec.y * axis.x * sin
            + vec.z * cos + vec.z * axis.z * axis.z * (1 - cos);

        return new Vector3(x, y, z);
    }



}

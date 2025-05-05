using UnityEngine;
 
public static class VectorExtensions
{ 
    public static Vector3 WithMagnitude(this Vector3 vector, float length)
    {
        return vector.normalized * length;
    } 
    /// <summary>
    /// Returns the vector, but with a new x value.
    /// </summary>
    /// <param name="x">The new x value</param>
    /// <returns>The "new" vector</returns>
    public static Vector3 WithX(this Vector3 vector, float x)
    {
        return new Vector3(x, vector.y, vector.z);
    }

    /// <summary>
    /// Returns the vector, but with a new y value.
    /// </summary>
    /// <param name="y">The new y value</param>
    /// <returns>The "new" vector</returns>
    public static Vector3 WithY(this Vector3 vector, float y)
    {
        return new Vector3(vector.x, y, vector.z);
    }

    /// <summary>
    /// Returns the vector, but with a new z value.
    /// </summary>
    /// <param name="z">The new z value</param>
    /// <returns>The "new" vector</returns>
    public static Vector3 WithZ(this Vector3 vector, float z)
    {
        return new Vector3(vector.x, vector.y, z);
    }

    /// <summary>
    /// Returns the vector, but with a new x value.
    /// </summary>
    /// <param name="x">The new x value</param>
    /// <returns>The "new" vector</returns>
    public static Vector2 WithX(this Vector2 vector, float x)
    {
        return new Vector2(x, vector.y);
    }

    /// <summary>
    /// Returns the vector, but with a new y value.
    /// </summary>
    /// <param name="y">The new y value</param>
    /// <returns>The "new" vector</returns>
    public static Vector2 WithY(this Vector2 vector, float y)
    {
        return new Vector2(vector.x, y);
    } 
    public static bool HasNaNComponent(this Vector3 vector)
    {
        return float.IsNaN(vector.x) || float.IsNaN(vector.y) || float.IsNaN(vector.z);
    }
}

using UnityEngine;
using System.Collections.Generic;

public static class WeaponUtils
{
    public static List<Vector3> GetVelocities(int bulletCount, Vector3 shootDirection, float maximumAngle)
    {
        List<Vector3> velocities = new List<Vector3>();
        int divisions = Mathf.Max(1, bulletCount - 1);
        float startAngle = -maximumAngle / 2;
        float stepAngle = maximumAngle / divisions;
        for (int i = 0; i < bulletCount; i++)
        {
            float currentAngle = startAngle + stepAngle * i;
            Vector3 bulletDirection = RotateVector(shootDirection, currentAngle);
            velocities.Add(bulletDirection);
        }
        return velocities;
    }

    private static Vector3 RotateVector(Vector3 v, float angle)
    {
        float radian = angle * Mathf.Deg2Rad;
        float sin = Mathf.Sin(radian);
        float cos = Mathf.Cos(radian);

        float tx = v.x;
        float ty = v.y;

        return new Vector3(cos * tx - sin * ty, sin * tx + cos * ty, 0);
    }
}
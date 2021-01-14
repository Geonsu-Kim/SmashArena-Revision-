using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class OverLapRaycast
{   
    public static Collider[] CheckSphere(float radius,Vector3 position,int layerMask=1<<10)
    {
        return Physics.OverlapSphere(position, radius, layerMask);
    }

    public static Collider[] CheckBox(Vector3 size, Vector3 position,Quaternion dir, int layerMask = 1 << 10)
    {
        return Physics.OverlapBox(position, size, dir, layerMask);
    }
}

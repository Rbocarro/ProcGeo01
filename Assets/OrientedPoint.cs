using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct OrientedPoint
{
    public Vector3 pos;
    public Quaternion rot;

    public OrientedPoint(Vector3 pos, Quaternion rot)
    {
        this.pos = pos;
        this.rot = rot;
    }

    public OrientedPoint(Vector3 pos, Vector3 forward)
    {
        this.pos = pos;
        this.rot = Quaternion.LookRotation(forward);
    }

    public Vector3 LocalToWorldPos(Vector3 localSpacePosition)
    {
       return pos+ rot* localSpacePosition;
    }

    public Vector3 LocalToWorldVector(Vector3 localSpacePosition)
    {
        return (rot * localSpacePosition);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcGeo : MonoBehaviour
{
    void Awake()
    {
        Mesh mesh = new Mesh();
        mesh.name = "Procedural quad".ToString();

        List<Vector3> points = new List<Vector3>()
        {
            new Vector3 (-1, 1),
            new Vector3 ( 3, 1),
            new Vector3 (-1,-1),
            new Vector3 ( 3,-1),
    };
        int[] triIndices = new int[]
        {
            2,0,1,
            2,1,3
        };
        mesh.SetVertices(points);
        mesh.triangles=triIndices;
        mesh.RecalculateNormals();
        GetComponent<MeshFilter>().sharedMesh = mesh;
    }

    
}

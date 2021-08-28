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
            1,0,2,
            3,1,2
        };

        List<Vector2> uvs = new List<Vector2>()
        {
            new Vector2(0,1),
            new Vector2(1,1),
            new Vector2(0,0),
            new Vector2(1,0),

        };

        List<Vector3> normals = new List<Vector3>()
        {
            new Vector3(0,0,1),
            new Vector3(0,0,1),
            new Vector3(0,0,1)
        };
        mesh.SetVertices(points);
        mesh.SetNormals(normals);
        mesh.SetUVs(0, uvs);
        mesh.triangles=triIndices;
        //mesh.RecalculateNormals();//comutational; expensive

        GetComponent<MeshFilter>().sharedMesh = mesh;
    }

    
}

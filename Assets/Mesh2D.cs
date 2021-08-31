using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu] public class Mesh2D : ScriptableObject
{   
    [System.Serializable]
    public class Vertex
    {   

        public Vector2 points;
        public Vector2 normals;
        public float u;
    }

    public Vertex[] vertices;

    public int[] lineIndices;

    public int VertexCount => vertices.Length;
    public int LineCount => lineIndices.Length;

}

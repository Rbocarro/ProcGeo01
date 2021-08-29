using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(MeshFilter))]
public class QuadRing : MonoBehaviour
{
    public enum UvProjection
    {
        Radial,Z
    }
    [Range(0.01f, 1f)][SerializeField] 
    private float radiusInner;
    
    [Range(0.01f, 5f)][SerializeField]
    private float thickness;

    [Range(3, 32)][SerializeField] 
    int angularSegmentCount = 3;

    [SerializeField] UvProjection uvProjection = UvProjection.Radial;

    Mesh mesh;

    float radiusOuter => radiusInner + thickness;
    int VertexCount => angularSegmentCount * 2;

    private void OnDrawGizmosSelected()
    {
        Gizmosfs.DrawWireCircle(this.transform.position, this.transform.rotation,radiusInner, angularSegmentCount);
        Gizmosfs.DrawWireCircle(this.transform.position, this.transform.rotation, radiusOuter, angularSegmentCount);
    }

    private void Awake()
    {
        mesh = new Mesh();
        mesh.name = "QuadRing";
        GetComponent<MeshFilter>().sharedMesh =mesh;
    }


    private void Update()=>GenerateMesh();

    private void GenerateMesh()
    {
        mesh.Clear();

        int vcount = VertexCount;
        List<Vector3> Vertices = new List<Vector3>();
        List<Vector3> Normals = new List<Vector3>();
        List<Vector2> uvs = new List<Vector2>();

        for (int i = 0; i < angularSegmentCount+1; i++)
        {
            float t = i / (float)angularSegmentCount;
            float angleRad = t * Mathfs.TAU;
            Vector2 dir = Mathfs.GetUnitVector2ByAngle(angleRad);
            Vertices.Add(dir * radiusOuter);
            Vertices.Add(dir * radiusInner);
            Normals.Add(Vector3.forward);
            Normals.Add(Vector3.forward);


            switch (uvProjection)
            {
                case UvProjection.Radial:
                    uvs.Add(new Vector2(t, 1));
                    uvs.Add(new Vector2(t, 0));
                    break;
                case UvProjection.Z:
                    uvs.Add(dir*0.5f+Vector2.one*0.5f);
                    uvs.Add(dir*(radiusInner * radiusOuter) * 0.5f + Vector2.one * 0.5f);
                    break;
                default:
                    uvs.Add(new Vector2(t, 1));
                    uvs.Add(new Vector2(t, 0));
                    break;
            }
        }

        List<int> triangelIndices = new List<int>();

        for (int i = 0; i < angularSegmentCount; i++)
        {
            int rootIndex = i * 2;
            
            int innerRootIndex = rootIndex + 1;
            int outerNextIndex = rootIndex + 2;
            int innerNextIndex = rootIndex + 3;

            triangelIndices.Add(rootIndex);
            triangelIndices.Add(outerNextIndex);
            triangelIndices.Add(innerNextIndex);

            triangelIndices.Add(rootIndex);
            triangelIndices.Add(innerNextIndex);
            triangelIndices.Add(innerRootIndex);

        }

        mesh.SetVertices(Vertices);
        mesh.SetTriangles(triangelIndices,0);
        mesh.SetNormals(Normals);
        mesh.SetUVs(0, uvs);

    }
}

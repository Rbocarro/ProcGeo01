using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;


[RequireComponent(typeof(MeshFilter))]
public class RoadSegment : MonoBehaviour

{

    [SerializeField] Mesh2D shape2D;

    [Range(2,32)]
    [SerializeField] int edgeRingCount=8;
    [Range(0, 1)]
    [SerializeField] float tTest = 0f;
    [SerializeField]Transform[] controlPoints= new Transform[4];

    Vector3 GetPos(int i) => controlPoints[i].position;

    Mesh mesh;

    private void Awake()
    {
        mesh = new Mesh();
        
        mesh.name = "Segment";
        GetComponent<MeshFilter>().sharedMesh = mesh;

    }


     void Update() => GenerateMesh();
    void GenerateMesh()
    {
        mesh.Clear();
        //verts
        List<Vector3> verts = new List<Vector3>();
        List<Vector3> normals = new List<Vector3>();
        List<Vector2> uvs = new List<Vector2>();
        for (int ring = 0; ring < edgeRingCount+1; ring++)
        {
            float t = ring/(edgeRingCount - 1f);
            OrientedPoint op =GetBezierPoint(t);
            for (int i = 0; i < shape2D.VertexCount; i++)
            {
                verts.Add(op.LocalToWorldPos(shape2D.vertices[i].points));
                normals.Add(op.LocalToWorldVector(shape2D.vertices[i].normals));
                uvs.Add(new Vector2(shape2D.vertices[i].u,t));
            }
        }
        //tris
        List<int> trangleIndices = new List<int>();
        for (int ring = 0; ring < edgeRingCount - 1; ring++)
        {
            int rootIndex = ring * shape2D.VertexCount;
            int rootIndexNext = (ring+1) * shape2D.VertexCount;

            for (int line = 0; line < shape2D.LineCount ; line+=2)
            {
                int lineIndexA = shape2D.lineIndices[line];
                int lineIndexB = shape2D.lineIndices[line +1];
                int CurrentA = rootIndex + lineIndexA;
                int CurrentB = rootIndex + lineIndexB;
                int nextA = rootIndexNext + lineIndexA;
                int nextB = rootIndexNext + lineIndexB;

                trangleIndices.Add(CurrentA);
                trangleIndices.Add(nextA);
                trangleIndices.Add(nextB);

                trangleIndices.Add(CurrentA);
                trangleIndices.Add(nextB);
                trangleIndices.Add(CurrentB);

            }
        }

        
        mesh.SetVertices(verts);
        mesh.SetNormals(normals);
        mesh.SetUVs(0, uvs);
        mesh.SetTriangles(trangleIndices,0);
        //mesh.RecalculateNormals();

    }


    public void OnDrawGizmos()
    {
        for (int i = 0; i < controlPoints.Length; i++)
        {
            Gizmos.DrawSphere(GetPos(i), 0.01f);
        }

        Handles.DrawBezier(GetPos(0), 
                           GetPos(3), 
                           GetPos(1), 
                           GetPos(2), 
                           Color.red, 
                           EditorGUIUtility.whiteTexture,
                           1f);

        Gizmos.color = Color.red;
        OrientedPoint testPoint = GetBezierPoint(tTest);
        //Gizmos.DrawSphere(testPoint, 0.005f);
        Handles.PositionHandle(testPoint.pos, testPoint.rot);

      

        void DrawPoint(Vector2 localPos)=> Gizmos.DrawSphere(testPoint.LocalToWorldPos(localPos),0.15f);

        Vector3[] vert= shape2D.vertices.Select(v => testPoint.LocalToWorldPos(v.points)).ToArray();
        for (int i = 0; i <shape2D.lineIndices.Length; i+=2)
        {
            Vector3 a=vert[shape2D.lineIndices[i]];
            Vector3 b = vert[shape2D.lineIndices[i+1]];

            Gizmos.DrawLine(a, b);
            DrawPoint(shape2D.vertices[i].points);
        }
        DrawPoint(Vector3.right * 0.2f);
        DrawPoint(Vector3.right * 0.1f);
        DrawPoint(Vector3.right * -0.2f);
        DrawPoint(Vector3.right * -0.1f);
        DrawPoint(Vector3.up    * 0.2f);
        DrawPoint(Vector3.up    * 0.1f);



        for (int i = 0; i < mesh.vertexCount; i++)
        {
            Gizmos.DrawSphere(mesh.vertices[i], 0.10f);
        }

        Gizmos.color = Color.white;
    }

    OrientedPoint GetBezierPoint(float t)
    {
        Vector3 p0 = GetPos(0);
        Vector3 p1 = GetPos(1);
        Vector3 p2 = GetPos(2);
        Vector3 p3 = GetPos(3);

        Vector3 a = Vector3.Lerp(p0, p1, t);
        Vector3 b = Vector3.Lerp(p1, p2, t);
        Vector3 c = Vector3.Lerp(p2, p3, t);

        Vector3 d = Vector3.Lerp(a, b, t);
        Vector3 e = Vector3.Lerp(b, c, t);
            
        Vector3 pos= Vector3.Lerp(d, e, t);
        Vector3 tangent= (e - d).normalized;
        return new OrientedPoint(pos, tangent);
    }
}






using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[RequireComponent(typeof(MeshFilter))]
public class RoadSegment : MonoBehaviour
{
    [Range(0, 1)]
    [SerializeField] float tTest = 0f;
    [SerializeField]Transform[] controlPoints= new Transform[4];

    Vector3 GetPos(int i) => controlPoints[i].position;

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






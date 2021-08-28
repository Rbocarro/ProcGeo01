using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuadRing : MonoBehaviour
{

    [Range(0.01f, 1f)]
    [SerializeField] private float radiusInner;
    
    [Range(0.01f, 2f)]
    [SerializeField] private float thickness;

    float radiusOuter => radiusInner + thickness;

    [Range(3, 32)]
    [SerializeField] int angularSegments = 3;



    private void OnDrawGizmosSelected()
    {
        QuadRing.DrawWireCircle(this.transform.position, this.transform.rotation,radiusInner, angularSegments);
        QuadRing.DrawWireCircle(this.transform.position, this.transform.rotation, radiusOuter, angularSegments);
    }

    const float TAU = Mathf.PI * 2;
    public static void DrawWireCircle(Vector3 pos,Quaternion rot,float radius,int detailLevel=32)
    {
        Vector3[] points3D = new Vector3[detailLevel];
        for(int i=0;i<detailLevel;i++)
        {
            float t=i/(float)detailLevel;
            float angleRad = t * TAU;

            Vector2 point2D= new Vector2(Mathf.Cos(angleRad)*radius,
                                          Mathf.Sin(angleRad)*radius);

           points3D[i] = pos+rot * point2D;
        }

        for (int i = 0; i < detailLevel-1; i++)
        {
            Gizmos.DrawLine(points3D[i], points3D[i + 1]);
        }
        Gizmos.DrawLine(points3D[detailLevel - 1], points3D[0]);


    }
    void Start()
    {
        
    }


    void Update()
    {
        
    }
}

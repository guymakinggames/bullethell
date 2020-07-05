using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(MeshCollider))]
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class pyramidShape : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        Vector3 p0 = new Vector3(-0.5f, -0.25f, 0);
        Vector3 p1 = new Vector3(0.5f, -0.25f, 0);
        Vector3 p2 = new Vector3(0f, 0f, Mathf.Sqrt(0.75f));
        Vector3 p3 = new Vector3(0f, -0.1f, 0f);
        Vector3 p4 = new Vector3(0f, 0.25f, 0);

        Mesh mesh = meshFilter.sharedMesh;
        if (mesh == null)
        {
            meshFilter.mesh = new Mesh();
            mesh = meshFilter.sharedMesh;
        }
        mesh.Clear();

        mesh.vertices = new Vector3[]{
                p0, p1, p2, p3, p4
            };
        mesh.triangles = new int[]{
                0, 4, 3,
                0, 2, 4,
                0, 3, 2,

                3, 4, 1,
                3, 1, 2,
                1, 4, 2
            };

        Vector2 uv0 = new Vector2(0, 0);
        Vector2 uv1 = new Vector2(1, 0);
        Vector2 uv2 = new Vector2(0.5f, 1);

        mesh.uv = new Vector2[]{
                uv0,uv1,uv2,
                uv0,uv1
            };

        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

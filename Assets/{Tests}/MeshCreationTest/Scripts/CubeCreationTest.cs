using UnityEngine;

public class CubeCreationTest : MonoBehaviour
{
    void Start()
    {
        Vector3[] vertices = {
        new Vector3(-1f, 1f, 1f),
        new Vector3(1f, 1f, 1f),
        new Vector3(1f, 1f, -1f),
        new Vector3(-1f, 1f, -1f),
        };

        Vector2[] Uvs =
        {
            new Vector2(0f,10f),
            new Vector2(10f,10f),
            new Vector2(10f,0f),
            new Vector2(0f,0f),
        };

        int[] triangles =
        {
            0,1,2,
            0,2,3,
        };

        Vector3[] normals =
            {
        Vector3.up,
        Vector3.up,
        Vector3.up,
        Vector3.up,
        };

        Mesh mesh = new();
        mesh.vertices = vertices;
        mesh.normals = normals;
        mesh.uv = Uvs;
        mesh.triangles = triangles;

        MeshFilter meshFilter = GetComponent<MeshFilter>();
        meshFilter.mesh = mesh;
    }
}

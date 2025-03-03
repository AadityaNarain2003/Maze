using System.IO;
using UnityEngine;

public class HeatmapMeshGenerator : MonoBehaviour
{
    public int gridSize = 10; // Number of cells in one direction
    public float cellSize = 1f; // Size of each cell
    private float[,] heatmap; // Heatmap matrix

    public GameObject player; // Reference to the player object

    private void Start()
    {
        // Initialize heatmap
        heatmap = new float[gridSize, gridSize];
    }

    private void Update()
    {
        UpdateHeatmap();
        GenerateMeshAndSave();
    }

    void UpdateHeatmap()
    {
        // Get player's current position
        Vector3 playerPosition = player.transform.position;

        // Convert player's position to grid coordinates
        int gridX = Mathf.Clamp((int)((playerPosition.x + (gridSize / 2f) * cellSize) / cellSize), 0, gridSize - 1);
        int gridZ = Mathf.Clamp((int)((playerPosition.z + (gridSize / 2f) * cellSize) / cellSize), 0, gridSize - 1);

        // Increment the heatmap value at the grid cell
        heatmap[gridX, gridZ] += Time.deltaTime;
    }

    public void GenerateMeshAndSave()
    {
        // Create a mesh from the heatmap
        Mesh heatmapMesh = GenerateMeshFromHeatmap();

        // Save the mesh to a file
        SaveMeshToFile(heatmapMesh, "HeatmapMesh.obj");
    }

    Mesh GenerateMeshFromHeatmap()
    {
        Mesh mesh = new Mesh();

        int verticesCount = gridSize * gridSize;
        Vector3[] vertices = new Vector3[verticesCount];
        int[] triangles = new int[(gridSize - 1) * (gridSize - 1) * 6];
        Vector2[] uvs = new Vector2[verticesCount];

        int vertexIndex = 0;
        int triangleIndex = 0;

        for (int z = 0; z < gridSize; z++)
        {
            for (int x = 0; x < gridSize; x++)
            {
                // Generate vertex position and UV
                float height = heatmap[x, z]; // Use heatmap value as height
                vertices[vertexIndex] = new Vector3(x * cellSize, height, z * cellSize);
                uvs[vertexIndex] = new Vector2((float)x / gridSize, (float)z / gridSize);

                // Generate triangles
                if (x < gridSize - 1 && z < gridSize - 1)
                {
                    int current = vertexIndex;
                    int next = vertexIndex + 1;
                    int below = vertexIndex + gridSize;
                    int belowNext = vertexIndex + gridSize + 1;

                    // Two triangles per square
                    triangles[triangleIndex++] = current;
                    triangles[triangleIndex++] = below;
                    triangles[triangleIndex++] = next;

                    triangles[triangleIndex++] = next;
                    triangles[triangleIndex++] = below;
                    triangles[triangleIndex++] = belowNext;
                }

                vertexIndex++;
            }
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uvs;

        mesh.RecalculateNormals();
        return mesh;
    }

    void SaveMeshToFile(Mesh mesh, string fileName)
    {
        string filePath = Path.Combine(Application.dataPath, fileName);
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            // Write vertices
            foreach (Vector3 vertex in mesh.vertices)
            {
                writer.WriteLine($"v {vertex.x} {vertex.y} {vertex.z}");
            }

            // Write UVs
            foreach (Vector2 uv in mesh.uv)
            {
                writer.WriteLine($"vt {uv.x} {uv.y}");
            }

            // Write normals
            foreach (Vector3 normal in mesh.normals)
            {
                writer.WriteLine($"vn {normal.x} {normal.y} {normal.z}");
            }

            // Write faces (1-based indexing for .obj files)
            for (int i = 0; i < mesh.triangles.Length; i += 3)
            {
                writer.WriteLine($"f {mesh.triangles[i] + 1}/{mesh.triangles[i] + 1}/{mesh.triangles[i] + 1} " +
                                 $"{mesh.triangles[i + 1] + 1}/{mesh.triangles[i + 1] + 1}/{mesh.triangles[i + 1] + 1} " +
                                 $"{mesh.triangles[i + 2] + 1}/{mesh.triangles[i + 2] + 1}/{mesh.triangles[i + 2] + 1}");
            }
        }

        Debug.Log($"Mesh saved to {fileName}");
    }
}

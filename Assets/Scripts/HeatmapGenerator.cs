using UnityEngine;

public class HeatmapMeshGenerator : MonoBehaviour
{
    public int gridSize = 10; // Number of cells in one direction
    public float cellSize = 1f; // Size of each cell
    private float[,] heatmap; // Heatmap matrix

    public GameObject player; // Reference to the player object
    public Material heatmapMaterial; // Material to apply to the heatmap mesh

    private MeshFilter meshFilter;
    private MeshRenderer meshRenderer;
    private Mesh heatmapMesh;
    public float xOffset = 20f; // Offset to move the heatmap to the right

    private void Start()
    {
        // Initialize heatmap
        heatmap = new float[gridSize, gridSize];

        // Create a GameObject to hold the heatmap mesh
        GameObject heatmapObject = new GameObject("HeatmapMesh");
        meshFilter = heatmapObject.AddComponent<MeshFilter>();
        meshRenderer = heatmapObject.AddComponent<MeshRenderer>();

        // Assign the material
        meshRenderer.material = heatmapMaterial;

        // Generate initial mesh
        heatmapMesh = GenerateMeshFromHeatmap();
        meshFilter.mesh = heatmapMesh;

        // Move the heatmap to the right
        heatmapObject.transform.position = new Vector3(xOffset, 0, 0);
    }

    private void Update()
    {
        UpdateHeatmap();
        UpdateMesh();
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

    public void UpdateMesh()
    {
        // Create a mesh from the heatmap
        heatmapMesh = GenerateMeshFromHeatmap();
        meshFilter.mesh = heatmapMesh;
    }

    Mesh GenerateMeshFromHeatmap()
    {
        if (heatmapMesh == null)
        {
            heatmapMesh = new Mesh();
        }
        else
        {
            heatmapMesh.Clear();
        }

        int verticesCount = gridSize * gridSize;
        Vector3[] vertices = new Vector3[verticesCount];
        int[] triangles = new int[(gridSize - 1) * (gridSize - 1) * 6];
        Color[] colors = new Color[verticesCount];
        int vertexIndex = 0;
        int triangleIndex = 0;

        for (int z = 0; z < gridSize; z++)
        {
            for (int x = 0; x < gridSize; x++)
            {
                // Generate vertex position and UV
                float height = heatmap[x, z]; // Use heatmap value as height
                vertices[vertexIndex] = new Vector3(x * cellSize, height, z * cellSize);

                // Generate vertex color
                float normalizedHeight = Mathf.Clamp01(height / 5f); // Normalize height for color mapping
                // Modified line:
                colors[vertexIndex] = Color.Lerp(Color.white, Color.red, normalizedHeight);

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

        heatmapMesh.vertices = vertices;
        heatmapMesh.triangles = triangles;
        heatmapMesh.colors = colors;

        heatmapMesh.RecalculateNormals();
        return heatmapMesh;
    }
}

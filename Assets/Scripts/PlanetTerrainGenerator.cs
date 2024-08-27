using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshCollider))]
public class PlanetTerrainGenerator : MonoBehaviour
{
    public float noiseScale = 0.3f;  // Controls the scale of the noise
    public float heightMultiplier = 0.5f;  // Controls the maximum height variation

    private Mesh sphereMesh;
    private Vector3[] originalVertices;
    private Vector3[] modifiedVertices;

    void Start()
    {
        // Get the mesh from the sphere
        sphereMesh = GetComponent<MeshFilter>().mesh;

        // Get the original vertices of the sphere
        originalVertices = sphereMesh.vertices;
        modifiedVertices = new Vector3[originalVertices.Length];

        // Apply noise to the sphere's vertices
        GenerateTerrain();

        // Update the collider with the new mesh
        UpdateCollider();
    }

    void GenerateTerrain()
    {
        for (int i = 0; i < originalVertices.Length; i++)
        {
            // Get the original position of the vertex
            Vector3 originalVertex = originalVertices[i];

            // Calculate the noise value based on the vertex position
            float noiseValue = Mathf.PerlinNoise(
                originalVertex.x * noiseScale + transform.position.x,
                originalVertex.z * noiseScale + transform.position.z
            );

            // Adjust the vertex height based on the noise value
            float height = 1 + (noiseValue * heightMultiplier);

            // Update the vertex position while keeping its direction the same
            modifiedVertices[i] = originalVertex.normalized * height;
        }

        // Update the mesh with the new vertices
        sphereMesh.vertices = modifiedVertices;

        // Recalculate normals to ensure lighting is correct
        sphereMesh.RecalculateNormals();

        // Recalculate bounds to update the mesh's bounding volume
        sphereMesh.RecalculateBounds();
    }

    void UpdateCollider()
    {
        // Get the MeshCollider component
        MeshCollider meshCollider = GetComponent<MeshCollider>();

        // Assign the modified mesh to the MeshCollider
        meshCollider.sharedMesh = null; // Clear the existing mesh to ensure proper update
        meshCollider.sharedMesh = sphereMesh; // Assign the modified mesh
    }
}

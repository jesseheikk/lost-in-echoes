using UnityEngine;
using System.Collections.Generic;

public class PlanetPopulator : MonoBehaviour
{
    [SerializeField] GameObject[] blockPrefabs;
    [SerializeField] float blockScaleFactor = 1.0f; // TODO: Scale this automatically to fill the planet

    Mesh icosphereMesh;
    Vector3[] vertices;

    void Start()
    {
        // Get the MeshFilter component and access the mesh and vertices
        icosphereMesh = GetComponent<MeshFilter>().mesh;
        vertices = icosphereMesh.vertices;

        // Populate the icosphere with blocks at each vertex
        Populate();
    }

    void Populate()
    {
        // Keep track vertex positions as they overlap each other
        // to prevent spawning blocks in duplicate locations
        HashSet<Vector3> uniquePositions = new HashSet<Vector3>();

        foreach (Vector3 vertex in vertices)
        {
            // Convert local vertex position to world position
            Vector3 spawnPosition = transform.TransformPoint(vertex);

            // Add the spawn position to the HashSet if it's not already there
            if (uniquePositions.Add(spawnPosition))
            {
                // Choose a random prefab from the array
                GameObject selectedPrefab = blockPrefabs[Random.Range(0, blockPrefabs.Length)];

                // Instantiate the block at the calculated position
                SpawnBlock(selectedPrefab, spawnPosition);
            }
        }
    }

    void SpawnBlock(GameObject prefab, Vector3 position)
    {
        // Instantiate the block at the specified position
        GameObject block = Instantiate(prefab, position, Quaternion.identity, transform);

        // Adjust the block's rotation to face outward from the planet's center
        block.transform.up = position.normalized;

        // Adjust the block's position to ensure it is perfectly on the surface
        float distanceFromCenter = position.magnitude;
        Vector3 surfacePosition = position.normalized * distanceFromCenter;
        block.transform.position = surfacePosition;

        // Scale the block according to the provided factor
        block.transform.localScale *= blockScaleFactor;
    }
}

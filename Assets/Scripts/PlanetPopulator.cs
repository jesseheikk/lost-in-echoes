using UnityEngine;
using System.Collections.Generic;

public class PlanetPopulator : MonoBehaviour
{
    [SerializeField] GameObject[] blockPrefabs;
    [SerializeField] float blockScaleFactor = 1.0f; // TODO: Scale this automatically according to planet size

    Mesh icosphereMesh;
    Vector3[] vertices;

    void Start()
    {
        // Get the MeshFilter component to access the vertices
        icosphereMesh = GetComponent<MeshFilter>().mesh;
        vertices = icosphereMesh.vertices;

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

            if (uniquePositions.Add(spawnPosition))
            {
                GameObject randomPrefab = blockPrefabs[Random.Range(0, blockPrefabs.Length)];
                SpawnBlock(randomPrefab, spawnPosition);
            }
        }
    }

    void SpawnBlock(GameObject prefab, Vector3 position)
    {
        GameObject block = Instantiate(prefab, position, Quaternion.identity, transform);

        // Adjust the block's rotation to face outward from the planet's center
        block.transform.up = position.normalized;

        // Adjust the block's position to ensure it lays on the surface without any gaps
        float distanceFromCenter = position.magnitude;
        Vector3 surfacePosition = position.normalized * distanceFromCenter;
        block.transform.position = surfacePosition;

        // Scale the block according to the provided factor
        block.transform.localScale *= blockScaleFactor;
    }
}

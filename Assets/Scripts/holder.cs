// using UnityEngine;

// public class PlanetPopulator : MonoBehaviour
// {
//     [SerializeField] GameObject[] rockPrefabs;
//     [SerializeField] GameObject[] treePrefabs;
//     [SerializeField] GameObject[] decorationPrefabs;

//     [SerializeField] int numberOfTrees = 10;
//     [SerializeField] int numberOfRocks = 10;
//     [SerializeField] int numberOfDecorations = 10;
//     //[SerializeField] float scaleMultiplier = 0.2f;

//     Mesh sphereMesh;
//     Vector3[] vertices;

//     void Start()
//     {
//         sphereMesh = GetComponent<MeshFilter>().mesh;
//         vertices = sphereMesh.vertices;

//         PopulateWholeSurface();

//         // PopulateSurface(rockPrefabs, numberOfRocks);
//         // PopulateSurface(treePrefabs, numberOfTrees);
//         // PopulateSurface(decorationPrefabs, numberOfDecorations);
//     }

//     void PopulateWholeSurface()
//     {
//         for (int i = 0; i < vertices.Length; i++)
//         {
//             Vector3 spawnPosition = transform.TransformPoint(vertices[i]);
//             GameObject selectedPrefab = rockPrefabs[Random.Range(0, rockPrefabs.Length)];
//             SpawnPrefabOnSurface(selectedPrefab, spawnPosition);
//         }
//     }

//     void PopulateSurface(GameObject[] prefabs, int numberOfSpawns)
//     {
//         for (int i = 0; i < numberOfSpawns; i++)
//         {
//             // Choose a random vertex from the terrain
//             int randomIndex = Random.Range(0, vertices.Length);
//             Vector3 spawnPosition = transform.TransformPoint(vertices[randomIndex]);

//             // Choose a random prefab from the array
//             GameObject selectedPrefab = prefabs[Random.Range(0, prefabs.Length)];
//             SpawnPrefabOnSurface(selectedPrefab, spawnPosition);
//         }
//     }

//     void SpawnPrefabOnSurface(GameObject prefab, Vector3 spawnPosition)
//     {
//         GameObject spawnedObject = Instantiate(prefab, spawnPosition, Quaternion.identity);
//         spawnedObject.transform.up = spawnPosition.normalized;
//     }

//     // void SpawnTrees()
//     // {
//     //     for (int i = 0; i < numberOfTrees; i++)
//     //     {
//     //         // Choose a random vertex from the terrain
//     //         int randomIndex = Random.Range(0, vertices.Length);
//     //         Vector3 spawnPosition = transform.TransformPoint(vertices[randomIndex]);

//     //         // Choose a random prefab from the array
//     //         GameObject selectedPrefab = treePrefabs[Random.Range(0, treePrefabs.Length)];
//     //         SpawnPrefabOnSurface(selectedPrefab, spawnPosition);
//     //     }
//     // }

//     // void SpawnRocks()
//     // {
//     //     for (int i = 0; i < numberOfRocks; i++)
//     //     {
//     //         // Choose a random vertex from the terrain
//     //         int randomIndex = Random.Range(0, vertices.Length);
//     //         Vector3 spawnPosition = transform.TransformPoint(vertices[randomIndex]);

//     //         // Choose a random prefab from the array
//     //         GameObject selectedPrefab = rockPrefabs[Random.Range(0, rockPrefabs.Length)];
//     //         SpawnPrefabOnSurface(selectedPrefab, spawnPosition);
//     //     }  
//     // }
// }

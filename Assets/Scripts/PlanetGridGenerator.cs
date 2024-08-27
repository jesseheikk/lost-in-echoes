using UnityEngine;

public class SphericalGridGenerator : MonoBehaviour
{
    public GameObject prefab;
    //public Transform planet;
    public int resolution = 10; // Number of divisions along each axis

    void Start()
    {
        GenerateGrid();
    }

    void GenerateGrid()
    {
        float phiStep = Mathf.PI / resolution;
        float thetaStep = 2 * Mathf.PI / resolution;

        for (int i = 0; i < resolution; i++)
        {
            float phi = i * phiStep;
            for (int j = 0; j < resolution; j++)
            {
                float theta = j * thetaStep;

                // Convert spherical coordinates to Cartesian
                float x = Mathf.Sin(phi) * Mathf.Cos(theta);
                float y = Mathf.Cos(phi);
                float z = Mathf.Sin(phi) * Mathf.Sin(theta);

                Vector3 spawnPosition = new Vector3(x, y, z) * transform.localScale.x / 2f;
                Vector3 spawnWorldPosition = transform.TransformPoint(spawnPosition);

                GameObject instance = Instantiate(prefab, spawnWorldPosition, Quaternion.identity, transform);
                instance.transform.up = spawnPosition.normalized; // Align with the surface
            }
        }
    }
}

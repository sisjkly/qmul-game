using UnityEngine;

public class NoiseBasedSphereGenerator : MonoBehaviour
{
    public int numberOfSpheres = 100;
    public float noiseScale = 5.0f;
    public float heightVariation = 1.0f; 
    public GameObject spherePrefab;
    public GameObject plane;

    void Start()
    {
        if (plane == null)
        {
            Debug.LogError("Plane GameObject is not assigned!");
            return;
        }

        GenerateSpheres();
    }

    void GenerateSpheres()
    {
        Bounds planeBounds = plane.GetComponent<Renderer>().bounds;
        for (int i = 0; i < numberOfSpheres; i++)
        {
            float x = Random.Range(planeBounds.min.x, planeBounds.max.x);
            float z = Random.Range(planeBounds.min.z, planeBounds.max.z);
           
            float noise = Mathf.PerlinNoise(x / noiseScale, z / noiseScale) * heightVariation;
            float y = plane.transform.position.y + noise;

            Vector3 spherePosition = new Vector3(x, y, z);
            Instantiate(spherePrefab, spherePosition, Quaternion.identity);
        }
    }
}

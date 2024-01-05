using UnityEngine;

public class NoiseTextureGenerator : MonoBehaviour
{
    public int textureWidth = 256;
    public int textureHeight = 256;
    public float noiseScale = 20.0f;

    private Texture2D GenerateNoiseTexture()
    {
        Texture2D noiseTexture = new Texture2D(textureWidth, textureHeight);
        for (int y = 0; y < noiseTexture.height; y++)
        {
            for (int x = 0; x < noiseTexture.width; x++)
            {
                float xCoord = (float)x / noiseTexture.width * noiseScale;
                float yCoord = (float)y / noiseTexture.height * noiseScale;
                float sample = Mathf.PerlinNoise(xCoord, yCoord);
                Color color = new Color(sample, sample, sample);
                noiseTexture.SetPixel(x, y, color);
            }
        }
        noiseTexture.Apply();
        return noiseTexture;
    }

    void Start()
    {
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            Texture2D noiseTexture = GenerateNoiseTexture();
            renderer.material.mainTexture = noiseTexture;
        }
        else
        {
            Debug.LogError("Renderer not found on the GameObject.");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _00_PerlinNoise : MonoBehaviour
{
    [SerializeField]
    int textureSize = 32;

    [SerializeField]
    GameObject textureSample;

    Texture2D noiseTexture;

    // Start is called before the first frame update
    void Start()
    {
        noiseTexture = new Texture2D(textureSize, textureSize);
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < textureSize; i++)
        {
            for(int j = 0; j < textureSize; j++)
            {
                float grayCol = Mathf.PerlinNoise((float)i / (float)textureSize, (float)j / (float)textureSize);
                Color color = new Color(grayCol, grayCol, grayCol);
                noiseTexture.SetPixel(i, j, color);
                noiseTexture.Apply();
            }
        }

        textureSample.GetComponent<Renderer>().material.mainTexture = noiseTexture;
    }
}

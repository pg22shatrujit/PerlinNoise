using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _00_PerlinNoise : MonoBehaviour
{
    [SerializeField]
    int textureSize = 32,
        sceneSize = 512;

    [SerializeField, Range(1, 200)]
    int heightMultiplier = 20;

    [SerializeField, Range(0, 20)]
    float zoomFactor = 1;

    [SerializeField]
    GameObject textureSample;

    Texture2D noiseTexture;

    [SerializeField]
    GameObject sampleCube;

    float offset = 0,
          cubeSize = 0;

    // Start is called before the first frame update
    void Start()
    {
        setOffset();
        setCubeSize();
        noiseTexture = new Texture2D(textureSize, textureSize);

        InvokeRepeating("runBlocks", 0, 0.25f);
    }

    private void setCubeSize()
    {
        cubeSize = (float)sceneSize / (float)textureSize;
    }

    // Update is called once per frame
    void runBlocks()
    {
        foreach(Transform any in gameObject.transform)
        {
            Destroy(any.gameObject);
        }
        makeTiles();
    }

    void makeTiles ()
    {
        for (int i = 0; i < textureSize; i++)
        {
            for (int j = 0; j < textureSize; j++)
            {
                //noise intensity
                float grayCol = Mathf.PerlinNoise(getTextureCoordinate(i), getTextureCoordinate(j));
                Color color = new Color(grayCol, grayCol, grayCol);
                noiseTexture.SetPixel(i, j, color);

                Vector3 copyPos = new Vector3(i * cubeSize, grayCol * heightMultiplier, j * cubeSize);
                GameObject copy = Instantiate(sampleCube);
                copy.transform.localScale = new Vector3(cubeSize, cubeSize, cubeSize);
                copy.transform.position = copyPos;

                //Make tiles a child of the manager
                copy.transform.parent = gameObject.transform;
            }
        }
        noiseTexture.Apply();

        textureSample.GetComponent<Renderer>().material.mainTexture = noiseTexture;
    }

    float getTextureCoordinate(int coordinate)
    {
        return (float)coordinate * zoomFactor / (float)textureSize + Time.time - offset;
    }

    void setOffset()
    {
        offset = (float)textureSize / 2;
    }
}

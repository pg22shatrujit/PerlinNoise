using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _01_TextureElevation : MonoBehaviour
{
    int textureSize = 1080;
    [SerializeField] int gridSize = 32;

    [SerializeField, Range(1, 200)]
    int heightMultiplier = 20;

    [SerializeField]
    GameObject textureSample;

    [SerializeField]
    GameObject sampleCube;

    Object[] textureSequence;
    int currentIndex = 0;

    float offset = 0,
          cubeSize = 0;

    // Start is called before the first frame update
    void Start()
    {
        textureSequence = Resources.LoadAll("Face_Archive", typeof(Texture2D));
        setOffset();
        setCubeSize();

        InvokeRepeating("runBlocks", 0, 0.25f);
    }

    private void setCubeSize()
    {
        cubeSize = (float)textureSize / (float)gridSize;
    }

    // Update is called once per frame
    void runBlocks()
    {
        foreach (Transform any in gameObject.transform)
        {
            Destroy(any.gameObject);
        }
        getNewFace();
    }

    void getNewFace()
    {
        currentIndex++;
        currentIndex %= textureSequence.Length;
        Texture2D myTexture = (Texture2D)textureSequence[currentIndex];
        for (int i = 0; i < gridSize; i++)
        {
            for (int j = 0; j < gridSize; j++)
            {

                Color thisPixel = myTexture.GetPixel(Mathf.FloorToInt(i * cubeSize),
                                                     Mathf.FloorToInt(j * cubeSize));

                float height = thisPixel.g * heightMultiplier;

                float xPos = (i - offset) * cubeSize,
                      zPos = (j - offset) * cubeSize;

                Vector3 copyPos = new Vector3(xPos, height, zPos);
                GameObject copy = Instantiate(sampleCube);
                copy.transform.localScale = new Vector3(cubeSize, cubeSize, cubeSize);
                copy.transform.position = copyPos;

                //Make tiles a child of the manager
                copy.transform.parent = gameObject.transform;
            }
        }

        textureSample.GetComponent<Renderer>().material.mainTexture = myTexture;

    }

    float getTextureCoordinate(int coordinate)
    {
        return coordinate - offset;
    }

    void setOffset()
    {
        offset = (float)gridSize / 2;
    }
}

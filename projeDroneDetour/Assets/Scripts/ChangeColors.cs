using UnityEngine;
using System;

public class ChangeColors : MonoBehaviour
{
    [SerializeField]
    Color[] color = new Color[20];

    public Material gameboyMaterial;
    public Material identityMaterial;

    private RenderTexture _downscaledRenderTexture;

    public static bool canChange = true;
    public static int n;

    void OnEnable()
    {
        var camera = GetComponent<Camera>();
        _downscaledRenderTexture = new RenderTexture(camera.pixelWidth, camera.pixelHeight, 16);
        _downscaledRenderTexture.filterMode = FilterMode.Point;
    }

    void OnDisable()
    {
        Destroy(_downscaledRenderTexture);
    }

    void OnRenderImage(RenderTexture src, RenderTexture dst)
    {
        try
        {
            ChangePalette();
            Graphics.Blit(src, _downscaledRenderTexture, gameboyMaterial);
            Graphics.Blit(_downscaledRenderTexture, dst, identityMaterial);
        }
        catch(Exception ex)
        {
            Debug.LogError(ex);
        }
    }

    public void ChangePalette()
    {
        //if true, will get a random number to set the game palette
        if (canChange)
        { 
            canChange = false;
            n = UnityEngine.Random.Range(0, 5);
        }
        //configuration of the game palette
        switch (n)
        {
            case 0:
                gameboyMaterial.SetColor("_Darkest", color[0]);
                gameboyMaterial.SetColor("_Dark", color[1]);
                gameboyMaterial.SetColor("_Ligt", color[2]);
                gameboyMaterial.SetColor("_Ligtest", color[3]);
                break;

            case 1:
                gameboyMaterial.SetColor("_Darkest", color[4]);
                gameboyMaterial.SetColor("_Dark", color[5]);
                gameboyMaterial.SetColor("_Ligt", color[6]);
                gameboyMaterial.SetColor("_Ligtest", color[7]);
                break;

            case 2:
                gameboyMaterial.SetColor("_Darkest", color[8]);
                gameboyMaterial.SetColor("_Dark", color[9]);
                gameboyMaterial.SetColor("_Ligt", color[10]);
                gameboyMaterial.SetColor("_Ligtest", color[11]);
                break;

            case 3:
                gameboyMaterial.SetColor("_Darkest", color[12]);
                gameboyMaterial.SetColor("_Dark", color[13]);
                gameboyMaterial.SetColor("_Ligt", color[14]);
                gameboyMaterial.SetColor("_Ligtest", color[15]);
                break;

            case 4:
                gameboyMaterial.SetColor("_Darkest", color[16]);
                gameboyMaterial.SetColor("_Dark", color[17]);
                gameboyMaterial.SetColor("_Ligt", color[18]);
                gameboyMaterial.SetColor("_Ligtest", color[19]);
                break;
        }
    }
}

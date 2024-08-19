using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTextureSetup : MonoBehaviour
{
    public Camera cameraB;
    public Camera cameraA;
    public Material cameraMaterialA;
    public Material cameraMaterialB;
    void Start()
    {
        if (cameraB.targetTexture != null)
            cameraB.targetTexture.Release();

        cameraB.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
        cameraMaterialB.mainTexture = cameraB.targetTexture;

        if (cameraA.targetTexture != null)
            cameraA.targetTexture.Release();

        cameraA.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
        cameraMaterialA.mainTexture = cameraA.targetTexture;

    }

    
}

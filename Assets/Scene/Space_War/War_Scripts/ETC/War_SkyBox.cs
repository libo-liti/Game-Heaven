using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class War_SkyBox : MonoBehaviour
{
    private float customTime = 0f;
    public Material skyboxMaterial;

    void Start()
    {
        customTime = 0f;
    }

    void Update()
    {
        customTime += Time.deltaTime;
        skyboxMaterial.SetFloat("_Rotation", customTime * 5f);
    }
}
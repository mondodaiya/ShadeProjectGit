using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorColor : MonoBehaviour
{
    MeshRenderer meshRenderer;

    public Texture Texture01;
    public Texture Texture02;
    public Texture Texture03;
    public string keyCode;

    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        keyCode = Input.inputString;
        Debug.Log(keyCode);

        switch (keyCode)
        {
            case "1":
                meshRenderer.material.SetTexture("_MainTex", Texture01);
                break;
            case "2":
                meshRenderer.material.SetTexture("_MainTex", Texture02);
                break;
            case "3":
                meshRenderer.material.SetTexture("_MainTex", Texture03);
                break;
            default:
                Debug.Log("Default");
                break;
        }
    }
}

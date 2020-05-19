using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CylinderController : MonoBehaviour
{
public GameObject CylinderPrefab;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(CylinderPrefab);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

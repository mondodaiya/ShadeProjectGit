using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SerialHandler : MonoBehaviour
{
    SerialPortUtility.SerialPortUtilityPro serialPort;
    private string _1 = "1";

    void Awake()
    {
        serialPort = this.GetComponent<SerialPortUtility.SerialPortUtilityPro>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && serialPort != null)
        {
            if (serialPort.IsOpened())
            {
                serialPort.WriteCR(_1);
            }
        }
        
    }
}

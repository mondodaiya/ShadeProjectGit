using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnRoad : MonoBehaviour
{
    GameObject trafficlight; //SoundMasterそのものが入る関数
    TrafficLight script;     //MasterScriptが入る変数
    public Color color;

    // Start is called before the first frame update
    void Start()
    {
        trafficlight = GameObject.Find("TrafficLight");
        script = trafficlight.GetComponent<TrafficLight>();       
        
    }

    // Update is called once per frame
    void Update()
    {
        bool stopGo = script.stopGo;
        if(stopGo == false){
            color.a = 1.0f;
            gameObject.GetComponent<Renderer>().material.color = color;
        }else if(stopGo == true){
            color.a = 0.0f;
            gameObject.GetComponent<Renderer>().material.color = color;
        }
    }
}

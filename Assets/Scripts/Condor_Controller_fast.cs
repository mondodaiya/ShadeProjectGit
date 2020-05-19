using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Condor_Controller_fast : MonoBehaviour
{
    public float height_max = 9.0f;
    public float height_min = 1.0f;
    public float speed = 0.06f;
    private bool updown;

    void Start()
    {
      updown = true;
          }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y < height_min | transform.position.y > height_max){
            updown = !updown;
        }

        if(updown == true){
            transform.Translate(0,speed,0);
        }else{
            transform.Translate(0,-speed,0);
        }
    }
}

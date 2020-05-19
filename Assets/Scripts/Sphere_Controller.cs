using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sphere_Controller : MonoBehaviour
{
    public float height_max = 3.0f;
    public float height_min = 1.0f;
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
            transform.Translate(0,0.01f,0);
        }else{
            transform.Translate(0,-0.01f,0);
        }
    }
}

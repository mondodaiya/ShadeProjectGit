using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMove : MonoBehaviour
{
    private float time;
    private float timeOut;
    // Start is called before the first frame update
    void Start()
    {
        timeOut = 6.0f;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        this.gameObject.transform.Translate(0,0,Random.Range(0.1f,0.2f));
        if(time >= timeOut){
            Destroy(this.gameObject);
            timeOut = 0.0f;
        }
    }
}

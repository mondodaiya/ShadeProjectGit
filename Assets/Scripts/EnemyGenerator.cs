using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    public GameObject enemyPrefab;  //Prefabを格納する場所
    public float interval;  // 敵生成時間間隔
    public float time = 0f; // 経過時間
    GameObject trafficlight; //SoundMasterそのものが入る関数
    TrafficLight script;     //MasterScriptが入る変数

    // Start is called before the first frame update
    void Start()
    {  
        interval = 5f;  //インターバルの決定
        trafficlight = GameObject.Find("TrafficLight");
        script = trafficlight.GetComponent<TrafficLight>();        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        bool stopGo = script.stopGo;
        if(time > interval && stopGo == false){
            GameObject enemy = Instantiate(enemyPrefab);
            time = 0f;
        }
        
    }
}

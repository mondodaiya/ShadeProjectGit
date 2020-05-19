using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vection2D : MonoBehaviour
{
    public Transform target;
    public float defaultDist;
    public float targetDist;
    public bool onOff;
    public bool turnOff;
    public float targetDistFrame;
    public float defaultDistFrame;
    Vector3 targetPos;
    Vector3 Pos;
    Vector3 direction;
    Vector3 directionReturn;
    public float accelerationY;
    public float circleR = 30.0f;   //地上サインが動く範囲の半径
    // public GameObject player;
    // public GameObject cylinder;
    public float dist;
    public float rate;
    public float ratio = 0.00001f;
    private float vectorDist;
    private float vectorDistRatio;

    void Start()
    {
        onOff = false;
        turnOff = false;
    }

    // Update is called once per frame
    void Update()
    {

        // Vector3 acceleration = OVRInput.GetLocalControllerAcceleration(OVRInput.Controller.RTouch);
        // accelerationY = acceleration.y;
        // if(accelerationY > 20.0f && onOff == false){ // Aボタンを押したら
        //     onOff = true;   //ボタンのフラグをONにする
        //     StartCoroutine("Raise"); //コルーチンを呼び出す
        // }else if(accelerationY < -20.0f && onOff == false){
        //     onOff = true;
        //     StartCoroutine("Fall");
        // }

        if(OVRInput.GetDown(OVRInput.RawButton.A) && onOff == false){ // Aボタンを押したら
        onOff = true;   //ボタンのフラグをONにする
        StartCoroutine("Fall"); //コルーチンを呼び出す
        }else if(OVRInput.GetDown(OVRInput.RawButton.B) && onOff == false){
            onOff = true;
            StartCoroutine("Fall");
        }
    }

    IEnumerator Raise(){
        Pos = transform.position;   //円柱のポジション
        // targetPos = target.position;    //プレイヤーのポジション
         Vector3 targetPos = GameObject.Find("OVRPlayerController").transform.position;
        dist = Vector3.Distance(Pos,targetPos);     //プレイヤーと円柱の間の距離を計算
        Debug.Log("Distance = " + dist);
        rate = circleR/dist;    //距離に応じた比率を計算
        Debug.Log("Rate = " + rate);

         int counter = 0;    //カウンターの用意

        while(true){    //ループ処理

            if(counter <= 20){
                //  f = 1.0f - (counter * 0.0495f);
                //  transform.localScale = new Vector3(1,1,1);

                 var heading = targetPos - Pos;  //自分からプレイヤーへのベクトル
                 heading.y = 0;  //垂直方向のベクトル計算を排除
                 vectorDist = Mathf.Abs(heading.x) + Mathf.Abs(heading.z);
                 vectorDistRatio = circleR / vectorDist;
                 Debug.Log("vectorDistRatio = " + vectorDistRatio);
                 heading.Normalize();   //向きはそのままで長さを１のベクトルに変更
                 var head = heading / 10000.0f * vectorDistRatio * ratio;
                 transform.Translate(head);
                 counter ++;
            }else if(counter <= 100){
                //  f = 0.01f + ((counter-20) * 0.012375f);
                //  transform.localScale = new Vector3(1,1,1);

                 var headingReturn = Pos - targetPos;
                 headingReturn.y = 0;
                 vectorDist = Mathf.Abs(headingReturn.x) + Mathf.Abs(headingReturn.z);
                 vectorDistRatio = circleR / vectorDist;
                 headingReturn.Normalize();   //向きはそのままで長さを１のベクトルに変更
                 var headReturn = headingReturn / 10000.0f * vectorDistRatio * ratio;
                 transform.Translate(headReturn);
                 counter ++;
            }else if(counter == 101){ //カウンターを100回まわす
                onOff =! onOff;  //ボタンの状態をオフにする
                counter = 0;    // カウンターの初期化
                yield break;    //回し終わったら抜け出す
            }
            yield return new WaitForSeconds(0.02f);
        }
    }

    IEnumerator Fall(){
        Pos = transform.position;   //円柱のポジション
         Vector3 targetPos = GameObject.Find("OVRPlayerController").transform.position;
                 var headingReturn = Pos - targetPos;
                 headingReturn.y = 0;
                 vectorDist = Mathf.Abs(headingReturn.x) + Mathf.Abs(headingReturn.z);
                 vectorDist = Mathf.Sqrt(vectorDist);
                 vectorDistRatio = 1 / vectorDist;
                 headingReturn.Normalize();   //向きはそのままで長さを１のベクトルに変更
                 var heading = headingReturn * -1;
                 var headReturn = headingReturn * vectorDistRatio / 0.5f / 81.0f ;
                 var head = heading * vectorDistRatio / 0.5f / 20.0f;
                //  Debug.Log(head);
         int counter = 0;    //カウンターの用意

        while(true){    //ループ処理

            if(counter <= 80){
                 transform.Translate(headReturn);
                 counter ++;
            }else if(counter <= 100){
                 transform.Translate(head);
                 counter ++;
            }else if(counter == 101){ //カウンターを100回まわす
                onOff =! onOff;  //ボタンの状態をオフにする
                counter = 0;    // カウンターの初期化
                yield break;    //回し終わったら抜け出す
            }
            yield return new WaitForSeconds(0.02f);
        }
    }
}


            // if(counter <= 80){
            //      var headingReturn = Pos - targetPos;
            //      headingReturn.y = 0;
            //      vectorDist = Mathf.Abs(headingReturn.x) + Mathf.Abs(headingReturn.z);
            //      vectorDistRatio = circleR / vectorDist;
            //      headingReturn.Normalize();   //向きはそのままで長さを１のベクトルに変更
            //      var headReturn = headingReturn / 1000.0f * 1.2375f * vectorDistRatio * ratio;
            //      transform.Translate(headReturn);
            //      counter ++;
            // }else if(counter <= 100){
            //      var heading = targetPos - Pos;  //自分からプレイヤーへのベクトル
            //      heading.y = 0;  //垂直方向のベクトル計算を排除
            //      vectorDist = Mathf.Abs(heading.x) + Mathf.Abs(heading.z);
            //      vectorDistRatio = circleR / vectorDist;
            //      Debug.Log("vectorDistRatio = " + vectorDistRatio);
            //      heading.Normalize();   //向きはそのままで長さを１のベクトルに変更
            //      var head = heading / 1000.0f * 4.71429f * vectorDistRatio * ratio;
            //      transform.Translate(head);
            //      counter ++;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vection3D_suzumushi : MonoBehaviour
{
    public Transform target;
    // public float moveSpeed = 2.0f;
    // public float stopDist = 10.0f;
    public float defaultDist;
    public float targetDist;
    public bool onOff;
    public bool turnOff;
    // public float timeReturn = 5.0f;
    // public float timeOut = 10.0f;
    // public float timeElapsed;
    // public float moveRatio = 2.0f;
    public float targetDistFrame;
    public float defaultDistFrame;
    Vector3 targetPos;
    Vector3 defaultPos;
    Vector3 Pos;
    Vector3 direction;
    Vector3 directionReturn;
    public float accelerationY;

    void Start()
    {
        defaultPos = transform.position;    //初期位置を代入
        Pos = transform.position;   //自分のポジション
        targetPos = target.position;    //プレイヤーのポジション
        onOff = false;
        turnOff = false;
    }

    // Update is called once per frame
    void Update()
    {

        // Vector3 acceleration = OVRInput.GetLocalControllerAcceleration(OVRInput.Controller.RTouch);
        // accelerationY = acceleration.y;
        // if(accelerationY < -20.0f && onOff == false){ // Aボタンを押したら
        //     onOff = true;   //ボタンのフラグをONにする
        //     StartCoroutine("Raise"); //コルーチンを呼び出す
        // }else if(accelerationY > 20.0f && onOff == false){
        //     onOff = true;
        //     StartCoroutine("Fall");
        // }

        Vector3 acceleration = OVRInput.GetLocalControllerAcceleration(OVRInput.Controller.RTouch);
        accelerationY = acceleration.y;
        if(accelerationY > 20.0f && onOff == false){ // Aボタンを押したら
            onOff = true;   //ボタンのフラグをONにする
            StartCoroutine("Raise"); //コルーチンを呼び出す
        }else if(accelerationY < -20.0f && onOff == false){
            onOff = true;
            StartCoroutine("Fall");
        }

        // if(OVRInput.GetDown(OVRInput.RawButton.A) && onOff == false){ // Aボタンを押したら
        // onOff = true;   //ボタンのフラグをONにする
        // StartCoroutine("Raise"); //コルーチンを呼び出す
        // }else if(OVRInput.GetDown(OVRInput.RawButton.B) && onOff == false){
        //     onOff = true;
        //     StartCoroutine("Fall");
        // }
    }

    IEnumerator Raise(){
         int counter = 0;    //カウンターの用意
         float f;

        while(true){    //ループ処理

            if(counter <= 20){
                //  f = 1.0f - (counter * 0.0495f);
                //  f *= 0.1f;
                //  transform.localScale = new Vector3(f,f,f);

                 var heading = targetPos - Pos;  //自分からプレイヤーへのベクトル
                 heading.y = 0;  //垂直方向のベクトル計算を排除
                 var head = heading / 100.0f * 4.71429f;
                 transform.Translate(-head);
                 counter ++;
            }else if(counter <= 100){
                //  f = 0.01f + ((counter-20) * 0.012375f);
                //  f *= 0.1f;
                //  transform.localScale = new Vector3(f,f,f);

                 var headingReturn = Pos - targetPos;
                 headingReturn.y = 0;
                 var headReturn = headingReturn / 100.0f * 1.2375f;
                 transform.Translate(-headReturn);
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
         int counter = 0;    //カウンターの用意
         float f;

        while(true){    //ループ処理

            if(counter <= 80){
                //  f = 1.0f + (counter * 1.2222f);
                //  f *= 0.1f;
                //  transform.localScale = new Vector3(f,f,f);

                 var headingReturn = Pos - targetPos;
                 headingReturn.y = 0;
                 var headReturn = headingReturn / 100.0f * 1.2375f;
                //  var headReturn = headingReturn * 99.0f / 81.0f;
                 transform.Translate(-headReturn);

                //  f = 1.0f + (counter * 1.2375f);
                //  transform.localScale = new Vector3(f,f,f);

                 counter ++;
            }else if(counter <= 100){
                //  f = 100.0f - ((counter-80) * 4.95f);
                //  f *= 0.1f;
                //  transform.localScale = new Vector3(f,f,f);

                 var heading = targetPos - Pos;  //自分からプレイヤーへのベクトル
                 heading.y = 0;  //垂直方向のベクトル計算を排除
                 var head = heading / 100.0f * 4.95f;
                //  var head = heading * 99.0f / 20.0f;
                 transform.Translate(-head);

                //  f = 100.0f - ((counter-80) * 4.95f);
                //  transform.localScale = new Vector3(f,f,f);

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


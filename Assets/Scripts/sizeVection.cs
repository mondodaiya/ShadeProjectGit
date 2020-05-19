﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sizeVection : MonoBehaviour
{
    public Transform target;
    public float moveSpeed = 2.0f;
    public float stopDist = 10.0f;
    public float defaultDist;
    private float targetDist;
    private bool onOff;
    private bool turnOff;
    public float timeReturn = 5.0f;
    public float timeOut = 10.0f;
    public float timeElapsed;
    public float moveRatio = 2.0f;
    Vector3 targetPos;
    Vector3 defaultPos;
    Vector3 Pos;
    Vector3 direction;
    Vector3 directionReturn;
    public float accelerationY;

    void Start()
    {
        defaultPos = transform.position;    //初期位置を代入
        onOff = false;
        turnOff = false;
    }

    // Update is called once per frame
    void Update()
    {
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
            //  onOff = true;   //ボタンのフラグをONにする
            //  StartCoroutine("Raise"); //コルーチンを呼び出す
            // }else if(OVRInput.GetDown(OVRInput.RawButton.B) && onOff == false){
            //  onOff = true;
            //  StartCoroutine("Fall");
            // }
    }

        IEnumerator Raise(){
         int counter = 0;    //カウンターの用意
         float f;

        while(true){    //ループ処理
            Pos = transform.position;   //自分のポジション
            targetPos = target.position;    //プレイヤーのポジション

            if(counter <= 20){
                 f = 10.0f - (counter * 0.495f);
                 transform.localScale = new Vector3(f,f,f);
                 counter ++;
            }else if(counter <= 100){
                 f = 0.1f + ((counter-20) * 0.12375f);
                 transform.localScale = new Vector3(f,f,f);
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
            Pos = transform.position;   //自分のポジション
            targetPos = target.position;    //プレイヤーのポジション

            if(counter <= 80){
                 f = 10.0f + (counter * 12.375f);
                 transform.localScale = new Vector3(f,f,f);
                 counter ++;
            }else if(counter <= 100){
                 f = 1000.0f - ((counter-80) * 49.5f);
                 transform.localScale = new Vector3(f,f,f);
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

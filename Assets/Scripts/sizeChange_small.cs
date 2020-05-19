﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sizeChange_small : MonoBehaviour
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
        // Pos = transform.position;   //自分のポジション
        // targetPos = target.position;    //プレイヤーのポジション

        // var heading = targetPos - Pos;  //自分からプレイヤーへのベクトル
        // heading.y = 0;  //垂直方向のベクトル計算を排除
        // targetDist = heading.magnitude; //自分とプレイヤーの距離
        // // targetDist = targetDist/100;
        // direction = heading / targetDist;   //自分からプレイヤーへの方向

        // var headingReturn = defaultPos - Pos;   //自分から元の位置へのベクトル
        // headingReturn.y = 0;    //垂直方向のベクトル計算を排除
        // defaultDist = headingReturn.magnitude;  //自分と元の位置の距離
        // // defaultDist = defaultDist/100;
        // directionReturn = headingReturn / defaultDist;  //自分から元の位置への方向
        
        Vector3 acceleration = OVRInput.GetLocalControllerAcceleration(OVRInput.Controller.RTouch);
        accelerationY = acceleration.y;
        if(accelerationY > 20.0f && onOff == false){ // Aボタンを押したら
            onOff = true;   //ボタンのフラグをONにする
            StartCoroutine("Size"); //コルーチンを呼び出す
        }

        // if(OVRInput.GetDown(OVRInput.RawButton.A)){ // Aボタンを押したら
        //     onOff = true;   //ボタンのフラグをONにする
        //     StartCoroutine("Size"); //コルーチンを呼び出す
        // }

        // if(onOff == true){  //ボタンが押されてから10秒間の処理
        //     timeElapsed += Time.deltaTime;//経過した時間を和算
        //     if(timeElapsed >= timeReturn){  //一定時間が経過したら
        //         turnOff = true; //引き返す
        //     }if(timeElapsed >= timeOut){    //timeOutしたら
        //         onOff = false;  //ボタンの状態をオフにする
        //         turnOff = false;    //引き返す状態を解除する
        //         timeElapsed = 0.0f;     //
        //     }

        //     // if(turnOff == false){
        //     //     transform.position = Pos + direction * moveSpeed * Time.deltaTime;
        //     // }else{
        //     //     transform.position = Pos + directionReturn * moveSpeed * Time.deltaTime;
        //     // }
        // }
    }

    IEnumerator Size(){
         int counter = 0;    //カウンターの用意
         float f;

        while(true){    //ループ処理
            Pos = transform.position;   //自分のポジション
            targetPos = target.position;    //プレイヤーのポジション

            if(counter < 20){
                 f = 1.0f - (counter * 0.0495f);
                 transform.localScale = new Vector3(f,f,f);
                //  var heading = targetPos - Pos;  //自分からプレイヤーへのベクトル
                //  heading.y = 0;  //垂直方向のベクトル計算を排除
                //  targetDist = heading.magnitude; //自分とプレイヤーの距離
                //  direction = heading / targetDist / moveRatio;   //自分からプレイヤーへの方向
                //  transform.position += direction;
                 counter ++;
            }else if(counter < 100){
                 f = 0.01f + ((counter-20) * 0.012375f);
                 transform.localScale = new Vector3(f,f,f);
                //  var headingReturn = defaultPos - Pos;   //自分から元の位置へのベクトル
                //  headingReturn.y = 0;    //垂直方向のベクトル計算を排除
                //  defaultDist = headingReturn.magnitude;  //自分と元の位置の距離
                //  directionReturn = headingReturn / defaultDist / moveRatio;  //自分から元の位置への方向
                //  transform.position += directionReturn;
                 counter ++;
            }else if(counter == 100){ //カウンターを100回まわす
                onOff =! onOff;  //ボタンの状態をオフにする
                counter = 0;    // カウンターの初期化
                yield break;    //回し終わったら抜け出す
            }
            // yield return null;
            yield return new WaitForSeconds(0.02f);
            

            //  var heading = targetPos - Pos;  //自分からプレイヤーへのベクトル
            //  heading.y = 0;  //垂直方向のベクトル計算を排除
            //  targetDist = heading.magnitude; //自分とプレイヤーの距離
            //  direction = heading / targetDist / 10;   //自分からプレイヤーへの方向
            //  transform.position += direction;
            //  counter ++;

            //  if(counter >= 100){ //カウンターを100回まわす
            //     yield break;    //回し終わったら抜け出す
            //     onOff = false;  //ボタンの状態をオフにする
            //     counter = 0;    // カウンターの初期化
            // }
            // yield return null;
        }
        // for(float f=1.0f; f>=0.1f; f-=0.01f){
        //     // transform.localScale = new Vector3(f,f,f);
        //     transform.position += direction;
        //     yield return new WaitForSeconds(0.02f);
        // }
        // for(float f=0.1f; f<=1.0f; f+=0.01f){
        //     // transform.localScale = new Vector3(f,f,f);
        //     transform.position += directionReturn;
        //     yield return new WaitForSeconds(0.02f);
        // }
        // yield break;
    }
}

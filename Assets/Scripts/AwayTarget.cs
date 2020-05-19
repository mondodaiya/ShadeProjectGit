using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AwayTarget : MonoBehaviour
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
        if(accelerationY < -20.0f && onOff == false){ // Aボタンを押したら
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
            // Pos = transform.position;   //自分のポジション
            // targetPos = target.position;    //プレイヤーのポジション

            if(counter < 80){
                 f = 1.0f + (counter * 1.2375f);
                 transform.localScale = new Vector3(f,f,f);

                 var headingReturn = Pos - targetPos;
                 headingReturn.y = 0;
                 var headReturn = headingReturn / 100.0f * 1.2375f;
                 transform.Translate(headReturn);

                //  var heading = targetPos - Pos;  //自分からプレイヤーへのベクトル
                //  heading.y = 0;  //垂直方向のベクトル計算を排除
                //  var head = heading / 100.0f * 4.95f;
                //  transform.Translate(head);
                 
                //  targetDist = heading.magnitude; //自分とプレイヤーの距離
                //  targetDist = Vector3.Distance(Pos,targetPos);
                //  targetDistFrame = targetDist/100.0f*4.95f;
                // //  direction = heading / targetDistFrame;   //自分からプレイヤーへの方向
                // //  direction = heading / targetDist;
                // //  transform.position = (direction/100.0f)*counter*4.95f;
                // transform.position = Vector3.MoveTowards(Pos,targetPos,targetDistFrame);
                 counter ++;
            }else if(counter < 100){
                 f = 100.0f - ((counter-80) * 4.95f);
                 transform.localScale = new Vector3(f,f,f);

                 var heading = targetPos - Pos;  //自分からプレイヤーへのベクトル
                 heading.y = 0;  //垂直方向のベクトル計算を排除
                 var head = heading / 100.0f * 4.95f;
                 transform.Translate(head);

                //  var headingReturn = Pos - targetPos;
                //  headingReturn.y = 0;
                //  var headReturn = headingReturn / 100.0f * 1.2375f;
                //  transform.Translate(headReturn);

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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using System.IO;
using UnityEngine.XR;


public class VectionIllusion : MonoBehaviour
{
    public float width;    //spot同士の幅
    public float speed;  //spotが動くスピード
    public bool OnOff;  //ボタンの状態
    public float timeElapsed;   //ボタンを押してからの経過時間
    public float timeOut;   //ボタンを押してからpointが動き続ける時間
    public float time_inverse;  //経過時間の逆数
    public float time_sin;  //Map()用変数
    public float alfa;      //オブジェクトの透明度
    public float red, green, blue;  //色の変数
    Vector3 pos_init;      //初期位置
    Vector3 pos_now;        //現在の位置
    Vector3 pos_player;     //プレイヤーの位置
    public float sin;   //sin(-1~1)
    public float dist_vector;   //spotとplayerの距離
    public float dist_ratio;    //距離の比率
    public float dist_player;     //playerとspotの初期位置との距離
    public float dist_ratio_sqrt;   //
    // public float dist_spot;     //spotとspotの初期位置との距離
    // public float dist_diff;     //dist_playerとdist_spotの差分
    public int count;　 //sinの周回数の変数
    public Color color;
    public float alfa_circle = 2.0f;   //白点が消える円の半径

    private VideoPlayer videoPlayer;
    public float txt;
    public float printSec;      //書き出す時間間隔
    private Vector3 head_init;  //最初の頭の位置
    private Vector3 head_now;   //現在の頭の位置
    public float pressed_button;    //ボタンが押された時間
    public float elapsed_button;    //ボタンが押されてから経過した時間
    public float start_fall;    //fall()の呼び出し開始時間
    public float end_print;     //printTxt()の終了時間

    // Start is called before daqthe first frame update
    void Start()
    {  
        OnOff = false;
        pos_init = transform.position;
        width = 3.0f;
        speed = 1.0f;
        timeElapsed = 0.01f;
        timeOut = 5.0f;
        color = gameObject.GetComponent<Renderer>().material.color;
        color.a = 1.0f;
        gameObject.GetComponent<Renderer>().material.color = color;
        videoPlayer = GetComponent<VideoPlayer>();
        printSec = 0.2f;
        head_init = InputTracking.GetLocalPosition(XRNode.Head);

        
    }

    // Update is called once per frame
    void Update()
    {
        pos_player = GameObject.Find("OVRPlayerController").transform.position;     //プレイヤーの位置を取得
        pos_player.y = 0;   //プレイヤーのy軸情報をなくす
        this.transform.LookAt(pos_player);      //プレイヤーの方向に向かせる
        this.transform.Rotate(0,90,0);  //動画の方向を調整する
        if(OVRInput.GetDown(OVRInput.RawButton.A) && OnOff == false){ // Aボタンを押したら
            OnOff = true;   //ボタンのフラグをONにする
            pressed_button = Time.time;
            StartCoroutine(printTxt());
        }
        if(OnOff == true){
            if(Time.time > pressed_button + 1.0f){
                StartCoroutine("Fall"); //コルーチンを呼び出す
            }
            if(Time.time > pressed_button + 9.0f){
                StopCoroutine(printTxt());
            }
        }
    }

    private IEnumerator Fall(){ 
        // while(true){
        //     timeElapsed += Time.deltaTime;  //while loopに入ってからの秒数を算出
        //     time_inverse = (timeOut - timeElapsed) / timeOut;    //経過時間の逆数を正規化
        //     time_sin = Mathf.Sin(Mathf.PI * (0.5f + (timeElapsed / (timeOut * 2))));
        //     sin = Mathf.Sin( Mathf.PI * timeElapsed * speed) * width * time_sin;    //時間に応じたsin値を算出
        //     pos_player = GameObject.Find("OVRPlayerController").transform.position;     //プレイヤーの位置を取得
            pos_now = transform.position;       //白点の位置を取得
            var dist_move = pos_now - pos_player;   //白点からプレイヤーまでのベクトルを取得
            dist_move.y = 0;    //縦方向のベクトルを0にする
            dist_vector = Mathf.Abs(dist_move.x) + Mathf.Abs(dist_move.z);   //ベクトルから距離(float型)に変換

        //     // if(dist_vector 
            dist_ratio = Mathf.Sqrt(dist_vector);  //距離の平方根を求める
            Debug.Log(dist_ratio);

            if(dist_ratio < 1.0f){
                videoPlayer.url = "C:/Users/i402/Desktop/Mondo/Image/4.mp4";
            }else if(dist_ratio < 2.0f){
                videoPlayer.url = "C:/Users/i402/Desktop/Mondo/Image/3.mp4";
            }else if(dist_ratio < 3.0f){
                videoPlayer.url = "C:/Users/i402/Desktop/Mondo/Image/2.mp4";
            }else{
                videoPlayer.url = "C:/Users/i402/Desktop/Mondo/Image/1.mp4";
            }

             videoPlayer.Play();
             OnOff = false;
        //     dist_ratio = 1 / dist_ratio;　//距離の平方根の逆数を求める
        //     dist_move.Normalize();  //大きさをのベクトルに正規化する

        //     dist_player = Vector3.Distance(pos_init, pos_player);
        //     // dist_spot = Vector3.Distance(pos_init, pos_now);
        //     // dist_diff = dist_player - dist_spot;

        //     if(dist_player > alfa_circle){
        //         color.a = 1.0f;
        //         gameObject.GetComponent<Renderer>().material.color = color;
        //     }else{
        //         color.a = 0.0f;
        //         gameObject.GetComponent<Renderer>().material.color = color; 
        //     }

        //     this.transform.position = pos_init + dist_move * sin * dist_ratio;
        //     // Vector3 v = transform.lossyScale;
        //     // v.y = 1;
        //     this.transform.localScale = new Vector3(0.2f + sin * dist_ratio * 0.05f, 0.002f, 0.2f + sin * dist_ratio * 0.05f);
        //     // Debug.Log( sin * dist_ratio);

        //     if(time_inverse <= 0){
        //         this.transform.position = pos_init;
        //         OnOff = false;
        //         timeElapsed = 0.01f;
        //         color.a = 1.0f;
        //         gameObject.GetComponent<Renderer>().material.color = color; 
        //         yield break;
        //     }
        //     yield return new WaitForSeconds(0.001f);
        // }
        yield break;
    }

    IEnumerator printTxt(){
        while(true){
        txt = Time.time;
        head_now = InputTracking.GetLocalPosition(XRNode.Head);
        head_now = head_init - head_now;
        float head_x = head_now.x;
        float head_y = head_now.y;
        float head_z = head_now.z;
        StreamWriter textfile = new StreamWriter("../TextData.txt",true);  //テキスト出力
        textfile.Write(txt);
        textfile.Write(",");
        textfile.Write(head_x);
        textfile.Write(",");
        textfile.Write(head_y);
        textfile.Write(",");
        textfile.WriteLine(head_z);
        textfile.Flush();
        textfile.Close();
        yield return new WaitForSeconds(printSec);
        }
    }
}
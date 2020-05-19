using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using System.IO;
using UnityEngine.XR;


public class VectionIllusionMR : MonoBehaviour
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

    GameObject lineupMR;
    LineUpMR script;
    Vector3 rotate_player;

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
        //head_init = InputTracking.GetLocalPosition(XRNode.Head);

        lineupMR = GameObject.Find("LineUpSin1");
        script = lineupMR.GetComponent<LineUpMR>();

    }

    // Update is called once per frame
    void Update()
    {

        //pos_player = GameObject.Find("ZED_Rig_Mono").transform.position;     //プレイヤーの位置を取得
        //pos_player.y = 0;   //プレイヤーのy軸情報をなくす
        //this.transform.LookAt(pos_player);      //プレイヤーの方向に向かせる
        //this.transform.Rotate(0,270,0);  //動画の方向を調整する
        if (Input.GetKeyDown(KeyCode.Space) && OnOff == false){ // Spaceを押したら
            OnOff = true;   //ボタンのフラグをONにする
            pressed_button = Time.time;
        }
        if(OnOff == true){
            if(Time.time > pressed_button + 1.0f){
                StartCoroutine("Fall"); //コルーチンを呼び出す
            }
            if(Time.time > pressed_button + 13.0f)
            {
               
               Destroy(gameObject);
            }
        }
    }


    private IEnumerator Fall(){

        Debug.Log("falling");
        pos_player = script.pos;
        pos_player.y = this.transform.position.y;
        this.transform.LookAt(pos_player);
        this.transform.Rotate(0,270,0);  //動画の方向を調整する

            pos_now = transform.position;       //白点の位置を取得
            var dist_move = pos_now - pos_player;   //白点からプレイヤーまでのベクトルを取得
            dist_move.y = 0;    //縦方向のベクトルを0にする
            dist_vector = Mathf.Abs(dist_move.x) + Mathf.Abs(dist_move.z);   //ベクトルから距離(float型)に変換

        //     // if(dist_vector 
            dist_ratio = Mathf.Sqrt(dist_vector);  //距離の平方根を求める
            // Debug.Log(dist_ratio);

            if(dist_ratio < 1.0f){
                videoPlayer.url = "C:/Users/i402/Documents/Image/4.mp4";
            }else if(dist_ratio < 2.0f){
                videoPlayer.url = "C:/Users/i402/Documents/Image/3.mp4";
            }else if(dist_ratio < 3.0f){
                videoPlayer.url = "C:/Users/i402/Documents/Image/2.mp4";
            }else{
                videoPlayer.url = "C:/Users/i402/Documents/Image/1.mp4";
            }
             videoPlayer.Play();

        OnOff = false;
        yield break;
    }
}
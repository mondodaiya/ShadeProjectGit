using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.XR;

public class VectionSinTestMR : MonoBehaviour
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


    // Start is called before daqthe first frame update
    void Start()
    {  
        OnOff = false;
        pos_init = transform.position;
        width = 3.0f;
        speed = 1.0f;
        timeElapsed = 0.01f;
        timeOut = 5.0f;
        //color = gameObject.GetComponent<Renderer>().material.color;   //Turn on when you set alpha color
        //color.a = 1.0f;
        //gameObject.GetComponent<Renderer>().material.color = color;

        //printSec = 0.2f;

        //head_init = InputTracking.GetLocalPosition(XRNode.Head);    //Get Headset position


        lineupMR = GameObject.Find("LineUpSpot1");
        script = lineupMR.GetComponent<LineUpMR>();
    }

    // Update is called once per frame
    void Update()
    {
        //if(OVRInput.GetDown(OVRInput.RawButton.A) && OnOff == false){ // Aボタンを押したら
          if (Input.GetKeyDown(KeyCode.Space) && OnOff == false)
            { // Spaceを押したら
                OnOff = true;   //ボタンのフラグをONにする
                pressed_button = Time.time;
        }
        if(OnOff == true){
            if(Time.time > pressed_button + 1.0f){
                StartCoroutine("Fall"); //コルーチンを呼び出す
            }
        }
    }

    private IEnumerator Fall(){ 
        OnOff = true;    
        while(true){
            timeElapsed = Time.time - pressed_button - 1.0f;  //while loopに入ってからの秒数を算出
            sin = Mathf.Sin(Mathf.PI * (1f / 12f) * timeElapsed);   //何秒でSinを１回転させるか（Sinの周期）
            pos_player = script.pos;    //QRcodeの位置を取得
            pos_now = transform.position;       //白点の位置を取得
            var dist_move = pos_now - pos_player;   //白点からプレイヤーまでのベクトルを取得
            dist_move.y = 0;    //縦方向のベクトルを0にする
            dist_vector = Mathf.Abs(dist_move.x) + Mathf.Abs(dist_move.z);   //ベクトルから距離(float型)に変換
            dist_ratio = Mathf.Sqrt(dist_vector);  //距離の平方根を求める
            dist_ratio = 1 / dist_ratio;　//距離の平方根の逆数を求める
            dist_move.Normalize();  //大きさをのベクトルに正規化する

            this.transform.position = pos_init + dist_move * sin * dist_ratio * 0.2f;
            this.transform.localScale = new Vector3(0.1f + sin * dist_ratio * 0.1f, 0.002f, 0.1f + sin * dist_ratio * 0.1f);

            if(timeElapsed > 12.0f){
                this.transform.position = pos_init;
                OnOff = false;
                Destroy(gameObject);
                yield break;
            }
            yield return new WaitForSeconds(0.001f);
        }
    }
}
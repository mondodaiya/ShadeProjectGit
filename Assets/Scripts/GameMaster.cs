using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.XR;

public class GameMaster : MonoBehaviour
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
        printSec = 0.2f;
         StreamWriter textfile = new StreamWriter("../TextData.txt",true);  //テキスト出力
        textfile.WriteLine("shunta : curve 1");
        textfile.Flush();
        textfile.Close();
    }

    // Update is called once per frame
    void Update()
    {
        if(OVRInput.GetDown(OVRInput.RawButton.A) && OnOff == false){ // Aボタンを押したら
            OnOff = true;   //ボタンのフラグをONにする
            pressed_button = Time.time;
            head_init = InputTracking.GetLocalPosition(XRNode.Head);
            StartCoroutine(printTxt());
        }
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
        textfile.Write(txt - pressed_button);
        textfile.Write(",");
        textfile.Write(head_x);
        textfile.Write(",");
        textfile.Write(head_y);
        textfile.Write(",");
        textfile.WriteLine(head_z);
        textfile.Flush();
        textfile.Close();
        if(Time.time > pressed_button + 15.0f){
           yield break;
        }
        yield return new WaitForSeconds(printSec);
        }
    }
}
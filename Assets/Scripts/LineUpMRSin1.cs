using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineUpMRSin1 : MonoBehaviour
{
    //public float high = 0.02f;  //高さ
    public float width = 0.2f; //オブジェクト間の幅
    public int vertical = 8;    //上から見て縦、z軸のオブジェクトの量
    public int horizontal = 8;  //上から見て横、x軸のオブジェクトの量
    
    public GameObject PrefabCylinder;   //Prefabを入れる欄を作る

    public Vector3 pos;    // 位置を入れる変数

    // Start is called before the first frame update
    void Start()
    {


        }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            StartCoroutine("Appear");
        }

    }

    private IEnumerator Appear()
    {
        pos = GameObject.Find("Cube").transform.position;   //このスクリプトを入れたオブジェクトの位置

        for (int vi = 0; vi < vertical; vi++)
        {   //Z軸にverticalの数だけ並べる
            for (int hi = 0; hi < horizontal; hi++)
            {     //x軸にhorizontalの数だけ並べる

                GameObject copy = Instantiate(PrefabCylinder, new Vector3(
                    pos.x + horizontal * width / 2 - hi * width - width / 2,
                    pos.y,
                    pos.z + vertical * width / 2 - vi * width - width / 2),
                    Quaternion.identity);
            }

        }
        yield break;
    }
}

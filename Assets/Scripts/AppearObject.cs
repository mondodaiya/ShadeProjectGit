using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AppearObject : MonoBehaviour
{
    private Vector3 pos;
    public GameObject PrefabCylinder;   //Prefabを入れる欄を作る

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            StartCoroutine("Appear");
        }
    }
    private IEnumerator Appear()
    {
        pos = GameObject.Find("Cube").transform.position; ;   //このスクリプトを入れたオブジェクトの位置
        Debug.Log(pos);
        GameObject copy = Instantiate(PrefabCylinder, new Vector3(pos.x, pos.y, pos.z),Quaternion.identity);
        yield break;
    }
}

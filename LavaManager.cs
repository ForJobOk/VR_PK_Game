using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaManager : MonoBehaviour
{

    Rigidbody rd;

    

    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void OnCollisionEnter(Collision other)
    {

        if (other.gameObject.tag == "Net")
        {
            BallGravity();

            Destroy(this.gameObject, 2.0f);
        }

        if (other.gameObject.tag == "Hand")　//もし手に当たったら...
        {
            Count_LavaBall();
        }

    }

    public int Count_LavaBall()  //溶岩ボールに当たった回数をカウント 
    {
        //後で当たった回数を他クラスに渡さないといけないので戻り値付きのメソッドにしておく
        int count_Lava = 0;
        count_Lava++;
        return count_Lava;
    }

    //ボールがネットに当たったら重力がオンになる

    void BallGravity()
    {
        rd = this.GetComponent<Rigidbody>();
        rd.useGravity = true;
    } 
}
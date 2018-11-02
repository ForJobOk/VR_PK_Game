using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallManager : MonoBehaviour
{

    //ボールが通過したらゴールラインのスクリプトを呼び出す
   
    GoalManager goalManager;
    Rigidbody rd;
    
    
    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }


    public void OnTriggerEnter(Collider other)
    {
        /*引数として受け取るotherはこの場合は壁
         * GoalManager goalManager = other.gameObject.GetComponent<GoalManager>();
         *↑GoalManager型のgoalManager変数にGetComponentでGoalManagerを取得して代入
         * GoalManager型のgoalManager変数からGoalManagerクラスのメソッドを呼び出し可能
         */


        if (other.gameObject.tag == "Wall")
        {
            GoalManager goalManager = other.gameObject.GetComponent<GoalManager>();
            goalManager.PlaySound();
            FindObjectOfType<ScoreManager>().AddScore(-10);
        }
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
            Count_Ball();
        }
    }

    public int Count_Ball()  //ボールに当たった回数をカウント 
    {
        //後で当たった回数を他クラスに渡さないといけないので戻り値付きのメソッドにしておく
        int count_Ball = 0;
        count_Ball++;
        CommonData.Instance.ballScore= count_Ball;
        return count_Ball; 
    }

    //ボールがネットに当たったら重力がオンになる
    void BallGravity()
    {
        rd = this.GetComponent<Rigidbody>();
        rd.useGravity = true;
    } 
}
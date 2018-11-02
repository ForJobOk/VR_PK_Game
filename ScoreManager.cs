using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class ScoreManager : MonoBehaviour {


    /*持ち点100からスタート
     * ボールがゴールを通過-10点
     * ボールをセーブ＋20点
     * 溶岩ボールをセーブ-10点
     *
     * スコアマネージャーにポイントが加算されるメソッドを作成  ->  ボール側,セーブハンド側でそのメソッドを呼び出す
     * 
     */

    public Text scorePoint;
    const int START_SCORE = 100;
    int score;
    
    public int AddScore(int point)
    {
        score =  score + point;
        CommonData.Instance.totalScore = score;
        return score;
    }

	// Use this for initialization
	void Start () {
        score = START_SCORE + score;
	}
	
	// Update is called once per frame
	void Update () {
        scorePoint.text = "Score : " +  score.ToString();
        if (score <=30)
        {
            scorePoint.color = Color.red;
        }
        else
        {
            scorePoint.color = Color.yellow;
        }
	}
}

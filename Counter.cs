using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Counter : MonoBehaviour
{
    //カウントダウンが終了したらタイマーを作動する



    float flashing_Speed = 3;

    //[Range (0,60)]
    public int PlayTimeSeconds = 120;
    int seconds = 0;


    float oldSeconds;
    float totalTime;
    int minute;


    bool playMusic = false;
    bool countFinished = false;

    public Text guide_Message;
    public Text timerText;
    Text countText;

    [Header("BGMマネージャー")]
    public BackGroundMusic m_BackGroundMusic;

    [Header("スコア")]
    public GameObject score;

    [Header("ボールショット")]
    public GameObject ballShot;

    [Header("CDMマネージャー")]
    public GameObject count_DownMusic;

    public FadeMode headCamera;

    // Use this for initialization
    void Start()
    {
        countText = this.gameObject.GetComponent<Text>();
        //minute = seconds / 60;//0;
        totalTime = PlayTimeSeconds;
    }

    void GuideMessage()  //ガイドメッセージを点滅させる
    {
        float timer = Time.time;


        /* ①guide_Messageの色のプロパティを取り出して_this_Text_Colorに入れる
         * ②_this_Text_Colorに変更した値を代入する
         * ③guide_Messageの色のプロパティに_this_Text_Colorを代入する          */

        Color _this_Text_Color = guide_Message.color;

        _this_Text_Color.a = Mathf.Sin(timer * flashing_Speed) * 0.5f + 0.5f;  //0 <= Sin(X)<= 1

        guide_Message.color = _this_Text_Color;
    }



    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
            GuideMessage();
            TimerAwake();
    }

    //トリガーを引いたらコルーチンスタート
    public void ViveTriggerOn()
    {
        StartCoroutine(CountTextTime());
    }


    IEnumerator CountTextTime()
    {

        //ガイドメッセージの表示をオフ…のように見せる
        guide_Message.text = "";

        //一秒後からカウントダウンスタート…のように見せる
        countText.text = "";
        yield return new WaitForSeconds(1.0f);

        //コルーチンのスタートと同時に音の再生とカウントダウンのスタートを行う
        count_DownMusic.SetActive(true);

        countText.color = Color.red;

        countText.text = "3";
        yield return new WaitForSeconds(1.0f);

        countText.text = "2";
        yield return new WaitForSeconds(1.0f);

        countText.text = "1";
        yield return new WaitForSeconds(1.0f);

        countText.text = "START!";
        yield return new WaitForSeconds(1.0f);

        countText.text = "";

        score.SetActive(true);
        ballShot.SetActive(true);

        playMusic = true;
        countFinished = true;

        //BGMの再生
        if (playMusic)
        {
            m_BackGroundMusic.PlayBGM();

            playMusic = false;
        }
    }

    void TimerAwake() //時間経過で減っていくタイマー
    {
        if (countFinished)
        {

            if ((int)seconds != (int)oldSeconds)
            {

                timerText.text = minute.ToString("00") + ":" + ((int)seconds).ToString("00");
            }
            oldSeconds = seconds;

            /*分と秒の境目をなくす
             *
             * 例)1分45秒
             * ①分数(1分)を秒数(60秒)に直して、秒数(45)と足す　-> 105秒
             * ②105秒から1秒(Time.deltaTime)減らす　-> 104秒
             * ③104秒を60秒で割ってint型でキャストする -> 少数が切り捨てられて分数(1分)が求まる
             * ④104秒から秒数換算した分数(1分×60＝60秒)を引く　-> 44秒
             */


            //totalTime = minute * 60 + seconds;  //totalTimeに秒数換算して代入
            totalTime -= Time.deltaTime; ;　//分を秒数換算してtotalTimeから引く

            minute = (int)totalTime / 60;  //int型なので切り捨て
            //seconds = (int)totalTime - minute * 60;
            seconds = (int)totalTime % 60;





            if (totalTime <= 0) //時間がきたら全部停止してボールも消す
            {
                timerText.text = "FINISH!";
                score.SetActive(false);
                ballShot.SetActive(false);
                m_BackGroundMusic.StopBGM();

                GameObject[] ball_S = GameObject.FindGameObjectsWithTag("Ball");
                GameObject[] ball_L = GameObject.FindGameObjectsWithTag("Lava");

                foreach (GameObject ball_Soccer in ball_S)
                {
                    Destroy(ball_Soccer);
                }

                foreach (GameObject ball_Lava in ball_L)
                {
                    Destroy(ball_Lava);
                }

                headCamera.f_FadeOut();
                Invoke("JumpToScene", 2.0f);
            }

            else if(totalTime <= 10.8f)
            {
                timerText.color = Color.red;
            }

            else if(totalTime <= 30.8f)
            {
                timerText.color = Color.yellow;
            }

        }
    }

    void JumpToScene() //シーンの切り替え
    {
        SceneManager.LoadScene("ResultScene");
    }
}  

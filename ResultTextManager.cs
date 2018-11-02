using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultTextManager : MonoBehaviour
{
    
    float cos_Wave;
    float up_Down_Speed = 0.05f;

    int random_Number;// = Random.Range(10, 999);
    int _random_Number;
    public string resultText;

    bool isLoop = true;
    bool isStopCoroutine = false;
    bool isBallscore_Display = false;

    public GameObject ballResult_X;
    public GameObject g_result;
    public GameObject[] balls;
    public GameObject ballScore;
    public GameObject lavaBallScore;
    public GameObject totalScore;
    public GameObject dummyObject;
    public GameObject dummyMovePoint;

    Vector3 ball;
    Vector3 lavaBall;

    Vector3 dummy_Position;
    Vector3 toGoPoint;

    // Use this for initialization
    void Start()
    {
        totalScore.GetComponent<Text>().text = CommonData.Instance.totalScore.ToString();
        //ballScore.GetComponent<Outline>().enabled=true;
        //resultText = CommonData.Instance.totalScore.ToString();
        StartCoroutine(Set_Result_Text(resultText));
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isBallscore_Display) //ボールのスコアの表示が終わり次第トータルスコアを表示
        {
            MoveDummy();
        }

        cos_Wave = Mathf.Cos(Time.time);

        /*①ボール(GameObject)のポジションを取り出してVecter型の変数(ball)に一旦代入する
         *②変更したい方向(y)のプロパティを呼び出して値を変更する
         *③元のポジション(_Balls[0].transform.position)に値を変更した変数(ball)を代入する　　*/

        ball = balls[0].transform.position;

        ball.y += cos_Wave * up_Down_Speed;

        balls[0].transform.position = ball;


        lavaBall = balls[1].transform.position;

        lavaBall.y += cos_Wave * up_Down_Speed;

        balls[1].transform.position = lavaBall;

    }

    IEnumerator Set_Result_Text(string give_text) //一文字ずつ表示するコルーチン
    {
        for (int i = 0; i <= give_text.Length; i++)
        {
            //Substring(int startIndex, int length) 取り出し始める文字の位置と取り出す文字の長さ
            g_result.GetComponent<Text>().text = give_text.Substring(0, i);
            yield return new WaitForSeconds(0.5f);
            
            if(i == resultText.Length)  //全部文字を表示しきったら...
            {
                //非表示にしていたオブジェクトをアクティブにする
                ballResult_X.SetActive(true);

                balls[0].SetActive(true);
                balls[1].SetActive(true);

                //ドラムロール的なやつを表示＆コルーチンスタート
                Invoke("Random_Display",2.0f);  
                StartCoroutine(RandomNumber());
            }
        }
    }
    
    IEnumerator RandomNumber() 
    {
        //70まで繰り返す 1フレーム当たり約0.016秒×70 ＋ コルーチンの0.05秒×70 = 約4.62秒　？
        for (int i = 0; i<=70; i++) 
        {
            random_Number = Random.Range(0, 999);
            _random_Number = Random.Range(0, 999);

            ballScore.GetComponent<Text>().text = random_Number.ToString();
            lavaBallScore.GetComponent<Text>().text = _random_Number.ToString();
            yield return new WaitForSeconds(0.05f);
        }

        int ball_Count_minus = FindObjectOfType<BallManager>().Count_Ball() - 1;
        int lavaBall_Count_minus = FindObjectOfType<LavaManager>().Count_LavaBall() - 1;
        //触ったボールのカウント　呼び出し時に１増えるので減らして呼び出し

        lavaBallScore.GetComponent<Text>().text = lavaBall_Count_minus.ToString();
        ballScore.GetComponent<Text>().text = ball_Count_minus.ToString();
        isBallscore_Display = true;

        print("OK"); //ここに効果音　ドン！

    }
    void MoveDummy()  //遠くに置いたdummyObjectをdummyMovePointのある座標まで移動させる
    {
        float timer = Time.time;
        float moveSpeed = 0.01f;

        dummy_Position = dummyObject.transform.position;
        toGoPoint = dummyMovePoint.transform.position;
        float between_Two_Points = dummy_Position.z - toGoPoint.z; //二点間の距離の差

        for (int i = (int)between_Two_Points; i > 0; i--)　//二点間の距離が０になるまで繰り返す
        {
            dummy_Position.z -= timer * moveSpeed;
            dummyObject.transform.position = dummy_Position;
        }

    }

    void Random_Display()　　　//ボールのスコアの表示
    {
        ballScore.SetActive(true);
        lavaBallScore.SetActive(true);
    }
}

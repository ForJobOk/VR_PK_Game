using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallShot : MonoBehaviour {
    
    //print("rogu deruyo");   ->　ログが出る
    [Range(0f,100f)]
    public float speedAdjust;

    //デバック用　溶岩ボールの出る確率
    [Range(1, 5)]
    public int randomBallNumber;


    GameObject shotMachine; 　　　　 //BallShotを取得するための変数
    Rigidbody ofBallShot;                     //BallShotのRigidbodyを取得するための変数
    public GameObject[] randomBall;　　//生成されるボールのための変数

    bool shotPossible =true;                 //ボールを発射できる状態かどうか

    int ballCount;                             //ボールのカウンター
    //int index;                                   //選ばれたボールの数字

	// Use this for initialization
	void Start () {
        //ボールが一定時間ごとに生成されるコルーチン
        StartCoroutine(Shotball());

        //BallShotの取得
        shotMachine = GameObject.Find("BallShot");
        ofBallShot = shotMachine.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator Shotball()
    {
        //発射される時間をランダムで設定

        //スコアに応じて発射速度、間隔を変更可能
        if (FindObjectOfType<ScoreManager>().AddScore(0) <200)
        {
            while (shotPossible)
            {
                float shotTime = Random.Range(3, 6);
                yield return new WaitForSeconds(shotTime);

                Shot(-10, -40);
                Invoke("RotationSet", 2.5f);
            }
        }
        else
        {
            while (shotPossible)
            {
                float shotTime = Random.Range(2, 4);
                yield return new WaitForSeconds(shotTime);

                Shot(-30, -80);
                Invoke("RotationSet", 1.5f);
            }
        }
    }

    void RotationSet()
    {
        ofBallShot.transform.rotation = Quaternion.identity;  //発射のたびに向きを元に戻す
    }

    private Vector3 Ballposition()
    {
        Vector3 ballPosi = new Vector3(0, 3, 20);
        return ballPosi;
    }

    public void Shot(float speedMin ,float speedMax)
    {
        //発射する方向(力)をランダムで設定
        float ballX = Random.Range(-11, 11);
        
        float ballY = Random.Range(-11, 11);

        float ballZ = Random.Range(speedMin ,speedMax);

        float ballSpeed = Random.Range(20, 50)+speedAdjust;
        print("X=" + ballX + "     Y=" + ballY +"      Z=" + ballZ +"         ボールスピード" + ballSpeed);
        GameObject ball = (GameObject)Instantiate(Randomball(), Ballposition(),Quaternion.identity);

        Vector3 ballRotation = ball.transform.rotation.eulerAngles; 　 //ボールのオイラー角を取得
        float _y = ballRotation.y;　　　　　　　　　　　　　　　　  //Y軸方向の回転を代入
        _y += ballY;　　　　　　　　　　                                      //さらにランダムで設定した角度を加える
        ballRotation.y = _y;　　　　　　　                                    //代入が終了したものをボールのオイラー角の入った変数に戻す
        ball.transform.rotation =  Quaternion.Euler( ballRotation);　//Quaternion.Euler(角度);   -> 角度を取得
        Vector3 ballFace_y = ball.transform.forward;                      //ボールのランダムな角度を取得できたのでその時点の正面を取得する

        Vector3 _ballRotation = ball.transform.rotation.eulerAngles; 　 //ボールのオイラー角を取得
        float _x = _ballRotation.x;　　　　　　　　　　　　　　　　  //X軸方向の回転を代入
        _x += ballX;　　　　　　　　　　                                      //さらにランダムで設定した角度を加える
        _ballRotation.x = _x;　　　　　　　                                    //代入が終了したものをボールのオイラー角の入った変数に戻す
        ball.transform.rotation = Quaternion.Euler(_ballRotation);　//Quaternion.Euler(角度);   -> 角度を取得
        Vector3 ballFace_x = ball.transform.forward;                      //ボールのランダムな角度を取得できたのでその時点の正面を取得する　 

        Rigidbody ballrigidbody = ball.GetComponent<Rigidbody>();
        ofBallShot.AddTorque(new Vector3(ballX, ballY, 0));
        ballrigidbody.AddForce(ballFace_y * ballSpeed *ballZ);
        ballrigidbody.AddForce(ballFace_x * ballSpeed * ballZ);


    }

    GameObject Randomball()
    {
        ballCount = Random.Range(1, 4);
        
        //randomBallNumber <- 溶岩ボールとの接触を確認したいとき用

        if(ballCount%3 == 0)
        {
            return randomBall[0];
        }

        return randomBall[1];
    }

    //時間制　アップデート内で座標を変えながら移動
}

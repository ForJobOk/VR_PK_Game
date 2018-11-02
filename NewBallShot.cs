using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBallShot : MonoBehaviour {

    Vector3 instanciate_ballPosi;

    Vector3 ballPosi;

    public GameObject[] randomBall;


    bool shotPossible = true;

    float randomPosition_X;
    float randomPosition_Y;

    // Update is called once per frame
    void Update () {
        
        

    }

    

    void Shot() //ボールを生成
    {
        GameObject ball = Instantiate(Randomball(), RandomPosition(), Quaternion.identity);

    }

    Vector3 RandomPosition()　//ボール生成の位置
    {
        randomPosition_X = Random.Range(-3, 3);
        randomPosition_Y = Random.Range(0.5f, 5);

        instanciate_ballPosi = new Vector3(randomPosition_X, randomPosition_Y, 20);
        return instanciate_ballPosi;
    }

    GameObject Randomball()  　//ランダムで溶岩ボールを出す
    {

        float ballCount = Random.Range(1, 4);

        if (ballCount % 3 == 0)
        {
            return randomBall[0]; //LAVA
        }

        return randomBall[1];　//BALL
    }

    IEnumerator ShotBall()　//コルーチン
    {
        while (shotPossible)    //While文はtrueであればループする
        {
            float randomCount = Random.Range(1, 3);
            yield return new WaitForSeconds(randomCount);
            Shot();
        }
        
    }
    // Use this for initialization
    void Start()
    {
        StartCoroutine(ShotBall());　//コルーチンスタート
    }


}

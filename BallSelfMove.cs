using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSelfMove : MonoBehaviour {

    Vector3 move_ballPosition;

    Vector3 toGoPoint;

    Vector3 _forward;

    int random_Number;
    float speedStar = 3f;

    Rigidbody rd;

    [Range(0,100)]
    public float moveSpeed;
    [Range(0, 100)]
    public float displacement;
    [Range(0, 100)]
    public float random_X;
    [Range(0, 100)]
    public float random_Y;

    // Use this for initialization
    void Start () {

        moveSpeed = Random.Range(0.001f, 0.005f);

        displacement = Random.Range(0.01f, 0.05f);

        random_X = Random.Range(-3.0f, 3.0f);

        random_Y = Random.Range(1, 4);

        toGoPoint = new Vector3(random_X, random_Y, -5.5f);

        random_Number = Random.Range(0, 5);
        print("型"+random_Number+"速"+ moveSpeed+"変位" + displacement+"X" + random_X+"Y" + random_Y);


        Destroy(this.gameObject, 20f);
        
    }
	
	// Update is called once per frame
	void Update () {

        MovePosition(random_Number);

    }

     

    public void OnCollisionEnter(Collision other)
    {
        
        if (other.gameObject.tag == "Net")
        {
            rd = this.GetComponent<Rigidbody>();
            rd.useGravity = true;

            MovePosition(random_Number = 5);
            Destroy(this.gameObject, 1.0f);
        }
    }

    


    Vector3 MovePosition(int number) //座標の変化パターンをランダムに決める
    {

        float _time = Time.time;  //時間で移動

        //x軸、y軸にSin,Cosをそれぞれ当てはめて円運動を再現できる
        float _sin = displacement * Mathf.Sin(_time);
        float _cos = displacement * Mathf.Cos(_time);


        this.transform.LookAt(toGoPoint); //設定したポイントに正面を向ける

        _forward = this.transform.forward;

        switch (random_Number)
        {
            case 0:


                move_ballPosition = new Vector3(_cos, _sin, _forward.z*_time * moveSpeed);
                this.transform.position += move_ballPosition;

                break;

           case 1:

                move_ballPosition = new Vector3(_cos, _cos, _forward.z * _time * moveSpeed);
                this.transform.position += move_ballPosition;

                break;

            case 2:

                move_ballPosition = new Vector3(_sin, _sin, _forward.z * _time * moveSpeed);
                this.transform.position += move_ballPosition;

                break;

            case 3:

                move_ballPosition = new Vector3(_forward.x,_forward.y, _forward.z * _time * moveSpeed);
                this.transform.position += move_ballPosition;
                break;

            case 4:

                move_ballPosition = new Vector3(_forward.x, _forward.y, _forward.z * _time * moveSpeed * speedStar);
                this.transform.position += move_ballPosition;
                break;

            case 5:

                this.transform.position = this.transform.position;
                break;

        }

        return this.transform.position;
        

        
    }
}

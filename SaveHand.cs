using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class SaveHand : MonoBehaviour
{

    [SteamVR_DefaultAction("Haptic")]
    public SteamVR_Action_Vibration hapticAction;
    public SteamVR_Input_Sources HandType;
    public GameObject ball;
    public GameObject lavaBall;
    public AudioSource audioSourceC;
    public AudioSource audioSourceW;
    
    GameObject soundBoxC;
    GameObject soundBoxW;
    public ParticleSystem[] particleBall;
    //ParticleSystem particleLavaBall;

    bool touchBall = false;
    bool touchLavaBall = false;
    int countTime=15;
    


    /*パーティクルがプレハブの場合はInspectorでアタッチしても生成されてないことになる。
     *Break Prefab Instanceしてアタッチするか、生成される側の子に設定して取り出す。*/

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ball")
        {
            audioSourceC.Play();
            FindObjectOfType<ScoreManager>().AddScore(20);
            
            particleBall[0].Play();
            Destroy(other.gameObject);
            touchBall = true;

        }
        else if (other.gameObject.tag == "Lava")
        {
            audioSourceW.Play();
            FindObjectOfType<ScoreManager>().AddScore(-10);
            
            particleBall[1].Play();
            Destroy(other.gameObject);
            touchLavaBall = true;
        }
    }

    //振動数をコントロール　溶岩ボールは触ると大きく振動
    public void TriggerHapticBall(ushort microSecondsDuration)
    {
        float seconds = (float)microSecondsDuration / 1000000f;
        hapticAction.Execute(0, seconds, 1f / seconds, 1, HandType);
    }

    // Use this for initialization
    void Start()
    {

        //スタート時にゲームオブジェクト、コンポーネント取得
        soundBoxC = GameObject.Find("AudioCorrect");
        soundBoxW = GameObject.Find("AudioWrong");
        audioSourceC = soundBoxC.GetComponent<AudioSource>();
        audioSourceW = soundBoxW.GetComponent<AudioSource>();
        //particleBall = this.GetComponentInChildren<ParticleSystem>();
        //particleLavaBall = this.GetComponentInChildren<ParticleSystem>();
    }

    
    // Update is called once per frame
    void Update()
    {
        /*Update内でボールとの接触を調べて振動させると、振動を止めれない(振動後にfalseにしたら一瞬で振動が止まる)。
         *なので、カウンターを用意して条件を満たしている間はカウンターが減る仕組みにした。
         *カウンターが０になったら振動が止まる。
         *再びカウンターを元の数値に戻さなければ、次のボールと接触した際にカウンターが０のままなので振動しない。
         *そこで、カウンターが０かどうかもUpdate内でチェックする仕組みにした。
         */


        if (touchBall && countTime > 0)
        {
            TriggerHapticBall(2000);
            //ボールが当たると黄色く光る
            this.GetComponentInChildren<Renderer>().material.color = Color.yellow;
            countTime--;

        }


        if (countTime == 0)
        {
            countTime = 15;
            touchBall = false;
            this.GetComponentInChildren<Renderer>().material.color = Color.white;
        }

        if (touchLavaBall && countTime > 0)
        {
            TriggerHapticBall(5000);
            //ボールが当たると赤く光る
            this.GetComponentInChildren<Renderer>().material.color = Color.red;
            countTime--;
        }

       // CountTimer(touchLavaBall = false);
        if (countTime == 0)
        {
            countTime = 15;
            touchLavaBall = false;
            this.GetComponentInChildren<Renderer>().material.color = Color.white;
        }
        
    }

    
    
}

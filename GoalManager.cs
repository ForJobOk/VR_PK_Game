using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalManager : MonoBehaviour {


    AudioSource wrongSound;  //サウンドを入れるための変数

    //音楽を再生する機能だけにする

    public void PlaySound()
    {
        wrongSound = GetComponent<AudioSource>();
        wrongSound.Play();
    }

    
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

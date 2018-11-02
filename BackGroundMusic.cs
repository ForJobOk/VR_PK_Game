using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundMusic : MonoBehaviour {

    AudioSource bgm;
    AudioSource cdm;

    public void PlayBGM()
    {
        bgm = GetComponent<AudioSource>();
        bgm.Play();
    }

    public void Count_DownMusic()
    {
        cdm = GetComponent<AudioSource>();
        cdm.Play();
    }


    public void StopBGM()
    {
        bgm = GetComponent<AudioSource>();
        bgm.Stop();
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

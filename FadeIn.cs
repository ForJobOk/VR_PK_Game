using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class FadeIn : MonoBehaviour {

    float _fadeDuration = 10f;

    // Use this for initialization
    void Start () {

        SteamVR_Fade.Start(Color.black, 0f);

        SteamVR_Fade.Start(Color.clear,_fadeDuration);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

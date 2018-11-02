using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class TriggerPull_PK_Scene : MonoBehaviour {

    public Counter _Counter;

    public SteamVR_Input_Sources HandType;
    public SteamVR_Action_Boolean GrabAction;


    bool count_true_or_false = true;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (GrabAction.GetState(HandType)&& count_true_or_false)
        {
            _Counter.ViveTriggerOn();
            count_true_or_false = false;
            
        }

    }

    
}

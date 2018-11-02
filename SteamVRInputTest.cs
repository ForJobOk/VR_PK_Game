using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class SteamVRInputTest : MonoBehaviour {

    public SteamVR_Input_Sources HandType;
    public SteamVR_Action_Boolean GrabAction;
    LineRenderer line;
    float range = 100.0f;
   

    private void Start()
    {
        line = this.GetComponent<LineRenderer>();
       
    }


    void Update() {
        if (GrabAction.GetState(HandType)) {
           
            GetComponent<LineRenderer>().enabled = true;
            //コントローラーのトリガーを引いたら…を加える
            RayRightHand rayRightHand =GetComponent<RayRightHand>();
            rayRightHand.Ray();


            Ray ray = new Ray(transform.position, transform.forward);

            line.SetPosition(0, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z));
            line.SetPosition(1, ray.origin + ray.direction * range);
        }
        else
        {
            GetComponent<LineRenderer>().enabled = false;

        }
    }
}

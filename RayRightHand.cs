using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RayRightHand : MonoBehaviour {

    public LayerMask startButton;

    [Range(0f, 10000f)]
    public float rayDistance;

    public GameObject startButton_Material;
    public GameObject selectMusic;

    bool rayHit_StartButton = false;

    public FadeMode headCamera;


    // Use this for initialization
    void Start () {
        
    }

    // Update is called once per frame
    void Update () {

        if (rayHit_StartButton == false)
        {
            startButton_Material.GetComponent<Renderer>().material.SetColor("_TintColor", Color.black);
        }

    }
    public void Ray()
    {

        Ray ray = new Ray(transform.position, transform.forward);

        RaycastHit hit; //レイが衝突したオブジェクト

        //Physics.Raycast(①Ray ray, ②out RaycasatHit hitInfo, ③float maxDistance, ④int layerMask) 
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, startButton))
        {
            //hit.collider.GetComponent<MeshRenderer>().material.color = Color.yellow;

            selectMusic.SetActive(true);
            startButton_Material.GetComponent<Renderer>().material.SetColor("_TintColor", Color.yellow);

            rayHit_StartButton = true;

            headCamera.f_FadeOut();
            Invoke("Change_Scene", 2.5f);
            
        }
        else
        {
            rayHit_StartButton = false;
        }

        //Debug.DrawRay(①Vector3 start, ②Vector3 dir, ③Color color, ④float duration, ⑤bool depthTest);
        //Debug.DrawRay(ray.origin, ray.direction * rayDistance, Color.red, Mathf.Infinity);

    }
    

    void Change_Scene()　　//シーンの変更
    {
        SceneManager.LoadScene("Pk");
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class FadeMode : MonoBehaviour {

    /*【使い方】
     * 《フェードインしたいとき(スタート時)》
     * Inspectorからの設定でスタートカラーを透明度255、アフターカラーを0に設定する。
     * Durationはフェードにかかる時間。
     *
     * 《フェードアウトしたいとき》
     * 処理を書いたスクリプトにpublic FadeMode 変数名;でカメラをアタッチする。
     * 別のスクリプトでさせたいタイミングの箇所に　変数名.f_FadeOut();
     * あとはフェードイン同様にInspectorでいじる
     * 
     */



    public float _fadeInDuration;
    public Color start_color_In;
    public Color after_color_In;
    public bool fade_In;

    public float _fadeOutDuration;
    public Color start_color_Out;
    public Color after_color_Out;
    

    // Use this for initialization
    void Start () {

        if(fade_In)
        {
            f_FadeIn();
        }

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void f_FadeIn()
    {
        SteamVR_Fade.Start(start_color_In, 0f);

        SteamVR_Fade.Start(after_color_In, _fadeInDuration);
    }

    public void f_FadeOut()
    {
        SteamVR_Fade.Start(start_color_Out, 0f);

        SteamVR_Fade.Start(after_color_Out, _fadeOutDuration);
    }
}

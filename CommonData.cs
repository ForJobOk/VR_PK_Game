using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonData {

    public readonly static CommonData Instance = new CommonData();

    // 全チェック項目
    public int totalScore = 0;
    public int ballScore = 0;
    public int lavaBallScore = 0;
   

    public void initCommonData() {
        totalScore = 0;
        ballScore = 0;
        lavaBallScore = 0;
    }

}

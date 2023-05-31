using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CarTimer : MonoBehaviour
{
    private bool timerStart;        // 開始時間
    private bool timerFinished;     // 終了時間
    private float SecondTime;       // 秒時間
    private int MinuteTime;         // 分時間
    public TextMeshProUGUI UItext;             // タイマー表示

    void Start()
    {
        timerStart = false;
        timerFinished = false;
        SecondTime = 0;
        MinuteTime = 0;
    }

    void FixedUpdate()
    {
        // タイマーが開始されたら時間を加算
        if (timerStart == true && timerFinished == false)
            SecondTime += Time.deltaTime * GManager.TimeScale;

        if (SecondTime >= 60)
        {
            MinuteTime++;
            SecondTime = 0;
        }

        UItext.text = MinuteTime.ToString() + ":" + SecondTime.ToString("F2");
    }

    private void OnTriggerEnter(Collider other)
    {
        // ゲームタグが"StartLine"のとき
        if (other.gameObject.tag == "StartLine")
        {
            // 時間を進める
            if (timerStart == false)
                timerStart = true;
        }

        if (other.gameObject.tag == "Finish")
        {
            timerFinished = true;
        }
    }
}
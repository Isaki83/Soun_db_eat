using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashingMesh : MonoBehaviour
{
    MeshRenderer mesh;
    float Alpha = 0.0f;                 // 透明度
    public float Step = 200.0f;         // 1秒あたりの上昇量
    int FlashCount = 0;                 // 点滅回数カウント

    /*==============================================================

        開始
     
    ==============================================================*/
    void Start()
    {
        mesh = GetComponent<MeshRenderer>();
    }


    /*==============================================================

        更新
     
    ==============================================================*/
    void Update()
    {
        Flashing(mesh);
    }


    /*==============================================================

       点滅させる
     
    ==============================================================*/
    void Flashing(MeshRenderer obj)
    {
        if (3 == FlashCount) { return; }

        Alpha += Step * Time.deltaTime * GManager.TimeScale;

        if (100.0f < Alpha)
        {
            Step *= -1.0f;
            Alpha = 100.0f;
        }
        else if (Alpha < 0.0f)
        {
            Step *= -1.0f;
            FlashCount++;
            Alpha = 0.0f;
        }

        obj.material.color = new Color32(0, 255, 0, (byte)Alpha);
    }
}

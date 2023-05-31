using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedEffect : MonoBehaviour
{
    // コンポーネント取得
    private PlayerMove _PlayerMove;
    private ParticleSystem _ParticleSystem;
    private ParticleSystem.EmissionModule _EmissionModule;
    private ParticleSystem.MinMaxCurve _MinMaxCurve;


    /*==============================================================

        開始
     
    ==============================================================*/
    void Start()
    {
        // コンポーネント取得
        _PlayerMove = GetComponentInParent<PlayerMove>();
        _ParticleSystem = GetComponent<ParticleSystem>();
        _EmissionModule = _ParticleSystem.emission;
        _MinMaxCurve = _EmissionModule.rateOverTime;
    }


    /*==============================================================

        更新
     
    ==============================================================*/
    void Update()
    {
        // 曲のピッチに合わせてパーティクルの放出量を変える
        _MinMaxCurve.constant = (_PlayerMove.AudioPitch - 1.0f) * 100.0f;
        _EmissionModule.rateOverTime = _MinMaxCurve;
    }
}

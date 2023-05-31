using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Effekseer;

public class Notes : MonoBehaviour
{
    // ノーツ衝突チェック
    protected bool isNotesHit = false;
    // 衝突中のオブジェクト
    protected Collider ColliObj;

    // --- エフェクト ---
    EffekseerEmitter emitter;           // エフェクシア
    protected string[] _LoadEffect = { "Notes3", "Star" };
    public enum Effects
    {
        Notes3,
        Star,
    }

    // --- マテリアル ---
    [SerializeField]
    private Renderer[] KART_Renderer;
    private readonly int ColorPropertyID = Shader.PropertyToID("_EmissionColor");
    private MaterialPropertyBlock ColorBlock;
    [ColorUsage(false, true)]
    public Color DefaultColor;
    [ColorUsage(false, true)]
    public Color HitColor;
    private Color[] _EmissionColor = new Color[2];
    public enum EmissionColor
    {
        Defalt,  // デフォルトの色
        Hit,     // ヒット中の色
    }

    // Input Action
    protected InputPlsyer _InputPlayer;

    // `PlayerMove.cs`スクリプト呼び出し
    protected PlayerMove playerMove;

    /*==============================================================

        開始
     
    ==============================================================*/
    void Start()
    {
        emitter = GetComponent<EffekseerEmitter>();

        _EmissionColor[(int)EmissionColor.Defalt] = DefaultColor;
        _EmissionColor[(int)EmissionColor.Hit] = HitColor;
        ColorBlock = new MaterialPropertyBlock();
        ColorBlock.SetColor(ColorPropertyID, _EmissionColor[(int)EmissionColor.Defalt]);

        // インスタンス生成
        _InputPlayer = new InputPlsyer();
        // Input Action有効化
        _InputPlayer.Enable();

        // スクリプト呼び出し
        playerMove = GetComponent<PlayerMove>();
    }


    /*==============================================================

        削除された時
     
    ==============================================================*/
    private void OnDestroy()
    {
        // Input Action無効化
        _InputPlayer?.Dispose();
    }


    /*==============================================================

        エフェクト再生
     
    ==============================================================*/
    protected void Effect(string loadEffect)
    {
        // エフェクトを取得する。
        EffekseerEffectAsset effect_notes = Resources.Load<EffekseerEffectAsset>(loadEffect);
        // transformの位置でエフェクトを再生する
        EffekseerHandle handle = EffekseerSystem.PlayEffect(effect_notes, transform.position);
        // transformの回転を設定する。
        handle.SetRotation(transform.rotation);
    }


    /*==============================================================

        判定のタイミングをを分かりやすくするためのボールの色変更
     
    ==============================================================*/
    protected void ColliBallColor(EmissionColor color)
    {
        ColorBlock.SetColor(ColorPropertyID, _EmissionColor[(int)color]);
        for (int i = 0; i < KART_Renderer.Length; i++) { KART_Renderer[i].SetPropertyBlock(ColorBlock); }
    }
}

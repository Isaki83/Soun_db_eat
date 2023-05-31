using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ButtonManager : MonoBehaviour
{
    // Input Action
    private InputPlsyer _InputPlayer = null;

    // --- ボタン ---
    [Tooltip("ボタンが２つ以上ある場合は,\n「ButtonManager.cs」に書いてある「ボタン要素番号」を元にセットして下さい。")]
    public List<GameObject> _Button;
    [HideInInspector]
    public List<ButtonAnim> _ButtonAnim;
    public int Element { get; set; }        // 要素番号
    private int prevElement = 0;            // 一個前の要素番号
    [SerializeField]
    private bool canPush = true;            // 一回の操作で2回分動かないようにする

    // １ステージ当たりの難易度数
    const int MAX_LVEL = 3;

    // シーン
    public Scene _Scene = Scene.None;
    public enum Scene
    {
        None,
        Title,
        StageSelect,
        MainGame,
    }

    // ボタン要素番号
    public enum Title
    {
        OpenOption,
        Start,
        Exit,

        ExitOption,
        CloseOption,
        BackTitle,

        BGM_Slider,
        SE_Slider,
    };
    public enum StageSelct
    {
        Stage01H,
        Stage01N,
        Stage01E,

        Stage02H,
        Stage02N,
        Stage02E,

        Stage03H,
        Stage03N,
        Stage03E,

        Back,
        Tutorial,
    };
    public enum MainGame
    {
        StageSelect,

        ExitOption,
        CloseOption,
        BackTitle,

        BGM_Slider,
        SE_Slider,
    }

    /*==============================================================

        開始
     
    ==============================================================*/
    void Start()
    {
        // インスタンス生成
        _InputPlayer = new InputPlsyer();
        // Input Action有効化
        _InputPlayer.Enable();

        // コンポーネント取得
        for (int i = 0; i < _Button.Count; i++) { _ButtonAnim.Add(_Button[i].GetComponent<ButtonAnim>()); }

        // 要素番号初期化
        switch (_Scene)
        {
            case Scene.Title:
                Element = (int)Title.Start;
                prevElement = 0;
                break;
            case Scene.StageSelect:
                Element = (int)StageSelct.Tutorial;
                prevElement = (int)StageSelct.Stage02H;
                break;
            case Scene.MainGame:
                Element = (int)MainGame.StageSelect;
                prevElement = 0;
                break;
            default:
                break;
        }
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

        更新
     
    ==============================================================*/
    void Update()
    {
        switch(_Scene)
        {
            case Scene.Title:
                #region Title
                if (Element == (int)Title.OpenOption || Element == (int)Title.Start)
                {
                    // 要素番号を上下させる
                    ElementUpDown((int)Title.Start, (int)Title.OpenOption, _InputPlayer.UI.UP, _InputPlayer.UI.Down);
                    // 要素番号を横移動
                    if (_InputPlayer.UI.Left.WasPressedThisFrame() && canPush)
                    {
                        // アニメーション再生
                        if (_ButtonAnim[Element] != null) { _ButtonAnim[Element].PlayAnim(ButtonAnim.Access.Exit); }
                        Element = (int)Title.Exit;
                        // アニメーション再生
                        if (_ButtonAnim[Element] != null) { _ButtonAnim[Element].PlayAnim(ButtonAnim.Access.Enter); }

                        // 押せない状態にする
                        canPush = false;
                    }
                }
                if (Element == (int)Title.Exit)
                {
                    // 要素番号を横移動
                    if (_InputPlayer.UI.Right.WasPressedThisFrame() && canPush)
                    {
                        // アニメーション再生
                        if (_ButtonAnim[Element] != null) { _ButtonAnim[Element].PlayAnim(ButtonAnim.Access.Exit); }
                        Element = (int)Title.OpenOption;
                        // アニメーション再生
                        if (_ButtonAnim[Element] != null) { _ButtonAnim[Element].PlayAnim(ButtonAnim.Access.Enter); }

                        // 押せない状態にする
                        canPush = false;
                    }
                }

                // --- オプション ---
                if ((int)Title.ExitOption <= Element && Element <= (int)Title.BackTitle)
                {
                    if (_InputPlayer.UI.UP.WasPressedThisFrame())
                    {
                        // アニメーション再生
                        if (_ButtonAnim[Element] != null) { _ButtonAnim[Element].PlayAnim(ButtonAnim.Access.Exit); }
                        Element = (int)Title.SE_Slider;
                        // アニメーション再生
                        if (_ButtonAnim[Element] != null) { _ButtonAnim[Element].PlayAnim(ButtonAnim.Access.Enter); }

                        // 押せない状態にする
                        canPush = false;
                    }
                    ElementUpDown((int)Title.BackTitle, (int)Title.ExitOption, _InputPlayer.UI.Left, _InputPlayer.UI.Right);
                }
                if ((int)Title.BGM_Slider <= Element && Element <= (int)Title.SE_Slider)
                {
                    if (Element == (int)Title.SE_Slider && _InputPlayer.UI.Down.WasPressedThisFrame())
                    {
                        // アニメーション再生
                        if (_ButtonAnim[Element] != null) { _ButtonAnim[Element].PlayAnim(ButtonAnim.Access.Exit); }
                        Element = (int)Title.CloseOption;
                        // アニメーション再生
                        if (_ButtonAnim[Element] != null) { _ButtonAnim[Element].PlayAnim(ButtonAnim.Access.Enter); }

                        // 押せない状態にする
                        canPush = false;
                    }
                    ElementUpDown((int)Title.SE_Slider, (int)Title.BGM_Slider, _InputPlayer.UI.Down, _InputPlayer.UI.UP);
                }
                #endregion
                break;
            case Scene.StageSelect:
                #region StageSelect
                // --- 要素番号を上下させる ---
                // レベル => EASY
                if (Element == (int)StageSelct.Stage01E || Element == (int)StageSelct.Stage02E || Element == (int)StageSelct.Stage03E)
                { ElementUpDown((int)StageSelct.Stage03E, (int)StageSelct.Stage01E, _InputPlayer.UI.Right, _InputPlayer.UI.Left, MAX_LVEL); }
                // レベル => NORMAL
                if (Element == (int)StageSelct.Stage01N || Element == (int)StageSelct.Stage02N || Element == (int)StageSelct.Stage03N)
                { ElementUpDown((int)StageSelct.Stage03N, (int)StageSelct.Stage01N, _InputPlayer.UI.Right, _InputPlayer.UI.Left, MAX_LVEL); }
                // レベル => HARD
                if (Element == (int)StageSelct.Stage01H || Element == (int)StageSelct.Stage02H || Element == (int)StageSelct.Stage03H)
                {
                    ElementUpDown((int)StageSelct.Stage03H, (int)StageSelct.Stage01H, _InputPlayer.UI.Right, _InputPlayer.UI.Left, MAX_LVEL);

                    // コントローラー => 下     チュートリアルへ
                    if (_InputPlayer.UI.Down.WasPressedThisFrame() && canPush)
                    {
                        // アニメーション再生
                        if (_ButtonAnim[Element] != null) { _ButtonAnim[Element].PlayAnim(ButtonAnim.Access.Exit); }
                        // 要素番号を更新
                        prevElement = Element;
                        Element = (int)StageSelct.Tutorial;
                        // アニメーション再生
                        if (_ButtonAnim[Element] != null) { _ButtonAnim[Element].PlayAnim(ButtonAnim.Access.Enter); }

                        // 押せない状態にする
                        canPush = false;
                    }
                }
                // ステージ０１
                if ((int)StageSelct.Stage01H <= Element && Element <= (int)StageSelct.Stage01E)
                { ElementUpDown((int)StageSelct.Stage01E, (int)StageSelct.Stage01H, _InputPlayer.UI.UP, _InputPlayer.UI.Down); }
                // ステージ０２
                if ((int)StageSelct.Stage02H <= Element && Element <= (int)StageSelct.Stage02E)
                { ElementUpDown((int)StageSelct.Stage02E, (int)StageSelct.Stage02H, _InputPlayer.UI.UP, _InputPlayer.UI.Down); }
                // ステージ０３
                if ((int)StageSelct.Stage03H <= Element && Element <= (int)StageSelct.Stage03E)
                { ElementUpDown((int)StageSelct.Stage03E, (int)StageSelct.Stage03H, _InputPlayer.UI.UP, _InputPlayer.UI.Down); }
                // チュートリアル
                if (Element == (int)StageSelct.Tutorial)
                {
                    if (_InputPlayer.UI.UP.WasPressedThisFrame() && canPush)
                    {
                        // アニメーション再生
                        if (_ButtonAnim[Element] != null) { _ButtonAnim[Element].PlayAnim(ButtonAnim.Access.Exit); }
                        // 要素番号を更新
                        Element = prevElement;
                        // アニメーション再生
                        if (_ButtonAnim[Element] != null) { _ButtonAnim[Element].PlayAnim(ButtonAnim.Access.Enter); }

                        // 押せない状態にする
                        canPush = false;
                    }
                }
                // チュートリアルとバック
                if (Element == (int)StageSelct.Tutorial || Element == (int)StageSelct.Back)
                { ElementUpDown((int)StageSelct.Tutorial, (int)StageSelct.Back, _InputPlayer.UI.Left, _InputPlayer.UI.Right); }
                #endregion
                break;
            case Scene.MainGame:
                #region MainGame
                if (Element == (int)MainGame.StageSelect)
                {
                    ElementUpDown(0, 0, _InputPlayer.UI.UP, _InputPlayer.UI.Down);
                    ElementUpDown(0, 0, _InputPlayer.UI.Right, _InputPlayer.UI.Left);
                }

                // --- オプション ---
                if ((int)MainGame.ExitOption <= Element && Element <= (int)MainGame.BackTitle)
                {
                    if (_InputPlayer.UI.UP.WasPressedThisFrame())
                    {
                        // アニメーション再生
                        if (_ButtonAnim[Element] != null) { _ButtonAnim[Element].PlayAnim(ButtonAnim.Access.Exit); }
                        Element = (int)MainGame.SE_Slider;
                        // アニメーション再生
                        if (_ButtonAnim[Element] != null) { _ButtonAnim[Element].PlayAnim(ButtonAnim.Access.Enter); }

                        // 押せない状態にする
                        canPush = false;
                    }
                    ElementUpDown((int)MainGame.BackTitle, (int)MainGame.ExitOption, _InputPlayer.UI.Left, _InputPlayer.UI.Right);
                }
                if ((int)MainGame.BGM_Slider <= Element && Element <= (int)MainGame.SE_Slider)
                {
                    if (Element == (int)MainGame.SE_Slider && _InputPlayer.UI.Down.WasPressedThisFrame())
                    {
                        // アニメーション再生
                        if (_ButtonAnim[Element] != null) { _ButtonAnim[Element].PlayAnim(ButtonAnim.Access.Exit); }
                        Element = (int)MainGame.CloseOption;
                        // アニメーション再生
                        if (_ButtonAnim[Element] != null) { _ButtonAnim[Element].PlayAnim(ButtonAnim.Access.Enter); }

                        // 押せない状態にする
                        canPush = false;
                    }
                    ElementUpDown((int)MainGame.SE_Slider, (int)MainGame.BGM_Slider, _InputPlayer.UI.Down, _InputPlayer.UI.UP);
                }
                #endregion
                break;
            default:
                #region default
                ElementUpDown(0, 0, _InputPlayer.UI.UP, _InputPlayer.UI.Down);
                ElementUpDown(0, 0, _InputPlayer.UI.Right, _InputPlayer.UI.Left);
                #endregion
                break;
        }

        // "canPush"をtrueに戻す
        if(_InputPlayer.UI.UP.WasReleasedThisFrame() || _InputPlayer.UI.Down.WasReleasedThisFrame()
            || _InputPlayer.UI.Right.WasReleasedThisFrame() || _InputPlayer.UI.Left.WasReleasedThisFrame())
        { canPush = true; }
    }


    /*==============================================================

        要素番号を上下させる

        max     => 最大値
        min     => 最小値
        dirUp   => コントローラの何で要素番号を上げるか
        dirDown => コントローラの何で要素番号を下げるか
        delta   => 変化量
     
    ==============================================================*/
    void ElementUpDown(int max, int min, InputAction dirUp, InputAction dirDown, int delta = 1)
    {
        if (!canPush) { return; }

        // コントローラー => 上 右
        if (dirUp.WasPressedThisFrame())
        {
            // アニメーション再生
            if (_ButtonAnim[Element] != null) { _ButtonAnim[Element].PlayAnim(ButtonAnim.Access.Exit); }
            // 要素番号を上げる
            if (Element + delta <= max) { Element += delta; }
            // アニメーション再生
            if (_ButtonAnim[Element] != null) { _ButtonAnim[Element].PlayAnim(ButtonAnim.Access.Enter); }

            // 押せない状態にする
            canPush = false;
        }
        // コントローラー => 下 左
        if (dirDown.WasPressedThisFrame())
        {
            // アニメーション再生
            if (_ButtonAnim[Element] != null) { _ButtonAnim[Element].PlayAnim(ButtonAnim.Access.Exit); }
            // 要素番号を下げる
            if(min <= Element - delta) { Element -= delta; }
            // アニメーション再生
            if (_ButtonAnim[Element] != null) { _ButtonAnim[Element].PlayAnim(ButtonAnim.Access.Enter); }

            // 押せない状態にする
            canPush = false;
        }
    }


    /*==============================================================

        選択中のGameObject
     
    ==============================================================*/
    public GameObject ButtonObjct
    {
        get { return _Button[Element]; }
    }
}

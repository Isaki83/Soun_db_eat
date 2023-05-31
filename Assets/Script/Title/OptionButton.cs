using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionButton : MonoBehaviour
{
    // Input Action
    private InputPlsyer _InputPlayer;

    public ButtonManager _ButtonManager;
    public PlayerMove _PlayerMove;

    [SerializeField] GameObject OptionPanel;

    [SerializeField]
    private OnClick _OnClick = OnClick.OptionButton;
    public enum OnClick
    {
        OptionButton,
        BackTitleButton,
        ExitButton,
        ButtonManager,
    }

    // Start is called before the first frame update
    void Start()
    {
        // インスタンス生成
        _InputPlayer = new InputPlsyer();
        // Input Action有効化
        _InputPlayer.Enable();
    }

    private void OnDestroy()
    {
        // Input Action無効化
        _InputPlayer?.Dispose();
    }

    private void FixedUpdate()
    {
        // esc(keybord) or start(gamepad)
        _InputPlayer.UI.Option.performed += (InputAction.CallbackContext context) =>
        {
            switch (_OnClick)
            {
                case OnClick.ButtonManager:
                    OnClickOptionButton();
                    break;
                default:
                    break;
            }
        };

        _InputPlayer.UI.Click.performed += (InputAction.CallbackContext context) =>
        {
            if (gameObject != _ButtonManager.ButtonObjct) { return; }
            switch(_OnClick)
            {
                case OnClick.OptionButton:
                    OnClickOptionButton();
                    break;
                case OnClick.BackTitleButton:
                    OnClickBackTitleButton();
                    break;
                case OnClick.ExitButton:
                    OnClickExitButton();
                    break;
            }
        };
    }

    public void OnClickOptionButton()
    {
        Debug.Log("OptionButton");  //オプションログ

        OptionPanel.SetActive(!OptionPanel.activeSelf); //オプションパネルの表示・非表示

        switch (_ButtonManager._Scene)
        {
            case ButtonManager.Scene.Title:
                if (OptionPanel.activeSelf) { _ButtonManager.Element = (int)ButtonManager.Title.SE_Slider; }
                else { _ButtonManager.Element = (int)ButtonManager.Title.OpenOption; }
                break;
            case ButtonManager.Scene.StageSelect:
                break;
            case ButtonManager.Scene.MainGame:
                if (_PlayerMove._State != PlayerMove.State.Play)
                {
                    OptionPanel.SetActive(false);
                    break;
                }
                if (OptionPanel.activeSelf)
                {
                    _ButtonManager.Element = (int)ButtonManager.MainGame.SE_Slider;
                    GManager.TimeScale = 0;
                    _PlayerMove.AudioPause = true;
                }
                else
                {
                    _ButtonManager.Element = (int)ButtonManager.MainGame.StageSelect;
                    GManager.TimeScale = 1;
                    _PlayerMove.AudioPause = false;
                }
                break;
            default:
                break;
        }
    }
    public void OnClickBackTitleButton()
    {
        Debug.Log("BackTitleButton");  //オプションログ
        GManager.TimeScale = 1;
        SceneManager.LoadScene("TitleScene");  //シーン遷移
    }
    public void OnClickExitButton()
    {
        Debug.Log("ExitButton");  //オプションログ
#if UNITY_EDITOR    //UnityEditorでプレイしているとき
        UnityEditor.EditorApplication.isPlaying = false;    //ゲーム終了
#else               //それ以外でプレイしているとき
        Application.Quit(); //ゲーム終了
#endif
    }
}


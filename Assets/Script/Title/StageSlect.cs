using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class StageSlect : MonoBehaviour
{
    // Input Action
    private InputPlsyer _InputPlayer;

    public ButtonManager _ButtonManager;

    public Type _Type = Type.StageSelect;
    public enum Type
    {
        StageSelect,
        Back,
        EndRoll,
        Tutorial,
        Stage01E,
        Stage01N,
        Stage01H,
        Stage02E,
        Stage02N,
        Stage02H,
        Stage03E,
        Stage03N,
        Stage03H,
    };

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

    // Update is called once per frame
    void Update()
    {
        // シーン遷移
        if (gameObject == _ButtonManager.ButtonObjct)
        { if (_InputPlayer.UI.Click.WasPressedThisFrame()) { OnClickChangeScene(); } }
    }

    public void OnClickChangeScene()
    {
        switch (_Type)
        {
            // ステージセレクト
            case Type.StageSelect:
                SceneManager.LoadScene("StageSelectScene");
                break;
            // タイトル
            case Type.Back:
                SceneManager.LoadScene("TitleScene");
                break;

            // エンドロール
            case Type.EndRoll:
                SceneManager.LoadScene("EndRollScene");
                break;

            // チュートリアル
            case Type.Tutorial:
                SceneManager.LoadScene("Tutorial");
                break;

            // ステージ０１[Sinse]
            case Type.Stage01E:
                SceneManager.LoadScene("Sinse Easy");
                break;
            case Type.Stage01N:
                SceneManager.LoadScene("Sinse Normal");
                break;
            case Type.Stage01H:
                SceneManager.LoadScene("Sinse Hard");
                break;

            // ステージ０２[Yoru]
            case Type.Stage02E:
                SceneManager.LoadScene("Yoru Easy");
                break;
            case Type.Stage02N:
                SceneManager.LoadScene("Yoru Normal");
                break;
            case Type.Stage02H:
                SceneManager.LoadScene("Yoru Hard");
                break;

            // ステージ０３[Shining]
            case Type.Stage03E:
                SceneManager.LoadScene("Shining Easy");
                break;
            case Type.Stage03N:
                SceneManager.LoadScene("Shining Normal");
                break;
            case Type.Stage03H:
                SceneManager.LoadScene("Shining Hard");
                break;
        }
    }
}

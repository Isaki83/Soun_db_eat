using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleButton : MonoBehaviour
{
    // Input Action
    private InputPlsyer _InputPlayer;

    public ButtonManager _ButtonManager;

    private AsyncOperation Async;
    [SerializeField] private GameObject LoadUI;
    [SerializeField] private Slider Slider;

    //private bool firstPush = false;
    //private bool goNextScene = false;

    public Type _Type = Type.Start;
    public enum Type
    {
        Start,
        Exit
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
        //if (!goNextScene)
        //{
        //    NextScene(); //シーン遷移
        //    goNextScene = true;
        //}

        //Spaceを押したら
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    //Startボタンが押されたら
        //    OnClickStartButton();
        //}

        if (gameObject == _ButtonManager.ButtonObjct)
        {
            if (_InputPlayer.UI.Click.WasPressedThisFrame())
            {
                switch (_Type)
                {
                    case Type.Start:
                        OnClickStartButton();
                        break;
                    case Type.Exit:
                        OnClickExitButton();
                        break;
                }
            }
        }
    }

    //Startボタンが押されたら
    public void OnClickStartButton()
    {
        Debug.Log("StartButton");   //スタートログ
        //if (!firstPush)
        //{
        //    //Debug.Log("Fade");
        //    firstPush = true;
        //}
        //SceneManager.LoadScene("StageSelect");  //シーン遷移
        NextScene(); //シーン遷移
    }

    public void OnClickExitButton()
    {
        Debug.Log("ExitButton");    //終了ログ

#if UNITY_EDITOR    //UnityEditorでプレイしているとき
        UnityEditor.EditorApplication.isPlaying = false;    //ゲーム終了
#else               //それ以外でプレイしているとき
            Application.Quit(); //ゲーム終了
#endif
    }
    public void NextScene()
    {
        //　ロード画面UIをアクティブにする
        LoadUI.SetActive(true);

        //　コルーチンを開始
        //SceneManager.LoadScene("testStage1");
        StartCoroutine("LoadData");
    }

    IEnumerator LoadData()
    {
        // シーンの読み込みをする
        //Async = SceneManager.LoadSceneAsync("testFadeScene");

        Scene scene = SceneManager.GetActiveScene();
        // 現在のシーンのビルド番号を取得
        int buildIndex = scene.buildIndex;
        // 現在のシーンのビルド番号を＋１（次のシーンのビルド番号になる）
        buildIndex = buildIndex + 1;
        // 取得したビルド番号のシーン（現在のシーン）を読み込む
        Async = SceneManager.LoadSceneAsync(buildIndex);
        //SceneManager.LoadScene("testStage1");
        //　読み込みが終わるまで進捗状況をスライダーの値に反映させる
        while (!Async.isDone)
        {
            var progressVal = Mathf.Clamp01(Async.progress / 0.9f);
            Slider.value = progressVal;
            yield return null;
        }
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    // Input Action
    private InputPlsyer _InputPlayer;
    [SerializeField]
    public GameObject RotChar;
    [SerializeField]
    public GameObject RotBody;
    //横の動きのスピード
    public float MoveSpeed = 3.0f;
    //前進の動きのスピード
    public float FrontSpeed_Start = 7.5f;
    float FrontSpeed;
    // 横移動の増加量
    public Vector3 SideMove { get; private set; }
    // 現在の時間
    private float CurrentTime = 0.0f;
    // 開始時の演出の時間
    public float StartEffectTime = 7.5f;
    //BGM用
    [SerializeField] AudioSource audioSource;
    public AudioClip soundSE;
    public bool AudioPause { get; set; }
    [SerializeField] private float AudioTime;
    //bpm用
    public int bpm = 5;
    GameObject BPMText;
    // クリア文字表示用
    public GameObject ClearPanel;

    // アニメーション用
    private Animator animator;

    //プレイヤー傾き

    //ガードレール判定用
    float Hit_Rg = 0;
    float Hit_Lg = 0;

    // プレイヤーの状態
    public State _State { get; set; }
    public enum State
    {
        Start,  // 開始時
        Play,   // プレイ中
        End     // ゴール後
    }

    /*==============================================================

        列挙型の定義
     
    ==============================================================*/
    public enum SpeedUD
    {
        Up, Down
    }


    /*==============================================================

        コンストラクタ
     
    ==============================================================*/
    private PlayerMove()
    {
        AudioPause = false;
        _State = State.Start;
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

        FrontSpeed = 0.0f;
        audioSource = GetComponent<AudioSource>();
        BPMText = GameObject.Find("BPMText");

        animator = GetComponentInChildren<Animator>();

        ClearPanel.SetActive(false);

        // 開始時の曲の時間
#if UNITY_EDITOR
        audioSource.time = AudioTime;
#else
        audioSource.time = 0;
#endif

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
        // 経過時間を返す
        CurrentTime += Time.deltaTime * GManager.TimeScale;

        switch(_State)
        {
            // 開始時
            case State.Start:
                if (CurrentTime >= StartEffectTime)
                {
                    FrontSpeed = FrontSpeed_Start;
                    _State = State.Play;
                }
                // 待機アニメーション
                animator.SetBool("IsDrive", false);

                if (AudioPause) { audioSource.Pause(); }
                else { audioSource.UnPause(); }
                break;

            // プレイ中
            case State.Play:
                if(Hit_Rg == 0)
                {
                    // Dキー, RightStick（右移動）
                    if (_InputPlayer.Player.RightMove.IsPressed())
                    {
                        //右に移動
                        SideMove += MoveSpeed * transform.right * Time.deltaTime * GManager.TimeScale;
                        Hit_Lg = 0;
                        //キャラの角度の上限-20まで
                        if(RotChar.transform.localEulerAngles.z >= 340 || RotChar.transform.localEulerAngles.z <= 30)
                        {
                            //角度　毎秒ずつ
                            RotChar.transform.Rotate(0f, 0f, -15.0f * Time.deltaTime * GManager.TimeScale);
                        }
                        //車の角度の上限
                        if (RotChar.transform.localEulerAngles.z >= 340 || RotChar.transform.localEulerAngles.z <= 30)
                        {
                            //角度　毎秒ずつ
                            RotBody.transform.Rotate(0f, 0f, -10.0f * Time.deltaTime * GManager.TimeScale);
                        }
                    }
                    
                }
               if(Hit_Lg == 0)
                {
                    // Aキー, LeftStick（左移動）
                    if (_InputPlayer.Player.LeftMove.IsPressed())
                    {
                        
                        //左に移動
                        SideMove -= MoveSpeed * transform.right * Time.deltaTime * GManager.TimeScale;
                        Hit_Rg = 0;
                        //キャラの角度の上限
                        if (RotChar.transform.localEulerAngles.z >= 330 || RotChar.transform.localEulerAngles.z <= 20)
                        {
                            RotChar.transform.Rotate(0f, 0f, 15.0f * Time.deltaTime * GManager.TimeScale);
                        }
                        //車の角度の上限
                        if (RotChar.transform.localEulerAngles.z >= 330 || RotChar.transform.localEulerAngles.z <= 20)
                        {
                            RotBody.transform.Rotate(0f, 0f, 10.0f * Time.deltaTime * GManager.TimeScale);
                        }
                    }
               
                }
                if (!_InputPlayer.Player.RightMove.IsPressed() &&
                     !_InputPlayer.Player.LeftMove.IsPressed())
                {
                    //char未操作時自動で元の体勢に戻る処理
                    //左
                    if (RotChar.transform.localEulerAngles.z >= 0 && RotChar.transform.localEulerAngles.z <= 30)
                    {
                        RotChar.transform.Rotate(0f, 0f, -20.0f * Time.deltaTime * GManager.TimeScale);

                    }
                    //右傾き戻り
                    if (RotChar.transform.localEulerAngles.z >= 330 && RotChar.transform.localEulerAngles.z < 360)
                    {
                        RotChar.transform.Rotate(0f, 0f, 20.0f * Time.deltaTime * GManager.TimeScale);

                    }
                    if (RotBody.transform.localEulerAngles.z >= 0 && RotBody.transform.localEulerAngles.z <= 30)
                    {
                        RotBody.transform.Rotate(0f, 0f, -20.0f * Time.deltaTime * GManager.TimeScale);

                    }
                    //右傾き戻り
                    if (RotBody.transform.localEulerAngles.z >= 330 && RotBody.transform.localEulerAngles.z < 360)
                    {
                        RotBody.transform.Rotate(0f, 0f, 20.0f * Time.deltaTime * GManager.TimeScale);

                    }


                }

                if (AudioPause) { audioSource.Pause(); }
                else { audioSource.UnPause(); }

                AudioTime = audioSource.time;

                // 運転中アニメーション
                animator.SetBool("IsDrive",true);

                break;

            // ゴール後
            case State.End:
                // 待機アニメーション
                animator.SetBool("IsDrive", false);
                break;
        }

        Debug.Log("NowState : " + _State); // ログを表示
        Debug.Log("pitch : " + audioSource.pitch); // ログを表示
    }


    /*==============================================================

        衝突した瞬間
     
    ==============================================================*/
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Dirt")
        {
            Debug.Log("DirtHit"); // ログを表示

            //スピードを下げる
            SpeedUpDown(SpeedUD.Down, 1.0f);
        }
    }
    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Finish")
        {
            ClearPanel.SetActive(true);
            _State = State.End;
            Debug.Log("finish"); // ログを表示
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.tag == "Right_G")
        {
            Hit_Rg = 1;
        }
        
        if(collision.gameObject.tag == "Left_G")
        {
            Hit_Lg = 1;
        }
   
    }


    /*==============================================================

        スピードの加減速
     
    ==============================================================*/
    public void SpeedUpDown(SpeedUD ud, float step)
    {
        switch (ud)
        {
            // スピードUP
            case SpeedUD.Up:
                //曲のスピード
                audioSource.pitch = Mathf.Min(audioSource.pitch + (0.01f * step), 1.50f);

                //bpmup
                BPMText.SendMessage("AddScore", bpm, SendMessageOptions.DontRequireReceiver);

                //音up
                audioSource.PlayOneShot(soundSE);

                break;

            // スピードDown
            case SpeedUD.Down:
                //曲のスピード
                audioSource.pitch = Mathf.Max(audioSource.pitch - (0.05f * step), 0.75f);

                //bpmup
                BPMText.SendMessage("MinusScore", bpm, SendMessageOptions.DontRequireReceiver);
                break;
        }
    }


    /*==============================================================

        オーディオソース ピッチ 受け渡し
     
    ==============================================================*/
    public float AudioPitch
    {
        get { return audioSource.pitch; }
        set { audioSource.pitch = value; }
    }


    /*==============================================================

        スプラインの時間
     
    ==============================================================*/
    public float distanceTravelled
    {
        get { return FrontSpeed * (audioSource.time - StartEffectTime); }
    }
}
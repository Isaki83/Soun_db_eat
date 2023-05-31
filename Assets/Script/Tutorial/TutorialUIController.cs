using UnityEngine;


public partial class TutorialUIController : MonoBehaviour
{
    // --- 共通 ---
    public float DeleteTime = 2.0f;     // パネルを消すまでの時間
    public float TimeBetween = 1.0f;    // 前パネルと次パネルの間の時間
    // "PlayerMove.cs"取得
    private PlayerMove _PlayerMove;

    // --- チュートリアル０１ ---
    [Header("チュートリアル０１")]
    public CanvasGroup CG_Tutorial_01;
    // Panel
    public GameObject GO_Panel01_01;
    private RectTransform RT_Panel01_01;
    public GameObject GO_Panel02_01;
    private RectTransform RT_Panel02_01;
    // Arrow
    public GameObject GO_Arrow_01;
    private CanvasGroup CG_Arrow_01;

    // --- チュートリアル０２ ---
    [Header("チュートリアル０２")]
    public CanvasGroup CG_Tutorial_02;
    // Panel
    public GameObject GO_Panel01_02;
    private RectTransform RT_Panel01_02;
    public GameObject GO_Panel02_02;
    private RectTransform RT_Panel02_02;
    public GameObject GO_Dirt_02;

    // --- チュートリアル０３ ---
    [Header("チュートリアル０３")]
    public CanvasGroup CG_Tutorial_03;
    // Panel
    public GameObject GO_Panel01_03;
    private RectTransform RT_Panel01_03;
    public GameObject GO_Panel02_03;
    private RectTransform RT_Panel02_03;

    // --- チュートリアル０４ ---
    [Header("チュートリアル０４")]
    public CanvasGroup CG_Tutorial_04;
    // Panel
    public GameObject GO_Panel01_04;
    private RectTransform RT_Panel01_04;
    public GameObject GO_Panel02_04;
    private RectTransform RT_Panel02_04;


    /*==============================================================

        開始
     
    ==============================================================*/
    void Start()
    {
        // "PlayerMove.cs"取得
        _PlayerMove = GetComponent<PlayerMove>();

        // --- チュートリアル０１ ---
        // コンポーネント取得
        RT_Panel01_01 = GO_Panel01_01.GetComponent<RectTransform>();
        RT_Panel02_01 = GO_Panel02_01.GetComponent<RectTransform>();
        CG_Arrow_01 = GO_Arrow_01.GetComponent<CanvasGroup>();
        // パラメータ初期化
        RT_Panel01_01.localScale = Vector3.zero;
        RT_Panel02_01.localScale = Vector3.zero;
        CG_Arrow_01.alpha = 0.0f;

        // --- チュートリアル０２ ---
        // コンポーネント取得
        RT_Panel01_02 = GO_Panel01_02.GetComponent<RectTransform>();
        RT_Panel02_02 = GO_Panel02_02.GetComponent<RectTransform>();
        // パラメータ初期化
        RT_Panel01_02.localScale = Vector3.zero;
        RT_Panel02_02.localScale = Vector3.zero;

        // --- チュートリアル０３ ---
        // コンポーネント取得
        RT_Panel01_03 = GO_Panel01_03.GetComponent<RectTransform>();
        RT_Panel02_03 = GO_Panel02_03.GetComponent<RectTransform>();
        // パラメータ初期化
        RT_Panel01_03.localScale = Vector3.zero;
        RT_Panel02_03.localScale = Vector3.zero;

        // --- チュートリアル０４ ---
        // コンポーネント取得
        RT_Panel01_04 = GO_Panel01_04.GetComponent<RectTransform>();
        RT_Panel02_04 = GO_Panel02_04.GetComponent<RectTransform>();
        // パラメータ初期化
        RT_Panel01_04.localScale = Vector3.zero;
        RT_Panel02_04.localScale = Vector3.zero;
    }


    /*==============================================================

        衝突した瞬間
     
    ==============================================================*/
    private void OnTriggerEnter(Collider other)
    {
        // --- チュートリアル０１ ---
        if (other.CompareTag("Tutorial01"))
        {
            Destroy(other.gameObject);
            Titorial01();
        }

        // --- チュートリアル０２ ---
        if (other.CompareTag("Tutorial02"))
        {
            Destroy(other.gameObject);
            Titorial02();
        }

        // --- チュートリアル０３ ---
        if (other.CompareTag("Tutorial03"))
        {
            Destroy(other.gameObject);
            Titorial03();
        }

        // --- チュートリアル０４ ---
        if (other.CompareTag("Tutorial04"))
        {
            Destroy(other.gameObject);
            Titorial04();
        }
    }
}

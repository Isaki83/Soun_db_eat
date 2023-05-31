using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using PathCreation;

[ExecuteInEditMode]
public class NotesSetter : MonoBehaviour
{
    private bool fileCheck = false;

    [Header("・Spline")]
    [Tooltip("仮のスプライン")]
    public PathCreator VirtuallySpline;
    [Tooltip("生成する仮のオブジェクトを格納する空オブジェクト")]
    public GameObject VirtuallyHolder;
    [Tooltip("実際のスプライン")]
    public PathCreator ActuallySpline;
    [Tooltip("生成する実際のオブジェクトを格納する空オブジェクト")]
    public GameObject ActuallyHolder;
    private float distanceTravelled;    // スプライトの始点からの時間
    
    [Header("・Object")]
    [Tooltip("プレイヤーと同じ速さに合わせる。")]
    public float _ForwardSpeed = 7.5f;
    // --- ノーマルノーツ ---
    [Tooltip("設置したいオブジェクト")]
    public GameObject _NormalNotes;
    [HideInInspector]
    public List<float> _NormalNotesTime = new List<float>();
    [HideInInspector]
    public List<GameObject> _NormalNotesList = new List<GameObject>();
    [HideInInspector, Tooltip("リストから削除したいノーツの要素番号")]
    public int N_RemoveListNumber = 0;
    // --- ロングノーツ ---
    [Tooltip("設置したいオブジェクト")]
    public GameObject _LongNotes;
    [HideInInspector]
    public List<float> _LongNotesTime = new List<float>();
    [HideInInspector]
    public List<GameObject> _LongNotesList = new List<GameObject>();
    [HideInInspector, Tooltip("リストから削除したいノーツの要素番号")]
    public int L_RemoveListNumber = 0;
    private bool isSetting = false;     // 設置中フラグ
    private float SetTimeSpacing = 0.1f;
    public enum NotesType { Normal, Long };

    [Header("・Audio")]
    [Tooltip("プレイヤーの開始時の演出の時間に合わせる。")]
    private float _StartEffectTime = 7.5f;
    private float AudioTime = 0.0f;
    private float prev_AudioTime;
    private AudioSource _AudioSource;
    [SerializeField]
    private bool IsAudioPlay = false;       // オーディオ再生中か


    /*==============================================================

        開始

    ==============================================================*/
    void Start()
    {
        _AudioSource = GetComponent<AudioSource>();
    }


    /*==============================================================

        更新
 
    ==============================================================*/
     private void OnEnable()
    {
#if UNITY_EDITOR
        EditorApplication.update += EditorUpdate;
#endif
    }

    void OnDisable()
    {
#if UNITY_EDITOR
        EditorApplication.update -= EditorUpdate;
#endif
    }


    /*==============================================================

        更新
 
    ==============================================================*/
    void EditorUpdate()
    {
        #region ファイル null チェック
        if (VirtuallySpline == null)
        {
            Debug.Log("[Notes Setter]仮のスプラインが設定されていません。");
            fileCheck = true;
        }
        else { fileCheck = false; }
        if (VirtuallyHolder == null)
        {
            Debug.Log("[Notes Setter]仮のホルダーが設定されていません。");
            fileCheck = true;
        }
        else { fileCheck = false; }
        if (ActuallySpline == null)
        {
            Debug.Log("[Notes Setter]実際のスプラインが設定されていません。");
            fileCheck = true;
        }
        else { fileCheck = false; }
        if (ActuallyHolder == null)
        {
            Debug.Log("[Notes Setter]実際のホルダーが設定されていません。");
            fileCheck = true;
        }
        else { fileCheck = false; }
        if (_NormalNotes == null)
        {
            Debug.Log("[Notes Setter]ノーマルノーツが設定されていません。");
            fileCheck = true;
        }
        else { fileCheck = false; }
        if (_LongNotes == null)
        {
            Debug.Log("[Notes Setter]ロングノーツが設定されていません。");
            fileCheck = true;
        }
        else { fileCheck = false; }
        if (_AudioSource.clip == null)
        {
            Debug.Log("[Notes Setter]曲が設定されていません。");
            fileCheck = true;
        }
        else { fileCheck = false; }
        if (fileCheck) { return; }
        #endregion

        #region スプライン関係
        // 曲の時間とスピードからスプラインの位置を合わせる
        distanceTravelled = _ForwardSpeed * (AudioTime - _StartEffectTime);
        // スプラインの位置に合わせる
        Vector3 position = new Vector3(VirtuallySpline.path.GetPointAtDistance(distanceTravelled, EndOfPathInstruction.Stop).x,
            VirtuallySpline.path.GetPointAtDistance(distanceTravelled, EndOfPathInstruction.Stop).y + 1.5f,
            VirtuallySpline.path.GetPointAtDistance(distanceTravelled, EndOfPathInstruction.Stop).z);
        transform.position = position;
        // スプラインの角度に合わせる
        transform.rotation = VirtuallySpline.path.GetRotationAtDistance(distanceTravelled, EndOfPathInstruction.Stop);
        #endregion

        // ロングノーツ設置
        _SetNotes(NotesType.Long);

        if (_AudioSource.isPlaying) { AudioTime = _AudioSource.time; }
        IsAudioPlay = _AudioSource.isPlaying;
    }


    /*==============================================================

            オブジェクト配置
            設置した時間とオブジェクトをリストに追加

    ==============================================================*/
    private void _SetNotes(NotesType type)
    {
        switch (type)
        {
            case NotesType.Normal:
                if (VirtuallyHolder == null && _NormalNotes == null) { return; }
                _NormalNotesTime.Add(AudioTime);
                _NormalNotesList.Add(Instantiate(_NormalNotes, transform.position, transform.rotation, VirtuallyHolder.transform));
                break;

            case NotesType.Long:
                if (!isSetting || !IsAudioPlay) { return; }
                if (AudioTime - prev_AudioTime > SetTimeSpacing) { return; }
                _LongNotesTime.Add(AudioTime);
                _LongNotesList.Add(Instantiate(_LongNotes, transform.position, transform.rotation, VirtuallyHolder.transform));
                prev_AudioTime = AudioTime;
                break;
        }
    }


    #region Gizmos
#if UNITY_EDITOR
    /*==============================================================

        ギズモ
 
    ==============================================================*/
    // Draw the path when path objected is not selected (if enabled in settings)
    void OnDrawGizmos()
    {
        // 選択中のオブジェクト数を表示
        if(0 < Selection.gameObjects.Length)
        { Handles.Label(new Vector3(0.0f, 0.0f, 0.0f), $"【オブジェクト選択数】{Selection.gameObjects.Length}"); }
    }
#endif
    #endregion


    //-----------------------------------------------------------------------------------------------------
    //      インスペクターのボタン
    //-----------------------------------------------------------------------------------------------------
    /*==============================================================

        オブジェクト生成
 
    ==============================================================*/
    public void SetNotes(NotesType type)
    {
        // 自分の位置にオブジェクト生成
        switch (type)
        {
            case NotesType.Normal:
                _SetNotes(NotesType.Normal);
                break;

            case NotesType.Long:
                if (VirtuallyHolder == null && _LongNotes == null) { return; }
                isSetting = !isSetting;
                prev_AudioTime = AudioTime;
                break;
        }
    }


    /*==============================================================

        オーディオ再生
 
    ==============================================================*/
    public void AudioPlay()
    {
        _AudioSource.Play();
        _AudioSource.time = AudioTime;
    }


    /*==============================================================

        オーディオ停止
 
    ==============================================================*/
    public void AudioStop()
    {
        _AudioSource.Stop();
    }


    /*==============================================================

            指定したオブジェクトをリストとシーンから消去

    ==============================================================*/
    public void DeleteObject(NotesType type)
    {
        switch (type)
        {
            case NotesType.Normal:
                _NormalNotesTime.RemoveAt(N_RemoveListNumber);
                DestroyImmediate(_NormalNotesList[N_RemoveListNumber]);
                _NormalNotesList.RemoveAt(N_RemoveListNumber);
                break;

            case NotesType.Long:
                _LongNotesTime.RemoveAt(L_RemoveListNumber);
                DestroyImmediate(_LongNotesList[L_RemoveListNumber]);
                _LongNotesList.RemoveAt(L_RemoveListNumber);
                break;
        }
    }

    /*==============================================================

            リストの中身を全部消去

    ==============================================================*/
    public void ListClear()
    {
        _NormalNotesTime.Clear();
        _NormalNotesList.Clear();
        _LongNotesTime.Clear();
        _LongNotesList.Clear();
    }


    /*==============================================================

            リストに沿ってオブジェクトを設置

    ==============================================================*/
    public void setObject()
    {
        if (ActuallyHolder == null || _NormalNotes == null) { return; }

        for (int i = 0; i < _NormalNotesTime.Count; ++i)
        {
            float distanceTravelled = _ForwardSpeed * (_NormalNotesTime[i] - _StartEffectTime);

            // スプラインの位置、角度に合わせる
            Vector3 position = new Vector3(ActuallySpline.path.GetPointAtDistance(distanceTravelled, EndOfPathInstruction.Stop).x,
                ActuallySpline.path.GetPointAtDistance(distanceTravelled, EndOfPathInstruction.Stop).y + 0.5f,
                ActuallySpline.path.GetPointAtDistance(distanceTravelled, EndOfPathInstruction.Stop).z);
            Quaternion rotation = ActuallySpline.path.GetRotationAtDistance(distanceTravelled, EndOfPathInstruction.Stop);
            // 生成
            Instantiate(_NormalNotes, position, rotation, ActuallyHolder.transform);
        }
        for (int i = 0; i < _LongNotesTime.Count; ++i)
        {
            float distanceTravelled = _ForwardSpeed * (_LongNotesTime[i] - _StartEffectTime);

            // スプラインの位置、角度に合わせる
            Vector3 position = new Vector3(ActuallySpline.path.GetPointAtDistance(distanceTravelled, EndOfPathInstruction.Stop).x,
                ActuallySpline.path.GetPointAtDistance(distanceTravelled, EndOfPathInstruction.Stop).y + 0.5f,
                ActuallySpline.path.GetPointAtDistance(distanceTravelled, EndOfPathInstruction.Stop).z);
            Quaternion rotation = ActuallySpline.path.GetRotationAtDistance(distanceTravelled, EndOfPathInstruction.Stop);
            // 生成
            Instantiate(_LongNotes, position, rotation, ActuallyHolder.transform);
        }
    }
}
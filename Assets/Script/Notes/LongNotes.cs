using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SocialPlatforms;

public class LongNotes : Notes
{
    int HitNotesNum = 0;        // ロングノーツカウント用
    int prevHitNotesNum = 0;    // ↑の"ResetTime"秒前の値を格納
    float time = 0.0f;          // ノーツから離れてからの時間
    public float ResetTime;     // ノーツカウントをリセットするまでの時間 / 単位 : 秒

    
    /*==============================================================

        更新
     
    ==============================================================*/
    void Update()
    {
        // `ResetTime`秒に1回カウントをリセット
        time += Time.deltaTime * GManager.TimeScale;
        if (time >= ResetTime)
        {
            time = 0.0f;
            // `ResetTime`秒前とカウント数が変わっていなかったら
            if (HitNotesNum == prevHitNotesNum)
            {
                HitNotesNum = 0;
                Debug.Log("Reset Count"); // ログを表示
            }
            prevHitNotesNum = HitNotesNum;
        }
        Debug.Log("Count : " + HitNotesNum); // ログを表示
    }


    /*==============================================================

       更新

    ==============================================================*/
    private void FixedUpdate()
    {
        _InputPlayer.Player.Notes.performed += PressNotes;
        _InputPlayer.Player.Notes.canceled += ReleaseNotes;
    }


    /*==============================================================

       衝突した瞬間

    ==============================================================*/
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("LongNotes"))
        {
            // ColliBallの色変更
            ColliBallColor(EmissionColor.Hit);
        }
    }


    /*==============================================================

        衝突中
     
    ==============================================================*/
    void OnTriggerStay(Collider collision)
    {
        if (collision.CompareTag("LongNotes"))
        {
            LongNotesType notestype = collision.gameObject.GetComponent<LongNotesType>();

            // 衝突中のオブジェクト格納
            ColliObj = collision;
            // 衝突チェックon
            isNotesHit = true;

            // --- ノーツタイプが中間点 ---
            if (notestype.NotesType == LongNotesType.TypeList.Middle)
            {
                // 押している
                if (_InputPlayer.Player.Notes.IsPressed())
                {
                    Debug.Log("LNotesType.Middle : Hit"); // ログを表示
                    // ノーツ数カウント
                    HitNotesNum++;
                    // エフェクト再生
                    Effect(_LoadEffect[(int)Effects.Star]);
                    // ノーツ削除
                    Destroy(collision.gameObject);
                    // ColliBallの色変更
                    ColliBallColor(EmissionColor.Defalt);
                }
            }
        }

    }


    /*==============================================================

       衝突が外れた

    ==============================================================*/
    private void OnTriggerExit(Collider collision)
    {
        if (collision.CompareTag("LongNotes"))
        {

            LongNotesType notestype = collision.gameObject.GetComponent<LongNotesType>();

            // --- ノーツタイプが終点 ---
            if (notestype.NotesType == LongNotesType.TypeList.End)
            {
                Debug.Log("Exit_LNotes");
                // ColliBallの色変更
                ColliBallColor(EmissionColor.Defalt);
                // --- スピードダウン ---
                playerMove.SpeedUpDown(PlayerMove.SpeedUD.Down, 1.0f);
            }

            // 衝突チェックoff
            isNotesHit = false;
            // 衝突中のオブジェクト削除
            ColliObj = null;
        }
    }


    /*==============================================================

       ノーツを押したときの処理

    ==============================================================*/
    void PressNotes(InputAction.CallbackContext context)
    {
        if (!isNotesHit) { return; }
        if (ColliObj == null) { return; }

        LongNotesType notestype = ColliObj.gameObject.GetComponent<LongNotesType>();

        // --- ノーツタイプが始点 ---
        if (notestype.NotesType != LongNotesType.TypeList.Start) { return; }

        // カウントリセット
        HitNotesNum = 0;
        // ノーツ数カウント
        HitNotesNum++;
        // ノーツ削除
        Destroy(ColliObj.gameObject);
        // ColliBallの色変更
        ColliBallColor(EmissionColor.Defalt);

        // 衝突チェックoff
        isNotesHit = false;
        // 衝突中のオブジェクト削除
        ColliObj = null;
    }


    /*==============================================================

       ノーツを離したときの処理

    ==============================================================*/
    void ReleaseNotes(InputAction.CallbackContext context)
    {
        if (!isNotesHit) { return; }
        if (ColliObj == null) { return; }

        LongNotesType notestype = ColliObj.gameObject.GetComponent<LongNotesType>();

        // --- ノーツタイプが終点 ---
        if (notestype.NotesType != LongNotesType.TypeList.End) { return; }

        // エフェクト再生
        Effect(_LoadEffect[(int)Effects.Notes3]);
        // ノーツ数カウント
        HitNotesNum++;
        // ノーツ削除
        Destroy(ColliObj.gameObject);
        // ColliBallの色変更
        ColliBallColor(EmissionColor.Defalt);

        // --- スピードアップ ---
        // 全部拾ったら
        if (notestype.GetNotesNum() == HitNotesNum)
        {
            // 2段階アップ
            playerMove.SpeedUpDown(PlayerMove.SpeedUD.Up, 2.0f);
            Debug.Log("スピードUP 2段階"); // ログを表示
        }
        // 半分以上拾ったら
        if (notestype.GetNotesNum() / 2 <= HitNotesNum && HitNotesNum < notestype.GetNotesNum())
        {
            // 1段階アップ
            playerMove.SpeedUpDown(PlayerMove.SpeedUD.Up, 1.0f);
            Debug.Log("スピードUP 1段階"); // ログを表示
        }

        // 衝突チェックoff
        isNotesHit = false;
        // 衝突中のオブジェクト削除
        ColliObj = null;
    }
}
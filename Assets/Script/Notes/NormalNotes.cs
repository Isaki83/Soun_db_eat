using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class NormalNotes : Notes
{
    /*==============================================================

       更新

    ==============================================================*/
    private void FixedUpdate()
    {
        _InputPlayer.Player.Notes.performed += PressNotes;
    }


    /*==============================================================

       衝突した瞬間

    ==============================================================*/
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("NormalNotes"))
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
        if (collision.CompareTag("NormalNotes"))
        {
            Debug.Log("NNotes"); // ログを表示

            // 衝突中のオブジェクト格納
            ColliObj = collision;
            // 衝突チェックon
            isNotesHit = true;
        }
    }


    /*==============================================================

       衝突が外れた

    ==============================================================*/
    private void OnTriggerExit(Collider collision)
    {
        if (collision.CompareTag("NormalNotes"))
        {
            Debug.Log("Exit_NNotes");
            // ColliBallの色変更
            ColliBallColor(EmissionColor.Defalt);
            // 衝突チェックoff
            isNotesHit = false;
            // 衝突中のオブジェクト削除
            ColliObj = null;
            // --- スピードダウン ---
            playerMove.SpeedUpDown(PlayerMove.SpeedUD.Down, 1.0f);
        }
    }


    /*==============================================================

       ノーツを押したときの処理

    ==============================================================*/
    void PressNotes(InputAction.CallbackContext context)
    {
        if (!isNotesHit) { return; }
        if (ColliObj == null) { return; }

        // エフェクト再生
        Effect(_LoadEffect[(int)Effects.Notes3]);
        // --- スピードアップ ---
        playerMove.SpeedUpDown(PlayerMove.SpeedUD.Up, 2.0f);
        // ノーツ削除
        Destroy(ColliObj.gameObject);
        // ColliBallの色変更
        ColliBallColor(EmissionColor.Defalt);

        // 衝突チェックoff
        isNotesHit = false;
        // 衝突中のオブジェクト削除
        ColliObj = null;
        Debug.Log("NNotes : Hit"); // ログを表示
    }
}

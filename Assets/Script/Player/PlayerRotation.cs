using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    // Input Action
    private InputPlsyer _InputPlayer;
    // 現在の時間
    private float CurrentTime = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        // インスタンス生成
        _InputPlayer = new InputPlsyer();
    }

    // Update is called once per frame
    void Update()
    {
        // 経過時間を返す
        CurrentTime += Time.deltaTime * GManager.TimeScale;

        // Dキー, RightStick（右移動）
        if (_InputPlayer.Player.RightMove.IsPressed())
        {
            //右に移動
            
            transform.Rotate(0f, 0f, 120.0f * Time.deltaTime * GManager.TimeScale);
        }
        // Aキー, LeftStick（左移動）
        if (_InputPlayer.Player.LeftMove.IsPressed())
        {
            //左に移動
            transform.Rotate(0f, 0f, -120.0f * Time.deltaTime * GManager.TimeScale);
        }
    }
}

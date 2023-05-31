using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SpeedDown : MonoBehaviour
{
    private PlayerMove playermove;

    //Start is called before the first frame update
    void Start()
    {
        playermove = GameObject.FindWithTag("Player").GetComponent<PlayerMove>();
    }

    //Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        //プレイヤータグと接触したら
        if (collision.gameObject.tag == "Player")
        {
            // ログを表示
            Debug.Log("FalseLine");

            // スピードDown
            playermove.SpeedUpDown(PlayerMove.SpeedUD.Down, 1.0f);
        }
    }
}

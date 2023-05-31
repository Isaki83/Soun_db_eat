using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BPMValue : MonoBehaviour
{
    // PlayerMove.csの呼び出し
    public GameObject player;
    private PlayerMove playerMove;
    private AudioSource audioSource;
    Text bpmValObj;
    int bpmValNum;

    // Start is called before the first frame update
    void Start()
    {
        // コンポーネント取得
        playerMove = player.GetComponent<PlayerMove>();
        audioSource = player.GetComponent<AudioSource>();
        bpmValObj = GetComponent<Text>();
        bpmValNum = UniBpmAnalyzer.AnalyzeBpm(audioSource.clip);
    }

    // Update is called once per frame
    void Update()
    {
        bpmValObj.text = ((int)(bpmValNum * playerMove.AudioPitch)).ToString();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BPMAnim : MonoBehaviour
{
    private Animator anim = null;
    int tempo;

    public enum BPM_UD
    {
        up, down
    }

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        tempo = 3;
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetInteger("Tempo", tempo);
    }

    public void SetAnimBPM(BPM_UD ud)
    {
        Debug.Log("BPM"); // ログを表示
        switch (ud)
        {
            case BPM_UD.up:
                tempo++;
                break;

            case BPM_UD.down:
                tempo--;
                break;
        }
    }
}

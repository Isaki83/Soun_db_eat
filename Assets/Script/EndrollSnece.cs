using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class EndrollSnece : MonoBehaviour
{
    private float CurrentTime = 0.0f;
    private float StartEffectTime = 3.5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnClickEndrollButton()
    {
        SceneManager.LoadScene("EndRollScene");  //シーン遷移
    }

}

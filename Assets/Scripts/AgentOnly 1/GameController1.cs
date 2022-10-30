using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using System.Linq;
using UnityEngine.SceneManagement;

public class GameController1 : MonoBehaviour
{
    public bool gameStart;
    private Text dText;
    private GameObject agent;
    public bool agentHasBall;
    //最初地面にボールが接触するところの制御
    //(もともとはPCRelation.csないにあったが、
    //射出時生成法に変えたことで生成ごとに最初地面にボールが接触した状態で始まってしまう)
    public bool firstState;

    private float tran_time;

    // Start is called before the first frame update
    void Start()
    {
        dText = GameObject.FindGameObjectWithTag("Text").GetComponent<Text>();
        agent = GameObject.FindGameObjectWithTag("Agent");
        init();
        //Line();
    }

    void init() 
    {
        gameStart = true;
        //gameStart = false;
        agentHasBall = false;
        firstState = true;
        tran_time = 0f;
    }
    // Update is called once per frame
    void Update()
    {
        //LineRendering();
        if (GameObject.FindGameObjectsWithTag("Obj").Count() == 0 || Time.time > 240)
        {
            dText.text = "Day2 : End";
            tran_time += Time.deltaTime;
            if (tran_time > 3.0f)
            {
                SceneManager.LoadScene(2);
            }
        } 
        if ((OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch) || OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.LTouch)) && !gameStart)
        {
            gameStart = true;
        }
        dText.text = "Day2";
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using System.Linq;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public bool gameStart;
    private Text dText;
    private GameObject agent;
    public GameObject effect;

    public bool agentTurn;
    public bool agentHasBall;
    public bool playerHasBall;
    //最初地面にボールが接触するところの制御
    //(もともとはPCRelation.csないにあったが、
    //射出時生成法に変えたことで生成ごとに最初地面にボールが接触した状態で始まってしまう)
    public bool firstState;

    private float trans_time;

    private bool clear;

    private float[] ttime = new float[4];

    // Start is called before the first frame update
    void Start()
    {
        dText = GameObject.FindGameObjectWithTag("Text").GetComponent<Text>();
        agent = GameObject.FindGameObjectWithTag("Agent");
        ttime[0] = 120.0f;
        ttime[1] = 240.0f;
        ttime[2] = 300.0f;
        ttime[3] = 420.0f;
        init();
        //Line();
    }

    void init() 
    {
        //gameStart = true;
        gameStart = false;
        agentHasBall = false;
        playerHasBall = false;
        firstState = true;
        trans_time = 0.0f;
        clear = false;
    }
    // Update is called once per frame
    void Update()
    {
        
        //LineRendering();
        if (GameObject.FindGameObjectsWithTag("Obj").Count() == 0 || Time.time > ttime[PlayerPrefs.GetInt("State")])
        {
            if (PlayerPrefs.GetInt("State") == 0)
            {
                dText.text = "Day 1 : End";
                trans_time += Time.deltaTime;
                if (trans_time > 3.0f)
                {
                    SceneManager.LoadScene(0);
                    PlayerPrefs.SetInt("State", 1);
                    PlayerPrefs.Save();
                }
            }
            else if (PlayerPrefs.GetInt("State") == 1)
            {
                dText.text = "Day 2 : End";
                trans_time += Time.deltaTime;
                if (trans_time > 3.0f)
                {
                    SceneManager.LoadScene(0);
                    PlayerPrefs.SetInt("State", 2);
                    PlayerPrefs.Save();
                }
            }
            else if (PlayerPrefs.GetInt("State") == 2)
            {
                dText.text = "Day 3 : End";
                trans_time += Time.deltaTime;
                if (trans_time > 3.0f)
                {
                    SceneManager.LoadScene(0);
                    PlayerPrefs.SetInt("State", 3);
                    PlayerPrefs.Save();
                }
            }
            else if (PlayerPrefs.GetInt("State") == 3)
            {
                dText.text = "Game Finished!";
                trans_time += Time.deltaTime;
                if(!clear)
                {
                    clear = true;
                    GameObject go = Instantiate(effect) as GameObject;
                    go.transform.position = agent.transform.position;
                }
                if (trans_time > 3.0f)
                {
                    PlayerPrefs.SetInt("State", 0);
                    PlayerPrefs.Save();
                    UnityEngine.Application.Quit();
                }
            }
        } 
        else
        {
            if (PlayerPrefs.GetInt("State") == 0)
            {
                dText.text = "Day 1";
            }
            else if (PlayerPrefs.GetInt("State") == 1)
            {
                dText.text = "Day 2";
            }
            else if (PlayerPrefs.GetInt("State") == 2)
            {
                dText.text = "Day 3";
            }
            else if (PlayerPrefs.GetInt("State") == 3)
            {
                dText.text = "Day 4";
            }
        }
        if ((OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch) || OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.LTouch)) && !gameStart)
        {
            gameStart = true;
            agentTurn = true;
        }
    }
}

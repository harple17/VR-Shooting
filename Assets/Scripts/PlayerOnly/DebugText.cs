using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugText2 : MonoBehaviour
{

    private GameObject gameManager;
    public Text dText;
    private GameObject ball;
    private GameObject agent;
    private GameObject contR;

    // Start is called before the first frame update
    void Start()
    {
        dText = GameObject.Find("Text").GetComponent<Text>();
        gameManager = GameObject.FindGameObjectWithTag("GameController");
        ball = GameObject.FindGameObjectWithTag("Ball");
        //contR = GameObject.Find("RightCollider");
    }

    // Update is called once per frame
    void Update()
    {
#if true
        dText.text = 
        //" agentHasBall:" + gameManager.GetComponent<GameController>().agentHasBall.ToString() +
        " Player Turn:" + (!gameManager.GetComponent<GameController>().agentTurn).ToString();
        //" Player Touched" + ball.GetComponent<PCRelation>().playerTouched.ToString()+
        //"fs:" + gameManager.GetComponent<GameController>().firstState.ToString();
        //"debug:" + agent.GetComponent<AgentController>().shootOK.ToString();
        //"targetNum:" + agent.GetComponent<AgentController>().targetNum.ToString();
#else
        if (!gameManager.GetComponent<GameController>().gameStart)
        {
            dText.text = "Pull the left or right trigger to Start";
        } else
        {
            dText.text = "";
        }
#endif
    }
}

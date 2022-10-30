using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnController : MonoBehaviour
{
    private GameObject gameManager;
    public bool notDestroyed;
    public GameObject effect;
    private GameObject agent;
    // Start is called before the first frame update
    void Start()
    {
        agent = GameObject.FindWithTag("Agent");
        gameManager = GameObject.FindWithTag("GameController");
        notDestroyed = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball") && gameManager.GetComponent<GameController>().agentTurn)
        {
            gameManager.GetComponent<GameController>().agentTurn = agent.GetComponent<AgentController>().shootPos = false;
            gameManager.GetComponent<GameController>().firstState = false;
            if (notDestroyed)
            {
                GameObject sadEffect = GameObject.Instantiate(effect) as GameObject;
                sadEffect.transform.position = agent.transform.position;
            }
            notDestroyed = true;
        }
        else if (collision.gameObject.CompareTag("Ball") && (!gameManager.GetComponent<GameController>().agentTurn) && (!gameManager.GetComponent<GameController>().firstState))
        {
            gameManager.GetComponent<GameController>().agentTurn = true;
            gameManager.GetComponent<GameController>().agentHasBall = agent.GetComponent<AgentController>().shootPos = false;
            if (notDestroyed)
            {
                GameObject sadEffect = GameObject.Instantiate(effect) as GameObject;
                sadEffect.transform.position = agent.transform.position;
            }
            notDestroyed = true;
        }
    }
}

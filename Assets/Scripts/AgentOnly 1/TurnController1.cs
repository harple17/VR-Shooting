using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnController1 : MonoBehaviour
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
        if (collision.gameObject.CompareTag("Ball"))
        {
            agent.GetComponent<AgentContOnly>().shootPos = false;
            gameManager.GetComponent<GameController1>().firstState = false;
            if (notDestroyed)
            {
                GameObject sadEffect = GameObject.Instantiate(effect) as GameObject;
                sadEffect.transform.position = agent.transform.position;
            }
            notDestroyed = true;
        }
        else if (collision.gameObject.CompareTag("Ball") && (!gameManager.GetComponent<GameController1>().firstState))
        {
            gameManager.GetComponent<GameController1>().agentHasBall = agent.GetComponent<AgentContOnly>().shootPos = false;
            if (notDestroyed)
            {
                GameObject sadEffect = GameObject.Instantiate(effect) as GameObject;
                sadEffect.transform.position = agent.transform.position;
            }
            notDestroyed = true;
        }
    }
}

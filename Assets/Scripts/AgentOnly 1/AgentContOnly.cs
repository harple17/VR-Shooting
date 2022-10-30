using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class AgentContOnly : MonoBehaviour
{
    private const int NUM_OBJ = 3;
    private const float th = 0.1f;
    private GameObject ball;
    private NavMeshAgent agent;
    private GameObject gameManager;

    private Animator anim;
    private float velocity;
    private Vector3 dir;
    private bool targetSet;

    public bool shootPos;
    public int targetNum;
    public bool shootOK;
    private int frameN;

    private GameObject[] obj = new GameObject[NUM_OBJ];
    private float[] objX = new float[NUM_OBJ];
    private float[] objY = new float[NUM_OBJ];


    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < NUM_OBJ; i++)
        {
            obj[i] = GameObject.Find("Target" + (i + 1).ToString());
            objX[i] = obj[i].transform.position.x;
            objY[i] = obj[i].transform.position.y;
        }
        agent = GetComponent<NavMeshAgent>();
        ball = GameObject.FindGameObjectWithTag("Ball");
        gameManager = GameObject.FindGameObjectWithTag("GameController");
        anim = GetComponent<Animator>();
        dir = transform.position;
        targetSet = false;
        shootOK = false;
        frameN = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.GetComponent<GameController1>().gameStart)
        {
            ball = GameObject.FindGameObjectWithTag("Ball");
            velocity = ((transform.position - dir).magnitude) / Time.deltaTime;
            dir = transform.position;

            if (gameManager.GetComponent<GameController1>().agentHasBall)
            {
                agent.SetDestination(new Vector3(0, 0, 0));
                if (transform.position.x * transform.position.x + transform.position.z * transform.position.z <= th * th)
                {
                    gameObject.transform.LookAt(new Vector3(0, 0, 1));
                    shootPos = true;
                    if (frameN > 40)
                    {
                        shootOK = true;
                        frameN = 0;
                    }
                    if (!targetSet)
                    {
                        for (int i = 0; i < NUM_OBJ; i++) 
                        {
                            if (obj[i] != null)
                            {
                                targetNum = i;
                                break;
                            }
                        }
                        targetSet = true;
                    }
                    frameN++;
                }
            }
            else if (!gameManager.GetComponent<GameController1>().agentHasBall)
            {
                shootPos = false;
                if (targetSet)
                {
                    targetSet = !targetSet;
                }
                agent.SetDestination(ball.transform.position);
            }
            if (shootOK)
            {
                float forcemag = 22.0f;
                Vector3 forcedir = obj[targetNum].transform.position - agent.transform.position;
                forcedir += addNoise();
                ball.GetComponent<Rigidbody>().useGravity = true;
                ball.GetComponent<Rigidbody>().isKinematic = false;
                ball.transform.parent = null;
                ball.GetComponent<Rigidbody>().AddForce(forcemag * forcedir / forcedir.magnitude, ForceMode.Impulse);
                gameManager.GetComponent<GameController1>().agentHasBall = shootOK = false;
            }
            anim.SetFloat("charaSpeed", velocity);
        } 
    }

    Vector3 addNoise()
    {
        System.Random r = new System.Random();
        double min = -2;
        double max = 2;
        return new Vector3((float)(r.NextDouble() * (max - min) + min), 0, (float)(r.NextDouble() * (max - min) + min));
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ball")
        {
            ball = GameObject.FindGameObjectWithTag("Ball");
            if (!agent.GetComponent<AgentContOnly>().shootPos)
            {
                ball.GetComponent<Rigidbody>().useGravity = false;
                ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
                ball.transform.position = new Vector3(agent.transform.position.x, 0.93f, agent.transform.position.z);
                ball.transform.parent = gameObject.transform;
                gameManager.GetComponent<GameController1>().agentHasBall = true;
            }
        }
    }
}

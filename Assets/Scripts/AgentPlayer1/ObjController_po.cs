using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjController_po : MonoBehaviour
{
    private bool clded;
    private int _tod;
    private GameObject gameManager;
    private GameObject agent;

    public GameObject effect1;
    public GameObject effect_destroy;
    private GameObject plane;

    // Start is called before the first frame update
    void Start()
    {
        clded = false;
        gameManager = GameObject.FindGameObjectWithTag("GameController");
        agent = GameObject.FindGameObjectWithTag("Agent");
        plane = GameObject.FindGameObjectWithTag("Plane");
    }

    // Update is called once per frame
    void Update()
    {
        if (clded)
        {
            _tod++;
            if (_tod > 40 && clded)
            {
                GameObject obj_destroy = GameObject.Instantiate(effect_destroy) as GameObject;
                obj_destroy.transform.position = transform.position;
                Destroy(gameObject);
                GameObject obj_reaction = GameObject.Instantiate(effect1) as GameObject;
                obj_reaction.transform.position = new Vector3(agent.transform.position.x, agent.transform.position.y + 1.0f, agent.transform.position.z);
                clded = false;
            }
        } 
        else
        {
            gameObject.GetComponent<Rigidbody>().isKinematic = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ball" && agent.GetComponent<AgentController>().objDestroy && gameManager.GetComponent<GameController>().agentTurn)
        {
            gameObject.GetComponent<Rigidbody>().isKinematic = false;
            clded = true;
            plane.GetComponent<TurnController>().notDestroyed = false;
        }
    }
}

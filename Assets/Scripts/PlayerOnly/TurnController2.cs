using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnController2 : MonoBehaviour
{
    private GameObject gameManager;
    public GameObject ball;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindWithTag("GameController");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            Destroy(GameObject.FindGameObjectWithTag("Ball"));
            gameManager.GetComponent<GameController2>().firstState = false;
            GameObject go = Instantiate(ball) as GameObject;
            go.transform.position = new Vector3(0f, 0.5f, -2.5f);
        }
    }
}

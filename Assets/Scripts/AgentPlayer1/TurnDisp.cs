using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnDisp : MonoBehaviour
{

    public Material mat1;
    public Material mat2;
    private GameObject gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameController");
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.GetComponent<GameController>().agentTurn)
        {
            GetComponent<Renderer>().material = mat1;
        }
        else
        {
            GetComponent<Renderer>().material = mat2;
        }
    }
}

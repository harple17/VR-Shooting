using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using System.Linq;

public class BlockBaz : MonoBehaviour
{
    private const int NUM_OBJ = 3;
    private GameObject[] obj = new GameObject[NUM_OBJ];
    private GameObject gameManager;
    private float[] objX = new float[NUM_OBJ];
    private float[] objY = new float[NUM_OBJ];
    public int target;
    private const float wallZPos = 10.0f;
    public bool playerTouched;

    public OVRInput.Controller controller;
    private GameObject go;
    void Fire(Vector3 startPos, GameObject obj_b)
    {
        Vector3 dir = transform.forward;
        Vector3 posCross = (wallZPos - transform.position.z) * dir / dir.z;
        float nearPos = 1.8f;
        float forcemag = 14.0f;
            for (target = 0; target < NUM_OBJ; target++)
        {
            if (System.Math.Abs(objX[target] - posCross.x) <= nearPos && System.Math.Abs(objY[target] - posCross.y) <= nearPos  +1.0f)
            {
                break;
            }
        }
        if (!ReferenceEquals(obj[target], null))
        {
            dir = obj[target].transform.position - startPos;
            dir.y += 4.0f;
        }
        go = Instantiate(obj_b) as GameObject;
        go.transform.position = startPos;
        go.GetComponent<Rigidbody>().AddForce(forcemag * dir / dir.magnitude, ForceMode.Impulse);
    }
    public GameObject blkObject;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < NUM_OBJ; i++)
        {
            obj[i] = GameObject.Find("Target" + (i + 1).ToString());
            objX[i] = obj[i].transform.position.x;
            objY[i] = obj[i].transform.position.y;
        }
        gameManager = GameObject.FindWithTag("GameController");
        playerTouched = false;
        go = GameObject.FindGameObjectWithTag("Ball");
        //Line();
    }

    // Update is called once per frame
    void Update()
    {
        //LineRendering();
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, controller) && gameManager.GetComponent<GameController>().playerHasBall)
        {
            Destroy(go);
            Fire(transform.position + new Vector3(0, 0.2f, 0.2f), blkObject);
            playerTouched = false;
            gameManager.GetComponent<GameController>().playerHasBall = false;
            gameManager.GetComponent<GameController>().agentTurn = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ball")
        {
            go = GameObject.FindGameObjectWithTag("Ball");
            go.transform.parent = gameObject.transform;
            go.GetComponent<Rigidbody>().isKinematic = true;
            playerTouched = true;
            gameManager.GetComponent<GameController>().agentHasBall = false;
            gameManager.GetComponent<GameController>().playerHasBall = true;
        }
    }
}

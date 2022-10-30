using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using System.Linq;

public class BlockBaz2 : MonoBehaviour
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
    GameObject go;
    Vector3 assistDir(Vector3 start)
    {
        Vector3 ret = transform.forward;
        Vector3 pos = transform.position;
        Vector3 posCross = (wallZPos - pos.z) * ret / ret.z;

        float nearPos = 2f;

        for (target = 0; target < NUM_OBJ; target++)
        {
            if (System.Math.Abs(objX[target] - posCross.x) <= nearPos && System.Math.Abs(objY[target] - posCross.y) <= nearPos)
            {
                break;
            }
        }
        if (!ReferenceEquals(obj[target], null))
        {
            ret = obj[target].transform.position - start;
            ret.y += 4.0f;
        }
        return ret;
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
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, controller) && gameManager.GetComponent<GameController2>().playerHasBall)
        {
            Destroy(go);
            Fire(transform.position + new Vector3(0, 0.2f, 0.2f), blkObject);
            playerTouched = false;
            gameManager.GetComponent<GameController2>().playerHasBall = false;
        }
        go = GameObject.FindGameObjectWithTag("Ball");
    }

    void Fire(Vector3 startPos, GameObject obj_b)
    {
        Vector3 dir = assistDir(startPos);
        float forcemag = 14f;
        go = Instantiate(obj_b) as GameObject;
        go.transform.position = startPos;
        go.GetComponent<Rigidbody>().isKinematic = false;
        go.GetComponent<Rigidbody>().AddForce(forcemag * dir / dir.magnitude, ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ball")
        {
            go.transform.parent = gameObject.transform;
            go.GetComponent<Rigidbody>().isKinematic = true;
            playerTouched = true;
            gameManager.GetComponent<GameController2>().playerHasBall = true;
        }
    }
}

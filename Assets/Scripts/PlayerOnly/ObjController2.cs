using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjController2 : MonoBehaviour
{
    private bool clded;
    private int _tod;
    private GameObject gameManager;

    public GameObject effect_destroy;

    // Start is called before the first frame update
    void Start()
    {
        clded = false;
        gameManager = GameObject.FindGameObjectWithTag("GameController");
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
        if (collision.gameObject.tag == "Ball")
        {
            gameObject.GetComponent<Rigidbody>().isKinematic = false;
            clded = true;
        }
    }
}

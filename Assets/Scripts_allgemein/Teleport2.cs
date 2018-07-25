using UnityEngine;
using System.Collections;

public class Teleport2 : MonoBehaviour
{
    public GameObject ui2;
    public GameObject objToTP2;
    public Transform tpLoc2;
    void Start()
     {
         ui2.SetActive(false);
     }

     void OnTriggerStay(Collider other)
     {
         ui2.SetActive(true);
         if ((other.gameObject.tag == "Player") && Input.GetButtonDown("Fire1"))
         {
             objToTP2.transform.position = tpLoc2.transform.position;
         }
     }
     void OnTriggerExit()
     {
         ui2.SetActive(false);
     }
    /*void Update()
    {
        if (Input.GetKeyDown("Fire1"))
        {
            objToTP2.transform.position = tpLoc2.transform.position;
        }
    }*/
}


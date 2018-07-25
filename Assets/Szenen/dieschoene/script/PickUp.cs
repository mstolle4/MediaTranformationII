using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    float throwForce = 600;          //variable, "600" gibt die weite des werfens an
    Vector3 objectPos;
    float distance;                   //distance des spielers, aufheben des objekts

    public bool canHold = true;
    public GameObject item;                 //Item z.B das Model : der verzauberte Spiegel (spiegel2),
    public GameObject tempParent;              //temParent : Gameobject welches z.B. der MainCamera untergeordnet ist
    public bool isHolding = false;


    void Update () 
    { 
      

        distance = Vector3.Distance(item.transform.position, tempParent.transform.position);
        if(distance >= 1f)
        {
            isHolding = false;
        }
       
        //Prüfen ob isholding
        if (isHolding==true)
        {
            item.GetComponent<Rigidbody>().velocity = Vector3.zero;                        //Objekt soll den Wert des angegeben Rigidbody annehmen,
            item.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            item.transform.SetParent(tempParent.transform);

            if(Input.GetMouseButtonDown(1))            //werfen
            {
                item.GetComponent<Rigidbody>().AddForce(tempParent.transform.forward * throwForce);
                isHolding = false;
            }
        }
        else
        {
            objectPos = item.transform.position;
            item.transform.SetParent(null);
            item.GetComponent<Rigidbody>().useGravity = true;
            item.transform.position = objectPos;
                }
    }
    void OnMouseDown()          //anstatt der Funktion OnMouseDown,kann man auch: OnMouseOver und dann eine
                                // additional line hinzufügen mit der if bedingung = if(Input.GetKeyDown(KeyCode.E)), dann funktioniert es über taste e                  
    {
        if (distance <= 1f)
        {
            isHolding = true;
            item.GetComponent<Rigidbody>().useGravity = false;                
            item.GetComponent<Rigidbody>().detectCollisions = true;
        }
    }
        void OnMouseUp()    // Objekt fallen lassen
        {
            isHolding = false;
        }
    }



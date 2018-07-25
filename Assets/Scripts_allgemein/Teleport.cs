using UnityEngine;
using System.Collections;

public class Teleport : MonoBehaviour
{
    public GameObject ui;
    public GameObject objToTP;
    public Transform tpLoc;
   void Start()
    {
        ui.SetActive(false);
    }
    /*Das Skript teleportiert den Spieler ins Badezimmer
         * */
    void OnTriggerStay(Collider other)
    {
        ui.SetActive(true);
        if ((other.gameObject.tag == "Player")/* && Input.GetKeyDown(KeyCode.E)*/)
        {
            StartCoroutine(CoFunktion());
        }
    }

    void OnTriggerExit()
    {
        ui.SetActive(false);
    }
    IEnumerator CoFunktion()
    {
        /*Das Skript lädt den Raum nach Abschluss neu
         * */
        yield return new WaitForSeconds(1);
        objToTP.transform.position = tpLoc.transform.position;
    }
}
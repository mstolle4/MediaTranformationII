using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Videotrigger : MonoBehaviour {

    public GameObject videoPlayer;
    public int timeToStop;   //"integer" damit das Video nach der angegeben Zeit aufhört abzuspielen z.b 45 sec


    // Use this for initialization
    void Start () {
        videoPlayer.SetActive(false); //Bedingung auf false, damit es nicht spielt

	}
	
	// Update is called once per frame
	void OnTriggerEnter (Collider player) {
        if (player.gameObject.tag == "Player")              //"Player" , der spieler, der den trigger überschreiten soll, 
        {                                                   // wichtig dass der Character, z.B Rigidbody Controller mit dem tag "Player" deklariert wird
            videoPlayer.SetActive(true);
            Destroy(videoPlayer, timeToStop);              // Nach dem das Video abgespielt wurde, "zerstört"sich der videoplayer,-- videosequenz beendet, zurück in szene
       
            

            /*GameObject[] gameObjectArray = GameObject.FindGameObjectsWithTag("Hintergrundmusik");      //wichtig jeweilige Musikspur mit tag "hintergrundmusik" deklarieren

            foreach (GameObject go in gameObjectArray)               // schaltet die Hintergrundmusik im Raum aus
            {
                go.SetActive(false);
            }*/
            

        }


    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class PlayVideo1 : MonoBehaviour
{

    public GameObject videoPlayer;
    public int timeToStop;
    public VideoPlayer myVideoPlayer;
    //public bool fertig=false;
    //public AudioSource[] _audioSource;

    //AudioSource audioSource;
    //public AudioClip otherClip;
    //public AudioClip alterClip;

    // Use this for initialization
    void Start()
    {
        videoPlayer.SetActive(false);
        //audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider player)
    {

        if (player.gameObject.tag == "Player")
        {
            /*Das Skript schaltet die Hintergrundmusik im Raum aus, damit die Beamerprojektion funktionieren kann
         * */

            GameObject[] gameObjectArray = GameObject.FindGameObjectsWithTag("Hintergrundmusik");

            foreach (GameObject go in gameObjectArray)
            {
                go.SetActive(false);
            }
            videoPlayer.SetActive(true);
            Destroy(videoPlayer, timeToStop);

           
        }
       

        /*if(!myVideoPlayer.isPlaying){
            GameObject[] gameObjectArray = GameObject.FindGameObjectsWithTag("Hintergrundmusik");

            foreach (GameObject go in gameObjectArray)
            {
                go.SetActive(true);
            }
        }*/


            

        
    }

}
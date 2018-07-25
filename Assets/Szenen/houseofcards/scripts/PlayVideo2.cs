using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class PlayVideo2 : MonoBehaviour
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
            
            videoPlayer.SetActive(true);
            Destroy(videoPlayer, timeToStop);

        }
    }

}
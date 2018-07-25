using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class Strudel : MonoBehaviour {

    public VideoPlayer myVideoPlayer;

    // Use this for initialization
    void Start () {

        myVideoPlayer.Play();

	}
	
	// Update is called once per frame
	void Update () {
        if (!myVideoPlayer.isPlaying)

        {
            // nächste szene

            // SceneManager.Loadscene(SceneManager.GetActiveScene().buildIndex + 1);
            SceneManager.LoadScene("dieschoene");
        }
        
	}
}





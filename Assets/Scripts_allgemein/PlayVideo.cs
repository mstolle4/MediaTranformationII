using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayVideo : MonoBehaviour {

    public GameObject videoPlayer;
    public int timeToStop;

    AudioSource audioSource;
    //AudioSource meinequelle;
    public AudioClip otherClip;
    public AudioClip alterClip;

    // Use this for initialization
    void Start () {
        audioSource = GetComponent<AudioSource>();
        videoPlayer.SetActive(false);
        Debug.Log("start");
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider player)
    {

        if (player.gameObject.tag == "Player")
        {
            /*Das Skript schaltet alle Lichter im Raum aus, damit die Beamerprojektion korrekt funktionieren kann
         * */
            Lightswitch.Trial();


            /*Das Skript schaltet die Hintergrundmusik im Raum aus, damit die Beamerprojektion funktionieren kann
         * */

            GameObject[] gameObjectArray = GameObject.FindGameObjectsWithTag("Hintergrundmusik");

            foreach (GameObject go in gameObjectArray)
            {
                go.SetActive(false);
            }
            AudioSource audio = GetComponent<AudioSource>();
            /*Das Skript setzt alle Lichter im Raum aus, damit die Beamerprojektion korrekt funktionieren kann
         * */
            audio.Play();
            new WaitForSeconds(audio.clip.length);
            audio.clip = otherClip;
            audio.Play();
            /*if (Input.GetKeyDown(KeyCode.F))
            {
                
                Debug.Log("f gedrueckt");
            }*/
            

            //audioSource.mute = !audioSource.mute;
            videoPlayer.SetActive(true);
            Destroy(videoPlayer, timeToStop);
            StartCoroutine(CoFunc());
            Debug.Log("video");
            /*if (GameObject.Find("Video Player") == null)
            {
                AudioSource audio2 = GetComponent<AudioSource>();
                audio2.clip = alterClip;
                audio2.Play();
            }*/
            if (GameObject.Find("Meintestobject") != null)
            {
                Debug.Log("trigger");
                AudioSource audio2 = GetComponent<AudioSource>();
                audio2.clip = alterClip;
                audio2.Play();
            }

        }
    }
    IEnumerator CoFunc()
    {
        /*Das Skript lädt den Raum nach Abschluss neu
         * */
        yield return new WaitForSeconds(timeToStop);
        SceneManager.LoadScene("zimmer1408");
    }
}


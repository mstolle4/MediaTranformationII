using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using System.Collections;
using System.Collections.Generic;

public class HoCDVDAnimation : MonoBehaviour {

    public GameObject AnimierteDVD;
    public GameObject Strudelvideo;
    public VideoPlayer Strudel;
    public int timeToStopStrudel;
    public GameObject objToTP;
    public Transform neuePosition;
    

    private void OnTriggerEnter(Collider other)
    {
        PlayableDirector pd = AnimierteDVD.GetComponent<PlayableDirector>();

        if (pd != null)
        {
            pd.Play();
            Dvdanimation.SetDvdAuswahl(2);
            /*1408 ist Zimmer 0
             * Dieschone ist Zimmer 1
             * House of Cards ist Zimmer 2
             * EntertheVoid ist Zimmer 3*/
        }

    }

    void Start()
    {

    }

    void Update()
    {
        
        if (Dvdanimation.GetAusgewaehlteDVD() == 2)
        {

            if (Input.GetButtonDown("Fire1"))
            {
                GameObject[] gameObjectArray = GameObject.FindGameObjectsWithTag("Hintergrundmusik");
                /*Das Skript transportiert den User in den schwarzen Raum und ändert die Skybox zu schwarz
         * */
                foreach (GameObject go in gameObjectArray)
                {
                    go.SetActive(false);
                }
                
                    objToTP.transform.position = neuePosition.transform.position;
                /*setzt Player in den schwarzen Raum*/
                Strudel.Play();
                Destroy(Strudel, timeToStopStrudel);
                StartCoroutine(CoFunktion());
            }
        }
    }
    IEnumerator CoFunktion()
    {
        /*Das Skript lädt den Raum nach Abschluss neu
         * */
        yield return new WaitForSeconds(timeToStopStrudel);
        SceneManager.LoadScene("houseofcards");
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lightswitch : MonoBehaviour
{

    /*

    //public Light sun1;
    //GameObject[] sun = GameObject.FindGameObjectsWithTag("Licht");
    public Material skyboxDay;
    public Material skyboxNight;

    public GameObject sign;
    public Texture commandment1;
    public Texture commandment2;

    public Color fullLight;
    public Color fullDark;

    public Light[] zimmerlichter;*/
    static public bool lightOn = true;
    public static bool night;


    // Use this for initialization
    void Start()
    {
        //
    

    GameObject[] sun = GameObject.FindGameObjectsWithTag("Licht");
        //sun.enabled = true;
        /*Das Skript schalltet alle Objekte im Raum auf inaktiv, 
         * welche den User an der Bewegung hindern könnten
         * */
        foreach (GameObject i in sun){
        i.SetActive(true);
        }
        GameObject[] entfernen = GameObject.FindGameObjectsWithTag("couch");
        GameObject[] tischentfernen = GameObject.FindGameObjectsWithTag("Tisch");

        foreach (GameObject k in tischentfernen)
        {
            k.SetActive(false);
        }
        foreach (GameObject r in entfernen)
        {
            r.SetActive(false);
        }
        /*RenderSettings.skybox = skyboxDay;
        RenderSettings.ambientLight = fullLight;
        sign.GetComponent<Renderer>().material.mainTexture = commandment1;*/
        lightOn = true;
    }

    void Update()
    {

        //zimmerlichter = allelichter.GetComponentsInChildren<Light>();


       /* if (Input.GetKeyDown(KeyCode.G))
        {
            Trial();
        }*/
        

    }
    static public void Trial()
    {
        /*Das Skript schaltet alle Lichter im Raum aus, damit die Beamerprojektionr korrekt funktionieren kann
         * */
        GameObject[] allelichter = GameObject.FindGameObjectsWithTag("Licht");

        if (lightOn == true)
        {
            //Debug.Log("ein");
            //sun.enabled = true;

            foreach (GameObject i in allelichter)
            {
                i.SetActive(false);
            }
            //RenderSettings.skybox = skyboxNight;
            //RenderSettings.ambientLight = fullDark;
            //sign.GetComponent<Renderer>().material.mainTexture = commandment2;
            //lightOn = false;
        }
        else
        {
            //sun.enabled = true;
            //Debug.Log("aus");
            SceneManager.LoadScene("zimmer1408");
            foreach (GameObject f in allelichter)
            {
                f.SetActive(true);
            }
            //RenderSettings.skybox = skyboxDay;
            //RenderSettings.ambientLight = fullLight;
            //sign.GetComponent<Renderer>().material.mainTexture = commandment1;
            //lightOn = true;
        }

    }

}
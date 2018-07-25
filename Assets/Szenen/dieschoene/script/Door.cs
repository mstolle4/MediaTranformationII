using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{      //Script mit allen Möglichen Varianten um Tur zu öffnen, kann dann In Szene jeweils ausgewählt werden (swipe door, Hinge doorund waypoint door)
    public enum DoorType
    {
        //swipe doors : öffnet die Tür in eine Richtung und gleitet zurück, wenn sie geschlossen ist
        SwipeUP,
        SwipeDown,
        SwipeLeft,
        SwipeRight,
        SwipeFront,
        SwipeBack,
        //hinge doors : Drehen,Modellen Rotation, zentrieren der Achse in einem 3D-Modellierungsprogramm, um Scharnierposition festzulegen
        //Scharnierabstand höchstens auf 175- 180 und höher wird nicht funktionieren - Scharniere können keine 360 ​​Grad Drehung machen, weil sie einen Abstandswert prüfen, der nur auf 180 geht
        HingeUp,
        HingeDown,
        HingeLeft,
        HingeRight,
        HingeFront,
        HingeBack,
        //waypoint: Wegpunkte: Tür bewegt sich zur Transformation eines Spielobjekts, das du als Wegpunkt auswählst - Wegpunkte individuel einstellbar
        MoveToWaypoint,
    }
    public GameObject player; //Überprüfung der Spielerdistanz erfolgt, sollte angeordnet an den Player sein
    public DoorType type;
    public GameObject waypoint; //Wenn MoveToWaypoint verwendet wird = Objekt als Wegpunkt festlegen
    public GameObject activateTarget; //Wenn ein anderes Spielobjekt als Aktivierungsziel verwendet wird, ist dies hilfreich, um den Aktivierungsbereich eines Gelenksgatter zu zentrieren    
    public bool distanceTrigger = true;//bei false muss die tür mit einem auslöser geöffnet werden, die Einstellung openNow = true öffnet die Tür
    public bool showOpenRange;//Bereich (rot) im Szenenfenster anzeigen
    public float openRange;//Größe des Türöffnungsauslösers einstellen
    public bool showCloseRange;//Bereich(blau) im Szenenfenster anzeigen
    public float closeRange;// Wenn der Nahbereich auf 0 eingestellt ist, schließt sich die Tür nicht. Bei jedem anderen Wert wird die Tür basierend auf der Spielerentfernung vom Auslöser geschlossen
    public float swipeDistance;// wenn man eine Swipe door benutzt, kann man hiermit festlegen,wie weit sich die Tür bewegen soll
    public float movementSpeed = 20; // Hiermit kann man festlegen, wie schnell sich die Tür bewegen wird
    [Range(0.0f, 178f)] public float HingeDistance = 90; //Bereich für offene Tür (hinge door)
    public float swayBuffer = 2.4f;//wie stark sich die Tür des Scharniers bewegt, wenn sie sich in der Position befindet (Hinge door)
    public AudioClip[] openSounds; //Sounds, die ausgewählt werden sollen, um den Türöffnungston zu spielen (In "schöne und das biest" Szene, ist keine Soundspur ausgewählt)


    AudioClip openAudio; //Verweis auf deathAudio Auswahl
    float state;
    Vector3 s_Distance;
    Vector3 originalPosition;
    Quaternion originalRotation;
    Quaternion currentRotation;
    bool openNow = false;
    bool swipingDoor;
    //float Distance;
    // Use this for initialization
    void Start()
    {
        //remember original location
        originalPosition = transform.position;
        originalRotation = transform.rotation;
        //Pick a Random sound from the set
        if (openSounds.Length > 0 ) {
            openAudio = openSounds[Random.Range(0, openSounds.Length)];
        }

        //create moveToward position
        if (type == DoorType.SwipeDown)
        {
            s_Distance = new Vector3(transform.position.x, (transform.position.y - swipeDistance), transform.position.z);
            swipingDoor = true;
        }
        if (type == DoorType.SwipeUP)
        {
            s_Distance = new Vector3(transform.position.x, (transform.position.y + swipeDistance), transform.position.z);
            swipingDoor = true;
        }
        if (type == DoorType.SwipeBack)
        {
            s_Distance = new Vector3(transform.position.x, transform.position.y, (transform.position.z - swipeDistance));
            swipingDoor = true;
        }
        if (type == DoorType.SwipeFront)
        {
            s_Distance = new Vector3(transform.position.x, transform.position.y, (transform.position.z + swipeDistance));
            swipingDoor = true;
        }
        if (type == DoorType.SwipeRight)
        {
            s_Distance = new Vector3((transform.position.x + swipeDistance), transform.position.y, transform.position.z);
            swipingDoor = true;
        }
        if (type == DoorType.SwipeLeft)
        {
            s_Distance = new Vector3((transform.position.x - swipeDistance), transform.position.y, transform.position.z);
            swipingDoor = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //   if (Input.GetAxis("Fire1") == 1)
        //    {
        // print("xxxxxxxxxxx");
        //print(Quaternion.Angle(originalRotation, currentRotation));
       // print(HingeDistance + "HD");
        print(state + "state");
        // print("xxxxxxxxxxx");
        // print(Quaternion.Angle(originalRotation, currentRotation) > HingeDistance);
        //   }
        if (distanceTrigger == true)
        {
            if (state == 0)
            {
                if (activateTarget != null)
                {
                    if (Vector3.Distance(activateTarget.transform.position, player.transform.position) <= openRange)
                    {
                        if (openSounds.Length > 0)
                        {
                            AudioSource.PlayClipAtPoint(openAudio, transform.position); // play door open sound
                        }
                        state = 1;

                    }
                }
                else
                {
                    if (Vector3.Distance(transform.position, player.transform.position) <= openRange)
                    {
                        if (openSounds.Length > 0)
                        {
                            AudioSource.PlayClipAtPoint(openAudio, transform.position); // play door open sound
                        }
                        state = 1;
                    }
                }
            }
        }
        else { WaitForTrigger(); }
        //print(state);
        if (state == 1)
        {
            //           print(Vector3.Distance(transform.position, originalLocation));
            //          print(swipeDistance);

            //open door
            if (type == DoorType.MoveToWaypoint)
            {
                float speed = movementSpeed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, waypoint.transform.position, speed);
                if (Vector3.Distance(transform.position, waypoint.transform.position) <= 0)
                {
                    state = 2;
                }
                return; //dont check other door types
            }

            if (swipingDoor == true)
            {
                float swipe = movementSpeed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, s_Distance, swipe);
                if (Vector3.Distance(transform.position, originalPosition) >= (swipeDistance - 1))
                {
                    state = 2;
                }
            }
            else
            {
                //hinge like turning - requires the model to have its axis placed in the hinge location(this would be done in modeling program) FYI - if the axis is in the center it will spin from there
                currentRotation = transform.rotation;
                if (type == DoorType.HingeRight)
                {
                    transform.Rotate(Vector3.right, movementSpeed * Time.deltaTime);//have item spin on X rotation (1,0,0)
                }
                if (type == DoorType.HingeLeft)
                {
                    transform.Rotate(Vector3.left, movementSpeed * Time.deltaTime);//have item spin on X rotation (-1,0,0)
                }
                if (type == DoorType.HingeUp)
                {
                    transform.Rotate(Vector3.up, movementSpeed * Time.deltaTime);//have item spin on Y rotation (0,1,0)
                }
                if (type == DoorType.HingeDown)
                {
                    transform.Rotate(Vector3.down, movementSpeed * Time.deltaTime);//have item spin on Y rotation (0,-1,0)
                }
                if (type == DoorType.HingeFront)
                {
                    transform.Rotate(Vector3.forward, movementSpeed * Time.deltaTime);//have item spin on z rotation (0,0,1)
                }
                if (type == DoorType.HingeBack)
                {
                    transform.Rotate(Vector3.back, movementSpeed * Time.deltaTime);//have item spin on Z rotation (0,0,-1)
                }

                if (Quaternion.Angle(originalRotation, currentRotation) > HingeDistance + swayBuffer)
                {
                    state = 1.5f;
                }
            }
        }
        if (state == 1.5f)
        {
            currentRotation = transform.rotation;

            if (type == DoorType.HingeRight)
            {
                transform.Rotate(Vector3.left, movementSpeed / 2 * Time.deltaTime);//have item spin on X rotation (-1,0,0)
            }
            if (type == DoorType.HingeLeft)
            {
                transform.Rotate(Vector3.right, movementSpeed / 2 * Time.deltaTime);//have item spin on X rotation (-1,0,0)
            }
            if (type == DoorType.HingeUp)
            {
                transform.Rotate(Vector3.down, movementSpeed / 2 * Time.deltaTime);//have item spin on Y rotation (0,1,0)
            }
            if (type == DoorType.HingeDown)
            {
                transform.Rotate(Vector3.up, movementSpeed / 2 * Time.deltaTime);//have item spin on Y rotation (0,-1,0)
            }
            if (type == DoorType.HingeFront)
            {
                transform.Rotate(Vector3.back, movementSpeed / 2 * Time.deltaTime);//have item spin on z rotation (0,0,1)
            }
            if (type == DoorType.HingeBack)
            {
                transform.Rotate(Vector3.forward, movementSpeed / 2 * Time.deltaTime);//have item spin on Z rotation (0,0,-1)
            }

            if (Quaternion.Angle(originalRotation, currentRotation) <= HingeDistance - swayBuffer)
            {
                state = 1.8f;
                //state = 0;
            }
        }
        if (state == 1.8f)
        {
            //add door swing back and forth
            currentRotation = transform.rotation;
            if (type == DoorType.HingeRight)
            {
                transform.Rotate(Vector3.right, movementSpeed / 6 * Time.deltaTime);//have item spin on X rotation (1,0,0)
            }
            if (type == DoorType.HingeLeft)
            {
                transform.Rotate(Vector3.left, movementSpeed / 6 * Time.deltaTime);//have item spin on X rotation (-1,0,0)
            }
            if (type == DoorType.HingeUp)
            {
                transform.Rotate(Vector3.up, movementSpeed / 6 * Time.deltaTime);//have item spin on Y rotation (0,1,0)
            }
            if (type == DoorType.HingeDown)
            {
                transform.Rotate(Vector3.down, movementSpeed / 6 * Time.deltaTime);//have item spin on Y rotation (0,-1,0)
            }
            if (type == DoorType.HingeFront)
            {
                transform.Rotate(Vector3.forward, movementSpeed / 6 * Time.deltaTime);//have item spin on z rotation (0,0,1)
            }
            if (type == DoorType.HingeBack)
            {
                transform.Rotate(Vector3.back, movementSpeed / 6 * Time.deltaTime);//have item spin on Z rotation (0,0,-1)
            }


            if (Quaternion.Angle(originalRotation, currentRotation) >= HingeDistance)
            {
                state = 2;
            }
        }
        if (state == 2)
        {
            //check player is out of open range
            if (closeRange <= 0) return;//dont use close range if it is set to zero, stop the script here
            if (activateTarget != null)
            {
                if (Vector3.Distance(activateTarget.transform.position, player.transform.position) >= closeRange)
                {
                    if (openSounds.Length > 0)
                    {
                        AudioSource.PlayClipAtPoint(openAudio, transform.position);
                    }
                    state = 3;


                }
            }
            else
            {
                if (Vector3.Distance(transform.position, player.transform.position) >= closeRange)
                {
                    if (openSounds.Length > 0)
                    {
                        AudioSource.PlayClipAtPoint(openAudio, transform.position);
                    }
                    state = 3;


                }
            }
        }
        if (state == 3)
        {
            // close door
            if (type == DoorType.MoveToWaypoint)
            {
                float speed = movementSpeed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, originalPosition, speed);
                if (Vector3.Distance(transform.position, originalPosition) <= 0)
                {
                    state = 0;
                }
                return; //dont check other door types
            }
            if (swipingDoor == true)
            {
                float swipe = movementSpeed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, originalPosition, swipe);
                if (transform.position == originalPosition)
                {
                    state = 0;
                }
            }
            else
            {
                //close hinge door
                currentRotation = transform.rotation;

                if (type == DoorType.HingeRight)
                {
                    transform.Rotate(Vector3.left, movementSpeed * Time.deltaTime);//have item spin on X rotation (-1,0,0)
                }
                if (type == DoorType.HingeLeft)
                {
                    transform.Rotate(Vector3.right, movementSpeed * Time.deltaTime);//have item spin on X rotation (-1,0,0)
                }
                if (type == DoorType.HingeUp)
                {
                    transform.Rotate(Vector3.down, movementSpeed * Time.deltaTime);//have item spin on Y rotation (0,1,0)
                }
                if (type == DoorType.HingeDown)
                {
                    transform.Rotate(Vector3.up, movementSpeed * Time.deltaTime);//have item spin on Y rotation (0,-1,0)
                }
                if (type == DoorType.HingeFront)
                {
                    transform.Rotate(Vector3.back, movementSpeed * Time.deltaTime);//have item spin on z rotation (0,0,1)
                }
                if (type == DoorType.HingeBack)
                {
                    transform.Rotate(Vector3.forward, movementSpeed * Time.deltaTime);//have item spin on Z rotation (0,0,-1)
                }

                if (Quaternion.Angle(originalRotation, currentRotation) <= 5)//set to 5 here as a buffer so the .Angle isnt missed
                {
                    state = 4;
                    //   originalRotation = currentRotation;
                    //   state = 0;
                }
            }
        }
        if (state == 4)
        {
            //add door swing back and forth
            currentRotation = transform.rotation;
            if (type == DoorType.HingeRight)
            {
                transform.Rotate(Vector3.right, movementSpeed / 2 * Time.deltaTime);//have item spin on X rotation (1,0,0)
            }
            if (type == DoorType.HingeLeft)
            {
                transform.Rotate(Vector3.left, movementSpeed / 2 * Time.deltaTime);//have item spin on X rotation (-1,0,0)
            }
            if (type == DoorType.HingeUp)
            {
                transform.Rotate(Vector3.up, movementSpeed / 2 * Time.deltaTime);//have item spin on Y rotation (0,1,0)
            }
            if (type == DoorType.HingeDown)
            {
                transform.Rotate(Vector3.down, movementSpeed / 2 * Time.deltaTime);//have item spin on Y rotation (0,-1,0)
            }
            if (type == DoorType.HingeFront)
            {
                transform.Rotate(Vector3.forward, movementSpeed / 2 * Time.deltaTime);//have item spin on z rotation (0,0,1)
            }
            if (type == DoorType.HingeBack)
            {
                transform.Rotate(Vector3.back, movementSpeed / 2 * Time.deltaTime);//have item spin on Z rotation (0,0,-1)
            }


            if (Quaternion.Angle(originalRotation, currentRotation) > HingeDistance / 10)
            {
                state = 5;
            }
        }
        if (state == 5)
        {
            currentRotation = transform.rotation;

            if (type == DoorType.HingeRight)
            {
                transform.Rotate(Vector3.left, movementSpeed / 4 * Time.deltaTime);//have item spin on X rotation (-1,0,0)
            }
            if (type == DoorType.HingeLeft)
            {
                transform.Rotate(Vector3.right, movementSpeed / 4 * Time.deltaTime);//have item spin on X rotation (-1,0,0)
            }
            if (type == DoorType.HingeUp)
            {
                transform.Rotate(Vector3.down, movementSpeed / 4 * Time.deltaTime);//have item spin on Y rotation (0,1,0)
            }
            if (type == DoorType.HingeDown)
            {
                transform.Rotate(Vector3.up, movementSpeed / 4 * Time.deltaTime);//have item spin on Y rotation (0,-1,0)
            }
            if (type == DoorType.HingeFront)
            {
                transform.Rotate(Vector3.back, movementSpeed / 4 * Time.deltaTime);//have item spin on z rotation (0,0,1)
            }
            if (type == DoorType.HingeBack)
            {
                transform.Rotate(Vector3.forward, movementSpeed / 4 * Time.deltaTime);//have item spin on Z rotation (0,0,-1)
            }

            if (Quaternion.Angle(originalRotation, currentRotation) <= 5)//set to 5 here as a buffer so the .Angle isnt missed
            {
                state = 0;
                //state = 0;
            }
        }
    }

    void WaitForTrigger()
    {
        //setting openNow to true elsewhere will set off trigger
        if (openNow == true)
        {
            distanceTrigger = true;
            openRange = 99999;//this will hopefully catch all ranges for player distance to activate the door...add more 9's if not.
        }

    }

    //void Open()
    //{
    //    if (type == DoorType.SwipeDown)
    //    {
    //       float swipe = swipeSpeed* Time.deltaTime;
    //       // Vector3 s_Distance = new Vector3(transform.position.x, (transform.position.y - swipeDistance));
    //        transform.position = Vector3.MoveTowards(transform.position,s_Distance, swipe);
    //    }
    //}

    void OnDrawGizmos()
    {
        if (type == DoorType.MoveToWaypoint)
        {
            Gizmos.color = new Color(1, 1, 0, 0.6f);//yelllow
            Gizmos.DrawSphere(waypoint.transform.position, 1); //changing the 1 here will change the size of the waypoint in your scene
        }
            if (showOpenRange == true)
        {
            if (openRange <= 0) return;
            Gizmos.color = new Color(1, 0, 0, 0.1f);//red
            if (activateTarget != null)
            {
                Gizmos.DrawSphere(activateTarget.transform.position, openRange);
            }
            else
            {
                Gizmos.DrawSphere(transform.position, openRange);
            }
        }
        if (showCloseRange == true)
        {
            if (closeRange <= 0) return;
            Gizmos.color = new Color(0, 0, 1, 0.1f);//blue
            if (activateTarget != null)
            {
                Gizmos.DrawSphere(activateTarget.transform.position, closeRange);
            }
            else
            {
                Gizmos.DrawSphere(transform.position, closeRange);
            }
        }
    }
}


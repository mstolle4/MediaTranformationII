using UnityEngine;
using System.Collections;

public class Spielbeenden : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKey("escape"))
            Application.Quit();

    }
}
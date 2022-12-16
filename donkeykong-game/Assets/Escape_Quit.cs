using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Escape_Quit : MonoBehaviour
{
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey ("escape")) {
                 Application.Quit();
                }
    }
}

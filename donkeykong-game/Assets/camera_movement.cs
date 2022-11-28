using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 public class camera_movement : MonoBehaviour {
public GameObject player;
public Vector3 offset;
void Update ()
    {
        gameObject.transform.position = player.transform.position + offset;
    }
  }

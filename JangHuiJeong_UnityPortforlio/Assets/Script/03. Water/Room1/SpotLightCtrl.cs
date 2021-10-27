using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotLightCtrl : MonoBehaviour
{
    private GameObject Player;

    private void Awake()
    {
        Player = GameObject.Find("Player");
    }

    private void FixedUpdate()
    {
        transform.LookAt(Player.transform);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockManager : MonoBehaviour
{
    [SerializeField] private GameObject[] Clocks;
    private int[] ViewClock;
    private int ViewClockTouchCount;


    void Start()
    {
        foreach(var Clock in Clocks)
        {
            Clock.SetActive(false);
        }

        Clocks[0].SetActive(true);
    }
}

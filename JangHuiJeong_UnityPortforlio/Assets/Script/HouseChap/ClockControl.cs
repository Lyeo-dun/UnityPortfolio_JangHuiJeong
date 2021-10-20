using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockControl : MonoBehaviour
{
    private TestDissolveItem ClockDissolve;
    private AudioSource AlarmSound;
    private bool LastAlarm;
    private void Awake()
    {
        ClockDissolve = GetComponent<TestDissolveItem>();
        AlarmSound = GetComponent<AudioSource>();
    }
    private void Start()
    {
        LastAlarm = false;
    }
    public void DissolveAlarm()
    {
        AlarmSound.Stop();
        if(!LastAlarm)
            ClockDissolve.ChangeDissolveState();
    }

    public void SetLastAlarm()
    {
        LastAlarm = true;
    }
    public void ViewAlarm()
    {
        GetComponent<Renderer>().material.SetFloat("_Cutoff", 0);
    }

    void Update()
    {
        
    }
}

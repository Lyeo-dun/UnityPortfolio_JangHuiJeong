using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockControl : MonoBehaviour
{
    private TestDissolveItem ClockDissolve;

    private AudioSource AlarmSound;
    private GameObject ClockManager;

    private bool EventAlarm = false; // ** 울리는 알람인지 아닌지

    private bool LastAlarm = false; // ** 마지막 시계는 들고 옮겨야 하기 때문에 다른 시계들처럼 사라져서는 안되기 때문에 따로 빼준다

    private void Awake()
    {
        ClockDissolve = GetComponent<TestDissolveItem>();
        AlarmSound = GetComponent<AudioSource>();
        ClockManager = GameObject.Find("ClockManager");
    }

    public void DissolveAlarm()
    {
        if(AlarmSound.isPlaying)
            AlarmSound.Stop();

        if(EventAlarm)
            ClockManager.GetComponent<ClockManager>().AddAlarmClockIndex();

        if (!LastAlarm)
            ClockDissolve.ChangeDissolveState();
    }

    public void SetLastAlarm()
    {
        LastAlarm = true;
    }

    public void SetEventAlarm()
    {
        EventAlarm = true;
    }

    public void PlayRingingAlarm()
    {
        AlarmSound.Play();
    }

    private void OnDisable() // ** 완전히 투명해지면 비활성화가 되므로 비활성화를 할때 투명화를 풀어준다
    {
        GetComponent<Renderer>().material.SetFloat("_Cutoff", 0);
    }

    void Update()
    {
        
    }
}

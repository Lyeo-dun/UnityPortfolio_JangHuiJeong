using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventAlarmControl : ClockControl
{
    public override bool LinkTable
    {
        get
        {
            return _LinkTable;
        }
        set
        {            
            _LinkTable = value;
        }
    }
    protected override void Awake()
    {
        base.Awake();
    }

    private void OnEnable()
    {
        AlarmSound.Play();
    }

    public override void EventClock()
    {
        if (AlarmSound.isPlaying)
            AlarmSound.Stop();

        ClockManager.GetInstance().AddAlarmClockIndex();

        ClockDissolve.ChangeDissolveState();
    }

    public void NextViewClock()
    {
        if (GameManager.GetInstance().ClockEventState)
            ClockManager.GetInstance().ViewClockEvent();
    }

    public void OnDisable()
    {
        NextViewClock();
    }
}

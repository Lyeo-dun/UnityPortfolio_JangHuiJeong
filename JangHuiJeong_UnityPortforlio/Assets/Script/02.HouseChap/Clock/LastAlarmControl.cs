using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastAlarmControl : ClockControl
{
    protected override void Awake()
    {
        base.Awake();
    }

    public override void EventClock()
    {
        if (AlarmSound.isPlaying)
            AlarmSound.Stop();


    }
}

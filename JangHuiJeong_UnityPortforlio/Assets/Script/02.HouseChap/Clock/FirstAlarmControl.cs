using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstAlarmControl : ClockControl
{
    protected override void Awake()
    {
        base.Awake();
    }

    public override void EventClock()
    {        
        GameManager.GetInstance().ClockEventState = true;

        base.EventClock();
    }
}

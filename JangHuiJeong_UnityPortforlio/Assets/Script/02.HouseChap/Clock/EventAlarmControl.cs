public class EventAlarmControl : ClockControl
{
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
        ClockManager.GetInstance().AddAlarmClockIndex();
        base.EventClock();
    }

    private void OnDisable()
    {
        if(GameManager.GetInstance().ClockEventState)
            ClockManager.GetInstance().ViewClockEvent();
    }
}

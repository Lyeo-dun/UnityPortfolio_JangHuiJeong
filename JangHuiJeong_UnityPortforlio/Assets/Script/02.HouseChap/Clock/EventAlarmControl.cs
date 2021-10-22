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

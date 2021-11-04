using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockControl : MonoBehaviour
{
    protected TestDissolveItem ClockDissolve;
    protected AudioSource AlarmSound;

    [SerializeField] protected bool _LinkTable;
    public bool LinkTable
    {
        get { return _LinkTable; }
        set { _LinkTable = value; }
    }

    protected virtual void Awake()
    {
        ClockDissolve = GetComponent<TestDissolveItem>();
        AlarmSound = GetComponent<AudioSource>();
    }

    public virtual void EventClock()
    {
        if(AlarmSound.isPlaying)
            AlarmSound.Stop();

        ClockDissolve.ChangeDissolveState();
    }

    public void PlayRingingAlarm()
    {
        AlarmSound.Play();
    }

    private void OnDisable() // ** 완전히 투명해지면 비활성화가 되므로 비활성화를 할때 투명화를 풀어준다
    {
        ClockDissolve.ResetRenderer();
    }

}

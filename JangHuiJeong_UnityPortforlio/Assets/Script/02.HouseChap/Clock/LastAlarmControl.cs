using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
public class LastAlarmControl : ClockControl
{
    [SerializeField] private bool isHold;

    protected override void Awake()
    {
        base.Awake();
    }

    private void OnEnable()
    {
        isHold = false;
        AlarmSound.Play();
    }

    public void EventClock(GameObject Parents = null)
    {
        if (AlarmSound.isPlaying)
            AlarmSound.Stop();

        if (!isHold)
            HoldItem(Parents);
        else
            PutItem();
    }

    public void HoldItem(GameObject ParentsObject)
    {
        isHold = true;

        LinkTable = false;
        GetComponent<Rigidbody>().isKinematic = true;
        transform.position = ParentsObject.transform.position;
        transform.parent = ParentsObject.transform;
    }

    void PutItem()
    {
        isHold = false;

        transform.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        transform.parent = null;
    }
}

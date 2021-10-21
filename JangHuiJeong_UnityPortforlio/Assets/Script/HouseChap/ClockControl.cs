using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockControl : MonoBehaviour
{
    private TestDissolveItem ClockDissolve;

    private AudioSource AlarmSound;
    private GameObject ClockManager;

    private bool EventAlarm = false; // ** �︮�� �˶����� �ƴ���

    private bool LastAlarm = false; // ** ������ �ð�� ��� �Űܾ� �ϱ� ������ �ٸ� �ð��ó�� ��������� �ȵǱ� ������ ���� ���ش�

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

    private void OnDisable() // ** ������ ���������� ��Ȱ��ȭ�� �ǹǷ� ��Ȱ��ȭ�� �Ҷ� ����ȭ�� Ǯ���ش�
    {
        GetComponent<Renderer>().material.SetFloat("_Cutoff", 0);
    }

    void Update()
    {
        
    }
}

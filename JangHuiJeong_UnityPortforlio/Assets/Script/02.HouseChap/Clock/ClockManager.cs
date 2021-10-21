using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockManager : MonoBehaviour
{
    static ClockManager _Instance;

    [SerializeField] private GameObject[] Clocks;

    private List<GameObject> ViewClock = new List<GameObject>();
    private int AlarmClockIndex; // ** 이벤트로 몇 번째 알람시계가 울릴건지 결정하는 Index
                                 // ** Index가 ViewClock의 Length보다 커지면 이후 이벤트 발생
    private GameObject LastAlarm;

    private void Awake()
    {
        {
            if(_Instance == null)
            {
                _Instance = this;
            }
            else
            {
                if(_Instance != this)
                    Destroy(gameObject);
            }
        }

        {
            List<int> ViewClockNum = new List<int>();

            for (int i = 0; i < 5;)
            {
                int ClockIndex = Random.Range(0, Clocks.Length);

                if (!ViewClockNum.Contains(ClockIndex))
                {
                    ViewClockNum.Add(ClockIndex);
                    ViewClock.Add(Clocks[ClockIndex]);
                    i++;
                }
            }

            while(true)
            {
                int ClockIndex = Random.Range(0, Clocks.Length);

                if (!ViewClockNum.Contains(ClockIndex))
                {                    
                    LastAlarm = Clocks[ClockIndex];
                    break;
                }
            }
        }
    }

    void Start()
    {
        {
            GameObject ClockParent = GameObject.Find("NonAlarm");
            foreach (var Clock in Clocks)
            {
                Clock.transform.parent = ClockParent.transform;
            }

            ClockParent = GameObject.Find("Alarm");
            foreach (var Clock in ViewClock)
            {
                Clock.transform.parent = ClockParent.transform;
                Destroy(Clock.GetComponent<ClockControl>());
                Clock.AddComponent<EventAlarmControl>();
            }
        }

        Destroy(LastAlarm.GetComponent<ClockControl>());
        LastAlarm.AddComponent<LastAlarmControl>();

        foreach (var Clock in Clocks)
        {
            Clock.SetActive(false);
        }

        Clocks[0].SetActive(true);
        Destroy(Clocks[0].GetComponent<ClockControl>());
        Clocks[0].AddComponent<FirstAlarmControl>();

        AlarmClockIndex = 0;
    }
    public static ClockManager GetInstance()
    {
        return _Instance;
    }

    public void AddAlarmClockIndex(int _Value = 1)
    {
        AlarmClockIndex += _Value;
    }

    public void ViewClockEvent()
    {
        if(AlarmClockIndex < ViewClock.Count)
            ViewClock[AlarmClockIndex].SetActive(true);
    }
}

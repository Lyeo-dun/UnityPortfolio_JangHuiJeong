using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockManager : MonoBehaviour
{
    [SerializeField] private GameObject[] Clocks;

    private bool ClockEvent;
    private List<GameObject> ViewClock = new List<GameObject>();
    private int AlarmClockIndex; // ** 이벤트로 몇 번째 알람시계가 울릴건지 결정하는 Index
                                 // ** Index가 ViewClock의 Length보다 커지면 이후 이벤트 발생
    private GameObject LastAlarm;

    private void Awake()
    {
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
                Clock.GetComponent<ClockControl>().SetEventAlarm();
            }
        }

        LastAlarm.GetComponent<ClockControl>().SetLastAlarm();

        foreach(var Clock in Clocks)
        {
            Clock.SetActive(false);
        }

        Clocks[0].SetActive(true);

        AlarmClockIndex = 0;
        ClockEvent = false;
    }

    public void AddAlarmClockIndex(int _Value = 1)
    {
        AlarmClockIndex += _Value;
    }
}

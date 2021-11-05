using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BallsHoleControl : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI CountText;
    [SerializeField] private int Count;
    private bool EventStart;

    private ObjectManager ObjectMn;

    private void Awake()
    {
        ObjectMn = transform.parent.GetComponent<ObjectManager>();
    }

    void Start()
    {
        Count = 0;
        EventStart = false;
    }

    void Update()
    {
        CountText.text = Count.ToString() + " / ??";

        if(Count >= 3)
        {
            if(!EventStart)
            {
                EventStart = true;
                ObjectMn.FloorEventStart();
            }
        }
    }

    public void AddCount()
    {
        Count += 1;
    }
}

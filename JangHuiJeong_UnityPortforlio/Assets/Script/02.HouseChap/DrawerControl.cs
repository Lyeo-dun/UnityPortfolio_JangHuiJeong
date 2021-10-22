using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawerControl : MonoBehaviour
{
    [SerializeField] private List<GameObject> InDrawerObjects; // ** 서랍 안의 오브젝트들
    [SerializeField] private GameObject Drawer; // ** 서랍 
    // ** 오브젝트가 서랍의 위치로 가게되면 모델링이 겹치는 사태가 발생하기 때문에 Offset 값으로 위치를 조정한다
    private Vector3[] Offset;

    void Awake()
    {
        Drawer = transform.GetChild(0).gameObject;
        Offset = new Vector3[InDrawerObjects.Count];
    }
    private void Start()
    {
        if (InDrawerObjects.Count > 0)
        {
            for(int i = 0; i < InDrawerObjects.Count; i++)
            {
                // ** 이때 편차는 현재 오브젝트의 위치 - 서랍의 위치로 계산한다
                Offset[i] = InDrawerObjects[i].transform.position - Drawer.transform.position;
                InDrawerObjects[i].GetComponent<ClockControl>().LinkTable = true;

                if (InDrawerObjects[i].GetComponent<EventAlarmControl>())
                {
                    InDrawerObjects[i].GetComponent<EventAlarmControl>().LinkTable = true;
                }
                if (InDrawerObjects[i].GetComponent<LastAlarmControl>())
                {
                    InDrawerObjects[i].GetComponent<LastAlarmControl>().LinkTable = true;
                    InDrawerObjects[i].GetComponent<Rigidbody>().isKinematic = true; //서랍에 있을 때는 물리엔진을 사용하지 않는다.
                }
            }
        }
    }

    void Update()
    {
        if (InDrawerObjects.Count > 0)
        {
            for (int i = 0; i < InDrawerObjects.Count; i++)
            {
                if(InDrawerObjects[i].GetComponent<ClockControl>().LinkTable)
                {
                    InDrawerObjects[i].transform.position = Drawer.transform.position + Offset[i];
                }
                //if(!InDrawerObjects[i].GetComponent<ClockControl>().LinkTable)
                //{
                //    InDrawerObjects.RemoveAt(i);
                //}
            }
        }
    }
}

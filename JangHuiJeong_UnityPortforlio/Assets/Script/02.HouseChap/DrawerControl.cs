using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawerControl : MonoBehaviour
{
    [SerializeField] private List<GameObject> InDrawerObjects; // ** ���� ���� ������Ʈ��
    [SerializeField] private GameObject Drawer; // ** ���� 
    // ** ������Ʈ�� ������ ��ġ�� ���ԵǸ� �𵨸��� ��ġ�� ���°� �߻��ϱ� ������ Offset ������ ��ġ�� �����Ѵ�
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
                // ** �̶� ������ ���� ������Ʈ�� ��ġ - ������ ��ġ�� ����Ѵ�
                Offset[i] = InDrawerObjects[i].transform.position - Drawer.transform.position;
                InDrawerObjects[i].GetComponent<ClockControl>().LinkTable = true;

                if (InDrawerObjects[i].GetComponent<EventAlarmControl>())
                {
                    InDrawerObjects[i].GetComponent<EventAlarmControl>().LinkTable = true;
                }
                if (InDrawerObjects[i].GetComponent<LastAlarmControl>())
                {
                    InDrawerObjects[i].GetComponent<LastAlarmControl>().LinkTable = true;
                    InDrawerObjects[i].GetComponent<Rigidbody>().isKinematic = true; //������ ���� ���� ���������� ������� �ʴ´�.
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

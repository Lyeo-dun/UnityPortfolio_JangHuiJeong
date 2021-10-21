using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawerControl : MonoBehaviour
{
    [SerializeField] private GameObject[] InDrawerObjects; // ** ���� ���� ������Ʈ��
    [SerializeField] private GameObject Drawer; // ** ���� 
    // ** ������Ʈ�� ������ ��ġ�� ���ԵǸ� �𵨸��� ��ġ�� ���°� �߻��ϱ� ������ Offset ������ ��ġ�� �����Ѵ�
    private Vector3[] Offset;

    void Awake()
    {
        Drawer = transform.GetChild(0).gameObject;
        Offset = new Vector3[InDrawerObjects.Length];
    }
    private void Start()
    {
        if (InDrawerObjects.Length > 0)
        {
            for(int i = 0; i < InDrawerObjects.Length; i++)
            {
                // ** �̶� ������ ���� ������Ʈ�� ��ġ - ������ ��ġ�� ����Ѵ�
                Offset[i] = InDrawerObjects[i].transform.position - Drawer.transform.position;
            }
        }
    }

    void Update()
    {
        if (InDrawerObjects.Length > 0)
        {
            for (int i = 0; i < InDrawerObjects.Length; i++)
            {
                InDrawerObjects[i].transform.position = Drawer.transform.position + Offset[i];
            }
        }
    }
}

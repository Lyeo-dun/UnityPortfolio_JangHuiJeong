using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawerControl : MonoBehaviour
{
    [SerializeField] private GameObject[] InDrawerObjects; // ** 서랍 안의 오브젝트들
    [SerializeField] private GameObject Drawer; // ** 서랍 
    // ** 오브젝트가 서랍의 위치로 가게되면 모델링이 겹치는 사태가 발생하기 때문에 Offset 값으로 위치를 조정한다
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
                // ** 이때 편차는 현재 오브젝트의 위치 - 서랍의 위치로 계산한다
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

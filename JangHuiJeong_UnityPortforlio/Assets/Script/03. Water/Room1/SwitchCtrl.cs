using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCtrl : MonoBehaviour
{
    [SerializeField] private GameObject Object1;

    private void Awake()
    {
        Object1 = transform.parent.gameObject;
    }

    public void ViewEyes()
    {
        Object1.GetComponent<ObjectManager>().ViewHeads();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextStageDoorCtl : MonoBehaviour
{
    [SerializeField] private Collider NextTrigger;
    [SerializeField] private Material NextDoorMat;

    private void Awake()
    {
        Collider[] colls = GetComponents<Collider>();

        foreach(var coll in colls)
        {
            if (coll.isTrigger)
                NextTrigger = coll;
        }

        NextDoorMat = Resources.Load("Material/Chap1/TunnelPortal", typeof(Material)) as Material;
    }

    private void Start()
    {
        NextTrigger.enabled = false;
    }
    public void SetNextTrigger(bool _Trigger = false)
    {
        NextTrigger.enabled = _Trigger;
        GetComponent<Renderer>().material = NextDoorMat;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            GameManager.GetInstance().NextStage();
        }
    }
}

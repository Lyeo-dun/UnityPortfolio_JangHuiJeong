using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    [SerializeField] private GameObject SpotLignts;
    [SerializeField] private GameObject Heads;
    [SerializeField] private GameObject SoulBox;

    private void Awake()
    {
        SpotLignts = GameObject.Find("Object1/SpotLignts");
        Heads = GameObject.Find("Object1/Heads");
        SoulBox = GameObject.Find("Object1/Box/BoxHead");
    }

    void Start()
    {
        SpotLignts.SetActive(false);
        Heads.SetActive(false);
    }

    public void Update()
    {
        if(Heads.activeSelf && SpotLignts.activeSelf)
        {
            SoulBox.GetComponent<BoxCtrl>().UnLock();
        }
        else
        {
            SoulBox.GetComponent<BoxCtrl>().Lock();
        }
    }

    public void Switching()
    {
        SpotLignts.SetActive(!SpotLignts.activeSelf);
        Heads.SetActive(!Heads.activeSelf);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            if (other.gameObject.GetComponent<PlayerMoveController>().PlayerGrab())
                SpotLignts.SetActive(!SpotLignts.activeSelf);
        }
    }
}

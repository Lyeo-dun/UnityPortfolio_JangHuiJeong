using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    [SerializeField] private GameObject SpotLignts;
    [SerializeField] private GameObject Heads;

    private void Awake()
    {
        SpotLignts = GameObject.Find("Object1/SpotLignts");
        Heads = GameObject.Find("Object1/Heads");
    }

    void Start()
    {
        SpotLignts.SetActive(false);
        Heads.SetActive(false);
    }

    public void ViewHeads()
    {
        SpotLignts.SetActive(false);
        Heads.SetActive(true);
    }

    public void ViewSpot()
    {
        SpotLignts.SetActive(true);
        Heads.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            if(other.gameObject.GetComponent<PlayerMoveController>().PlayerGrab())
                ViewSpot();
        }
    }
}

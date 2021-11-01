using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    [SerializeField] private GameObject SpotLignts;
    [SerializeField] private GameObject Heads;
    [SerializeField] private GameObject Switch;

    [SerializeField] private GameObject SoulBox;
    [SerializeField] private List<GameObject> Souls;
    [SerializeField] private GameObject SoulPos;
    private int SoulNumber;

    [SerializeField] private GameObject RoomDoor;

    private void Awake()
    {
        SpotLignts = GameObject.Find("Object1/SpotLignts");
        Heads = GameObject.Find("Object1/Heads");
        SoulBox = GameObject.Find("Object1/Box/BoxHead");
        SoulPos = GameObject.Find("Object1/SoulPosition");
        Switch = GameObject.Find("Object1/Switch");

        RoomDoor = GameObject.Find("Door");

        Object[] objs = Resources.LoadAll("Prefabs/Gems Prefabs");
        foreach(var obj in objs)
        {
            Souls.Add(obj as GameObject);
        }
    }

    void Start()
    {
        SpotLignts.SetActive(false);
        Heads.SetActive(false);

        SoulNumber = Random.Range(0, Souls.Count) + 1;
        GameManager.GetInstance().SettingPassword(SoulNumber);

        GameObject Soul = Instantiate<GameObject>(Souls[SoulNumber - 1], SoulPos.transform);
        Soul.GetComponent<KeyControl>().LinkDoor = RoomDoor;

        Switch.SetActive(false);
    }

    private void Update()
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
            {
                SpotLignts.SetActive(!SpotLignts.activeSelf);
            }

            Switch.SetActive(true);
        }
    }
}

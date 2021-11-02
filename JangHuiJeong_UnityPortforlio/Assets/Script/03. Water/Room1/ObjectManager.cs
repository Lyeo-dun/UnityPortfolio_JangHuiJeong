using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    [SerializeField] private int ObjectManagerNum;

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
        {
            string ObjName = gameObject.name;
            ObjName = ObjName.Replace("Object", "");

            int.TryParse(ObjName, out ObjectManagerNum);
        }
        {
            Object[] objs = Resources.LoadAll("Prefabs/Gems Prefabs");
            foreach(var obj in objs)
            {
                Souls.Add(obj as GameObject);
            }

            SoulBox = GameObject.Find("Box/BoxHead");
            SoulPos = GameObject.Find("SoulPosition");
            RoomDoor = GameObject.Find("Door");
        }

        if(ObjectManagerNum == 1)
        {
            SpotLignts = GameObject.Find("Object1/SpotLignts");
            Heads = GameObject.Find("Object1/Heads");
            Switch = GameObject.Find("Object1/Switch");
        }
    }

    void Start()
    {
        GameManager.GetInstance().isInRoom = true;
        GameManager.GetInstance().RoomNum = ObjectManagerNum;

        {
            SoulNumber = Random.Range(0, Souls.Count) + 1;
            GameManager.GetInstance().SettingPassword(SoulNumber);

            GameObject Soul = Instantiate<GameObject>(Souls[SoulNumber - 1], SoulPos.transform);
            Soul.GetComponent<KeyControl>().LinkDoor = RoomDoor;
        }

        if (ObjectManagerNum == 1)
        {
            SpotLignts.SetActive(false);
            Heads.SetActive(false);
            Switch.SetActive(false);
        }

        if(ObjectManagerNum == 2)
        {

        }
    }

    private void Update()
    {
        if (ObjectManagerNum == 1)
        {
            if (Heads.activeSelf && SpotLignts.activeSelf)
            {
                SoulBox.GetComponent<BoxCtrl>().UnLock();
            }
            else
            {
                SoulBox.GetComponent<BoxCtrl>().Lock();
            }
        }
    }

    public void Switching()
    {
        SpotLignts.SetActive(!SpotLignts.activeSelf);
        Heads.SetActive(!Heads.activeSelf);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (ObjectManagerNum == 1)
        {
            if (other.gameObject.tag == "Player")
            {
                if (other.gameObject.GetComponent<PlayerMoveController>().PlayerGrab())
                {
                    SpotLignts.SetActive(!SpotLignts.activeSelf);
                }

                Switch.SetActive(true);
            }
        }
    }
}

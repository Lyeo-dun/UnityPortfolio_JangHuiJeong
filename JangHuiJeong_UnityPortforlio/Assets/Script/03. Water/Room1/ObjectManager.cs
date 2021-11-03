using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    [SerializeField] private int ObjectManagerNum;

    private int SoulNumber;
    [SerializeField] private GameObject SoulBox;
    [SerializeField] private List<GameObject> Souls;
    [SerializeField] private GameObject SoulPos;

    [SerializeField] private GameObject RoomDoor;

    [Header("Object1")]
    [SerializeField] private GameObject SpotLignts;
    [SerializeField] private GameObject Heads;
    [SerializeField] private GameObject Switch;

    [Header("Object2")]
    [SerializeField] private List<GameObject> Balls;
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
        if(ObjectManagerNum == 2)
        {
            BringItem[] Brings = transform.GetComponentsInChildren<BringItem>();

            for(int i = 0; i < Brings.Length; i++)
            {
                if(Brings[i].gameObject.layer == LayerMask.NameToLayer("Ball"))
                {
                    Balls.Add(Brings[i].gameObject);
                }
            }
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
            foreach(var Ball in Balls)
            {
                Rigidbody Rigid = Ball.GetComponent<Rigidbody>();
                float RandomX, RandomY, RandomZ;
                if (Rigid != null)
                {
                    RandomX = Random.Range(-1f, 1f);
                    RandomY = Random.Range(-1f, 1f);
                    RandomZ = Random.Range(-1f, 1f);

                    Vector3 dir = new Vector3(RandomX, RandomY, RandomZ);

                    Rigid.AddForce(dir * 100f);
                }
            }
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventPlaceCtrl : MonoBehaviour
{
    [SerializeField] private List<GameObject> Walls;
    [SerializeField] private AudioSource WallsDropSound;

    [SerializeField] private GameObject InRoomDoor;

    private void Awake()
    {
        {
            Transform[] Trans = transform.GetChild(0).GetComponentsInChildren<Transform>();
            
            for (int i = 1; i < Trans.Length; i++)
            {
                if(Trans[i].gameObject.tag == "Wall")
                {
                    Walls.Add(Trans[i].gameObject);
                }
            }
        }

        InRoomDoor = transform.GetChild(1).gameObject;
    }
    private void Start()
    {
        WallsDropSound = GetComponent<AudioSource>();

        if(!GameManager.GetInstance().PlayerSettingPos)
            foreach (var Wall in Walls)
            {
                Wall.SetActive(false);
            }
        else
        {
            foreach (var Wall in Walls)
            {
                Wall.SetActive(true);
            }
        }
    }

    private void Update()
    {
        if(!GameManager.GetInstance().isNextRoom)
            if (GameManager.GetInstance().RoomNum + GameManager.GetInstance().SceneNumber > GameManager.GetInstance().LastRoomNum)
            {
                InRoomDoor.SetActive(false);
                GameManager.GetInstance().isNextRoom = true;
            }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if(!GameManager.GetInstance().PlayerSettingPos)
            {
                GameManager.GetInstance().PlayerSettingPos = true;

                StartCoroutine("WallEventStart");
            }
        }
    }

    IEnumerator WallEventStart()
    {
        foreach (var Wall in Walls)
        {
            Wall.SetActive(true);
            WallsDropSound.Play();
            yield return new WaitForSeconds(0.65f);
            WallsDropSound.Stop();
        }
    }
}

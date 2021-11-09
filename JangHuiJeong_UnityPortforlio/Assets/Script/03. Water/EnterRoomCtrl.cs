using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterRoomCtrl : MonoBehaviour
{
    private GameObject Door;

    private void Awake()
    {
        Door = transform.parent.gameObject;

    }

    private void Update()
    {
        GetComponent<Collider>().enabled = Door.GetComponent<Door>().GetDoorOpen();
    }

    private void OnCollisionEnter(Collision collision)
    {
        // ** 방을 한번도 가지 않았을 때는 첫 번째 방으로 도달해야 하므로 Room 넘버를 세팅해주고 이후에는
        // ** 방을 클리어 했을 시 다음 방의 Room 넘버가 셋팅 될 것이기 때문에 최초의 방 이동 시 단 한번만 초기화를 해준다
        if(!GameManager.GetInstance().isNextRoom)
        {
            if (!GameManager.GetInstance().isInRoom)
            {
                if (GameManager.GetInstance().RoomNum == 0)
                    GameManager.GetInstance().RoomNum = 1;

                GameManager.GetInstance().isInRoom = true;
                GameManager.GetInstance().InRoom();
            }
            else
            {
                GameManager.GetInstance().isInRoom = false;
                GameManager.GetInstance().RoomNum += 1;

                GameManager.GetInstance().OutRoom();
            }
        }
        else
        {
            GameManager.GetInstance().NextStage(GameManager.GetInstance().LastRoomNum - 1);
        }
    }
}

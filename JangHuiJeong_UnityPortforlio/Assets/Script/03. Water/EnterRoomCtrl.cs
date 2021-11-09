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
        // ** ���� �ѹ��� ���� �ʾ��� ���� ù ��° ������ �����ؾ� �ϹǷ� Room �ѹ��� �������ְ� ���Ŀ���
        // ** ���� Ŭ���� ���� �� ���� ���� Room �ѹ��� ���� �� ���̱� ������ ������ �� �̵� �� �� �ѹ��� �ʱ�ȭ�� ���ش�
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

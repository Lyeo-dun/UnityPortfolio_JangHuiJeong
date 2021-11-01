using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterRoomCtrl : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        // ** ���� �ѹ��� ���� �ʾ��� ���� ù ��° ������ �����ؾ� �ϹǷ� Room �ѹ��� �������ְ� ���Ŀ���
        // ** ���� Ŭ���� ���� �� ���� ���� Room �ѹ��� ���� �� ���̱� ������ ������ �� �̵� �� �� �ѹ��� �ʱ�ȭ�� ���ش�
        if(!GameManager.GetInstance().isInRoom)
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
}

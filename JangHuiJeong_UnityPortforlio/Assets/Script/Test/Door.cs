using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private Animator DoorAni;
    private bool isOpen;
    [SerializeField] private Collider[] Colliders;
    
    [SerializeField] private bool isKey;
    //[SerializeField] private GameObject Key;

    private bool KeyDoor;

    private void Awake()
    {
        DoorAni = GetComponent<Animator>();
        Colliders = GetComponents<Collider>();

    }
    private void Start()
    {
        isOpen = false;
        isKey = false;

        if (gameObject.tag == "KeyDoor")
            KeyDoor = true;
        else
            KeyDoor = false;
    }

    public void DoorCtl()
    {
        if (KeyDoor)
        {
            if (isKey)
            {
                DoorAniCtrl();
            }
        }
        else
        {
            DoorAniCtrl();
        }
    }

    private void DoorAniCtrl()
    {
        isOpen = !isOpen;
        DoorAni.SetBool("isOpen", isOpen);
        for (int i = 0; i < Colliders.Length; i++)
        {
            Colliders[i].enabled = !Colliders[i].enabled;
        }
    }

    public void OpenDoor()
    {
        isKey = true;
    }
}

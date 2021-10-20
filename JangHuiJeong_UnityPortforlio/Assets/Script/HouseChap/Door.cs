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
    [SerializeField] private GameObject NeedKeyMessageUI;
    private void Awake()
    {
        DoorAni = GetComponent<Animator>();
        Colliders = GetComponents<Collider>();

        if (gameObject.tag == "KeyDoor")
            KeyDoor = true;
        else
            KeyDoor = false;

        if (KeyDoor)
            NeedKeyMessageUI = GameObject.Find("NeedKeyMessage");
    }
    private void Start()
    {
        isOpen = false;
        isKey = false;

        if (KeyDoor)
            NeedKeyMessageUI.SetActive(false);
    }

    public void DoorCtl()
    {
        if (KeyDoor)
        {
            if (isKey)
            {
                DoorAniCtrl();
            }
            else
            {                
                StartCoroutine("ViewingCountMessage");
            }
        }
        else
        {
            DoorAniCtrl();
        }
    }
    IEnumerator ViewingCountMessage()
    {
        NeedKeyMessageUI.SetActive(true);

        yield return new WaitForSeconds(1.0f);
        NeedKeyMessageUI.SetActive(false);
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

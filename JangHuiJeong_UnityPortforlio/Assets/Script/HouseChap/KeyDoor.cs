using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyDoor : Door
{
    [SerializeField] private bool isKey;
    [SerializeField] private GameObject Key;
    [SerializeField] private GameObject NeedKeyMessageUI;

    protected override void Awake()
    {
        base.Awake();

        Key = Resources.Load("Prefabs/Key") as GameObject;
        NeedKeyMessageUI = GameObject.Find("NeedKeyMessage");
    }

    protected override void Start()
    {
        base.Start();

        isKey = false;
        NeedKeyMessageUI.SetActive(false);
    }

    public override void DoorCtl()
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

    public GameObject GetKey()
    {
        return Key;
    }

    public void OpenDoor()
    {
        isKey = true;
    }

    IEnumerator ViewingCountMessage()
    {
        NeedKeyMessageUI.SetActive(true);

        yield return new WaitForSeconds(1.0f);
        NeedKeyMessageUI.SetActive(false);
    }
}

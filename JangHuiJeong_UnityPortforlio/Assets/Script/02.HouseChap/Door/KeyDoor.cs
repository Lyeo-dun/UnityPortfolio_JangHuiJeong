using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyDoor : Door
{
    [SerializeField] private bool isKey;
    [SerializeField] private GameObject Key;
    [SerializeField] private GameObject NeedKeyMessageUI;
    [SerializeField] private AudioSource DoorAudio;

    protected override void Awake()
    {
        base.Awake();

        GameObject _Key = Resources.Load("Prefabs/Key") as GameObject;
        Key = Instantiate<GameObject>(_Key);
        Key.GetComponent<KeyControl>().LinkDoor = gameObject;
        FireControl.GetInstance().SettingKey(Key);

        NeedKeyMessageUI = GameObject.Find("NeedKeyMessage");

        DoorAudio = GetComponent<AudioSource>();
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

            if(ClockManager.GetInstance() != null && GameManager.GetInstance().ClockEventState)
            {
                ClockManager.GetInstance().ViewClockEvent();
            }

            if (ClockManager.GetInstance() != null && GameManager.GetInstance().ClockEventEnd)
            {
                ClockManager.GetInstance().CallLastAlarmEvent();
            }

        }
    }

    public GameObject GetKey()
    {
        return Key;
    }

    public void OpenDoor()
    {
        isKey = true;
        DoorAudio.Play();
        GameManager.GetInstance().GoThirdStage = true;
    }

    IEnumerator ViewingCountMessage()
    {
        NeedKeyMessageUI.SetActive(true);

        yield return new WaitForSeconds(1.0f);
        NeedKeyMessageUI.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(AudioSource))]
public class KeyDoor : Door
{
    [SerializeField] private bool isKey;
    [SerializeField] private GameObject Key;
    [SerializeField] private GameObject NeedKeyMessageUI;

    [SerializeField] private AudioSource DoorAudio;
    private AudioClip DoorOpenSound;
    
    protected override void Awake()
    {
        base.Awake();

        NeedKeyMessageUI = GameObject.Find("NeedKeyMessage");

        DoorAudio = GetComponent<AudioSource>();
        DoorOpenSound = Resources.Load("Audio/door-open") as AudioClip;
        DoorAudio.clip = DoorOpenSound;
    }

    protected override void Start()
    {
        base.Start();

        isKey = false;
        NeedKeyMessageUI.SetActive(false);
        
        if (GameManager.GetInstance().SceneNumber == 2) // ** Stage�� 2�� �ƴϸ� ������� �ʴ´�
        {
            GameObject _Key = Resources.Load("Prefabs/Key") as GameObject;
            Key = Instantiate<GameObject>(_Key);
            Key.GetComponent<KeyControl>().LinkDoor = gameObject;

            if (FireControl.GetInstance())
                FireControl.GetInstance().SettingKey(Key); // ** Stage 1�� ������ �̺�Ʈ ��ü�� �ѱ��.
        }
    }

    public override void DoorCtl()
    {
        if (isKey)
        {
            base.DoorCtl();
        }
        else
        {
            StartCoroutine("ViewingCountMessage");

            if (GameManager.GetInstance().SceneNumber == 2)
            {
                if (ClockManager.GetInstance() != null && GameManager.GetInstance().ClockEventState)
                {
                    ClockManager.GetInstance().ViewClockEvent();
                }
    
                if (ClockManager.GetInstance() != null && GameManager.GetInstance().ClockEventEnd)
                {
                    ClockManager.GetInstance().CallLastAlarmEvent();
                }
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

        if (GameManager.GetInstance().SceneNumber == 2)
            GameManager.GetInstance().GoThirdStage = true;
    }

    IEnumerator ViewingCountMessage()
    {
        NeedKeyMessageUI.SetActive(true);

        yield return new WaitForSeconds(1.0f);
        NeedKeyMessageUI.SetActive(false);
    }
}

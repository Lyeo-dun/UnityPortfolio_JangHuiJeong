using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PasswordCtrl : ItemControler
{
    [SerializeField] private GameObject PasswordUI;
    [SerializeField] private Image[] OutputPanel;
    [SerializeField] private List<int> InputPasswordList;

    [SerializeField] private GameObject MessageUI;

    private GameObject LinkDoor;

    private bool isViewUI;

    private void Awake()
    {
        PasswordUI = transform.GetChild(0).gameObject;
        MessageUI = GameObject.Find("PasswordUI/MessageUI");
        LinkDoor = GameObject.Find("DoorWall/NextStageDoor");
    }

    private void Start()
    {
        InputPassword[] InputButtons = transform.GetComponentsInChildren<InputPassword>();
        OutputPanel = PasswordUI.transform.GetChild(0).GetChild(0).GetComponentsInChildren<Image>();

        for (int i = 0; i < InputButtons.Length; i++)
        {
            InputButtons[i].ButtonNum = i + 1;           
        }
        
        isViewUI = false;
        PasswordUI.SetActive(isViewUI);
        MessageUI.SetActive(false);
    }

    public override void EventItem()
    {
        isViewUI = !isViewUI;

        PasswordUI.SetActive(isViewUI);
        GameManager.GetInstance().OnUI = isViewUI;

        if(isViewUI)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            ExitPasswordPanel();
        }
    }

    public void InputPasswords(int _InputPassword, Color _PanelColor)
    {
        if(InputPasswordList.Count < 2)
        {
            InputPasswordList.Add(_InputPassword);
            OutputPanel[InputPasswordList.Count - 1].color = _PanelColor;
        }
    }

    public void ExitPasswordPanel()
    {
        InputPasswordList.Clear();

        foreach(var Output in OutputPanel)
        {
            Output.color = Color.black;
        }
    }

    public void CompairPassWord()
    {
        if (InputPasswordList.Count < 2)
        {
            MessageUI.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = 2 + " 자리 암호를\n입력하여 주세요!";
            StartCoroutine("ViewMessage");
        } else if(GameManager.GetInstance().PasswordLength < 2)
        {
            MessageUI.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "암호를 알 수 없습니다!";
            StartCoroutine("ViewMessage");
        }

        //비밀번호가 맞다면
        //LinkDoor.GetComponent<KeyDoor>().OpenDoor();
    }

    IEnumerator ViewMessage()
    {
        MessageUI.SetActive(true);

        yield return new WaitForSeconds(1.0f);
        MessageUI.SetActive(false);
    }
}

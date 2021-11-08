using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PasswordCtrl : ItemControler
{
    [SerializeField] private GameObject PasswordUI;
    [SerializeField] private int InputPassword;

    private bool isViewUI;
    private bool isSetPassword;

    private void Awake()
    {
        PasswordUI = transform.GetChild(0).gameObject;

    }

    private void Start()
    {
        InputPassword[] InputButtons = transform.GetComponentsInChildren<InputPassword>();

        for(int i = 0; i < InputButtons.Length; i++)
        {
            InputButtons[i].ButtonNum = i + 1;           
        }
        
        isViewUI = false;
        PasswordUI.SetActive(isViewUI);

        isSetPassword = false;        
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
        }
    }

    public void CompairPassWord()
    {
        isSetPassword = false;

        isSetPassword = true;
    }
}

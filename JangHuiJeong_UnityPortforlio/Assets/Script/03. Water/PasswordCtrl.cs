using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PasswordCtrl : ItemControler
{
    [SerializeField] private GameObject PasswordUI;

    private bool isViewUI;
    private bool isSetPassword;

    private void Awake()
    {
        PasswordUI = transform.GetChild(0).gameObject;
    }

    private void Start()
    {
        isViewUI = false;
        PasswordUI.SetActive(isViewUI);
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
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputPassword : MonoBehaviour
{
    [SerializeField] private GameObject PasswordManager;
    [SerializeField] private Button InputButton;
    [SerializeField] private int _ButtonNum;
    public int ButtonNum
    {
        set
        {
            _ButtonNum = value;
        }
        get
        {
            return _ButtonNum;
        }
    }

    public void Awake()
    {
        InputButton = GetComponent<Button>();
        PasswordManager = GameObject.Find("PassWord");
    }

    public void Start()
    {
        InputButton.onClick.AddListener(Input);
    }

    public void Input()
    {
        PasswordManager.GetComponent<PasswordCtrl>().InputPasswords(_ButtonNum, GetComponent<Image>().color);
    }
}

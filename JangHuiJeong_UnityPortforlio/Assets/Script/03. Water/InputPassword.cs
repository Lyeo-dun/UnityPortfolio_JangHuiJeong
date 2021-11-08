using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputPassword : MonoBehaviour
{
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
    }

    public void Start()
    {
        InputButton.onClick.AddListener(Test);
    }

    public void Test()
    {
        Debug.Log(ButtonNum);
    }
}

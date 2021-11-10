using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuUICtrl : MonoBehaviour
{
    public void Awake()
    {
        Cursor.lockState = CursorLockMode.None;
    }
    public void StartButton()
    {
        GameManager.GetInstance().NextStage();
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    public void ReStartButton()
    {
        GameManager.GetInstance().Initialized();
        GameManager.GetInstance().SceneNumber = 0;

        GameManager.GetInstance().OutRoom();
    }
}

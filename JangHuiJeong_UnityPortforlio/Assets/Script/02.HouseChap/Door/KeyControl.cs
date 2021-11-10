using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyControl : MonoBehaviour
{
    private GameObject mLinkDoor;        
    public GameObject LinkDoor
    {
        get
        {
            return mLinkDoor;
        }
        set
        {
            mLinkDoor = value;
        }
    }

    private GameObject KeyModeling;

    private void Awake()
    {
        if(GameManager.GetInstance().SceneNumber == 2)
            KeyModeling = transform.GetChild(0).gameObject;
    }

    private void Start()
    {
        if (GameManager.GetInstance().SceneNumber == 2)
            KeyModeling.SetActive(false);
    }    
    
    public void KeyEvent()
    {
        if (GameManager.GetInstance().SceneNumber == 2)
        {
            LinkDoor.GetComponent<KeyDoor>().OpenDoor();
            gameObject.SetActive(false);
        }
        else
        {
            {
                LinkDoor.GetComponent<KeyDoor>().OpenDoor();
                gameObject.SetActive(false);
            }
        }
    }

    public void KeyShowing()
    {
        KeyModeling.SetActive(true);
    }
}

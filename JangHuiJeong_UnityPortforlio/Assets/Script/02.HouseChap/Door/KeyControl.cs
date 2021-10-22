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
        KeyModeling = transform.GetChild(0).gameObject;
    }

    private void Start()
    {
        KeyModeling.SetActive(false);
    }    
    
    public void KeyEvent()
    {
        LinkDoor.GetComponent<KeyDoor>().OpenDoor();
    }

    public void KeyShowing()
    {
        KeyModeling.SetActive(true);
    }
}

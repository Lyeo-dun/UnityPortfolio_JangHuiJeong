using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextControll : MonoBehaviour
{
    private GameObject CtrlText;

    private void Awake()
    {
        CtrlText = transform.GetChild(0).gameObject;
    }
        
    void Start()
    {
        CtrlText.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && GameManager.GetInstance().ViewText)
        {
            CtrlText.SetActive(true);
        }
    }
}

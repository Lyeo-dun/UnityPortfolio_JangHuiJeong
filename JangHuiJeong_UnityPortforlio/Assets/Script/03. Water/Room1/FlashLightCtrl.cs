using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLightCtrl : MonoBehaviour
{

    private GameObject FlashLigntBody;

    private void Awake()
    {
        FlashLigntBody = transform.GetChild(0).GetChild(0).gameObject;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.parent != null)
        {
            if(Input.GetKeyDown(KeyCode.F))
            {
                FlashLigntBody.SetActive(!FlashLigntBody.activeSelf);
            }
        }
    }
}

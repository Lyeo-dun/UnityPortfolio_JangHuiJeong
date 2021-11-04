using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Animator DoorAni;
    private bool isOpen;
    [SerializeField] private Collider[] Colliders;
    
    protected virtual void Awake()
    {
        DoorAni = GetComponent<Animator>();
        Colliders = GetComponents<Collider>();
    }

    protected virtual void Start()
    {
        isOpen = false;
        gameObject.tag = "Door";
    }

    public virtual void DoorCtl()
    {        
        DoorAniCtrl();
    }

    protected void DoorAniCtrl()
    {
        isOpen = !isOpen;
        DoorAni.SetBool("isOpen", isOpen);
        for (int i = 0; i < Colliders.Length; i++)
        {
            Colliders[i].enabled = !Colliders[i].enabled;
        }
    }
    protected virtual void FixedUpdate()
    {
    }
}

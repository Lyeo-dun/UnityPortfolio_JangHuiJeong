using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxCtrl : Door
{
    [SerializeField] private bool isBoxOpen;
    [SerializeField] private bool LockBox;
    private GameObject LockBoxUI;

    private float CloseCoverY;
    private float OpenCoverY;

    protected override void Awake()
    {
        LockBoxUI = GameObject.Find("LockBoxUI");
    }

    protected override void Start()
    {
        isBoxOpen = false;
        LockBox = true;

        LockBoxUI.SetActive(false);

        CloseCoverY = transform.position.y;
        OpenCoverY = CloseCoverY;
        OpenCoverY += 1.0f;
    }

    protected override void FixedUpdate()
    {
        if (isBoxOpen)
        {
            if (transform.position.y <= OpenCoverY)
            {
                transform.position = transform.position + (Vector3.up * 0.5f * Time.deltaTime);
            }
        }
        else
        {
            if (transform.position.y >= CloseCoverY)
            {
                transform.position = transform.position - (Vector3.up * 0.5f * Time.deltaTime);
            }
        }
    }

    public void UnLock()
    {
        LockBox = false;
    }
    public void Lock()
    {
        LockBox = true;
    }

    public override void DoorCtl()
    {
        if (LockBox)
        {
            StartCoroutine("ShowLockMessage");
            return;
        }

        isBoxOpen = !isBoxOpen;
    }

    IEnumerator ShowLockMessage()
    {
        LockBoxUI.SetActive(true);
        
        yield return new WaitForSeconds(1.2f);
        LockBoxUI.SetActive(false);
    }
}

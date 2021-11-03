using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BringItem : MonoBehaviour
{
    [SerializeField] private bool isHold;

    public bool Hold
    {
        get
        {
            return isHold;
        }
    }
    private void Start()
    {
        isHold = false;
    }

    public void HoldItem(GameObject ParentsObject)
    {
        isHold = true;

        GetComponent<Collider>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
        transform.position = ParentsObject.transform.position;
        transform.rotation = ParentsObject.transform.rotation;
        transform.parent = ParentsObject.transform;
    }

    void PutItem()
    {
        isHold = false;

        GetComponent<Collider>().enabled = true;
        transform.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        transform.parent = null;
    }

    public void EventItem(GameObject ParentsObject = null)
    {
        if(!isHold)
            HoldItem(ParentsObject);
        else
            PutItem();
    }
}

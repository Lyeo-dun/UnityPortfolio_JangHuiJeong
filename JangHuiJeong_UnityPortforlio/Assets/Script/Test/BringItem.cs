using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BringItem : MonoBehaviour
{
    [SerializeField] private bool isHold;

    private void Start()
    {
        isHold = false;
    }

    public void HoldItem(GameObject ParentsObject)
    {
        isHold = true;

        GetComponent<Rigidbody>().isKinematic = true;
        transform.position = ParentsObject.transform.position;
        transform.parent = ParentsObject.transform;
    }

    void PutItem()
    {
        isHold = false;

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

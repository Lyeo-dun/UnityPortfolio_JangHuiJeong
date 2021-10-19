using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] private GameObject OtherPortal;
    void Start()
    {
           
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            other.gameObject.transform.position = OtherPortal.transform.position + Vector3.forward * 2.0f + Vector3.up;

            GameManager.GetInstance().AddPortalCount();
        }
    }
}

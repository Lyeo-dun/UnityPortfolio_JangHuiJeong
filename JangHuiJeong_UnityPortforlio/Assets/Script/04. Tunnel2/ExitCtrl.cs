using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitCtrl : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            GameManager.GetInstance().NextStage();
        }
    }
}

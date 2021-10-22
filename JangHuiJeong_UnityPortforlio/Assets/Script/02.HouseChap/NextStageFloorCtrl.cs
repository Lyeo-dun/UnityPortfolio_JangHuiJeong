using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextStageFloorCtrl : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && GameManager.GetInstance().GoThirdStage)
        {
            GameManager.GetInstance().NextStage();
        }
    }
}

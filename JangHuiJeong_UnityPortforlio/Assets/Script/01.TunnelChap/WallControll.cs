using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallControll : MonoBehaviour
{
    [SerializeField] private GameObject[] Walls;

    [SerializeField] private GameObject NextStageDoor;

    private void Awake()
    {
        foreach (var Wall in Walls)
        {
            Wall.SetActive(false);
        }
        NextStageDoor = GameObject.Find("NextStageDoor");
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && GameManager.GetInstance().GetPortalCount() >= 2)
        {
            foreach(var Wall in Walls)
            {
                Wall.SetActive(true);

                NextStageDoor.GetComponent<NextStageDoorCtl>().SetNextTrigger(true);
            }
        }
    }
}

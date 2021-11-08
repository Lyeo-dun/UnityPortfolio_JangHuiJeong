using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallEvent : MonoBehaviour
{
    [SerializeField] private List<GameObject> Walls;
    [SerializeField] private AudioSource WallsDropSound;

    private void Awake()
    {
        {
            Transform[] Trans = transform.GetComponentsInChildren<Transform>();

            int Count = 0;
            for (int i = 1; i < Trans.Length; i++)
            {
                if(Trans[i].gameObject.tag == "Wall")
                {
                    Walls.Add(Trans[i].gameObject);                    
                }
            }
        }
    }
    private void Start()
    {
        WallsDropSound = GetComponent<AudioSource>();

        if(!GameManager.GetInstance().PlayerSettingPos)
            foreach (var Wall in Walls)
            {
                Wall.SetActive(false);
            }
        else
        {
            foreach (var Wall in Walls)
            {
                Wall.SetActive(true);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if(!GameManager.GetInstance().PlayerSettingPos)
            {
                GameManager.GetInstance().PlayerSettingPos = true;

                StartCoroutine("WallEventStart");
            }
        }
    }

    IEnumerator WallEventStart()
    {
        foreach (var Wall in Walls)
        {
            Wall.SetActive(true);
            WallsDropSound.Play();
            yield return new WaitForSeconds(0.65f);
            WallsDropSound.Stop();
        }
    }
}

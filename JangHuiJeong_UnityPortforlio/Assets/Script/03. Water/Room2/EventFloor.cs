using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventFloor : MonoBehaviour
{
    [SerializeField] private List<GameObject> Floors = new List<GameObject>();
    private AudioSource InvisibleAudio;

    private void Awake()
    {
        Transform[] ChildTrans = transform.GetComponentsInChildren<Transform>();

        foreach (var ChildTran in ChildTrans)
        {
            if(transform != ChildTran)
                Floors.Add(ChildTran.gameObject);
        }

        InvisibleAudio = GetComponent<AudioSource>();
    }

    IEnumerator FloorEventFunction()
    {
        foreach(var Floor in Floors)
        {
            Floor.SetActive(false);
            InvisibleAudio.Play();
            yield return new WaitForSeconds(0.5f);
            InvisibleAudio.Stop();
        }
    }
}

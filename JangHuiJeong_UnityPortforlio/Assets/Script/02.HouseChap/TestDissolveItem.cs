using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDissolveItem : MonoBehaviour
{
    private bool isDissolve;
    private float Value;
    [SerializeField] private Renderer[] Dissolves;
    private void Awake()
    {
        Dissolves = gameObject.GetComponentsInChildren<Renderer>(); // ** 자식 오브젝트들에 있는 모든 Renderer를 가져온다
    }
    void Start()
    {
        isDissolve = false;
        Value = 0.0f;
    }
    void Update()
    {
        if(isDissolve)
        {
            Value += 0.003f;

            // ** 쉐이더 값 조정
            foreach (var Dissolve in Dissolves)
            {
                foreach (var material in Dissolve.materials)
                {
                    material.SetFloat("_Cutoff", Value);
                }
            }

            if (Value >= 1.0f)
            {
                isDissolve = false;
                Value = 0;
                GetComponent<Collider>().enabled = true;

                gameObject.SetActive(false);
            }
        }
    }

    public void ResetRenderer()
    {
        foreach (var Dissolve in Dissolves)
        {
            foreach (var material in Dissolve.materials)
            {
                material.SetFloat("_Cutoff", 0);
            }
        }
    }
    public void ChangeDissolveState()
    {
        isDissolve = true;
        GetComponent<Collider>().enabled = false;
    }
}

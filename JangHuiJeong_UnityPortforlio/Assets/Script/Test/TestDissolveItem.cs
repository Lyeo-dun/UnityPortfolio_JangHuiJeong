using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDissolveItem : MonoBehaviour
{
    private bool isDissolve;
    private float Value;
    private void Awake()
    {

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
            Value += 0.002f;

            // ** 쉐이더 값 조정
            GetComponent<Renderer>().material.SetFloat("_Cutoff", Value);

            if (Value >= 1.0f)
            {
                isDissolve = false;
                gameObject.SetActive(false);
            }
        }
    }
    public void ChangeDissolveState()
    {
        isDissolve = true;
    }
}

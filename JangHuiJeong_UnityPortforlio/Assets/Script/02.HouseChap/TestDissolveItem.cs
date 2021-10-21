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
        Dissolves = gameObject.GetComponentsInChildren<Renderer>(); // ** �ڽ� ������Ʈ�鿡 �ִ� ��� Renderer�� �����´�
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

            // ** ���̴� �� ����
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

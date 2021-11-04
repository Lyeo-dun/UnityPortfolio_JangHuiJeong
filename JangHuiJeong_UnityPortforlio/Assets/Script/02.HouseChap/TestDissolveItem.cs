using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDissolveItem : MonoBehaviour
{
    [SerializeField] private bool isDissolve;
    [SerializeField] private float Value;
    [SerializeField] private Shader ThisGameObjectShader;

    [SerializeField] private Renderer[] Dissolves;
    private void Awake()
    {
        Dissolves = gameObject.GetComponentsInChildren<Renderer>(); // ** �ڽ� ������Ʈ�鿡 �ִ� ��� Renderer�� �����´�
    }
    void Start()
    {
        isDissolve = false;
        ThisGameObjectShader = Dissolves[0].material.shader;

        if (ThisGameObjectShader == Shader.Find("Ultimate 10+ Shaders/Dissolve"))
            Value = 0.0f;
        if (ThisGameObjectShader == Shader.Find("Standard"))
            Value = 1.0f;
    }
    void Update()
    {
        if(isDissolve)
        {
            // ** ���̴� �� ����
            foreach (var Dissolve in Dissolves)
            {
                foreach (var material in Dissolve.materials)
                {
                    if (ThisGameObjectShader == Shader.Find("Ultimate 10+ Shaders/Dissolve"))
                    {
                        material.SetFloat("_Cutoff", Value);
                        Value += 0.003f;
                    }                
                    if (ThisGameObjectShader == Shader.Find("Standard"))
                    {
                        Color ColorAlpha = material.color;

                        ColorAlpha.a = Value;
                        material.color = ColorAlpha;
                        Value -= 0.003f;
                    }                
                }
            }

            if (Value > 1.0f)
            {
                isDissolve = false;
                Value = 0;
                GetComponent<Collider>().enabled = true;

                gameObject.SetActive(false);
            }
            if(Value < 0)
            {
                isDissolve = false;
                Value = 1;
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
                if (ThisGameObjectShader == Shader.Find("Ultimate 10+ Shaders/Dissolve"))
                    material.SetFloat("_Cutoff", 0);

                if (ThisGameObjectShader == Shader.Find("Standard"))
                {
                    Color ColorAlpha = material.color;

                    ColorAlpha.a = 1;
                    material.color = ColorAlpha;
                }
            }
        }
    }
    public void ChangeDissolveState()
    {
        isDissolve = true;

        if (ThisGameObjectShader == Shader.Find("Ultimate 10+ Shaders/Dissolve"))
            GetComponent<Collider>().enabled = false;
    }
}

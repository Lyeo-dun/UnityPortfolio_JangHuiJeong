using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireControl : MonoBehaviour
{
    static private FireControl _Instance;
    static public FireControl GetInstance()
    {
        return _Instance;
    }

    [SerializeField] private GameObject[] Fires;
    [SerializeField] private GameObject Key;
    private bool isFireOff;
    [SerializeField] private float OffValue;

    private void Awake()
    {
        if(_Instance == null)
        {
            _Instance = this;
        }
        else
        {
            if(this != _Instance)
            {
                Destroy(gameObject);
            }
        }
    }
    public void SettingKey(GameObject _Key)
    {
        Key = _Key;
    }

    private void Start()
    {
        isFireOff = false;
        OffValue = Fires[0].GetComponent<ParticleSystem>().startSize;
    }

    private void Update()
    {
        if(isFireOff)
        {
            Fires[2].SetActive(false);

            OffValue -= 0.005f;
            
            for(int i = 0; i < Fires.Length - 1; i++)
            {
                Fires[i].GetComponent<ParticleSystem>().startSize = OffValue;
            }
            
            if (OffValue <= 0)
            {
                isFireOff = false;
                KeyShowing();
                Fires[0].SetActive(false);
            }
        }
    }
    public void TurnOffFire()
    {
        isFireOff = true;
    }
    public void TurnOnFire()
    {
        foreach(var Particle in Fires)
        {
            Particle.GetComponent<ParticleSystem>().Play();
        }
    }

    public void KeyShowing()
    {
        Key.transform.position = transform.position + Vector3.up * 1.5f;
        Key.GetComponent<KeyControl>().KeyShowing();
    }
}

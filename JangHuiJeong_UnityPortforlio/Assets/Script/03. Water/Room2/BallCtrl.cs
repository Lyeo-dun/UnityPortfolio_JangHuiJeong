using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCtrl : MonoBehaviour
{
    private GameObject ObjectMn;
    private Rigidbody Rigid;

    private bool isTrue;

    public bool TrueBall
    {
        get
        {
            return isTrue;
        }
        set
        {
            isTrue = false;
        }
    }

    private void Awake()
    {
        ObjectMn = transform.parent.gameObject;
    }
    void Start()
    {
        Rigid = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if(transform.parent == null)
        {
            transform.parent = ObjectMn.transform;
        }

        // ** 플레이어가 잡지 않는다면 공이 계속 움직일 수 있도록 한다.
        if(!GetComponent<BringItem>().Hold && Rigid.velocity.magnitude < 1.0f) 
        {
            float RandomX, RandomY, RandomZ;

            RandomX = Random.Range(-1f, 1f);
            RandomY = Random.Range(-1f, 1f);
            RandomZ = Random.Range(-1f, 1f);

            Vector3 dir = new Vector3(RandomX, RandomY, RandomZ);

            Rigid.AddForce(dir * 100f);
        }


        //GetComponent<Renderer>().material.color += Color.yellow * 0.1f;
    }
}

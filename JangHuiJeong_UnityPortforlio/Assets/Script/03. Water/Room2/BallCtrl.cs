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

        // ** �÷��̾ ���� �ʴ´ٸ� ���� ��� ������ �� �ֵ��� �Ѵ�.
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

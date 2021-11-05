using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCtrl : ItemControler
{
    private GameObject ObjectMn;

    private bool isDivision;

    [SerializeField] private bool isMoving;
    private Rigidbody Rigid;

    private GameObject ParentsObject;
    [SerializeField] private bool isTrue;

    public bool TrueBall
    {
        get
        {
            return isTrue;
        }
        set
        {
           isTrue = value;
        }
    }

    private void Awake()
    {
        ObjectMn = transform.parent.gameObject;
    }
    void Start()
    {
        if(TrueBall)
            ParentsObject = GameObject.Find("BringBalls");
        Rigid = GetComponent<Rigidbody>();

        isDivision = false;

        isMoving = true;
    }

    public override void EventItem()
    {
        if(!TrueBall)
        {
            if(!isDivision)
            {
                GetComponent<TestDissolveItem>().ChangeDissolveState();
                ObjectMn.GetComponent<ObjectManager>().AddColorBringBall();
                isDivision = true;
            }
        }
    }

    void Update()
    {        
        if(TrueBall)
            if(transform.parent == null)
            {
                transform.parent = ParentsObject.transform;
                isMoving = true;
            }

    }
    private void FixedUpdate()
    {
        if(isMoving)
            BallExercise();
    }

    public void AddColorYellow()
    {
        GetComponent<Renderer>().material.color += Color.yellow * 0.1f;
    }

    void BallExercise()
    {
        if (TrueBall)
            if (GetComponent<BringItem>().Hold)
            {
                isMoving = false;
                return;
            }// ** 잡을 수 있는 공을 잡은 상태라면 공을 움직일 필요가 없으므로 함수를 빠져나간다

        if (Rigid.velocity.magnitude < 1.0f)
        {
            float RandomX, RandomY, RandomZ;

            RandomX = Random.Range(-1f, 1f);
            RandomY = Random.Range(-1f, 1f);
            RandomZ = Random.Range(-1f, 1f);

            Vector3 dir = new Vector3(RandomX, RandomY, RandomZ);

            Rigid.AddForce(dir * 100f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "CountHole")
        {
            if(TrueBall)
            {
                GetComponent<BringItem>().PutItem();

                isMoving = false;
                Rigid.isKinematic = true;

                other.GetComponent<BallsHoleControl>().AddCount();

                gameObject.SetActive(false);
            }
        }
    }
}

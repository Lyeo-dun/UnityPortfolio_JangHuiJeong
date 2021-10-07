using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveController : MonoBehaviour
{
    [SerializeField] private float MoveSpeed;
    [SerializeField] private float RotateSpeed;

    [SerializeField] private GameObject MainCamera;
    private float CameraAngle;

    [SerializeField] private GameObject FlashLight;
    private bool isFlash;

    // ** ���� ����
    private bool Jumping;
    private bool Drop;
    private float JumpSpeed;
    private float OldPositionY;
    private float MaximumY;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        MainCamera = Camera.main.gameObject;
        FlashLight = GameObject.Find("FlashLight");
    }

    void Start()
    {        
        MoveSpeed = 5.0f;
        RotateSpeed = 3.5f;

        FlashLight.SetActive(false);
        isFlash = FlashLight.activeSelf;

        CameraAngle = 0.0f;

        Jumping = false;
        Drop = false;
        JumpSpeed = 0.0f;
        OldPositionY = transform.position.y;
        MaximumY = 5.0f;
    }

    void Update()
    {
        // ** �÷��̾� �̵�
        float Hor = Input.GetAxisRaw("Horizontal");
        float Ver = Input.GetAxisRaw("Vertical");
        
        // ** �÷��̾ �� �������� ���̸� �� ��, �浹��ü�� �����ٸ� �������� �ʵ��� �Ѵ�.
        // ** NavMash�� Collider�θ� ���Ҵ� ���, �÷��̾ ���� ���� ������ ���� ���ϵ��� �浹�ϰ� �Ǹ� �÷��̾ ������ ������ �߻��Ͽ� ���� �÷��̾ �������� ��������
        // ** �÷��̾ ���ư��� ������ �߰��Ͽ� ������ ���� ���� ���̸� ��� �浹��ü�� ���� �Ÿ��� �Ǹ� �������� �ʵ��� ��
        {
            float ObstacleMinDistance = 1.0f;
            RaycastHit hit;
            if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward * Ver), out hit, Mathf.Infinity))
            {
                if (Vector3.Distance(hit.point, transform.position) > ObstacleMinDistance)
                {
                    transform.Translate(0.0f, 0.0f, Ver * MoveSpeed * Time.deltaTime);                   
                }
            }
            
            if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.right * Hor), out hit, Mathf.Infinity))
            {
                if (Vector3.Distance(hit.point, transform.position) > ObstacleMinDistance)
                {
                    transform.Translate(Hor * MoveSpeed * Time.deltaTime, 0.0f, 0.0f);
                }
            }
        }

        if(Input.GetMouseButton(1))
            PlayerRotate();

        if(Input.GetKeyDown(KeyCode.E))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if(Vector3.Distance(transform.position ,hit.transform.position) <= 3.0f)
                    if(hit.transform.tag == "InterectionObject")
                    {
                        hit.transform.gameObject.GetComponent<TestDissolveItem>().ChangeDissolveState();
                    }
            }
        }

        if(Input.GetKeyDown(KeyCode.F))
        {
            isFlash = !isFlash;
            FlashLight.SetActive(isFlash);
        }

        if(Input.GetKeyDown(KeyCode.Space))
            Jump();

        if(Jumping)
        {
            if(Drop)
                transform.Translate(Vector3.up * -JumpSpeed * Time.deltaTime);
            else
            {
                transform.Translate(Vector3.up * JumpSpeed * Time.deltaTime);

                if (MaximumY + OldPositionY <= transform.position.y)
                {
                    Drop = true;
                }
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(Jumping)
            if(Drop)
                if(collision.gameObject)
                    Jumping = false;
    }
    private void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.tag != "Ground")
            Jumping = true;
    }

    // ** ���콺 ��ġ�� ���� �÷��̾ �� ������ ���� ����
    void PlayerRotate()
    {
        float MouseX = Input.GetAxis("Mouse X");
        transform.Rotate(Vector3.up * MouseX * RotateSpeed);

        float MouseY = Input.GetAxis("Mouse Y");
        CameraAngle -= MouseY * RotateSpeed;
        CameraAngle = Mathf.Clamp(CameraAngle, -90, 90);

        // ** �θ� �ִ� ��� local�� ����
        MainCamera.transform.localEulerAngles = Vector3.right * CameraAngle;    
    }

    // ** ������ ����
    void Jump()
    {
        if (Jumping)
            return;

        Jumping = true;
        Drop = false;
        JumpSpeed = 8.0f;
        OldPositionY = transform.position.y;
    }
    
}

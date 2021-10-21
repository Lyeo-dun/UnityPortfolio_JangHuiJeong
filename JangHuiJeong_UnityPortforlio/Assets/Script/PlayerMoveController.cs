using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveController : MonoBehaviour
{
    [SerializeField] private float MoveSpeed;
    [SerializeField] private float RotateSpeed;
    //float RotationPlayerValue; // ** �ε巯�� ȸ�� ����
    private float InterectionDistance;

    [SerializeField] private GameObject MainCamera;
    private float CameraAngle;
    private Vector3 CameraPos;

    [SerializeField] private GameObject FlashLight;
    private bool isFlash;

    // ** ���� ����
    [SerializeField] private bool Jumping;
    private Rigidbody Rigid;

    [SerializeField] private GameObject PressEKeyUI;


    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        MainCamera = Camera.main.gameObject;
        FlashLight = GameObject.Find("FlashLight");
        PressEKeyUI = GameObject.Find("PressEKeyUI");
        Rigid = GetComponent<Rigidbody>();
    }

    void Start()
    {        
        MoveSpeed = 5.0f;
        RotateSpeed = 3.5f;
        //RotationPlayerValue = 0;
        InterectionDistance = 1.5f;

        FlashLight.SetActive(false);
        isFlash = FlashLight.activeSelf;

        CameraAngle = 0.0f;

        CameraPos = new Vector3(0.0f, 0.5f, 0.0f);

        PressEKeyUI.SetActive(false);
    }

    void Update()
    {
        // ** ��ü���� ��ȣ�ۿ�
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            int _LayerMask = 1 << LayerMask.NameToLayer("Interaction");

            if(PressEKeyUI != null)
            {
                PressEKeyUI.SetActive(false); // ** ���ǹ� ���� Ȱ��ȭ�� ���θ� ������ �������� �ʴ� �̻��� ��� false ���·� ������ �� �ִ�.
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, _LayerMask))
                {
                    if(Vector3.Distance(hit.point, transform.position) <= InterectionDistance)
                    {
                        PressEKeyUI.SetActive(true);
                    }
                }
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                if(Physics.Raycast(ray, out hit, Mathf.Infinity))
                {
                    if(Vector3.Distance(transform.position ,hit.point) <= InterectionDistance)
                    {
                        if(hit.transform.tag == "Clock")
                        {
                            hit.transform.gameObject.GetComponent<ClockControl>().DissolveAlarm();
                        }
                        if(hit.transform.tag == "Door") 
                        {
                            hit.transform.gameObject.GetComponent<Door>().DoorCtl();
                        }
                    }                
                }
            }

        }

        if(Input.GetKeyDown(KeyCode.F))
        {
            isFlash = !isFlash;
            FlashLight.SetActive(isFlash);
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            Jumping = true;            
        }       
    }
    private void FixedUpdate()
    {
        // ** �÷��̾� �̵�
        float Hor = Input.GetAxisRaw("Horizontal");
        float Ver = Input.GetAxisRaw("Vertical");

        // ** �÷��̾ �� �������� ���̸� �� ��, �浹��ü�� �����ٸ� �������� �ʵ��� �Ѵ�.
        // ** NavMash�� Collider�θ� ���Ҵ� ���, �÷��̾ ���� ���� ������ ���� ���ϵ��� �浹�ϰ� �Ǹ� �÷��̾ ������ ������ �߻��Ͽ� ���� �÷��̾ �������� ��������
        // ** �÷��̾ ���ư��� ������ �߰��Ͽ� ������ ���� ���� ���̸� ��� �浹��ü�� ���� �Ÿ��� �Ǹ� �������� �ʵ��� ��
        {
            float ObstacleMinDistance = 0.5f;
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward * Ver), out hit, Mathf.Infinity))
            {
                if (Vector3.Distance(hit.point, transform.position) > ObstacleMinDistance)
                {
                    transform.Translate(0.0f, 0.0f, Ver * MoveSpeed * Time.deltaTime);
                }

                if (hit.transform.gameObject.GetComponent<Collider>().isTrigger == true)
                {
                    transform.Translate(0.0f, 0.0f, Ver * MoveSpeed * Time.deltaTime);
                }
            }
            else // ** ���� ���� �ִ� ���� ������ ���� ���� ��߶�� ���Ѿ��� ������ �� �ְ� �Ѵ�.
            {
                transform.Translate(0.0f, 0.0f, Ver * MoveSpeed * Time.deltaTime);
            }

            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.right * Hor), out hit, Mathf.Infinity))
            {
                if (Vector3.Distance(hit.point, transform.position) > ObstacleMinDistance)
                {
                    transform.Translate(Hor * MoveSpeed * Time.deltaTime, 0.0f, 0.0f);
                }
                if (hit.transform.gameObject.GetComponent<Collider>().isTrigger == true)
                {
                    transform.Translate(Hor * MoveSpeed * Time.deltaTime, 0.0f, 0.0f);
                }
            }
            else
            {
                transform.Translate(Hor * MoveSpeed * Time.deltaTime, 0.0f, 0.0f);
            }
        }

        if (Jumping)
        {
            Rigid.AddForce(Vector3.up * 6.0f, ForceMode.Impulse);
            Jumping = false;
        }

        if (Input.GetMouseButton(1))
            PlayerRotate();

        MainCamera.transform.position = transform.position + CameraPos;

    }

    // ** ���콺 ��ġ�� ���� �÷��̾ �� ������ ���� ����
    void PlayerRotate()
    {
        float MouseX = Input.GetAxis("Mouse X");
        transform.Rotate(Vector3.up * MouseX * RotateSpeed);     
        
        // ** �ε巯�� ȸ�� ����
        //RotationPlayerValue += MouseX * RotateSpeed;
        //transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(Vector3.up * RotationPlayerValue), 0.2f);

        float MouseY = Input.GetAxis("Mouse Y");
        CameraAngle -= MouseY * RotateSpeed;
        CameraAngle = Mathf.Clamp(CameraAngle, -90, 90);

        // ** �θ� �ִ� ��� EulerAngles�� ���� �۵��� ���� ����. localEulerAngles�� ����
        MainCamera.transform.localEulerAngles = Vector3.right * CameraAngle;    
    } 
}

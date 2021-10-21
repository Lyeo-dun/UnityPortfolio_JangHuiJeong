using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveController : MonoBehaviour
{
    [SerializeField] private float MoveSpeed;
    [SerializeField] private float RotateSpeed;
    //float RotationPlayerValue; // ** 부드러운 회전 보류
    private float InterectionDistance;

    [SerializeField] private GameObject MainCamera;
    private float CameraAngle;
    private Vector3 CameraPos;

    [SerializeField] private GameObject FlashLight;
    private bool isFlash;

    // ** 점프 구현
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
        // ** 물체와의 상호작용
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            int _LayerMask = 1 << LayerMask.NameToLayer("Interaction");

            if(PressEKeyUI != null)
            {
                PressEKeyUI.SetActive(false); // ** 조건문 전에 활성화를 꺼두면 조건이 만족하지 않는 이상은 계속 false 상태로 설정할 수 있다.
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
        // ** 플레이어 이동
        float Hor = Input.GetAxisRaw("Horizontal");
        float Ver = Input.GetAxisRaw("Vertical");

        // ** 플레이어가 갈 방향으로 레이를 쏜 후, 충돌물체와 가깝다면 움직이지 않도록 한다.
        // ** NavMash나 Collider로만 막았던 결과, 플레이어가 가고 싶은 곳으로 가지 못하도록 충돌하게 되면 플레이어가 떨리는 현상이 발생하여 추후 플레이어를 조작하지 않음에도
        // ** 플레이어가 돌아가는 현상을 발견하여 현상을 막기 위해 레이를 쏘고 충돌물체와 일정 거리가 되면 움직이지 않도록 함
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
            else // ** 만약 현재 있는 곳이 막히지 않은 넓은 평야라면 제한없이 움직일 수 있게 한다.
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

    // ** 마우스 위치에 따라 플레이어가 그 방향을 보게 만듦
    void PlayerRotate()
    {
        float MouseX = Input.GetAxis("Mouse X");
        transform.Rotate(Vector3.up * MouseX * RotateSpeed);     
        
        // ** 부드러운 회전 보류
        //RotationPlayerValue += MouseX * RotateSpeed;
        //transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(Vector3.up * RotationPlayerValue), 0.2f);

        float MouseY = Input.GetAxis("Mouse Y");
        CameraAngle -= MouseY * RotateSpeed;
        CameraAngle = Mathf.Clamp(CameraAngle, -90, 90);

        // ** 부모가 있는 경우 EulerAngles를 쓰면 작동이 되지 않음. localEulerAngles로 변경
        MainCamera.transform.localEulerAngles = Vector3.right * CameraAngle;    
    } 
}

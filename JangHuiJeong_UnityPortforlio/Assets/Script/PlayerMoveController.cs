using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveController : MonoBehaviour
{
    [SerializeField] private float MoveSpeed;
    [SerializeField] private float RotateSpeed;
    //float RotationPlayerValue; // ** 부드러운 움직임을 위한 변수
    private float InterectionDistance;

    [SerializeField] private GameObject MainCamera;
    private float CameraAngle;
    private Vector3 CameraPos;

    // ** 점프 관련
    [SerializeField] private bool Jumping;
    private int JumpCount;
    private Rigidbody Rigid;

    [SerializeField] private GameObject PressEKeyUI;

    [SerializeField] private GameObject BringGameObjectPosition;
    [SerializeField] private GameObject HoldItem;

    public bool PlayerGrab()
    {
        if (HoldItem)
            return true;

        return false;
    }

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        MainCamera = Camera.main.gameObject;
        PressEKeyUI = GameObject.Find("PressEKeyUI");
        Rigid = GetComponent<Rigidbody>();
        BringGameObjectPosition = GameObject.Find("Player/Main Camera/BringObject");
    }

    void Start()
    {
        if (GameManager.GetInstance().SceneNumber == 2 && !GameManager.GetInstance().isInRoom)
            if (GameManager.GetInstance().PlayerSettingPos)
            {
                transform.position = GameManager.GetInstance().PlayerPos;
                transform.rotation = new Quaternion(0.0f, 180.0f, 0.0f, 0.0f);
            }

        MoveSpeed = 5.0f;
        RotateSpeed = 3.5f;
        //RotationPlayerValue = 0;
        InterectionDistance = 2.0f;

        CameraAngle = 0.0f;
        CameraPos = new Vector3(0.0f, 0.5f, 0.0f);

        HoldItem = null;

        Jumping = false;
        JumpCount = 0;

        PressEKeyUI.SetActive(false);
    }

    void Update()
    {
        // ** 현재 인터렉션 할 수 있는지 UI 띄움
        //if(!GameManager.GetInstance().OnUI)
        {
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (HoldItem == null)
                {
                    int _LayerMask = 1 << LayerMask.NameToLayer("Interaction") | 1 << LayerMask.NameToLayer("Ball");
                    if (PressEKeyUI != null)
                    {
                        PressEKeyUI.SetActive(false); // ** UI 띄우기
                        if (Physics.Raycast(ray, out hit, Mathf.Infinity, _LayerMask))
                        {
                            if(Vector3.Distance(hit.point, transform.position) <= InterectionDistance)
                            {
                                PressEKeyUI.SetActive(true);
                            }
                        }
                    }
                }
                else
                {
                    PressEKeyUI.SetActive(true);
                }

                if (Input.GetKeyDown(KeyCode.E))
                {
                    if(HoldItem == null)
                    {

                        if(Physics.Raycast(ray, out hit, Mathf.Infinity))
                        {
                            if(Vector3.Distance(transform.position ,hit.point) <= InterectionDistance)
                            {
                                if(hit.transform.tag == "Clock")
                                {
                                    if(!GameManager.GetInstance().ClockEventEnd)
                                        hit.transform.gameObject.GetComponent<ClockControl>().EventClock();

                                    else
                                    {
                                        HoldItem = hit.transform.gameObject;
                                        HoldItem.gameObject.GetComponent<LastAlarmControl>().EventClock(BringGameObjectPosition);
                                    }
                                }
                                if(hit.transform.tag == "Door") 
                                {
                                    hit.transform.gameObject.GetComponent<Door>().DoorCtl();
                                }
                                if (hit.transform.tag == "Key")
                                {
                                    if(GameManager.GetInstance().SceneNumber == 1)
                                        hit.transform.parent.gameObject.GetComponent<KeyControl>().KeyEvent();
                                    else
                                    {
                                        hit.transform.gameObject.GetComponent<KeyControl>().KeyEvent();
                                    }

                                }
                                if(hit.transform.tag == "Item")
                                {
                                    if(hit.transform.gameObject.GetComponent<BringItem>())
                                    {
                                        HoldItem = hit.transform.gameObject;
                                        HoldItem.gameObject.GetComponent<BringItem>().EventItem(BringGameObjectPosition);
                                    }

                                    hit.transform.gameObject.GetComponent<ItemControler>().EventItem();
                                }
                                if(hit.transform.tag == "Bring")
                                {
                                    HoldItem = hit.transform.gameObject;
                                    HoldItem.gameObject.GetComponent<BringItem>().EventItem(BringGameObjectPosition);
                                }
                                if(hit.transform.tag == "Switch")
                                {
                                    hit.transform.gameObject.GetComponent<SwitchCtrl>().ViewEyes();
                                }
                            }
                        }
                    }
                    else
                    {
                        if(HoldItem.gameObject.GetComponent<LastAlarmControl>())
                          HoldItem.gameObject.GetComponent<LastAlarmControl>().EventClock();

                        if (HoldItem.gameObject.GetComponent<BringItem>())
                          HoldItem.gameObject.GetComponent<BringItem>().EventItem(BringGameObjectPosition);
                        
                          HoldItem = null;
                    }
                }

            }

            if(BringGameObjectPosition.transform.childCount <= 0)
            {
                HoldItem = null; // ** 물건을 들고있어야 할 위치 게임 오브젝트에 자식 오브젝트가 없다면 들고 있는 아이템은 없으므로 null로 변경한다 
            }

            if(Input.GetKeyDown(KeyCode.Space))
            {
                if(JumpCount == 0)
                    Jumping = true;
            }
        }
    }
    private void FixedUpdate()
    {
        //if(!GameManager.GetInstance().OnUI)
        {
            // ** 플레이어 움직임
            float Hor = Input.GetAxisRaw("Horizontal");
            float Ver = Input.GetAxisRaw("Vertical");

            // ** Collider나 NavMesh로 제어하면 벽에 부딪힌 후 벽 쪽으로 이동하려 할 시 캐릭터가 덜덜 떨리는 현상이 있음
            // ** 따라서 가려는 방향에 벽이 있다면 일정 거리를 두게 만듦
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
                else // ** 부딪히는 벽이 없다는 것은 넓은 평야라는 것이기 때문에 자유롭게 움직이게 한다
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

            Jump();

            if (Input.GetMouseButton(1))
                PlayerRotate();

            MainCamera.transform.position = transform.position + CameraPos;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        JumpCount = 0;
    }
    // ** 플레이어 회전
    void PlayerRotate()
    {
        float MouseX = Input.GetAxis("Mouse X");
        transform.Rotate(Vector3.up * MouseX * RotateSpeed);     
        
        // ** 부드러운 회전과 관련
        //RotationPlayerValue += MouseX * RotateSpeed;
        //transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(Vector3.up * RotationPlayerValue), 0.2f);

        float MouseY = Input.GetAxis("Mouse Y");
        CameraAngle -= MouseY * RotateSpeed;
        CameraAngle = Mathf.Clamp(CameraAngle, -90, 60);

        // ** EulerAngles을 쓸 시 카메라는 회전하지만 플레이어는 회전하지 않음
        // ** 카메라의 회전만 담당해야하므로 localEulerAngles로 변경
        MainCamera.transform.localEulerAngles = Vector3.right * CameraAngle;    
    } 

    void Jump()
    {
        if (!Jumping)
            return;

        Rigid.AddForce(Vector3.up * 6.0f, ForceMode.Impulse);
        Jumping = false;
        JumpCount = 1;
    }
}

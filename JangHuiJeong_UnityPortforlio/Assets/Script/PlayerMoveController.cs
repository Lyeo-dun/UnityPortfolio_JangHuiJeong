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

    private void Awake()
    {
        MainCamera = Camera.main.gameObject;
        FlashLight = GameObject.Find("FlashLight");
    }

    void Start()
    {
        MoveSpeed = 5.0f;
        RotateSpeed = 3.5f;

        FlashLight.SetActive(false);
        isFlash = FlashLight.activeSelf;
    }

    void Update()
    {
        // ** 플레이어 이동
        float Hor = Input.GetAxisRaw("Horizontal");
        float Ver = Input.GetAxisRaw("Vertical");
        transform.Translate(Hor * MoveSpeed * Time.deltaTime, 0.0f, Ver * MoveSpeed * Time.deltaTime);

        // ** 시작했을 때 마우스가 갑자기 어디론가 팍 튀어버리는 현상이 있음
        // ** 우선 현상 방지를 위해 우클릭을 하고 있을 시에만 시야 회전
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
    }

    // ** 마우스 위치에 따라 플레이어가 그 방향을 보게 만듦
    void PlayerRotate()
    {
        float MouseX = Input.GetAxis("Mouse X");
        transform.Rotate(Vector3.up * MouseX * RotateSpeed);

        float MouseY = Input.GetAxis("Mouse Y");
        CameraAngle -= MouseY * RotateSpeed;
        CameraAngle = Mathf.Clamp(CameraAngle, -90, 90);
        // ** 부모가 있는 경우 local로 변경
        MainCamera.transform.localEulerAngles = Vector3.right * CameraAngle;
    }
}

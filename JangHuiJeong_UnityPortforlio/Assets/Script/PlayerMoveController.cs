using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveController : MonoBehaviour
{
    [SerializeField] private float MoveSpeed;
    [SerializeField] private float RotateSpeed;

    [SerializeField] private GameObject MainCamera;
    private float CameraAngle;

    private void Awake()
    {
        MainCamera = Camera.main.gameObject;
    }

    void Start()
    {
        MoveSpeed = 5.0f;
        RotateSpeed = 3.5f;
    }

    void Update()
    {
        // ** �÷��̾� �̵�
        float Hor = Input.GetAxisRaw("Horizontal");
        float Ver = Input.GetAxisRaw("Vertical");
        transform.Translate(Hor * MoveSpeed * Time.deltaTime, 0.0f, Ver * MoveSpeed * Time.deltaTime);

        // ** �������� �� ���콺�� ���ڱ� ���а� �� Ƣ������� ������ ����
        // ** �켱 ���� ������ ���� ��Ŭ���� �ϰ� ���� �ÿ��� �þ� ȸ��
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
}

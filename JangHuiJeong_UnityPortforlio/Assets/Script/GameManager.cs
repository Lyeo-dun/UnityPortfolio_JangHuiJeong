using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    static GameManager _Instance = null;
    public static GameManager GetInstance()
    {
        return _Instance;
    }

    [SerializeField] private int _SceneNumber;
    public int SceneNumber
    {
        get
        {
            return _SceneNumber;
        }
    }

    [Header("Chapter 1")]
    [SerializeField] private int PortalCount;
    public bool ViewText;

    [Header("Chapter 2")]
    [SerializeField] private bool _isClockEvent;
    [SerializeField] private bool isClockEventEnd; // ** ������ ���� �̺�Ʈ ���� üũ
    private bool isHaveKey;
    private bool _GoThirdStage;

    [Header("Chapter 3")]
    [SerializeField] private int RoomNumber;
    [SerializeField] private List<int> LastPassWord;
    [SerializeField] private bool _isInRoom;
    private Vector3 PlayerRepawnPos;
    private bool WallsEvent;

    private void Awake()
    {
        if (_Instance == null)
        {
            _Instance = this;
            DontDestroyOnLoad(this.gameObject);
            // ** ���������� �Ѿ�� �ϴ��� ��� �����ϵ��� �Ѵ�.
        }
        else 
        {
        // ** �̹� ����� ���� �Ŵ����� ���� ��� ���� �ִ� ���� ������� �ȵǹǷ� �ڽŰ� ����� �ν��Ͻ��� ���Ͽ� �ڽ��� �Ŀ� ������ ���� �Ŵ������ �ڱ� �ڽ��� �����Ѵ�.
            if(this != _Instance)
                Destroy(gameObject);
        }
    }

    void Start()
    {
        _SceneNumber = SceneManager.GetActiveScene().buildIndex;

        { // ** chap 1
            PortalCount = 0;
            ViewText = false;
        }

        { // ** chap 2
            _isClockEvent = false;
            isHaveKey = false;
            isClockEventEnd = false;
            _GoThirdStage = false;
        }

        {
            RoomNumber = 0; // ** 0�� ���� ���� �� ���� ���� �ʴ� ���¸� �ǹ��Ѵ�.
            PlayerRepawnPos = new Vector3(0.0f, 1.0f, 20.0f);
            WallsEvent = false;
        }
    }


    // ** Scene ����
    public void NextStage(int _Index = 1)
    {
        _SceneNumber += _Index;
        SceneManager.LoadScene(_SceneNumber);
    }

    // ** chap 1
    public int GetPortalCount()
    {
        if (PortalCount >= 2)
            ViewText = true;
        
        return PortalCount;
    }

    public void AddPortalCount(int _AddNum = 1)
    {
        PortalCount += _AddNum;
    }

    // ** chap 2
    public bool HaveKey
    {
        get
        {
            return isHaveKey;
        }
        set
        {
            isHaveKey = value;
        }
    }
    public bool ClockEventEnd
    {
        get
        {
            return isClockEventEnd;
        }
        set
        {
            isClockEventEnd = value;
        }
    }
    public bool ClockEventState
    {
        get
        {
            return _isClockEvent;
        }
        set
        {
            _isClockEvent = value;
        }
    }
    public bool GoThirdStage
    {
        get
        {
            return _GoThirdStage;
        }
        set
        {
            _GoThirdStage = value;
        }
    }

    // ** Chap 3
    public int RoomNum
    {
        set
        {
            RoomNumber = value;
        }
        get
        {
            return RoomNumber;
        }
    }
    public bool isInRoom
    {
        set
        {
            _isInRoom = value;
        }
        get
        {
            return _isInRoom;
        }
    }
    public Vector3 PlayerPos
    {
        get
        {
            return PlayerRepawnPos;
        }
    }

    public bool PlayerSettingPos
    {
        get
        {
            return WallsEvent;
        }
    }
    public void InRoom()
    {
        _SceneNumber = 2; // ** chap 3���� ���ư� ��
        
        SceneManager.LoadScene(RoomNumber + SceneNumber);
    }
    public void OutRoom()
    {
        SceneManager.LoadScene(SceneNumber);
    }
    public void SettingPassword(int _Index)
    {
        LastPassWord.Add(_Index);
    }
}

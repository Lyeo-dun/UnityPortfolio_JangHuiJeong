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
    private bool isHaveKey;
    [SerializeField] private bool _isClockEvent;
    [SerializeField] private bool isClockEventEnd; // ** ������ ���� �̺�Ʈ ���� üũ
    private bool _GoThirdStage;

    //Chapter 3
    [SerializeField] private int RoomNumber;

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
    }


    // ** Scene ����
    public void NextStage()
    {
        _SceneNumber += 1;
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
}

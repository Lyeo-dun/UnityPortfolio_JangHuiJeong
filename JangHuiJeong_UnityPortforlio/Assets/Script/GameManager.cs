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
        set
        {
            _SceneNumber = value;
        }
        get
        {
            return _SceneNumber;
        }
    }

    private bool _isOnUI;

    [Header("Chapter 1")]
    [SerializeField] private int PortalCount;
    public bool ViewText;

    [Header("Chapter 2")]
    [SerializeField] private bool _isClockEvent;
    [SerializeField] private bool isClockEventEnd; // ** 마지막 최종 이벤트 시작 체크
    private bool isHaveKey;
    private bool _GoThirdStage;

    [Header("Chapter 3")]
    [SerializeField] private int RoomNumber;
    [SerializeField] private List<int> LastPassWord;
    [SerializeField] private bool _isInRoom;
    [SerializeField] private int _LastRoomNumber;
    [SerializeField] private int NextSceneIndex;
    private Vector3 PlayerRepawnPos;
    private bool WallsEvent;

    [SerializeField] private bool _isNextRoom;

    private void Awake()
    {
        if (_Instance == null)
        {
            _Instance = this;
            DontDestroyOnLoad(this.gameObject);
            // ** 다음씬으로 넘어간다 하더라도 계속 존재하도록 한다.
        }
        else 
        {
        // ** 이미 선언된 게임 매니저가 있을 경우 원래 있던 값이 사라지면 안되므로 자신과 저장된 인스턴스를 비교하여 자신이 후에 생성된 게임 매니저라면 자기 자신을 삭제한다.
            if(this != _Instance)
                Destroy(gameObject);
        }
    }

    void Start()
    {
        Initialized();
    }

    public void Initialized()
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

        { // ** chap 3
            RoomNumber = 0; // ** 0은 현재 룸을 한 번도 가지 않는 상태를 의미한다.
            PlayerRepawnPos = new Vector3(0.0f, 1.7f, 20.0f);
            WallsEvent = false;

            NextSceneIndex = 6;
            _LastRoomNumber = (NextSceneIndex - 3) - 1;

            _isNextRoom = false;
        }
    }

    // ** Scene 관리
    public void NextStage(int _Index = 1)
    {
        _SceneNumber += _Index;
        SceneManager.LoadScene(_SceneNumber);
    }
    
    public bool OnUI
    {
        set
        {
            _isOnUI = value;
        }
        get
        {
            return _isOnUI;
        }
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
        set
        {
            WallsEvent = value;
        }
        get
        {
            return WallsEvent;
        }
    }
    public int LastRoomNum
    {
        get
        {
            return _LastRoomNumber;
        }
    }
    public bool isNextRoom
    {
        set
        {
            _isNextRoom = value;
        }
        get
        {
            return _isNextRoom;
        }
    }
    public int PasswordLength
    {
        get { return LastPassWord.Count; }
    }

    public void InRoom()
    {
        _SceneNumber = 3; // ** chap 3으로 돌아갈 것
        
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

    public bool CompairPassword(List<int> _InputPassword)
    {
        for(int i = 0; i < 2; i++)
        {
            if (LastPassWord[i] != _InputPassword[i])
                return false;
        }
        return true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    static GameManager _Instance = null;

    private int SceneNumber;

    [Header("Chapter 1")]
    [SerializeField] private int PortalCount;
    public bool ViewText;

    [Header("Chapter 2")]
    private bool isHaveKey;
    [SerializeField] private bool _isClockEvent;
    private bool isClockEventEnd;

    public static GameManager GetInstance()
    {
        return _Instance;
    }

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
        { // ** chap 1
            PortalCount = 0;
            SceneNumber = 0;
            ViewText = false;
        }

        { // ** chap 2
            _isClockEvent = false;
            isHaveKey = false;
            isClockEventEnd = false;
        }
    }

    // ** Scene 관리
    public void NextStage()
    {
        SceneNumber += 1;
        SceneManager.LoadScene(SceneNumber);
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
}

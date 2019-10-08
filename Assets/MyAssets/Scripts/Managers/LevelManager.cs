using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private int m_iLevelIndex;
    public int LevelIndex { get { return m_iLevelIndex; } }

    private static bool m_bTutorialTime;
    public static bool TutorialTime { get { return m_bTutorialTime; } }
        
    //time, in seconds
    [SerializeField] private float m_fTimer = 0;
    public int Timer { get { return (int)m_fTimer; } }

    //tree counters
    //The level is pretty much an observer
    [SerializeField] private int m_iTrees = 0;
    public int Trees { get { return m_iTrees; } }

    private GameObject[] m_aTrees;
    public int AllTrees { get { return m_aTrees.Length; } }

    private GameObject m_level;

    void Awake() {

        if (GameManager.Instance.CurrentState == GameStates.LVL1) m_bTutorialTime = true;
        else m_bTutorialTime = false;
        m_aTrees = GameObject.FindGameObjectsWithTag("Tree");    
    }

    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (m_bTutorialTime || GameManager.Instance.CurrentState != GameStates.PAUSE) m_fTimer += Time.deltaTime;
    }

    void OnReset() {

        m_fTimer = 0;
        m_iTrees = 0;
    }

    public void RequestTreeFall() {
        m_iTrees++;
    }
}

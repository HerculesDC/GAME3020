using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    //time, in seconds
    [SerializeField] private int m_iLevelIndex;
    public int LevelIndex { get { return m_iLevelIndex; } }

    private float m_fTimer = 0;
    public int Timer { get { return (int)m_fTimer; } }

    [SerializeField] private int m_iTrees;
    public int Trees { get { return m_iTrees; } }
    
    void Awake() {

    
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        m_fTimer += Time.deltaTime;
    }

    void Reset() {

        m_fTimer = 0;
        m_iTrees = 0;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager m_instance = null;
    public static GameManager Instance { get { return m_instance; } }

    private GameStates m_previousState; //memento state
    private  GameStates m_gState;
    public GameStates CurrentState { get { return m_gState; } }

    private Scene m_scene;

    void Awake() {
        if (!m_instance) m_instance = this;
        else if (m_instance != this) m_instance = this;

        m_scene = SceneManager.GetActiveScene();

        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (m_scene.buildIndex == 0) {
            if (Input.GetMouseButtonDown(0)) {
                SceneManager.LoadScene(1);
            }
        }
    }
}

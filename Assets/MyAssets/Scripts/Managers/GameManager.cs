using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager m_instance = null;
    public static GameManager Instance { get { return m_instance; } }

    //FOR VISUALIZATION PURPOSES ONLY
    [SerializeField] private GameStates m_gPrevState; //memento state
    [SerializeField] private  GameStates m_gCurState;
    public GameStates CurrentState { get { return m_gCurState; } }

    private Scene m_scene;

    void Awake() {

        if (!m_instance) m_instance = this;
        else if (m_instance != this) m_instance = this;

        m_scene = SceneManager.GetActiveScene();

        m_gCurState = m_gPrevState = GameStates.INTRO;

        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        LevelLoader();
        CheckPause();
    }

    void LevelLoader() { 

        m_scene = SceneManager.GetActiveScene();

        //To do: Rethink using GetButton. Looks a tad more expensive than GetKey
        if (m_scene.buildIndex == 0) {
            if (Input.GetButtonDown("Pause")){

                m_gCurState = GameStates.LVL1;
                SceneManager.LoadScene(1);
            }
        }
        if (m_scene.buildIndex == 1) {
            if (Input.GetKey(KeyCode.JoystickButton0)) {

                /*
                m_gPrevState = m_gCurState;
                m_gCurState = GameStates.LOSE;
                SceneManager.LoadScene(2);
                */
            }
        }
        UIManager.Instance.OnLevelChange();
    }

    void CheckPause() {

        //will require refining
        if (m_gCurState == GameStates.LVL1 ||
            m_gCurState == GameStates.LVL2 ||
            m_gCurState == GameStates.LVL3) {

            if (Input.GetKeyDown(KeyCode.Return) || Input.GetButtonDown("Pause")) {
                m_gPrevState = m_gCurState;
                m_gCurState = GameStates.PAUSE;
            }
        }
        if (m_gCurState == GameStates.PAUSE &&
                (m_gCurState == GameStates.LVL1 ||
                 m_gCurState == GameStates.LVL2 ||
                 m_gCurState == GameStates.LVL3)) {

            m_gCurState = m_gPrevState;
        }
    }
}

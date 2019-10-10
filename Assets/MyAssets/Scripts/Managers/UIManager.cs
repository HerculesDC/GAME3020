using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    private static UIManager m_instance;
    public static UIManager Instance { get { return m_instance; } }

    private Canvas m_canvas = null;
    //Question: is it better to make an array from the get-go,
    //or try and find things dynamically?
    [SerializeField] private Transform[] m_gUIElements;

    [SerializeField] private LevelManager m_level = null;

    void Awake() {

        if (!m_instance) m_instance = this;
        if (m_instance != this) m_instance = this;

        if (!m_canvas) m_canvas = this.gameObject.GetComponent<Canvas>();

        m_gUIElements = this.gameObject.GetComponentsInChildren<Transform>(true); //true refers to "include inactive"

        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        DetectScene();
    }

    // Update is called once per frame
    void Update()
    {
        DetectScene();
        UpdateUI();
    }

    //WILL REQUIRE REFACTORING
    void DetectScene() {

        foreach (Transform t in m_gUIElements) {

            if (t.gameObject.name == "Canvas") t.gameObject.SetActive(true);
            else t.gameObject.SetActive(false);
        }

        switch (GameManager.Instance.CurrentState) {
            case GameStates.INTRO:
                foreach (Transform t in m_gUIElements) {
                    if (t.gameObject.tag == "INTRO_UI") t.gameObject.SetActive(true);
                }
                break;
            case GameStates.LVL1:
            case GameStates.LVL2:
            case GameStates.LVL3:
                foreach (Transform t in m_gUIElements) {
                    if (t.gameObject.tag == "LEVEL_UI") t.gameObject.SetActive(true);
                    if (m_level.Warning && t.gameObject.tag == "WARNING_UI") t.gameObject.SetActive(true);
                    if (m_level.Snapped && t.gameObject.tag == "SNAPPED_UI") t.gameObject.SetActive(true);
                }
                break;
            default:
                break;
        
        }
        /*
        if (GameManager.Instance.CurrentState == GameStates.LVL1 ||
            GameManager.Instance.CurrentState == GameStates.LVL2 ||
            GameManager.Instance.CurrentState == GameStates.LVL3)
        {
            foreach (Text t in m_aTexts) {
                if (t != null && t.gameObject.tag != "LEVEL_UI" && t.isActiveAndEnabled) {
                    t.SetActive( false;
                }
                if (t != null && t.gameObject.tag == "LEVEL_UI" && !t.isActiveAndEnabled) {
                    t.SetActive( true;
                }
            }
        }
        if (GameManager.Instance.CurrentState == GameStates.INTRO ||
            GameManager.Instance.CurrentState == GameStates.START ||
            GameManager.Instance.CurrentState == GameStates.LOSE  ||
            GameManager.Instance.CurrentState == GameStates.WIN) {
            foreach (Text t in m_aTexts) {
                if (t != null && t.gameObject.tag == "MENU_UI" && !t.isActiveAndEnabled) {
                    t.SetActive( true;
                }
                if (t != null && t.gameObject.tag != "MENU_UI" && t.isActiveAndEnabled){
                    t.SetActive( false;
                }
            }
        }
        if (GameManager.Instance.CurrentState == GameStates.PAUSE) {
            foreach (Text t in m_aTexts) {
                if (t != null && t.gameObject.tag == "PAUSE_UI" && !t.isActiveAndEnabled) {
                    t.SetActive( true;
                }
                if (t != null && t.gameObject.tag != "PAUSE_UI" && t.isActiveAndEnabled) {
                    t.SetActive( false;
                }
            }
        }
        */
    }

    void UpdateUI() {

        if (GameManager.Instance.CurrentState == GameStates.LVL1 ||
            GameManager.Instance.CurrentState == GameStates.LVL2 ||
            GameManager.Instance.CurrentState == GameStates.LVL3) {

            if (m_level) {
                foreach (Transform t in m_gUIElements) {

                    if (t.name == "Timer") t.GetComponent<Text>().text = "Time: " + m_level.Timer;
                    if (t.name == "Trees") t.GetComponent<Text>().text = "Trees: " + m_level.Trees + "/" +m_level.AllTrees;
                }
            }
        }
    }

    public void OnLevelChange() {

        //Looks for a gameObject containing a Level Manager
        GameObject temp = GameObject.Find("LevelScripts");

        if (temp) {
            //There being such a Level Manager, 
            //it compares with the level manager it currently holds,
            //ignoring it if it's the same
            LevelManager temp_level = temp.GetComponent<LevelManager>();
            if (temp_level != m_level) {
                m_level = temp_level;
            }
        }
        else {
            m_level = null;
        }
    }
    /*
    public void OnPause() {
        foreach (Text t in m_aTexts) {
            if (t && t.gameObject.tag != "PAUSE_UI" && t.isActiveAndEnabled) t.SetActive( false;
            if (t && t.gameObject.tag == "PAUSE_UI" && !t.isActiveAndEnabled) t.SetActive( true;
        }        
    }

    public void OnResume() {
        foreach (Text t in m_aTexts) {
            
            if (t && t.gameObject.tag == "PAUSE_UI" && t.isActiveAndEnabled) t.SetActive( false;
        }
    }
    //*/
}

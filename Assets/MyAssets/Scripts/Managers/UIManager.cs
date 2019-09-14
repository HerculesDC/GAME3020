using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager m_instance;
    public static UIManager Instance { get { return m_instance; } }

    private Canvas m_canvas = null;
    //Question: is it better to make an array from the get-go,
    //or try and find things dynamically?
    [SerializeField] private Text[] m_aTexts;

    void Awake() {

        if (!m_instance) m_instance = this;
        if (m_instance != this) m_instance = this;

        if (!m_canvas) m_canvas = this.gameObject.GetComponent<Canvas>();

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
    }

    void DetectScene() {

        if (GameManager.Instance.CurrentState == GameStates.LVL1 ||
            GameManager.Instance.CurrentState == GameStates.LVL2 ||
            GameManager.Instance.CurrentState == GameStates.LVL3)
        {
            foreach (Text t in m_aTexts) {
                if (t != null && t.isActiveAndEnabled) {
                    t.enabled = false;
                }
            }
        }
        else {
            foreach (Text t in m_aTexts) {
                if (t != null && !t.isActiveAndEnabled) {
                    t.enabled = true;
                }
            }
        }
    }
}

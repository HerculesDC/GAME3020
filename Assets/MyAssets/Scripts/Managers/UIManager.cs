﻿using System.Collections;
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
    [SerializeField] private Text[] m_aTexts;

    [SerializeField] private LevelManager m_level = null;

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
        UpdateUI();
    }

    void DetectScene() {

        if (GameManager.Instance.CurrentState == GameStates.LVL1 ||
            GameManager.Instance.CurrentState == GameStates.LVL2 ||
            GameManager.Instance.CurrentState == GameStates.LVL3)
        {
            foreach (Text t in m_aTexts) {
                if (t != null && t.tag == "MENU_UI" && t.isActiveAndEnabled) {
                    t.enabled = false;

                }
                if (t != null && t.tag == "LEVEL_UI" && !t.isActiveAndEnabled) {
                    t.enabled = true;
                }
            }
        }
        else {
            foreach (Text t in m_aTexts) {
                if (t != null && t.tag == "MENU_UI" && !t.isActiveAndEnabled) {
                    t.enabled = true;
                }
                if (t != null && t.tag == "LEVEL_UI" && t.isActiveAndEnabled){
                    t.enabled = false;
                }
            }
        }
    }

    void UpdateUI() {

        if (GameManager.Instance.CurrentState == GameStates.LVL1 ||
            GameManager.Instance.CurrentState == GameStates.LVL2 ||
            GameManager.Instance.CurrentState == GameStates.LVL3) {

            if (m_level) {
                foreach (Text t in m_aTexts) {

                    if (t.name == "Timer") t.text = "Time: " + m_level.Timer;
                    if (t.name == "Trees") t.text = "Trees: " + m_level.Trees + "/" +m_level.AllTrees;
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
}

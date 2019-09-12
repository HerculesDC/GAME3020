﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager m_instance;
    public static UIManager Instance { get { return m_instance; } }

    private Canvas m_canvas = null;

    void Awake() {

        if (!m_instance) m_instance = this;
        if (m_instance != this) m_instance = this;

        if (!m_canvas) m_canvas = this.gameObject.GetComponent<Canvas>();

        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

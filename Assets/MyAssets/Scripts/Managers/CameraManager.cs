using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private static CameraManager m_instance = null;
    public static CameraManager Instance { get { return m_instance; } }

    private Camera m_cam = null;

    void Awake() {
        if (!m_instance) m_instance = this;
        else if (m_instance != this) m_instance = this;

        if (!m_cam) m_cam = this.gameObject.GetComponent<Camera>();

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

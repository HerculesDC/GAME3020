using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
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
            m_cam.orthographic = false;
            m_cam.clearFlags = CameraClearFlags.Skybox;
        }
        else
        {
            m_cam.orthographic = true;
            m_cam.clearFlags = CameraClearFlags.Color;
        }
    }
}

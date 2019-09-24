using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{

    private static InputManager m_instance = null;
    public static InputManager Instance { get { return m_instance; } }

    [SerializeField] private string m_VerticalLeftAxisName;
    private float m_lAccel;
    public float LeftAccel { get { return m_lAccel; } }
    [SerializeField] private string m_HorizontalLeftAxisName;
    private float m_lTurn;
    public float LeftTurn { get { return m_lTurn; } }
    [SerializeField] private string m_VerticalRightAxisName;
    private float m_rAccel;
    public float RightAccel { get { return m_rAccel; } }
    [SerializeField] private string m_HorizontalRightAxisName;
    private float m_rTurn;
    public float RightTurn { get { return m_rTurn; } }

    [SerializeField] private string m_ConfirmName;
    private bool m_bConfirm;
    public bool Confirm { get { return m_bConfirm; } }
    [SerializeField] private string m_CancelName;
    private bool m_bCancel;
    public bool Cancel { get { return m_bCancel; } }

    void Awake() {
        if (!m_instance) m_instance = this;
        else if (m_instance != this) m_instance = this;

        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        m_lAccel = Input.GetAxis(m_VerticalLeftAxisName);
        m_lTurn = Input.GetAxis(m_HorizontalLeftAxisName);
        m_rAccel = Input.GetAxis(m_VerticalRightAxisName);
        m_rTurn = Input.GetAxis(m_HorizontalRightAxisName);

        m_bConfirm = Input.GetMouseButtonDown(0) || Input.GetButtonDown(m_ConfirmName);
        m_bCancel = Input.GetMouseButton(1) || Input.GetButtonDown(m_CancelName);
    }
}

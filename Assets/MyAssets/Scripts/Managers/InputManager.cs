using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private Tractor m_tractor;
    public Tractor Tractor { get { return m_tractor; } }

    [SerializeField] private string m_VerticalAxisName;
    private float m_Accel;
    public float Accel { get { return m_Accel; } }
    [SerializeField] private string m_HorizontalAxisName;
    private float m_Turn;
    public float Turn { get { return m_Turn; } }
    [SerializeField] private string m_BrakeName;
    private float m_Brake;
    public float Brake { get { return m_Brake; } }

    [SerializeField] private string m_ConfirmName;
    private bool m_bConfirm;
    public bool Confirm { get { return m_bConfirm; } }
    [SerializeField] private string m_CancelName;
    private bool m_bCancel;
    public bool Cancel { get { return m_bCancel; } }

    void Awake() { }

    // Update is called once per frame
    void Update()
    {
        m_Accel = Input.GetAxis(m_VerticalAxisName);
        m_Turn = Input.GetAxis(m_HorizontalAxisName);

        m_Brake = Input.GetAxis(m_BrakeName);
        Debug.Log(m_BrakeName + ": " + m_Brake.ToString());
        m_bConfirm = Input.GetMouseButtonDown(0) || Input.GetButtonDown(m_ConfirmName);
        m_bCancel = Input.GetMouseButton(1) || Input.GetButtonDown(m_CancelName);
    }

    public void SetTractor(Tractor tractor) {
        m_tractor = tractor;
    }
}

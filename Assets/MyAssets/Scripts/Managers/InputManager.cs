using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private Tractor m_tractor;
    public Tractor Tractor { get { return m_tractor; } }
    private AIPlay m_ai = null;

    [SerializeField] private string m_VerticalAxisName;
    private float m_Accel;
    public float Accel { get { return m_Accel; } }
    [SerializeField] private string m_HorizontalAxisName;
    private float m_Turn;
    public float Turn { get { return m_Turn; } }

    /* IMPORTANT!
     * Unity maps joystick buttons as an "array", whereas 
     * Windows maps joystick buttons starting at 
     * 1 for the "southern" button of the right hand
     */
    [SerializeField] private string m_BrakeName;
    private bool m_Brake;
    public bool Brake { get { return m_Brake; } }
    [SerializeField] private string m_ConfirmName;
    private bool m_bConfirm;
    public bool Confirm { get { return m_bConfirm; } }
    [SerializeField] private string m_CancelName;
    private bool m_bCancel;
    public bool Cancel { get { return m_bCancel; } }

    void Awake() {
        if (m_tractor == Tractor.AI && m_ai == null) m_ai = this.gameObject.GetComponent<AIPlay>();
    }

    //enforcing AI check
    void Start() {
        if (m_tractor == Tractor.AI && m_ai == null) m_ai = this.gameObject.GetComponent<AIPlay>();
    }

    // Update is called once per frame
    void Update()
    {
        //enforcing AI check, because I'not confident in Unity's component initialization
        if (m_tractor == Tractor.AI && m_ai == null) m_ai = this.gameObject.GetComponent<AIPlay>();

        if (m_tractor != Tractor.AI) {
            //It may be the case for creating a dead zone for acceleration
            m_Accel = Input.GetAxis(m_VerticalAxisName);
            m_Turn = Input.GetAxis(m_HorizontalAxisName);

            //Brakes have to be applied every frame the brakes are pulled
            m_Brake = Input.GetButton(m_BrakeName);

            m_bConfirm = m_ConfirmName != "" ? Input.GetButtonDown(m_ConfirmName) : false;
            m_bCancel = m_CancelName != "" ? Input.GetButtonDown(m_CancelName) : false;
        }
        else {
            m_Accel = m_ai.Accelerate();
            m_Turn = m_ai.Steer();
            m_Brake = m_ai.Brake();
            
            //"Confirm" and "Cancel" are irrelevant for AI
        }
    }

    public void SetTractor(Tractor tractor) {
        m_tractor = tractor;
    }
}

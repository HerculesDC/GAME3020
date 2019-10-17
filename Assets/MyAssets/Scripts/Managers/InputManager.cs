using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private Tractor m_tTractor;
    public Tractor Tractor { get { return m_tTractor; } }
    private AIPlay m_ai = null;

    [SerializeField] private string m_VerticalAxisName;
    [SerializeField] private float m_fForwardOffset;
    private float m_fAccel;
    public float Accel { get { return m_fAccel; } }
    [SerializeField] private string m_HorizontalAxisName;
    private float m_fTurn;
    public float Turn { get { return m_fTurn; } }

    /* IMPORTANT!
     * Unity maps joystick buttons as an "array", whereas 
     * Windows maps joystick buttons starting at 
     * 1 for the "southern" button of the right hand
     */
    [SerializeField] private string m_BrakeName;
    private bool m_bBrake;
    public bool Brake { get { return m_bBrake; } }

    [SerializeField] private string m_PauseName;
    private bool m_bPause;
    public bool Pause { get { return m_bPause; } }

    [SerializeField] private string m_ConfirmName;
    private bool m_bConfirm;
    public bool Confirm { get { return m_bConfirm; } }
    [SerializeField] private string m_CancelName;
    private bool m_bCancel;
    public bool Cancel { get { return m_bCancel; } }

    void Awake() {
        if (m_tTractor == Tractor.AI && m_ai == null) m_ai = this.gameObject.GetComponent<AIPlay>();
    }

    //enforcing AI check
    void Start() {
        if (m_tTractor == Tractor.AI && m_ai == null) m_ai = this.gameObject.GetComponent<AIPlay>();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_tTractor != Tractor.AI) {

            if (GameManager.Instance.CurrentState == GameStates.LVL1 && LevelManager.TutorialTime) {

                m_bConfirm = m_ConfirmName != "" ? Input.GetButtonDown(m_ConfirmName) : false;
            }
            else if(GameManager.Instance.CurrentState == GameStates.LVL1 ||
                    GameManager.Instance.CurrentState == GameStates.LVL2 ||
                    GameManager.Instance.CurrentState == GameStates.LVL3 &&
                    !LevelManager.TutorialTime)
            {

                //It may be the case for creating a dead zone for acceleration
                m_fAccel = Input.GetAxis(m_VerticalAxisName) + m_fForwardOffset;
                m_fTurn = Input.GetAxis(m_HorizontalAxisName);

                //Brakes have to be applied every frame the brakes are pulled
                m_bBrake = Input.GetButton(m_BrakeName);

                m_bPause = m_PauseName != "" ? Input.GetButtonDown(m_PauseName) : false;
                m_bConfirm = m_ConfirmName != "" ? Input.GetButtonDown(m_ConfirmName) : false;
                m_bCancel = m_CancelName != "" ? Input.GetButtonDown(m_CancelName) : false;
            }
            
        }
        else { //for whatever reason, this is running even when the tractors are NOT AI
            //AI-wise, only movement input is relevant
            if (m_ai) {

                m_fAccel = m_ai.Accelerate();
                m_fTurn = m_ai.Steer();
                m_bBrake = m_ai.Brake();
            }
            else {
                m_ai = this.gameObject.GetComponent<AIPlay>();
            }
        }
    }

    public void SetTractor(Tractor tractor) {
        m_tTractor = tractor;
    }
}

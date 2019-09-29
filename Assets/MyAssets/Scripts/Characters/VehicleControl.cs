using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InputManager))]
[RequireComponent(typeof(Rigidbody))]
public class VehicleControl : MonoBehaviour
{
    [SerializeField] private Tractor m_Tractor;
    private InputManager m_input;    

    [SerializeField] private Vector3 m_vrbOffset;
    private Rigidbody m_rb;

    [SerializeField] private float m_fAcceleration;
    [SerializeField] private float m_fTurn;

    [SerializeField] private WheelCollider[] m_wTurnWheels;
    [SerializeField] private WheelCollider[] m_wAccelWheels;

    void Awake() {
        m_rb = this.gameObject.GetComponent<Rigidbody>();
        m_input = this.gameObject.GetComponent <InputManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        m_rb.centerOfMass += m_vrbOffset;

        if (m_input) m_input.SetTractor(m_Tractor);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
        Turn();
    }

    void Move() {

        if (!m_input.Brake) {
            foreach (WheelCollider w in m_wAccelWheels)
                w.motorTorque = m_fAcceleration * Time.fixedDeltaTime * m_input.Accel;
        }
        Brake();
    }

    void Turn() {
        foreach (WheelCollider w in m_wTurnWheels)
            w.steerAngle = m_fTurn * m_input.Turn;
    }

    void Brake() {
        foreach (WheelCollider w in m_wAccelWheels)
            w.brakeTorque = m_input.Brake ? m_fAcceleration * Time.fixedDeltaTime : 0.0f;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class VehicleControl : MonoBehaviour
{
    [SerializeField] private Tractor m_Tractor;
    [SerializeField] private AIManager m_AI = null;

    [SerializeField] private Vector3 m_vrbOffset;
    private Rigidbody m_rb;

    [SerializeField] private float m_fAcceleration;
    [SerializeField] private float m_fTurn;

    [SerializeField] private WheelCollider[] m_wTurnWheels;
    [SerializeField] private WheelCollider[] m_wAccelWheels;

    void Awake() {
        m_rb = this.gameObject.GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        m_rb.centerOfMass += m_vrbOffset;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
        Turn();
    }

    void Move() {

        foreach (WheelCollider w in m_wAccelWheels) {
            if (m_Tractor == Tractor.LEFT) {
                w.motorTorque = m_fAcceleration * Time.fixedDeltaTime * InputManager.Instance.LeftAccel;
            }
            if (m_Tractor == Tractor.RIGHT) {
                w.motorTorque = m_fAcceleration * Time.fixedDeltaTime * InputManager.Instance.RightAccel;
            }
        }
    }

    void Turn() {
        foreach (WheelCollider w in m_wTurnWheels) {
            if (m_Tractor == Tractor.LEFT) {
                w.steerAngle = m_fTurn * InputManager.Instance.LeftTurn;
            }
            if (m_Tractor == Tractor.RIGHT) {
                w.steerAngle = m_fTurn * InputManager.Instance.RightTurn;
            }
        }
    }
}

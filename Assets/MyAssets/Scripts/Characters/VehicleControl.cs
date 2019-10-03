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
        if (m_input) m_input.SetTractor(m_Tractor);
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
        /* TODO: Think of alternatives to constrain tractors' positions 
         * so as to avoid breaking the chain.
         *  The possibilities (so far) are:
         *      -> "THE CHAIN WILL SNAP!!!" kinda warning
         *      -> Having each tractor know of the other
         *      -> Have the "player manager" constrain those positions
         *      -> Play around with the forces so that the physics system does the managing
         *      (Worth noting: IRL, the chain would break...)
         */

        if (!m_input.Brake)
        {
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
        foreach (WheelCollider w in m_wAccelWheels) {
            w.brakeTorque = m_input.Brake ? 2 * m_fAcceleration : 0.0f;
        }
    }
}

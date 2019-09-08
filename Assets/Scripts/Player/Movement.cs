using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Movement : MonoBehaviour
{
    //Input-related variables
    [SerializeField] private string m_sHorAxisName;
    [SerializeField] private string m_sVertAxisName;

    //Physics movement variables
    [SerializeField] private float m_fMaxVel;
    [SerializeField] private float m_fImpulse;
    [SerializeField] private float m_fMaxAngVel;
    [SerializeField] private float m_fTorque;

    //Physics correction/stabilization variables
    //I'm using ray casting to stabilize the car and count "grounded"
    [SerializeField] private float m_fOffset;
    [SerializeField] private float m_fGravityOverride;
    [SerializeField] private float m_fRayLength;
    [SerializeField] private bool m_bIsGrounded; //***FOR VISUALIZATION PURPOSES ONLY!!!***

    private Rigidbody m_rb;

    void Awake() {
        m_rb = this.gameObject.GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (m_rb) m_rb.centerOfMass = Vector3.down * m_fOffset;
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(this.gameObject.transform.position, Vector3.down);
        RaycastHit hit;

        if (m_rb) {

            if (Physics.Raycast(ray, out hit, m_fRayLength))
                Move();
        }
    }

    void Move() {

        m_rb.AddForce(Vector3.down * m_fGravityOverride);
        Turn();
        Drive();
    }

    void Drive() {

        if (m_rb.velocity.sqrMagnitude < m_fMaxVel)
            m_rb.AddForce(this.gameObject.transform.forward * m_fImpulse * Input.GetAxis(m_sVertAxisName));
    }

    void Turn() {

        if (m_rb.angularVelocity.sqrMagnitude < m_fMaxAngVel)
            m_rb.AddTorque(Vector3.up * m_fTorque * Input.GetAxis(m_sHorAxisName));
    }
}

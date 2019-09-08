using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Movement : MonoBehaviour
{

    [SerializeField] private float maxVel;
    [SerializeField] private float impulse;
    [SerializeField] private float torque;

    private Rigidbody m_rb;

    void Awake() {
        m_rb = this.gameObject.GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (m_rb) {
            //m_rb.AddTorque(this.gameObject.transform.up * torque);
            if (m_rb.velocity.sqrMagnitude < maxVel)
                m_rb.AddForce(this.gameObject.transform.forward * impulse);
        }
    }
}

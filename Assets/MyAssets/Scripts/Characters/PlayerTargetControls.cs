using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(InputManager))]
public class PlayerTargetControls : MonoBehaviour
{
    [SerializeField] private Tractor m_tractor;
    [SerializeField] private float m_fVelocity;
    private InputManager m_im;
    public bool Braking { get { return m_im.Brake; } }
    private Rigidbody m_rb;

    void Awake() {
        m_rb = this.gameObject.GetComponent<Rigidbody>();
        m_im = this.gameObject.GetComponent<InputManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        m_im.SetTractor(m_tractor);
    }

    // Update is called once per frame
    void Update()
    {
        Move();    
    }

    void Move() {
        Vector3 temp = Vector3.zero;
        temp.x += m_im.Turn * m_fVelocity * Time.deltaTime;
        temp.z += m_im.Accel * m_fVelocity * Time.deltaTime;
        //m_rb.position += temp; //DEBUG CODE FOR TESTING!!!
        m_rb.position += !m_im.Brake? temp : Vector3.zero;
    }
}

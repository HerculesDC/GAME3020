using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(InputManager))]
public class PlayerTargetControls : MonoBehaviour
{
    [SerializeField] private Tractor m_tractor;
    [SerializeField] private float m_fVelocity;
    [SerializeField] private Transform m_tTarget; //tracks the tractor it's responsible for
                     private AIPlay m_ai; //to extract the angle
    [SerializeField] private float m_fMaxDistance;
    [SerializeField] private float m_fFactor; //Not 100% sure what this will be about...

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
        m_ai = m_tTarget.gameObject.GetComponent<AIPlay>();
        m_im.SetTractor(m_tractor);
    }

    // Update is called once per frame
    void Update()
    {
        Move();    
    }

    void Move() {
        Vector3 temp = Vector3.zero;
        temp.x = RepositionX();
        temp.z = RepositionZ();
        //m_rb.position += temp; //DEBUG CODE FOR TESTING!!!
        m_rb.position = !m_im.Brake? temp : m_rb.position;
    }

    float RepositionX() {

        float temp = this.gameObject.transform.position.x;
        temp += m_im.Turn * m_fVelocity * Time.deltaTime;
        /*
        if (m_tTarget && m_ai) {
            if (m_tTarget.position.x > this.gameObject.transform.position.x) {

                temp = Mathf.Max(temp, m_tTarget.position.x + (m_fMaxDistance / m_fFactor * Mathf.Cos(m_ai.Angle)));
            }
            else {

                temp = Mathf.Min(temp, m_tTarget.position.x + (m_fMaxDistance / m_fFactor * Mathf.Cos(m_ai.Angle)));
            }
        }
        */
        return temp;
    }

    float RepositionZ() {
        float temp = this.gameObject.transform.position.z;
        temp += m_im.Accel * m_fVelocity * Time.deltaTime;
        /*
        if (m_tTarget && m_ai) {

            if (m_tTarget.position.z > this.gameObject.transform.position.z) {
                
                temp = Mathf.Max(temp, m_tTarget.position.z + (m_fMaxDistance / m_fFactor * Mathf.Sin(m_ai.Angle)));
            }
            else {

                temp = Mathf.Min(temp, m_tTarget.position.z + (m_fMaxDistance / m_fFactor * Mathf.Sin(m_ai.Angle)));
            }
        }
        */
        return temp;
    }
}

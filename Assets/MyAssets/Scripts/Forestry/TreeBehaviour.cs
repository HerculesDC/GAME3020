using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class TreeBehaviour : MonoBehaviour
{
    [SerializeField] private string m_sChain;

    [SerializeField] private float m_fResetTime;
    [SerializeField] private float m_fLowering;

    
    private Rigidbody m_rb;

    void Awake() {

        m_rb = this.gameObject.GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (m_rb) m_rb.centerOfMass = this.gameObject.transform.up * -m_fLowering;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision other) {

        if (other.gameObject.tag == m_sChain) StartCoroutine(Reset());
    }

    IEnumerator Reset(){

        yield return new WaitForSeconds(m_fResetTime);
        m_rb.ResetCenterOfMass();
    }
}
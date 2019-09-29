using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ProvCamFollow : MonoBehaviour
{
    [SerializeField] private GameObject m_gPlayer;
    [SerializeField] private Vector3 m_vOffset;
    private Rigidbody m_rb = null;

    void Awake() {
        m_rb = this.gameObject.GetComponent<Rigidbody>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (m_gPlayer) {
            m_rb.position = m_gPlayer.transform.position + m_vOffset;
            this.gameObject.transform.LookAt(m_gPlayer.transform);
        }
    }
}

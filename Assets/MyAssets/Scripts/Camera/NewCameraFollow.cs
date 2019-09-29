using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class NewCameraFollow : MonoBehaviour
{
    [SerializeField] private GameObject m_gPlayers;
    private PlayerPositioning m_pPositioning; //needed for the distance
    [SerializeField] private Vector3 m_vOffset;

    private Rigidbody m_rb;

    void Awake() {
        m_pPositioning = m_gPlayers? m_gPlayers.GetComponent<PlayerPositioning>(): null;
        m_rb = this.gameObject.GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //I like how this vertical distance works, but the horizontal controls got even more confusing...
        if (m_pPositioning) {
            m_vOffset.y = 0.5f * Mathf.Sqrt(m_pPositioning.TractorDistance());
        }
        this.m_rb.position = m_gPlayers.transform.position + m_vOffset;
        this.gameObject.transform.LookAt(m_gPlayers.transform);
    }
}

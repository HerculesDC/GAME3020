using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineBehaviour : MonoBehaviour
{
    [SerializeField] private Transform m_tLeft;
    [SerializeField] private Transform m_tRight;

    private LineRenderer m_lr;

    void Awake() {

        m_lr = this.gameObject.GetComponent<LineRenderer>();

        if (m_lr) {
            if (m_tLeft) m_lr.SetPosition(0, m_tLeft.position);
            if (m_tRight) m_lr.SetPosition(1, m_tRight.position);
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (m_lr)
        {
            this.gameObject.transform.position = new Vector3((m_tLeft.position.x + m_tRight.position.x) / 2, (m_tLeft.position.y + m_tRight.position.y) / 2, (m_tLeft.position.z + m_tRight.position.z) / 2);
            if (m_tLeft) m_lr.SetPosition(0, m_tLeft.position -  this.gameObject.transform.position);
            if (m_tRight) m_lr.SetPosition(1, m_tRight.position - this.gameObject.transform.position);
        }
    }
}

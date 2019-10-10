﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPositioning : MonoBehaviour
{
    //The idea is to create a point towards which the camera can look at
    //opted to look for Game Objects instead of Transforms, in case I need some other property from them
    [SerializeField] private string m_sLeftName;
    [SerializeField] private GameObject m_gLeft;
    [SerializeField] private string m_sRightName;
    [SerializeField] private GameObject m_gRight;

    [SerializeField] private string m_sLinkName;
    private Transform[] m_gLinks;

    [SerializeField] private float m_fWarningDistance;
    [SerializeField] private float m_fSnapDistance;
    [SerializeField] private float m_fDistance; //***for tractor distance viewing purposes***

    private bool m_bIsWarning;
    public bool IsWarning { get { return m_bIsWarning; } }
    private bool m_bSnapped;
    public bool Snapped { get { return m_bSnapped; } }

    private Vector3 m_vFacing = Vector3.zero; /***FOR READING PURPOSES ONLY***/
    public Vector3 Facing { get { return m_vFacing.normalized; } }

    void Awake() {

        m_vFacing = Vector3.zero;
        if (!m_gLeft) m_gLeft = GameObject.Find(m_sLeftName);
        if (!m_gRight) m_gRight = GameObject.Find(m_sRightName);

        GameObject[]temp = GameObject.FindGameObjectsWithTag(m_sLinkName);
        m_gLinks = new Transform[temp.Length];

        for (int i = 0; i < temp.Length; ++i) m_gLinks[i] = temp[i].transform;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (!m_gLeft) m_gLeft = GameObject.Find(m_sLeftName);
        if (!m_gRight) m_gRight = GameObject.Find(m_sRightName);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (m_gLeft && m_gRight) {

            SetPosition();
            SetFacing();
            UpdateDistances();
        }

        m_fDistance = TractorDistance();

        m_bIsWarning = m_fDistance > m_fWarningDistance;
        m_bSnapped = m_fDistance > m_fSnapDistance;

        CheckSnap();
    }

    void SetPosition() {

        this.gameObject.transform.position = (m_gLeft.transform.position + m_gRight.transform.position) / 2;
        this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x, 1, this.gameObject.transform.position.z);
    }

    void SetFacing() {

        this.gameObject.transform.rotation = Quaternion.Slerp(m_gLeft.transform.rotation, m_gRight.transform.rotation, 0.5f);
        m_vFacing = this.gameObject.transform.position + this.gameObject.transform.forward;
    }

    public float TractorDistance() {

        Vector3 dist = (m_gRight.transform.position - m_gLeft.transform.position);
        return dist.magnitude;
    }

    void UpdateDistances() {
        //think of a mechanism to signal the the number of links has changed.
        //TODO: Refactor these hard-coded numbers...
        m_fWarningDistance = 2.0f * (m_gLinks.Length - 1);
        m_fSnapDistance = m_fWarningDistance + 10.0f;
    }

    private void CheckSnap() {

        if (m_bSnapped) {
            Destroy(m_gRight.GetComponent<HingeJoint>());

            foreach (Transform g in m_gLinks)
                Destroy(g.GetComponent<HingeJoint>());

            DisableInput();
        }
    }

    public void DisableInput() {

        m_gRight.GetComponent<InputManager>().enabled = false;
        m_gLeft.GetComponent<InputManager>().enabled = false;
    }
}

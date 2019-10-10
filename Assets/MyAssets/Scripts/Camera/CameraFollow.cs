using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Camera))]
public class CameraFollow : MonoBehaviour
{
    //opted for the player to pass stuff
    [SerializeField] private string m_sPlayerName;
    [SerializeField] private GameObject m_gPlayer;
                     private PlayerPositioning m_ppPlayer = null;

    [SerializeField] private Vector3 m_vOffset;
    [SerializeField] private float m_fMinDistance;
    [SerializeField] private float m_fDistance;
    [SerializeField] private float m_fMaxDistance;
    [SerializeField] private float m_fApproach;
    

    private Camera m_cam = null;
    private Rigidbody m_rb = null;

    //singleton code
    private static CameraFollow instance = null;
    public static CameraFollow Instance { get { return instance; } }

    void Awake() {

        if (!instance) instance = this;
        else if (instance != this) instance = this;


        m_rb = this.gameObject.GetComponent<Rigidbody>();
        m_cam = this.gameObject.GetComponent<Camera>();

        if (!m_gPlayer) {

            m_gPlayer = GameObject.Find(m_sPlayerName);
            if (m_gPlayer) m_ppPlayer = m_gPlayer.GetComponent<PlayerPositioning>();
            else { }
        }

        //Camera Manager does that already
        //DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        if (!m_gPlayer) {

            m_gPlayer = GameObject.Find(m_sPlayerName);
            if (m_gPlayer) m_ppPlayer = m_gPlayer.GetComponent<PlayerPositioning>();
        }

        m_rb.centerOfMass = Vector3.zero;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //Code for finding player. Will have to be updated, based on game state
        if (!m_gPlayer) {

            m_gPlayer = GameObject.Find(m_sPlayerName);
            if (m_gPlayer) m_ppPlayer = m_gPlayer.GetComponent<PlayerPositioning>();
        }        
        else {
            if (!m_ppPlayer) m_ppPlayer = m_gPlayer.GetComponent<PlayerPositioning>();
            else HandlePlayer();
        }
        
    }

    void HandlePlayer() {
        //Rework this math
        //CHECK AGAIN

        //m_fDistance = Mathf.Clamp((m_ppPlayer.TractorDistance() * m_fApproach), m_fMinDistance, m_fMaxDistance);
        //Vector3 temp = m_gPlayer.transform.forward * -1;
        //temp *= m_fDistance;
        //this.gameObject.transform.position += Vector3.up * 5;
        //this.gameObject.transform.LookAt(m_gPlayer.transform.position);

        m_fDistance = Mathf.Max(m_fMinDistance, m_ppPlayer.TractorDistance());
        m_vOffset = m_gPlayer.transform.forward * m_fDistance * -1;
        //m_vOffset += m_gPlayer.transform.position;
        m_vOffset.y = 0.5f * m_ppPlayer.TractorDistance();
        
        this.m_rb.position = m_gPlayer.transform.position + m_vOffset;
        this.gameObject.transform.LookAt(m_gPlayer.transform);
    }
}

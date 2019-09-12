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

        if (!m_gPlayer)
        {
            m_gPlayer = GameObject.Find(m_sPlayerName);
            if (m_gPlayer) m_ppPlayer = m_gPlayer.GetComponent<PlayerPositioning>();
            else { }
        }

        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        if (!m_gPlayer)
        {
            m_gPlayer = GameObject.Find(m_sPlayerName);
            if (m_gPlayer) m_ppPlayer = m_gPlayer.GetComponent<PlayerPositioning>();
        }

        m_rb.centerOfMass = Vector3.zero;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //Code for finding player. Will have to be updated, based on game state
        if (!m_gPlayer)
        {
            m_gPlayer = GameObject.Find(m_sPlayerName);
            if (m_gPlayer) m_ppPlayer = m_gPlayer.GetComponent<PlayerPositioning>();
        }
        else {

            HandlePlayer();
        }
    }

    void HandlePlayer() {
        //MUST ACCOUNT FOR FACING!!!
        //CHECK AGAIN
        m_fDistance = Mathf.Clamp((m_ppPlayer.TractorDistance() * m_fApproach), m_fMinDistance, m_fMaxDistance);
        this.gameObject.transform.position = new Vector3(m_ppPlayer.Facing.x, 0.1f, -m_ppPlayer.Facing.z) * m_fDistance;
        this.gameObject.transform.position += m_gPlayer.transform.position;
        this.gameObject.transform.LookAt(m_gPlayer.transform.position);
    }
}

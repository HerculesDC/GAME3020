using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(InputManager))]
[RequireComponent(typeof(VehicleControl))]
public class AIPlay : MonoBehaviour
{
    [SerializeField] private float m_fDetectionRange;
    [SerializeField] private GameObject[] m_gPlayer;
    [SerializeField] private GameObject sphere; //DEBUG
    [SerializeField] private Vector3[] m_vDifferences;
    [SerializeField] private float[] m_fDistances;

    private LineRenderer m_lr = null; //DEBUG

    void Awake() {
        m_vDifferences = new Vector3[m_gPlayer.Length];
        m_fDistances = new float[m_gPlayer.Length];
        this.gameObject.AddComponent<LineRenderer>(); //DEBUG
        m_lr = this.gameObject.GetComponent<LineRenderer>(); //DEBUG
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {  
        UpdatePositions();
        Follow(ChooseTarget());
        
        //THIS IS DEBUG CODE
        //The detection range is returning only half a radius, for whatever reason
        sphere.transform.localScale = Vector3.one * 2 * m_fDetectionRange;

        if (ChooseTarget()) {
            m_lr.enabled = true;
            m_lr.SetPosition(0, this.gameObject.transform.position);
            m_lr.SetPosition(1, ChooseTarget().position);
        }
        else m_lr.enabled = false;
    }

    /***THESE FUNCTIONS ASSUME THE EXISTENCE OF 2 TRUCKS, ALWAYS***/
    void UpdatePositions() {

        ushort counter = 0;
        foreach (GameObject player in m_gPlayer){
            //there should be an easier way
            m_vDifferences[counter] = player.transform.position - this.gameObject.transform.position;
            m_fDistances[counter] = m_vDifferences[counter].magnitude;
            counter++;
        }
    }

    Transform ChooseTarget() {

        if (m_fDistances[0] > m_fDetectionRange && m_fDistances[1] > m_fDetectionRange) return null;
        return m_fDistances[0] < m_fDistances[1] ? m_gPlayer[0].transform: m_gPlayer[1].transform;
    }

    void Follow(Transform t) {

    }

    public float Accelerate() {
        //for now, AI will return max accel.
        if (ChooseTarget()) return 1.0f;
        return 0.0f;
    }

    public float Steer() { //The current approach is too naive and requires fine-tuning

        Transform temp = ChooseTarget();
        if (temp) {
            return Mathf.Cos(Mathf.Atan2(temp.position.z, temp.position.x));
        }
        return 0.0f;
    }

    public bool Brake() {

        if (!ChooseTarget()) return true;
        return false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(VehicleControl))]
public class AIPlay : MonoBehaviour, InputHandler
{
    [SerializeField] private float m_fDetectionRange;
    [SerializeField] private GameObject[] m_gPlayer;
    [SerializeField] private Transform m_tTarget = null;
                     private PlayerTargetControls m_pTargetControls = null;
    [SerializeField] private float m_fAngle; //it's easier to store a single angle and calculate upon it
                     public float Angle { get { return m_fAngle; } }
   
    [SerializeField] private Vector3[] m_vDifferences;
    [SerializeField] private float[] m_fDistances;
    
    private LineRenderer m_lr = null; //DEBUG

    void Awake() {
        m_vDifferences = new Vector3[m_gPlayer.Length];
        m_fDistances = new float[m_gPlayer.Length];
    }

    // Start is called before the first frame update
    void Start()
    {
        //I'll assume there will ALWAYS be a first element in the m_gPlayer array:
        m_pTargetControls = m_gPlayer[0].GetComponent<PlayerTargetControls>();
    }

    // Update is called once per frame
    void Update()
    {  
        UpdatePositions();
        m_tTarget = ChooseTarget();
        Follow();

        //*/
        if (m_tTarget) {
            Vector3 temp = m_tTarget.position - this.gameObject.transform.position;
            m_fAngle = Vector3.SignedAngle(this.gameObject.transform.forward, temp, Vector3.up) * Mathf.Deg2Rad;
            Debug.DrawRay(this.gameObject.transform.position, m_tTarget.position - this.gameObject.transform.position, Color.red);
            Debug.DrawLine(this.gameObject.transform.position, (this.gameObject.transform.forward * 10) + this.gameObject.transform.position, Color.green);
        }
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
        int index = 0;
        float temp = m_fDistances[0];
        for (int i = 0; i < m_fDistances.Length; ++i)
            index = m_fDistances[i] < temp ? i : index;
        return m_fDistances[index] < m_fDetectionRange ? m_gPlayer[index].transform : null;
    }

    void Follow() {
        Accelerate();
        Steer();
    }

    public float Accelerate() {

        //for now, AI will return max accel.
        if (m_tTarget)
            return Mathf.Cos(m_fAngle);
        return 0.0f;
    }

    public float Steer() {
        
        //The current approach is too naive and requires fine-tuning
        if (m_tTarget) {
            return Mathf.Sin(m_fAngle);
        }
        return 0.0f;
    }

    public bool Brake() {

        if (!ChooseTarget() || (m_pTargetControls && m_pTargetControls.Braking)) return true;
        return false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(VehicleControl))]
public class AIPlay : MonoBehaviour
{
    [SerializeField] private float m_fDetectionRange;
    [SerializeField] private GameObject[] m_gPlayer;
    [SerializeField] private Transform m_tTarget = null;
                     private PlayerTargetControls m_pTargetControls = null;
    [SerializeField] private float angle;//for reading purposes

    //private GameObject m_Sphere;
    //[SerializeField] private GameObject Sphere; //DEBUG
    [SerializeField] private Vector3[] m_vDifferences;
    [SerializeField] private float[] m_fDistances;
    
    private LineRenderer m_lr = null; //DEBUG

    void Awake() {
        m_vDifferences = new Vector3[m_gPlayer.Length];
        m_fDistances = new float[m_gPlayer.Length];

        //DEBUG: LEARN HOW TO HANDLE RAYCASTS!!!
        this.gameObject.AddComponent<LineRenderer>(); //DEBUG
        m_lr = this.gameObject.GetComponent<LineRenderer>(); //DEBUG
        m_lr.startWidth = m_lr.endWidth = 0.1f;
    }

    // Start is called before the first frame update
    void Start()
    {
        //I'll assume there will ALWAYS be a first element in the m_gPlayer array:
        m_pTargetControls = m_gPlayer[0].GetComponent<PlayerTargetControls>();
        //if (Sphere) m_Sphere = GameObject.Instantiate<GameObject>(Sphere, this.gameObject.transform);
    }

    // Update is called once per frame
    void Update()
    {  
        UpdatePositions();
        m_tTarget = ChooseTarget();
        Follow();

        //THIS IS DEBUG CODE
        /*The detection range is returning only half a radius, for whatever reason
        if (m_Sphere) {
            m_Sphere.transform.position = this.gameObject.transform.position;
            m_Sphere.transform.localScale = Vector3.one * 2 * m_fDetectionRange;
        }
        //*/

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

    public float Accelerate() { return AccelerateInternal(m_tTarget); }
    
    private float AccelerateInternal(Transform t){
        //for now, AI will return max accel.
        if (t) return 1.0f;
        return 0.0f;
    }

    public float Steer() { return SteerInternal(m_tTarget); }

    private float SteerInternal(Transform t) {
        
        //The current approach is too naive and requires fine-tuning
        if (t) {
            //angle = Mathf.Atan2(t.position.z, t.position.x);//two calls, but it doesn't matter much now
            return Mathf.Cos(Mathf.Atan2(t.position.z-this.gameObject.transform.position.z, t.position.x-this.gameObject.transform.position.x));
        }
        return 0.0f;
    }

    public bool Brake() {

        if (!ChooseTarget() || (m_pTargetControls && m_pTargetControls.Braking)) return true;
        return false;
    }
}

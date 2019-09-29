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
    [SerializeField] private GameObject sphere;
    [SerializeField] private GameObject sphere2;
    [SerializeField] private Vector3[] m_vDifferences;
    [SerializeField] private float[] m_fDistances;

    void Awake() {
        m_vDifferences = new Vector3[m_gPlayer.Length];
        m_fDistances = new float[m_gPlayer.Length];
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {  
        sphere.transform.localScale = Vector3.one * m_fDetectionRange;
        UpdatePositions();
        DrawSecondSphere(SelectTarget());
        Follow(ChooseTarget());
    }

    /***THESE FUNCTIONS ASSUME THE EXISTENCE OF 2 TRUCKS, ALWAYS***/
    void UpdatePositions() {

        ushort counter = 0;
        foreach (GameObject player in m_gPlayer){
            //there should be an easier way
            m_vDifferences[counter] = this.gameObject.transform.position - this.gameObject.transform.position;
            m_fDistances[counter] = m_vDifferences[counter].magnitude;
            counter++;
        }
    }

    Transform ChooseTarget() {
        return null;
    }

    float SelectTarget() {

        return m_fDistances[0] < m_fDistances[1] ? m_fDistances[0]: m_fDistances[1];
    }

    void DrawSecondSphere(float radius) {
        sphere2.transform.localScale = radius < m_fDetectionRange ? Vector3.one * radius: Vector3.zero;
    }

    void Follow(Transform t) {

    }
}

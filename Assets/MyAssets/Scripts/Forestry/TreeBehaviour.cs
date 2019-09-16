using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class TreeBehaviour : MonoBehaviour
{
    [SerializeField] private string m_sChain;
    [SerializeField] private string m_sTractor;

    [SerializeField] private float m_fResetTime;
    [SerializeField] private float m_fLowering;

    //This may be redundant for mow
    [SerializeField] private bool m_bFallen;
    public bool Fallen { get { return m_bFallen; } }

    [SerializeField] private string m_sLevel;
    private LevelManager m_lv = null;

    private Rigidbody m_rb;

    void Awake() {
        m_bFallen = false;
        m_rb = this.gameObject.GetComponent<Rigidbody>();
        GameObject temp = GameObject.Find(m_sLevel);
        if (temp) m_lv = temp.GetComponent<LevelManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (m_rb) m_rb.centerOfMass = this.gameObject.transform.up * -m_fLowering;
        if (!m_lv) {
            GameObject temp = GameObject.Find(m_sLevel);
            if (temp) m_lv = temp.GetComponent<LevelManager>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckFall();
    }

    void CheckFall() {
        //check whether this is the best approach later
        if (!m_bFallen && this.gameObject.transform.position.y < 1.0f) {
            m_bFallen = true;
            if (m_lv) m_lv.RequestTreeFall();
        }
    }

    /*
    void OnCollisionEnter(Collision other) {

        if (other.gameObject.tag == m_sChain || other.gameObject.tag == m_sTractor) {
            
        }
    }
    //*/
    void OnJointBreak(float jb) {
        //will require fine-tuning
        Debug.Log("Tree Broken");
        StartCoroutine(Reset());
    }

    IEnumerator Reset(){

        yield return new WaitForSeconds(m_fResetTime);
        m_rb.ResetCenterOfMass();
    }
}
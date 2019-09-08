using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPositioning : MonoBehaviour
{
    //The idea is to create a point towards which the camera can look at
    //opted to look for Game Objects instead of Transforms, in case I need some other property from them
    [SerializeField] private GameObject m_gLeft;
    [SerializeField] private GameObject m_gRight;
    [SerializeField] private Vector3 m_vFacing; //***FOR READING PURPOSES ONLY***

    void Awake() {
        m_vFacing = Vector3.zero;
        if (!m_gLeft) m_gLeft = GameObject.Find("Left");
        if (!m_gRight) m_gRight = GameObject.Find("Right");
    }

    // Start is called before the first frame update
    void Start()
    {
        if (!m_gLeft) m_gLeft = GameObject.Find("Left");
        if (!m_gRight) m_gRight = GameObject.Find("Right");
        //StartCoroutine(DelayedPrint());
    }

    // Update is called once per frame
    void Update()
    {
        if (m_gLeft && m_gRight)
        {
            this.gameObject.transform.position = (m_gLeft.transform.position + m_gRight.transform.position) / 2;
            SetFacing();
            this.gameObject.transform.LookAt(this.gameObject.transform.position + m_vFacing);
        }
        
    }

    void SetFacing() {

        //THIS LOOKS UGLY AND INEFFICIENT... (but it works, so I'll leave it for now)
        float tempX = m_gRight.transform.position.x - m_gLeft.transform.position.x;
        float tempZ = m_gRight.transform.position.z - m_gLeft.transform.position.z;

        float angle = Mathf.Atan2(tempZ, tempX);

        tempX = Mathf.Cos(angle);
        tempZ = Mathf.Sin(angle);
        m_vFacing = new Vector3(-tempZ, 0, tempX);
        m_vFacing.Normalize();
    }
    /*
    IEnumerator DelayedPrint() {
        while (true) {
            yield return new WaitForSeconds(2.0f);
            Debug.Log(m_vFacing);
        }
    }
    //*/
}

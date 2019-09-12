using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimTreeBehaviour : MonoBehaviour
{
    [SerializeField] private string m_sChain;
    [SerializeField] private string m_sTrigger;

    private TreeCollision m_tcTree;
    private bool m_bFallen;

    private Animator m_anim = null;
    
    void Awake() {

        m_anim = this.gameObject.GetComponent<Animator>();

        m_tcTree = this.gameObject.GetComponentInChildren<TreeCollision>();
        m_bFallen = false;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!m_bFallen) {
            if (m_tcTree.disturbed) {
                m_anim.SetTrigger(m_sTrigger);
                m_bFallen = true;
            }
        }
    }
}

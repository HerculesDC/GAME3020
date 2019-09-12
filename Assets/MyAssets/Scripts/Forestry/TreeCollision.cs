using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeCollision : MonoBehaviour
{
    private bool m_bDisturbed;
    public bool disturbed { get { return m_bDisturbed; } }

    void Awake() {
        m_bDisturbed = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision other) {
        m_bDisturbed = true;
    }
}

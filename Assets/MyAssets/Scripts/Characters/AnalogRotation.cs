using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnalogRotation : MonoBehaviour
{
    [SerializeField] private float m_fTurnSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //in the future, may account for the parent's transform
        this.gameObject.transform.Rotate(0.0f, m_fTurnSpeed*Time.fixedDeltaTime, 0.0f);
    }
}

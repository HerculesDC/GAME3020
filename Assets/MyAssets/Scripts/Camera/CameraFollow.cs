using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    //Question: easier to pass the data through the player,
            //  or have the camera map the tractors directly?
    [SerializeField] private GameObject m_gPlayer;
    [SerializeField] private float m_fMinDistance;
    [SerializeField] private float m_fDistance;
    [SerializeField] private float m_fMaxDistance;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (m_gPlayer) this.gameObject.transform.LookAt(m_gPlayer.transform.position);
    }
}

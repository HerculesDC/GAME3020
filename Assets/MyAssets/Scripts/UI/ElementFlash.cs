using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ElementFlash : MonoBehaviour
{
    //There ought to be a more efficient way of coding this...
    private Text m_Text;
    private Image m_Image;
    [SerializeField] private float m_fFadeFactor;
    private float m_fTimer;

    void Awake() {

        m_Text = this.gameObject.GetComponent<Text>();
        m_Image = this.gameObject.GetComponent<Image>();
        m_fTimer = 0.0f;
    }    

    // Update is called once per frame
    void Update()
    {
        m_fTimer += Time.deltaTime;
        Color c = m_Text ? m_Text.color : m_Image.color;
        c.a = 0.5f + 0.5f * Mathf.Sin(m_fTimer*m_fFadeFactor);

        if (m_Text) m_Text.color = c;
        if (m_Image) m_Image.color = c;
    }
}

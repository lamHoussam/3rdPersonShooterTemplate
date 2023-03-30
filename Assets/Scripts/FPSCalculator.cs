using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FPSCalculator : MonoBehaviour
{

    private TextMeshProUGUI m_Text;
    private int m_avgFrameRate;
    public int AverageFrameRate => m_avgFrameRate;

    private float m_timer;
    [SerializeField] private float m_FpsRefreshValueTime = 1;

    private void Awake()
    {
        m_Text = GetComponent<TextMeshProUGUI>();
        m_timer = 0;
    }

    private void Update()
    {
        m_timer += Time.deltaTime;
        if(m_timer >= m_FpsRefreshValueTime)
        {
            m_timer = 0;
            m_avgFrameRate = (int)(1f / Time.unscaledDeltaTime);
            m_Text.text = "FPS : " + m_avgFrameRate.ToString();
        }
    }
}

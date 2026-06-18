using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CountDownUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_TextMeshPro;
    private void Start()
    {
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
    }

    private void Update()
    {
        if (GameManager.Instance.IsCountDownState())
        {
            m_TextMeshPro.text=Mathf.CeilToInt(GameManager.Instance.GetCountDownTimer()).ToString();
        }
    }
    private void GameManager_OnStateChanged(object sender, System.EventArgs e)
    {
        if (GameManager.Instance.IsCountDownState())
        {
            m_TextMeshPro.gameObject.SetActive(true);
        }
        else
        {
            m_TextMeshPro.gameObject.SetActive(false);
        }
    }

}

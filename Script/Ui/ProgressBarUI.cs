using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] private Image ptogressImage;

    public void Show()
    {
        gameObject.SetActive(true);
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void UpdateProgress(float progrss)
    {
        Show();
        ptogressImage.fillAmount = progrss;
        if (progrss == 1)
        {
            Invoke("Hide", 0.5f);
        }
    }
}

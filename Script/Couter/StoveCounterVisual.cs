using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterVisual : MonoBehaviour
{
    [SerializeField] private GameObject stoveOnVisual;
    [SerializeField] private GameObject sizzlingParticles;

    // 显示灶台火焰和滋滋粒子效果
    public void ShowStoveEffect()
    {
        stoveOnVisual.SetActive(true);
        sizzlingParticles.SetActive(true);
    }

    // 隐藏灶台火焰和滋滋粒子效果
    public void HideStoveEffect()
    {
        stoveOnVisual.SetActive(false);
        // ✅ 修正截图中的bug：这里应该是false，不是true
        sizzlingParticles.SetActive(false);
    }
}
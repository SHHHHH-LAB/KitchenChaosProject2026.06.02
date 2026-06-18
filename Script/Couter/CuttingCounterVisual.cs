using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounterVisual : MonoBehaviour
{
    public Animator animator;
    private string CUT = "Cut";


    // ✅ 关键：在Awake时就禁用Animator，完全阻止自动初始化
    private void Awake()
    {
        animator = GetComponent<Animator>();
        // 确保初始不播放
        animator.enabled = false;
    }

    public void PlayCut()
    {
        if (!animator.enabled) animator.enabled = true;
        animator.SetTrigger(CUT);
    }
}
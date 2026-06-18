using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*using static UnityEditor.Rendering.CameraUI;*/

public class CuttingCounter : BaseCounter
{
    public static event EventHandler OnCut;


    [SerializeField] private CuttingRecipeListSO cuttingRecipeList;
    [SerializeField] private ProgressBarUI progressBarUI;

    [SerializeField] private CuttingCounterVisual cuttingCounterVisual;

    private int cuttingCount = 0;
    public override void Interact(Player player)
    {
        if (player.IsHaveKitchenObject())
        {//有
            if (IsHaveKitchenObject() == false)
            {
                TransferKitchenObject(player, this);
                cuttingCount = 0;
               
            }
            else
            {
                Debug.Log("没有");

            }
        }
        else
        {//没有
            if (IsHaveKitchenObject() == false)
            {
                Debug.Log("没有");
            }
            else
            {
                TransferKitchenObject(this, player);
                cuttingCount = 0;
               

            }
        }
    }
    public override void InteractOperate(Player player)
    {
        Debug.Log("1. InteractOperate 被调用");

        if (!IsHaveKitchenObject())
        {
            Debug.Log("2. 柜台上没有食材，退出");
            return;
        }
        Debug.Log("3. 柜台上已有食材");

        if (cuttingRecipeList == null)
        {
            Debug.LogError("4. cuttingRecipeList 为空！");
            return;
        }
        Debug.Log("5. cuttingRecipeList 不为空");

        var inputSO = GetKitchenObject().GetKitchenObjectSO();
        if (inputSO == null)
        {
            Debug.LogError("6. 食材的 KitchenObjectSO 为空");
            return;
        }
        Debug.Log("7. 食材 SO: " + inputSO.name);

        if (!cuttingRecipeList.TryGetCuttingRecipe(inputSO, out CuttingRecipe cuttingRecipe))
        {
            Debug.LogWarning("8. 没有找到该食材的切割配方");
            return;
        }
        Debug.Log("9. 找到配方，最大切割次数: " + cuttingRecipe.cuttingCountMax);

        if (progressBarUI == null)
        {
            Debug.LogError("10. progressBarUI 为空！");
            return;
        }
        if (cuttingCounterVisual == null)
        {
            Debug.LogError("11. cuttingCounterVisual 为空！");
            return;
        }

        Cut();  // 内部会增加 cuttingCount 并播放动画
        Debug.Log("12. Cut() 执行完毕，当前切割次数: " + cuttingCount);

        float progress = (float)cuttingCount / cuttingRecipe.cuttingCountMax;
        progressBarUI.UpdateProgress(progress);
        Debug.Log("13. 进度条更新为 " + progress);

        if (cuttingCount == cuttingRecipe.cuttingCountMax)
        {
            Debug.Log("14. 切割完成，准备生成新物品");
            DestroyKitchenObject();
            CreateKitchenObject(cuttingRecipe.output.prefab);
            cuttingCount = 0;
            progressBarUI.UpdateProgress(0f);
            Debug.Log("15. 新物品已生成，切割次数重置");
        }
    }
    /*    public override void InteractOperate(Player player) 
        {

            if (IsHaveKitchenObject())
            {
               if(cuttingRecipeList.TryGetCuttingRecipe(GetKitchenObject().GetKitchenObjectSO(), out CuttingRecipe cuttingRecipe)) {
                    Cut();
                    Debug.Log("触发");
                    progressBarUI.UpdateProgress((float)cuttingCount/cuttingRecipe.cuttingCountMax);
                    if (cuttingCount == cuttingRecipe.cuttingCountMax)
                    {
                        DestroyKitchenObject();
                        CreateKitchenObject(cuttingRecipe.output.prefab);
                        cuttingCount = 0;
                        progressBarUI.UpdateProgress(0f);
                    }

                }


            }
        }*/
    private void Cut()
    {
        OnCut?.Invoke(this, EventArgs.Empty);
        cuttingCount++;
        cuttingCounterVisual.PlayCut();
    }

}

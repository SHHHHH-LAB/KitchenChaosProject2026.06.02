using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounter : BaseCounter
{
    [SerializeField] private FryingRecipeListSO fryingRecipeList;
    [SerializeField] private FryingRecipeListSO burninRecipeList;
    [SerializeField] private StoveCounterVisual stoveCounterVisual;
    [SerializeField] private ProgressBarUI progressBarUI;
    [SerializeField] private AudioSource sound;
    // 灶台状态枚举
    public enum StoveState
    {
        Idle,       // 空闲
        Frying,     // 煎制中
        Burning     // 烧焦中
    }

    // 私有字段
    private FryingRecipe fryingRecipe;
    private float fryingTimer = 0;
    private StoveState state = StoveState.Idle;

    // 玩家交互逻辑
    public override void Interact(Player player)
    {
        if (player.IsHaveKitchenObject())
        {
            // 玩家手上有食材
            if (!IsHaveKitchenObject() && fryingRecipeList.TryGetFryingRecipe(
                player.GetKitchenObject().GetKitchenObjectSO(), out FryingRecipe fryingRecipe))
            {
                // 灶台为空且有对应煎制配方：把食材放到灶台上开始煎制
                TransferKitchenObject(player, this);
                stoveCounterVisual.ShowStoveEffect();
                StartFrying(fryingRecipe);
            }
        }
        else
        {
            // 玩家手上没有食材
            if (IsHaveKitchenObject())
            {
                // 灶台有物品：把煎好/烧焦的食材拿起来
                TransferKitchenObject(this, player);

                // ✅ 新增：拿走食材后进度条0.5秒后消失
                progressBarUI.Hide(); // 先隐藏，或用Invoke延迟隐藏
                Invoke(nameof(HideProgressBarDelayed), 0.5f);

                // ✅ 修正拼写错误：IurnToIdle → ReturnToIdle
                ReturnToIdle();
                fryingTimer = 0;
            }
        }
    }

    // ✅ 新增：延迟隐藏进度条（封装逻辑）
    private void HideProgressBarDelayed()
    {
        progressBarUI.Hide();
    }

    // 每帧更新：处理煎制和烧焦计时
    private void Update()
    {
        switch (state)
        {
            case StoveState.Idle:
                progressBarUI.Hide(); // ✅ 新增：闲置时隐藏进度条
                break;

            case StoveState.Frying:
                // 累加煎制时间
                fryingTimer += Time.deltaTime;
                // ✅ 新增：计算煎制进度并更新UI
                float fryingProgress = fryingTimer / fryingRecipe.fryingTime;
                progressBarUI.UpdateProgress(fryingProgress);

                if (fryingTimer >= fryingRecipe.fryingTime)
                {
                    // 煎制完成：销毁生食材，生成熟食材
                    DestroyKitchenObject();
                    CreateKitchenObject(fryingRecipe.output.prefab);

                    // 尝试获取熟食材对应的烧焦配方
                    fryingRecipeList.TryGetFryingRecipe(GetKitchenObject().GetKitchenObjectSO(),
                        out FryingRecipe newFryingRecipe);
                    StartBurning(newFryingRecipe);
                }
                break;

            case StoveState.Burning:
                // 累加烧焦时间
                fryingTimer += Time.deltaTime;
                // ✅ 新增：计算烧焦进度并更新UI
                float burningProgress = fryingTimer / fryingRecipe.fryingTime;
                progressBarUI.UpdateProgress(burningProgress);

                if (fryingTimer >= fryingRecipe.fryingTime)
                {
                    // 烧焦完成：销毁熟食材，生成烧焦食材
                    DestroyKitchenObject();
                    CreateKitchenObject(fryingRecipe.output.prefab);
                    ReturnToIdle();
                }
                break;

            default:
                break;
        }
    }

    // 开始煎制
    private void StartFrying(FryingRecipe fryingRecipe)
    {
        progressBarUI.UpdateProgress(0f); // 初始化进度条为0
        stoveCounterVisual.ShowStoveEffect();
        fryingTimer = 0;
        this.fryingRecipe = fryingRecipe;
        state = StoveState.Frying;
        sound.Play();
    }

    // 开始烧焦
    private void StartBurning(FryingRecipe fryingRecipe)
    {
        if (fryingRecipe == null)
        {
            Debug.LogWarning("无法获取Burning的食谱，无法进行Burning。");
            ReturnToIdle();
            ;
            return;
        }

        fryingTimer = 0;
        this.fryingRecipe = fryingRecipe;
        state = StoveState.Burning;
    
    }

    // ✅ 修正拼写错误：IurnToIdle → ReturnToIdle
    private void ReturnToIdle()
    {
        state = StoveState.Idle;
        stoveCounterVisual.HideStoveEffect();
        // ✅ 新增：闲置时重置进度条
        progressBarUI.UpdateProgress(0f);
        sound.Pause();
    }
}
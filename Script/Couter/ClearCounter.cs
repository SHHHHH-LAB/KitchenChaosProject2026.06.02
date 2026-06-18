using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter
{
    public override void Interact(Player player)
    {
        if (player.IsHaveKitchenObject())
        {
            if (player.GetKitchenObject().TryGetComponent<PlateKitchenObject>(out PlateKitchenObject plateKitchenObject))
            {
                // 玩家拿的是盘子
                if (!IsHaveKitchenObject())
                {
                    // 柜台空：放盘子
                    TransferKitchenObject(player, this);
                }
                else
                {
                    // 柜台有食材：把食材放到盘子里
                    bool isSuccess = plateKitchenObject.AddKitchenObjectSO(GetKitchenObjectSO());
                    if (isSuccess)
                    {
                        DestroyKitchenObject();
                    }
                }
            }
            else
            {
                // 玩家拿的是普通食材
                if (IsHaveKitchenObject()==false)
                {
                    // 柜台空：放食材
                    TransferKitchenObject(player, this);
                }
                else
                {//柜台不为空
                    // ✅ 修复逻辑错误：柜台有东西时不能再放，直接提示
                    Debug.LogWarning("柜台已有物品，请先拿走");

                    if (GetKitchenObject().TryGetComponent<PlateKitchenObject>(out plateKitchenObject))
                    {
                        if (plateKitchenObject.AddKitchenObjectSO(player.GetKitchenObjectSO()))
                        {
                            player.DestroyKitchenObject() ;
                        }
                    }


                }
            }
        }
        else
        {
            // 玩家空手：拿柜台上的东西
            if (IsHaveKitchenObject())
            {
                TransferKitchenObject(this, player);
            }
        }
    }
}
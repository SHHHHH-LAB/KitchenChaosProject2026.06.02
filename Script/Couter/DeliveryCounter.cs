using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryCounter : BaseCounter
{
    public override void Interact(Player player)
    {
        if(player.IsHaveKitchenObject()&&player.GetKitchenObject().
            TryGetComponent<PlateKitchenObject>(out PlateKitchenObject plateKitchenObject))
        {
            //判断上菜是否正确
            OrderMananger.Instance.DeliveryRecipe(plateKitchenObject);
            player.DestroyKitchenObject();
        }

    }
}

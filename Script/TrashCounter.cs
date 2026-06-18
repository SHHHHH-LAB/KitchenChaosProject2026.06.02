using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TrashCounter : BaseCounter
{
    public static event EventHandler OnObjiecttrashed;
    public override void Interact(Player player)
    {
        if (player.IsHaveKitchenObject())
        {
            player.DestroyKitchenObject();
            OnObjiecttrashed?.Invoke(this,EventArgs.Empty);

        }
    }

}

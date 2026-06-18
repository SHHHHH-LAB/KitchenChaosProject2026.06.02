using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//¥¥ø‚¿‡πÒÃ®
public class ContainerCounter : BaseCounter
{
    
    [SerializeField] private KitchenObjctSO kitchenObjctSO;
    [SerializeField] private ContainerCounterVisual containerCounterVisual;
    // Start is called before the first frame update


    public override void Interact(Player player)
    {
        if (IsHaveKitchenObject()) return;
        CreateKitchenObject(kitchenObjctSO.prefab);
        TransferKitchenObject(this, player);
        containerCounterVisual.PlayOpen();
    }

}

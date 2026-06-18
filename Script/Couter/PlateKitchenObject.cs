using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject
{
    // ✅ 修正拼写：KitchenObjctSO → KitchenObjctSO
    [SerializeField] private List<KitchenObjctSO> validKitchenObjectList;

    [SerializeField] private PlateCompleteVisual plateCompleteVisual;
    [SerializeField] private KitchenObjectGridUI kitchenObjectGridUI;

    private List<KitchenObjctSO> kitchenObjectSOList = new List<KitchenObjctSO>();
    public bool AddKitchenObjectSO(KitchenObjctSO kitchenObjectSO)
    {
        if (kitchenObjectSOList.Contains(kitchenObjectSO))
        {
            Debug.LogWarning("盘子上已经有这个食材了");
            return false;
        }

        if (!validKitchenObjectList.Contains(kitchenObjectSO))
        {
            Debug.LogWarning("这个食材不能放在盘子上");
            return false;
        }
        plateCompleteVisual.ShowKitchenObject(kitchenObjectSO);
        kitchenObjectGridUI.ShowKitchenObjectUI(kitchenObjectSO);
        kitchenObjectSOList.Add(kitchenObjectSO);
        Debug.Log("成功添加食材到盘子：" + kitchenObjectSO.name);
        return true;
    }

    public List<KitchenObjctSO> GetKitchenObjctSOList()
    {
        return kitchenObjectSOList;
    }
}
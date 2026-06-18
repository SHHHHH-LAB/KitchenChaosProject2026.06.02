using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*using static UnityEngine.Video.VideoPlayer;*/

public class KitchenObjectHolder : MonoBehaviour
{
    public static event EventHandler OnDrop;
    public static event EventHandler OnPickup;

    [SerializeField] private Transform holdPoint;
    private KitchenObject kitchenObject;

    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }

    public bool IsHaveKitchenObject()
    {
        return kitchenObject != null;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {

        if (this.kitchenObject!=kitchenObject&&kitchenObject!=null&&this is BaseCounter)
        {
            OnDrop?.Invoke(this, EventArgs.Empty);
        }
        else if (this.kitchenObject != kitchenObject && kitchenObject != null && this is Player)
        {
            OnPickup?.Invoke(this, EventArgs.Empty);
        }

        this.kitchenObject = kitchenObject;
        if (kitchenObject != null)
        {
            kitchenObject.transform.SetParent(holdPoint);
            kitchenObject.transform.localPosition = Vector3.zero;
        }

    }

    // ✅ 修正拼写：GetHoldpoint → GetHoldPoint
    public Transform GetHoldPoint()
    {
        return holdPoint;
    }

    // ✅ 修正拼写：tragertHold → targetHold
    public void TransferKitchenObject(KitchenObjectHolder sourceHold, KitchenObjectHolder targetHold)
    {
        if (sourceHold.GetKitchenObject() == null)
        {
            Debug.LogWarning("源持有者不存在食材");
            return;
        }
        if (targetHold.GetKitchenObject() != null)
        {
            Debug.LogWarning("目标持有者已经存在食材");
            return;
        }

        targetHold.AddKitchenObject(sourceHold.GetKitchenObject());
        sourceHold.ClearKitchenObject();
    }

    // ✅ 修正拼写：AddKitchenObjct → AddKitchenObject
    public void AddKitchenObject(KitchenObject kitchenObject)
    {
        kitchenObject.transform.SetParent(holdPoint);
        SetKitchenObject(kitchenObject);
     
    }

    // ✅ 修正拼写：KitchenObjctSO → KitchenObjctSO
    // ✅ 确保这里没有throw语句！
    public KitchenObjctSO GetKitchenObjectSO()
    {
        if (kitchenObject == null)
        {
            return null;
        }
        return kitchenObject.GetKitchenObjectSO();
    }

    public void ClearKitchenObject()
    {
        this.kitchenObject = null;
    }

    public void DestroyKitchenObject()
    {
        if (kitchenObject != null)
        {
            Destroy(kitchenObject.gameObject);
            ClearKitchenObject();
        }
    }

    public void CreateKitchenObject(GameObject kitchenObjectPrefab)
    {
        KitchenObject kitchenObject = GameObject.Instantiate(kitchenObjectPrefab, GetHoldPoint()).GetComponent<KitchenObject>();
        SetKitchenObject(kitchenObject);
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlateCompleteVisual : MonoBehaviour
{
    [Serializable]
    public class KitchenObjectSO_Model
    {
        public KitchenObjctSO kitchenObjectSO;
        public GameObject model;
    }

    [SerializeField] private List<KitchenObjectSO_Model> modelMap;

    public void ShowKitchenObject(KitchenObjctSO kitchenObjectSO)
    {
        foreach (KitchenObjectSO_Model item in modelMap)
        {
            if (item.kitchenObjectSO == kitchenObjectSO)
            {
                print("已经显示了");
                item.model.SetActive(true);
                return;
            }
        }
    }
}
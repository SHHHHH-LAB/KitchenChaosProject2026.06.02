using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class KitchenObjectGridUI : MonoBehaviour
{
    [SerializeField] private KIchenObjectIconUI icoTempLateUI;
    private void Start()
    {
        icoTempLateUI.Hide();
    }
    public void ShowKitchenObjectUI(KitchenObjctSO kitchenObjctSO)
    {
        KIchenObjectIconUI  newIconUI= GameObject.Instantiate(icoTempLateUI,transform);
        newIconUI.Show(kitchenObjctSO.sprite) ;
    }
}

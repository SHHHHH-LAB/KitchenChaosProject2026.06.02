using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCounter : KitchenObjectHolder
{
    [SerializeField] private GameObject selectedCounter;
    // Start is called before the first frame update
    public virtual void Interact(Player player)
    {
      

    }

    public virtual void InteractOperate(Player player)
    {

    }
    public void SelectCounter()
    {
        selectedCounter.SetActive(true);
    }

    public void CancelSelect()
    {
        selectedCounter.SetActive(false);
    }

  
}

using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjctSO _kitchenObjectSO;

    public KitchenObjctSO GetKitchenObjectSO()
    {
        return _kitchenObjectSO;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class RecipeSO : ScriptableObject
{
    public string recipeName;
    public List<KitchenObjctSO> kitchenObjctSOsLiat;
    internal IEnumerable<KitchenObjctSO> kitchenObjctSOList;
    internal string recipleName;
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class FryingRecipeListSO : ScriptableObject
{
    public List<FryingRecipe> list;

    public bool TryGetFryingRecipe(KitchenObjctSO input, out FryingRecipe fryingRecipe)
    {
        foreach (FryingRecipe recipe in list)
        {
            if (recipe.input == input)
            {
                fryingRecipe = recipe;
                return true;
            }
        }

        fryingRecipe = null;
        return false;
    }
}

[Serializable]
public class FryingRecipe
{
    public KitchenObjctSO input;
    public KitchenObjctSO output;
    public float fryingTime;
}

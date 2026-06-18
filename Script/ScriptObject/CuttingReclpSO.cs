using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 关键！加这个标签，Unity才能序列化这个类
[System.Serializable]
public class CuttingRecipe
{
    // 顺便修正拼写错误（Recip → Recipe）
    public KitchenObjctSO input;
    public KitchenObjctSO output;
    public int cuttingCountMax;
}

// 完善CreateAssetMenu，指定菜单名和文件名
[CreateAssetMenu()]
public class CuttingRecipeListSO : ScriptableObject
{
    public List<CuttingRecipe> list;

    public KitchenObjctSO GetOutput(KitchenObjctSO intput)
    {
        foreach (CuttingRecipe c in list)
        {
            if(c.input == intput)
            {
                return c.output;
            }
        }
        return null;
    }
    public bool TryGetCuttingRecipe(KitchenObjctSO input,out CuttingRecipe cuttingRecipe)
    {
        foreach (CuttingRecipe c in list)
        {
            if (c.input == input)
            {
                cuttingRecipe = c;
                return true;
            }
        }
        cuttingRecipe = null;
        return false;
    }
}
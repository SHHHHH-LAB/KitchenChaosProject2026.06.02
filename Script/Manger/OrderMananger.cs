using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderMananger : MonoBehaviour
{
    public static OrderMananger Instance { get; private set; }

    public event EventHandler OnRecipeSpawned;
    public event EventHandler OnRecipSuccessed;
    public event EventHandler OnRecipFailed;

    [SerializeField] private RecipeListSO recipeSOList;
    [SerializeField] private float oderRate = 2F;
    [SerializeField] private int orderMaxCount = 5;

    private List<RecipeSO> orderRecipSOList = new List<RecipeSO>();

    private float orderTime = 0;
    private bool isStartOrder = false;
    private int orderCount = 0;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
    }

    private void GameManager_OnStateChanged(object sender, EventArgs e)
    {
        if (GameManager.Instance.IsGamePlayingState())
        {
            StartSpamnOrder();
        }
    }

    private void Update()
    {
        if (isStartOrder)
        {
            OrderUpdate();
        }
    }

    private void OrderUpdate()
    {
        orderTime += Time.deltaTime;
        if (orderTime >= oderRate)
        {
            orderTime = 0;
            OrderANewRecipe();
        }
    }

    private void OrderANewRecipe()
    {
        if (orderCount >= orderMaxCount) return;
        orderCount++;
        int index = UnityEngine.Random.Range(0, recipeSOList.recipeSOList.Count);
        orderRecipSOList.Add(recipeSOList.recipeSOList[index]);
        OnRecipeSpawned?.Invoke(this, EventArgs.Empty);
    }

    public void DeliveryRecipe(PlateKitchenObject plateKitchenObject)
    {
        RecipeSO correctRecipe = null;
        foreach (RecipeSO recipe in orderRecipSOList)
        {
            if (IsCorrect(recipe, plateKitchenObject))
            {
                correctRecipe = recipe;
            }
        }
        if (correctRecipe == null)
        {
            OnRecipFailed?.Invoke(this, EventArgs.Empty);
            print("上菜失败");
        }
        else
        {
            orderRecipSOList.Remove(correctRecipe);
            // ✅ 修改1：成功时触发 OnRecipSuccessed 事件，让 UI 及时刷新
            OnRecipSuccessed?.Invoke(this, EventArgs.Empty);
            print("上菜成功");
        }
    }

    private bool IsCorrect(RecipeSO recipe, PlateKitchenObject plateKitchenObject)
    {
        List<KitchenObjctSO> list = recipe.kitchenObjctSOsLiat;
        List<KitchenObjctSO> list1 = plateKitchenObject.GetKitchenObjctSOList();

        if (list.Count != list1.Count) return false;

        // ✅ 修改2：检查盘子里的食材是否包含食谱所需的每一项
        foreach (KitchenObjctSO kitchenObjctSO in list)
        {
            if (!list1.Contains(kitchenObjctSO))
            {
                return false;
            }
        }
        return true;
    }

    public List<RecipeSO> GetRecipesList()
    {
        return orderRecipSOList;
    }

    public void StartSpamnOrder()
    {
        isStartOrder = true;
    }
}
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OrderRecipListUI : MonoBehaviour
{
    [SerializeField] private Transform recipeParent;
    [SerializeField] private ReicpeUi recipeUITemplatr;

    private void Start()
    {
        recipeUITemplatr.gameObject.SetActive(false);
        OrderMananger.Instance.OnRecipeSpawned += Instanc_OnRecipeSpawned;
        OrderMananger.Instance.OnRecipSuccessed += Instanc_OnRecipSuccessed;
    }

    private void Instanc_OnRecipSuccessed(object sender, System.EventArgs e)
    {
        UpdateUI();
    }

    private void Instanc_OnRecipeSpawned(object sender, System.EventArgs e)
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        foreach (Transform child in recipeParent)
        {
            if (child != recipeUITemplatr.transform)
            {
                Destroy(child.gameObject);
            }
        }
        List<RecipeSO> recipeSOList=OrderMananger.Instance.GetRecipesList();
     
        foreach (RecipeSO recipeSO in recipeSOList)
        {
            ReicpeUi recipeUI =GameObject.Instantiate(recipeUITemplatr);
            recipeUI.transform.SetParent(recipeParent);
            recipeUI.gameObject.SetActive(true);
            recipeUI.UpdateUI(recipeSO);
        }
    }
}

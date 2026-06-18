using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ReicpeUi : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI recipeNameText;
    [SerializeField] private Transform kitchenObjectParent;
    [SerializeField] private Image iconUITemplate;

    private void Start()
    {
        iconUITemplate.gameObject.SetActive(false);
    }

    public void UpdateUI(RecipeSO recipeSO)
    {
     
        recipeNameText.text = recipeSO.recipeName;
        foreach (KitchenObjctSO kitchenObjectSO in recipeSO.kitchenObjctSOsLiat)
        {
            Debug.LogError("1");
            Image newIcon = GameObject.Instantiate(iconUITemplate);
            newIcon.transform.SetParent(kitchenObjectParent);
            newIcon.sprite = kitchenObjectSO.sprite;
            newIcon.gameObject.SetActive(true);
        }
    }
}

/*
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ReicpeUi : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI recipeNameText;
    [SerializeField] private Transform kitchenObjectParent;

    public void UpdateUI(RecipeSO recipeSO)
    {
        Debug.Log("=== UpdateUI 开始 ===");

        // 1. 检查 recipeSO
        if (recipeSO == null)
        {
            Debug.LogError("recipeSO 为 null");
            return;
        }
        Debug.Log($"菜谱名称: {recipeSO.recipeName}");

        // 2. 检查 UI 组件赋值
        if (recipeNameText == null)
        {
            Debug.LogError("recipeNameText 未赋值，请在 Inspector 中拖拽");
            return;
        }
        if (kitchenObjectParent == null)
        {
            Debug.LogError("kitchenObjectParent 未赋值，请在 Inspector 中拖拽");
            return;
        }

        // 3. 设置菜名
        recipeNameText.text = recipeSO.recipeName;
        Debug.Log($"菜名已设置: {recipeSO.recipeName}");

        // 4. 清除旧图标
        foreach (Transform child in kitchenObjectParent)
        {
            Destroy(child.gameObject);
        }
        Debug.Log("旧图标已清除");

        // 5. 检查食材列表 (注意字段名是 kitchenObjctSOsLiat)
        if (recipeSO.kitchenObjctSOsLiat == null)
        {
            Debug.LogError($"菜谱 {recipeSO.recipeName} 的 kitchenObjctSOsLiat 为 null");
            return;
        }
        if (recipeSO.kitchenObjctSOsLiat.Count == 0)
        {
            Debug.LogError($"菜谱 {recipeSO.recipeName} 的 kitchenObjctSOsLiat 数量为 0，请在 Inspector 中添加食材");
            return;
        }
        Debug.Log($"食材列表数量: {recipeSO.kitchenObjctSOsLiat.Count}");

        // 6. 创建图标
        foreach (var kitchenObjectSO in recipeSO.kitchenObjctSOsLiat)
        {
            if (kitchenObjectSO == null)
            {
                Debug.LogWarning("遇到空的食材条目，跳过");
                continue;
            }

            Debug.Log($"正在创建图标: {kitchenObjectSO.name}");

            GameObject iconGO = new GameObject($"Icon_{kitchenObjectSO.name}");
            iconGO.transform.SetParent(kitchenObjectParent, false);
            iconGO.transform.localScale = Vector3.one;

            RectTransform rt = iconGO.AddComponent<RectTransform>();
            rt.sizeDelta = new Vector2(50, 50);
            rt.anchoredPosition = Vector2.zero;

            Image img = iconGO.AddComponent<Image>();
            if (kitchenObjectSO.sprite != null)
            {
                img.sprite = kitchenObjectSO.sprite;
                img.color = Color.white;
                Debug.Log($"图标图片已设置: {kitchenObjectSO.sprite.name}");
            }
            else
            {
                img.color = Color.gray;
                Debug.LogWarning($"食材 {kitchenObjectSO.name} 没有 Sprite，显示灰色方块");
            }

            iconGO.SetActive(true);
            Debug.Log($"图标 {iconGO.name} 已激活");
        }

        Debug.Log("=== UpdateUI 完成 ===");
    }
*/
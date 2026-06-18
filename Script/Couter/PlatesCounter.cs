using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounter : BaseCounter
{
    // ✅ 修正拼写错误：KitchenObjctSO → KitchenObjctSO
    [SerializeField] private KitchenObjctSO plateSO;
    [SerializeField] private float spawnRate = 3f;
    [SerializeField] private int plateCountMax = 5;

    private List<KitchenObject> platesList = new List<KitchenObject>();
    private float timer = 0f;

    private void Update()
    {
        // 只有盘子数量没到上限时，才累加计时器
        if (platesList.Count < plateCountMax)
        {
            timer += Time.deltaTime;
            if (timer > spawnRate)
            {
                timer = 0f;
                SpawnPlate();
            }
        }
    }

    // ✅ 完全重写Interact方法，逻辑正确
    public override void Interact(Player player)
    {
        if (!player.IsHaveKitchenObject())
        {
            // 玩家空手 → 正常拿盘子
            if (platesList.Count > 0)
            {
                KitchenObject topPlate = platesList[platesList.Count - 1];
                platesList.RemoveAt(platesList.Count - 1);
                TransferKitchenObject(this, player);
            }
        }
        else
        {
            // 玩家有物品 → 新增逻辑：如果柜台无物品，先放食物，再拿盘子（二选一）
            KitchenObject playerObj = player.GetKitchenObject();

            // 情况1：玩家拿的是普通食物 → 先放到盘子柜台（临时存储），再拿盘子
            if (!playerObj.TryGetComponent<PlateKitchenObject>(out _) && !IsHaveKitchenObject())
            {
                // 1. 把玩家的食物放到盘子柜台
                TransferKitchenObject(player, this);
                // 2. 若有盘子，自动拿盘子（可选：也可让玩家再次交互拿）
                if (platesList.Count > 0)
                {
                    KitchenObject topPlate = platesList[platesList.Count - 1];
                    platesList.RemoveAt(platesList.Count - 1);
                    TransferKitchenObject(this, player);
                }
            }
            // 情况2：玩家拿的是盘子 → 盘子柜台不接收，提示
            else
            {
                Debug.Log("盘子柜台不能放盘子/已有物品");
            }
        }
    }
    public void SpawnPlate()
    {
        if (platesList.Count >= plateCountMax)
        {
            timer = 0f;
            return;
        }

        // 1. 先生成盘子
        KitchenObject kitchenObject = GameObject.Instantiate(plateSO.prefab, GetHoldPoint()).GetComponent<KitchenObject>();

        // 2. 先告诉父类（这一步会把位置改成(0,0,0)）
        SetKitchenObject(kitchenObject);

        // ✅ 关键：在父类设置完位置后，再重新设置我们的堆叠位置
        kitchenObject.transform.localPosition = Vector3.zero + Vector3.up * 0.1f * platesList.Count;

        // 3. 最后添加到列表
        platesList.Add(kitchenObject);
    }

    // ✅ 必须添加：同步父类和子类的物体状态
    /*    public override void SetKitchenObject(KitchenObject kitchenObject)
        {
            base.SetKitchenObject(kitchenObject);

            // 当盘子被拿走时，自动更新父类的kitchenObject为下一个盘子
            if (kitchenObject == null && platesList.Count > 0)
            {
                base.SetKitchenObject(platesList[platesList.Count - 1]);
            }
        }*/
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "新食材数据", menuName = "Kitchen Object SO")]
public class KitchenObjctSO : ScriptableObject
{
    public GameObject prefab;
    public Sprite sprite;
    public string objectName;
  
}

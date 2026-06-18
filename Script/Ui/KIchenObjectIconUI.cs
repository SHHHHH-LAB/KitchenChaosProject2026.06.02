using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KIchenObjectIconUI : MonoBehaviour
{
    [SerializeField] private Image icoImage;
    public void Show(Sprite sprite)
    {
        gameObject.SetActive(true);
        icoImage.sprite = sprite;
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}

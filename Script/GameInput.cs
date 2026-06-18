using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    public event EventHandler OnInteractAction;
    public event EventHandler OnOprateAction;
    private GameControl gameControl;
    private void Awake()
    {
        // 添加这行日志，检查Input Action Asset在打包后是否为空
        if (gameControl == null)
        {
            Debug.LogError("❌ GameControl is NULL! Input won't work.");
        }
        else
        {
            Debug.Log("✅ GameControl initialized successfully.");
        }
        gameControl = new GameControl();
        gameControl.Player.Enable();

        gameControl.Player.Interact.performed += Interact_performed;
        gameControl.Player.Operate.performed += Oprate_performed;
    }

    private void Oprate_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        Debug.Log("Operate_performed 被触发了！");  // 新增
        OnOprateAction?.Invoke(this, EventArgs.Empty);
    }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
       OnInteractAction.Invoke(this, EventArgs.Empty);
    }

    public Vector3 GetMovementDirectionNormalized()
    {
        Vector2 inputVlue = gameControl.Player.Move.ReadValue<Vector2>();
        Vector3 direction = new Vector3(inputVlue.x, 0, inputVlue.y);
       /* float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0, vertical);*/
        direction = direction.normalized;
        return direction;
    }
}

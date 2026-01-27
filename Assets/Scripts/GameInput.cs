using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    private PlayerInputActions playerInputActions;
    
    public static GameInput Instance {get; private set;}

    public event EventHandler OnPlayerAttack; // Объявление события
    
    private void Awake()
    {
        Instance = this;
        
        
        playerInputActions = new PlayerInputActions();
        playerInputActions.Enable();
        
        // Регистрируем обработчик нажатия кнопки атаки        
        playerInputActions.Combat.Attack.started += PlayerAttack_started;
    }

    private void PlayerAttack_started(InputAction.CallbackContext obj)
    {
        // Когда кнопка атаки нажата, вызываем событие
        OnPlayerAttack?.Invoke(this, null);
    }
    
    public Vector2 GetInputVector()
    {
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();
        return inputVector;
    }

    public Vector3 GetMousePosition()
    {
        Vector3 mousePos = Mouse.current.position.ReadValue();
        return mousePos;
    }

    public Vector3 GetMouseWorldPosition(Camera camera = null)
    {
        camera ??= Camera.main;
        return camera.ScreenToWorldPoint(GetMousePosition());
    }

}

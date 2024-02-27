using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    public Action<Vector3> OnAimingAction;
    public Action<Vector2> OnMoveAction;
    public Action OnFireAction;
    public Action OnDashAction;
    public Action OnChangeGunAction;

    void OnAiming(InputValue inputValue)
    {
        Vector3 position = inputValue.Get<Vector2>();
        OnAimingAction?.Invoke(position);
    }

    void OnChangeGun()
    {
        OnChangeGunAction?.Invoke();
    }

    void OnMove(InputValue inputValue)
    {
        Vector2 direction = inputValue.Get<Vector2>();
        direction.Normalize();
        OnMoveAction?.Invoke(direction);
    }

    void OnFire()
    {
        OnFireAction?.Invoke();
    }

    void OnDash()
    {
        OnDashAction?.Invoke();
    }
}

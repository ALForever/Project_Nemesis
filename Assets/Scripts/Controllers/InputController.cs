using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{

    public event Action<Vector3> OnAimingAction;
    public event Action<Vector2> OnMoveAction;
    public event Action OnFireAction;
    public event Action OnDashAction;
    public event Action OnChangeGunAction;
    public event Action OnInterractAction;

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

    void OnInterract()
    {
        OnInterractAction?.Invoke();
    }
}

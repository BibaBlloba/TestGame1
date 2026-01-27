using System;
using Unity.VisualScripting;
using UnityEngine;

public class ActiveWeapon : MonoBehaviour
{
    
    public static ActiveWeapon Instance {get; private set;}
    
    [SerializeField] private Sword sword;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        AdjustWeaponFacingDirection();
    }

    public Sword GetActiveWeapon()
    {
        return sword;
    }
    
    private void AdjustWeaponFacingDirection()
    {
        Vector3 mouseWorldPos = GameInput.Instance.GetMousePosition();
        Vector3 playerWorldPosition = Player.Instance.GetPlayerScreenPosition();

        if (mouseWorldPos.x < playerWorldPosition.x)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
    
}

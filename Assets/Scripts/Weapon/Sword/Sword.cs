using System;
using UnityEngine;

public class Sword : MonoBehaviour
{
    [SerializeField] private int damage = 1;
    
    private PolygonCollider2D _collider;
    
    public event EventHandler OnSwordSwing;

    private void Awake()
    {
        _collider = GetComponent<PolygonCollider2D>();
    }

    private void Start()
    {
        AttackColliderTurnOff();
    }

    public void Attack()
    {
        AttackColliderTurnOffOn();
        OnSwordSwing?.Invoke(this, EventArgs.Empty);
    }

    public void AttackColliderTurnOff()
    {
        _collider.enabled = false;
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // if (collision.tag == "Enemy")
        // {
        //     
        // }

        if (collision.transform.TryGetComponent(out EnemyEntity enemyEntity))
        {
            enemyEntity.TakeDamage(damage);
        }
    }

    private void AttackColliderTurnOn()
    {
        _collider.enabled = true;
    }
    
    private void AttackColliderTurnOffOn()
    {
        AttackColliderTurnOff();
        AttackColliderTurnOn();
    }
    
}

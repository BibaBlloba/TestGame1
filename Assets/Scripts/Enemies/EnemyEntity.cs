using System;
using UnityEngine;

[RequireComponent(typeof(PolygonCollider2D))]
public class EnemyEntity : MonoBehaviour
{
    [SerializeField] private int maxHealth = 1;
    [SerializeField] private PolygonCollider2D _collider;
    
    private int _currentHealth;
    
    private void Awake()
    {
        _collider = GetComponent<PolygonCollider2D>();
    }

    private void Start()
    {
        _currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        DetechDeath();
    }
    
    public void AttackColliderTurnOn()
    {
        _collider.enabled = true;
    }
    
    public void AttackColliderTurnOffOn()
    {
        AttackColliderTurnOff();
        AttackColliderTurnOn();
    }
    
    public void AttackColliderTurnOff()
    {
        _collider.enabled = false;
    }

    private void DetechDeath()
    {
        if (_currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
    
}

using System;
using UnityEngine;

public class EnemyEntity : MonoBehaviour
{
    [SerializeField] private int maxHealth = 1;
    private int _currentHealth;

    private void Start()
    {
        _currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        
        DetechDeath();
    }

    public void DetechDeath()
    {
        if (_currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
    
    private void Awake()
    {
    }
}

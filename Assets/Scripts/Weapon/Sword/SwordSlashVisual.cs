using System;
using UnityEngine;

public class SwordSlashVisual : MonoBehaviour
{
   
   private Animator animator;

   private const string ATTACK = "Attack";

   private void Awake()
   {
      animator = GetComponent<Animator>();
   }

   private void Start()
   {
      GameInput.Instance.OnPlayerAttack += OnPlayerAttack;
   }

   private void OnPlayerAttack(object sender, EventArgs e)
   {
      animator.SetTrigger(ATTACK);
   }
}

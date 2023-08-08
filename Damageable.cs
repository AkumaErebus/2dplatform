using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;

public class Damageable : MonoBehaviour
{
    public UnityEvent<int, Vector2> damageableHit;
    Animator animator;

    [SerializeField]
    private int _maxHealth = 100;
    public int MaxHealth
    { get
        {
            return _maxHealth;
        }
        private set
        {
            _maxHealth = value;
        }
    }
    [SerializeField]
    private int _health = 100;

    public int Health
    { get
        {
            return _health;
        }
        private set
        {
            _health = value;

            // if the character health is 0

            if (_health <= 0)
            {
                IsAlive = false;
            }
        }
    }

    [SerializeField]
    private bool _isAlive = true;

    [SerializeField]
    private bool isInvincible = false;


    [SerializeField]
    private float timeSinceHit = 0f;
    public float invincibilityTime = 1f;

    public bool IsAlive 
    { get 
        { 
        return _isAlive;
        } 
        private set 
        { 
        _isAlive= value;
            animator.SetBool(AnimationString.isAlive, value);
            Debug.Log("IsAlive set " + value);
        } 
    }
    // The velocity should not be changed while this is true but needs to be respected by other physics components like
    // the player controller

    public bool LockVelocity
    {
        get
        {
            return animator.GetBool(AnimationString.lockVelocity);
        }
        set
        {
            animator.SetBool(AnimationString.lockVelocity, value);
        }
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Update()
    {
        if(isInvincible) 
        { 
          if(timeSinceHit > invincibilityTime)
            {
                // Remove invincibility
                isInvincible= false;
                timeSinceHit = 0;
            }
            timeSinceHit += Time.deltaTime;
        }
    }
    public bool Hit(int damage , Vector2 knockback)
    {
        if(IsAlive && !isInvincible)
        {
            Health -= damage;
            isInvincible= true;

            // Notify other subscribed compounts that the damageable was hit to handle  the knockback and such
            animator.SetTrigger(AnimationString.hitTrigger);
            LockVelocity = true;
            damageableHit?.Invoke(damage, knockback);
            CharacterEvents.characterDamaged.Invoke(gameObject, damage);

            return true;
        }
        // unable to hit
        return false;
    }
    public bool Heal(int healthRestore)
    {
        if(IsAlive && Health < MaxHealth) 
        {
            int maxHeal = Mathf.Max(MaxHealth - Health, 0);
            int actualHeal = Mathf.Min(maxHeal, healthRestore);
            Health += actualHeal;
            CharacterEvents.characterHealed.Invoke(gameObject, actualHeal);
            return true;
         }
        return false;
    }
    
    
}

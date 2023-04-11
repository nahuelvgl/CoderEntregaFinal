using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SocialPlatforms;
using System;
using UnityEngine.Rendering;
using Unity.VisualScripting;
using UnityEngine.Rendering.Universal;

public class RobotCharacterController : MonoBehaviour
{
    [SerializeField] private float speed;
    public float health;
    public float stamina;
    public float maxHealth = 100;
    public float maxStamina = 100;
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private StaminaBar staminaBar;
    [SerializeField] private Animator robotCharacterAnimationController;

    [SerializeField] private Volume g_volume;
    private ColorAdjustments newHueValue;
    bool characterMovement;
    bool rotation;
    public UnityEvent OnHealthChanged;
    public UnityEvent OnStaminaChanged;
    private bool hasLoggedWarning = false;
    private bool isDead = false;
    public int[] attackStaminaCosts;
    private float staminaRegenTimer = 1.5f;
    private float staminaTimer = 1.5f;
    public ParticleSystem hitEffect;
    public ParticleSystem moveDust;
    public ParticleSystem warningEffect;
    public Transform moveDustSpawn;
    public Transform hitEffectSpawn;
    public Transform warningEffectSpawn;
    private bool attackDistance = false;
    private float moveDustCooldownTimer = 0;
    public float moveDustCooldownDuration = 0.2f;
    [SerializeField] private float warningCooldownTimer = 0;
    public float warningCooldownDuration = 1;
    void Start()
    { 
        speed = 2f;
        health = maxHealth;
        stamina = maxStamina;
        attackStaminaCosts = new int[3];
        attackStaminaCosts[0] = 25; // J Attack
        attackStaminaCosts[1] = 50; // K Attack
        attackStaminaCosts[2] = 75; // L Attack

    }
    void Update()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        var direction = new Vector3(horizontal, 0, vertical);
        if (direction != Vector3.zero && health >0)
        {
            transform.rotation = Quaternion.LookRotation(direction);
        }
        if (health > 0)
        {
            Move(direction);
        }
        CharacterMovement();
        StaminaTimerStart();
        StaminaRegen();
        JAttack();
        KAttack();
        LAttack();
        Death();
        RedTint(); 
    }
    private void Move(Vector3 moveDirection)
    {
        transform.position += moveDirection * (speed * Time.deltaTime);
      
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
        {
            characterMovement = true;
            rotation = true;
        }
        else
        {
            characterMovement = false;
            rotation = false;
        }
    }
    private void CharacterMovement()
    {
     if (characterMovement == true || rotation == true)
        {
            robotCharacterAnimationController.SetFloat("Speed", 0.15f);
            if (moveDustCooldownTimer <= 0.0f)
            {
                Instantiate(moveDust, moveDustSpawn.position, moveDustSpawn.rotation);
                moveDustCooldownTimer = moveDustCooldownDuration;
            }


        }
     else if (characterMovement == false)
        {
            robotCharacterAnimationController.SetFloat("Speed", 0f);
           
        }
         if (moveDustCooldownTimer > 0.0f)
        {
         moveDustCooldownTimer -= Time.deltaTime;
        }
    }
   
    private void JAttack()
    {
        if (Input.GetKeyDown(KeyCode.J) && stamina >= attackStaminaCosts[0] && health > 0)
        {
            StartCoroutine(DecreaseStaminaWithDelay());
            robotCharacterAnimationController.SetTrigger("JAttack");
            if(attackDistance)
            { 
            Instantiate(hitEffect, hitEffectSpawn.position, hitEffectSpawn.rotation);
            }
            IEnumerator DecreaseStaminaWithDelay()
            {
                yield return new WaitForSeconds(0.5f);
                stamina -= attackStaminaCosts[1];
                UpdateStaminaBar();
            }
        }
    }
    private void KAttack()
    {
        if (Input.GetKeyDown(KeyCode.K) && stamina >= attackStaminaCosts[1] && health > 0)
        {
            StartCoroutine(DecreaseStaminaWithDelay());
            robotCharacterAnimationController.SetTrigger("KAttack");
            if (attackDistance)
            {
                Instantiate(hitEffect, hitEffectSpawn.position, hitEffectSpawn.rotation);
            }
            IEnumerator DecreaseStaminaWithDelay()
            {
                yield return new WaitForSeconds(0.5f);
                stamina -= attackStaminaCosts[1];
                UpdateStaminaBar();
            }
        }
    }
    private void LAttack()
    {
        if (Input.GetKeyDown(KeyCode.L) && stamina >= attackStaminaCosts[2] && health > 0)
        {
            StartCoroutine(DecreaseStaminaWithDelay());
            robotCharacterAnimationController.SetTrigger("LAttack");
            if (attackDistance)
            {
                Instantiate(hitEffect, hitEffectSpawn.position, hitEffectSpawn.rotation);
            }

        }
        IEnumerator DecreaseStaminaWithDelay()
        {
            yield return new WaitForSeconds(0.5f);
            stamina -= attackStaminaCosts[2];
            UpdateStaminaBar();
        }
    }
    private void Death()
    {
        if (health <= 0 && !isDead)
        {
            robotCharacterAnimationController.SetTrigger("Death");
            isDead = true;
        }
    }
    public void UpdateHealthBar()
    {
        healthBar.UpdateHealthBar(health);
        Debug.Log("UpdateHealthBar called");
    }
    public void UpdateStaminaBar()
    {
        staminaBar.UpdateStaminaBar(stamina);
        Debug.Log("UpdateStaminahBar called");
    }
    public void ApproachWarning()
    {
        if (!hasLoggedWarning) { 
        Debug.Log("Enemy attack timer started");
        hasLoggedWarning = true;
        }
    }
    private void RedTint()
    {
        if (health <= 25f)
        { 
        var g_profile = g_volume.profile;
        g_profile.TryGet<ColorAdjustments>(out newHueValue);
        newHueValue.hueShift.Override(-35f);
        }
    }
    private void StaminaRegen()
    {
        if(staminaRegenTimer <= 0 && stamina != maxStamina)
        {
            stamina = 100;
            staminaBar.UpdateStaminaBar(stamina);
            StaminaTimerReset();
        }
    }
    private void StaminaTimerStart()
    {
        if(stamina < 100)
        {
            staminaRegenTimer -= Time.deltaTime;
        }
    }
    private void StaminaTimerReset()
    {
        staminaRegenTimer = staminaTimer;
    }
    protected void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            attackDistance = true;
            if (warningCooldownTimer <= 0.0f)
            {
                Instantiate(warningEffect, warningEffectSpawn.position, warningEffectSpawn.rotation);
                warningCooldownTimer = warningCooldownDuration;
            }
            if (warningCooldownTimer > 0.0f)
            {
                warningCooldownTimer -= Time.deltaTime;
            }
        }
    }
    protected void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            attackDistance = false;
        }
    }
}

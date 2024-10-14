using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // For the Stamina UI

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Normal walk speed
    public float sprintSpeed = 8f; // Sprint speed
    public float maxStamina = 100f; // Maximum stamina
    public float stamina = 100f; // Current stamina
    public float staminaRegenRate = 5f; // Stamina regeneration rate per second
    public float sprintStaminaDrain = 10f; // Stamina drain rate while sprinting

    // Stamina UI management
    public Slider staminaBar; // Reference to the Stamina UI bar

    // Footstep sound management
    public AudioSource footstepSound;
    public float footstepInterval = 0.5f;
    private float footstepTimer = 0f;

    // Tired sound management
    public AudioSource tiredSound; // AudioSource for tired sound
    public float tiredStaminaThreshold = 20f; // Stamina value where the tired sound plays
    public float tiredSoundInterval = 10f; // Time interval between tired sounds
    private float tiredSoundTimer = 0f;

    private Rigidbody2D rb;
    private Vector2 movement;
    private bool isSprinting = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Initialize stamina bar
        staminaBar.maxValue = maxStamina;
        staminaBar.value = stamina;
    }

    void Update()
    {
        // Get movement input
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // Check if sprinting
        isSprinting = Input.GetKey(KeyCode.LeftShift) && stamina > 0;

        // Adjust speed based on sprinting
        float currentSpeed = isSprinting ? sprintSpeed : moveSpeed;

        // Handle stamina system
        if (isSprinting)
        {
            stamina -= sprintStaminaDrain * Time.deltaTime;
            if (stamina < 0) stamina = 0;
        }
        else
        {
            stamina += staminaRegenRate * Time.deltaTime;
            if (stamina > maxStamina) stamina = maxStamina;
        }

        // Update the stamina bar
        staminaBar.value = stamina;

        // Handle footstep sound
        if (movement.magnitude > 0)
        {
            footstepTimer -= Time.deltaTime;

            if (footstepTimer <= 0)
            {
                footstepSound.Play(); // Play the footstep sound
                footstepTimer = isSprinting ? footstepInterval / 2f : footstepInterval; // Faster footsteps when sprinting
            }
        }
        else
        {
            footstepSound.Stop();
        }

        // Handle tired sound when stamina is low
        if (stamina <= tiredStaminaThreshold)
        {
            tiredSoundTimer -= Time.deltaTime;

            if (tiredSoundTimer <= 0)
            {
                tiredSound.Play(); // Play tired sound
                tiredSoundTimer = tiredSoundInterval; // Reset the timer
            }
        }
        else
        {
            tiredSoundTimer = 0f; // Reset the timer when stamina is above threshold
        }
    }

    void FixedUpdate()
    {
        // Move the player based on sprinting or normal speed
        rb.MovePosition(rb.position + movement * (isSprinting ? sprintSpeed : moveSpeed) * Time.fixedDeltaTime);
    }
}

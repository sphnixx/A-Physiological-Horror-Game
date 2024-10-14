using UnityEngine;

public class NPCBehavior : MonoBehaviour
{
    [SerializeField] private float normalMoveSpeed = 2f;
    [SerializeField] private float erraticMoveSpeed = 5f;
    [SerializeField] private float behaviorChangeTime = 10f; // Time after which NPC starts acting weird
    [SerializeField] private int minSteps = 5; // Minimum steps NPC will take in one direction
    [SerializeField] private int maxSteps = 10; // Maximum steps NPC will take in one direction
    [SerializeField] private int roundsBeforeBreak = 3; // Number of rounds before NPC takes a break
    [SerializeField] private float minBreakTime = 1f; // Minimum break time
    [SerializeField] private float maxBreakTime = 2f; // Maximum break time

    private int stepsToTake;
    private Vector2 movementDirection;
    private bool isErratic = false;
    private float timer = 0f;
    private int currentSteps = 0;
    private bool canMove = true;
    private int roundsCompleted = 0;
    private bool isOnBreak = false;
    private float breakTimer = 0f;
    private float breakDuration;

    void Start()
    {
        ChooseNewDirection();
    }

    void Update()
    {
        // Behavior change based on timer
        timer += Time.deltaTime;
        if (timer > behaviorChangeTime)
        {
            isErratic = true;
        }

        // If the NPC is on a break, count the break time
        if (isOnBreak)
        {
            breakTimer += Time.deltaTime;
            if (breakTimer >= breakDuration)
            {
                isOnBreak = false;
                breakTimer = 0f;
                ChooseNewDirection();
            }
            return; // Prevent movement during break
        }

        // Move NPC if allowed
        if (canMove)
        {
            MoveNPC();
        }

        // Check if the player is interacting
        if (Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }
    }

    // Method to move the NPC left and right
    void MoveNPC()
    {
        if (currentSteps < stepsToTake)
        {
            float moveSpeed = isErratic ? erraticMoveSpeed : normalMoveSpeed;
            transform.Translate(movementDirection * moveSpeed * Time.deltaTime);
            currentSteps++;
        }
        else
        {
            canMove = false; // Stop moving when steps are completed
            roundsCompleted++;

            // If enough rounds are completed, take a break
            if (roundsCompleted >= roundsBeforeBreak)
            {
                StartBreak();
            }
            else
            {
                ChooseNewDirection(); // Choose a new direction after steps are completed
            }
        }
    }

    // Choose a new direction (either left or right)
    void ChooseNewDirection()
    {
        movementDirection = Random.value < 0.5f ? Vector2.left : Vector2.right;
        stepsToTake = Random.Range(minSteps, maxSteps);
        currentSteps = 0;
        canMove = true;
    }

    // Start the break for 1-2 seconds
    void StartBreak()
    {
        isOnBreak = true;
        roundsCompleted = 0;
        breakDuration = Random.Range(minBreakTime, maxBreakTime);
        Debug.Log("NPC is taking a break for " + breakDuration + " seconds.");
    }

    // Method for interaction using the 'E' key
    void Interact()
    {
        Debug.Log("Player is interacting with NPC!");
        // Add whatever functionality you want for the interaction
    }
}

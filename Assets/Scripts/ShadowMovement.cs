using UnityEngine;

public class ShadowMovement : MonoBehaviour
{
    public GameObject shadowPrefab; // Reference to the shadow object
    public float minAppearanceTime = 20f; // Minimum time for shadow to appear
    public float maxAppearanceTime = 40f; // Maximum time for shadow to appear
    private bool shadowAppearedToday = false;

    private float appearanceTimer;
    private float appearanceTime; // When the shadow will appear today

    void Start()
    {
        UpdateShadowTiming(1); // Initialize for day 1
    }

    void Update()
    {
        appearanceTimer += Time.deltaTime;

        if (!shadowAppearedToday && appearanceTimer >= appearanceTime)
        {
            // Spawn the shadow
            Instantiate(shadowPrefab, new Vector3(Random.Range(-5, 5), Random.Range(-5, 5), 0), Quaternion.identity);
            shadowAppearedToday = true;
        }
    }

    public void UpdateShadowTiming(int day)
    {
        // Adjust shadow appearance time for the current day
        appearanceTime = Random.Range(minAppearanceTime - day, maxAppearanceTime - day);
        appearanceTimer = 0f;
        shadowAppearedToday = false;
    }
}

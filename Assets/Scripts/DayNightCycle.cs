using UnityEngine;
using UnityEngine.UI;

public class DayNightCycle : MonoBehaviour
{
    public float dayDuration = 60f; // Duration of one full day in seconds
    public int currentDay = 1; // The current day in the game
    public Text dayText; // UI Text to display the day
    public Color dayColor = Color.white; // Normal screen color for daytime
    public Color nightColor = new Color(0.1f, 0.1f, 0.3f, 1f); // Darker screen color for nighttime
    public Image screenOverlay; // Overlay to darken the screen at night

    private float timeOfDay = 0f; // Keeps track of the current time within the day

    void Start()
    {
        UpdateDayText(); // Update the day UI at the start
    }

    void Update()
    {
        // Update the time of day
        timeOfDay += Time.deltaTime;

        // If the time exceeds the duration of a day, move to the next day
        if (timeOfDay > dayDuration)
        {
            timeOfDay = 0f; // Reset the time of day
            currentDay++; // Increment the day count
            UpdateDayText(); // Update the day display in the UI
        }

        // Adjust screen brightness based on time of day
        float t = Mathf.PingPong(timeOfDay / dayDuration, 1f); // Smooth transition between day and night
        screenOverlay.color = Color.Lerp(dayColor, nightColor, t);
    }

    // Update the day count display in the top-right corner
    void UpdateDayText()
    {
        if (dayText != null)
        {
            dayText.text = "Day: " + currentDay;
        }
    }
}

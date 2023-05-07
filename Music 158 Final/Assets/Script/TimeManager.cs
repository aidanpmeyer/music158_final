using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager: MonoBehaviour
{
    public float dayLengthInSeconds = 120f;     // Length of a full day cycle in seconds
    public float nightAngle = -10f;             // Angle at which it becomes night
    public float dayAngle = 170f;               // Angle at which it becomes day
    public Light sun;                           // Directional light source
    private bool isDay = true;                  // Boolean value for day/night
    public int DayNum = 0;
    public TerrainController TerrainController;
    public RainController RainController;


    

    private bool isFilling = false;             // Flag to check if coroutine is running
    public GameObject water;                    // The water plane 
    public grow grow;

   
    void Start()
    {
        // Set initial light angle to be daytime
        sun.transform.rotation = Quaternion.Euler(dayAngle, 0f, 0f);

       
    }

    void Update()
    {
        // Calculate the angle of the sun based on the current time of day
        float angle = (Time.time / dayLengthInSeconds) * 360f % 360;
     

        // Update the angle of the sun
        sun.transform.rotation = Quaternion.Euler(angle, 0f, 0f);
        // Switch to night if the sun angle is below the night threshold
        if (isDay && sun.transform.rotation.eulerAngles.x > nightAngle)
        {
            isDay = false;
            //TODO: call on sound manager to start night sounds
            Debug.Log("Switched to night");
            switch (DayNum)
            {
                case 1:
                    Debug.Log("NIGHT 1 start");
                    break;
                case 2:
                    Debug.Log("NIGHT 2 start");
                    //TODO make a storm Coroutine //day two the rain picks up there is a huge storm
                    break;
                case 3:
                    Debug.Log("NIGHT 3 start");
                    //day three the pond dries up
                    break;
                case 4:
                    Debug.Log("NIGHT 4 start");

                    break;
            }
        }

        // Switch to day if the sun angle is above the day threshold
        if (!isDay && sun.transform.rotation.eulerAngles.x < nightAngle)
        {
            isDay = true;
            DayNum += 1;
            Debug.Log("hit the switch");
            switch (DayNum)
            {
                case 1:
                    Debug.Log("DAY 1 start");
                    //start the light rain
                    //StartCoroutine(TerrainController.Flatten(dayLengthInSeconds, false));
                    StartCoroutine(Fill(4.11f,dayLengthInSeconds)); //day 1 the pond fills life appears
                    StartCoroutine(grow.Grow(dayLengthInSeconds, true));
                    StartCoroutine(RainController.RainSequence(dayLengthInSeconds));
                    break;
                case 2:
                    Debug.Log("DAY 2 start");
                    //heavy rain
                    StartCoroutine(Fill(25f, dayLengthInSeconds));
                    //TODO make a storm Coroutine //day two the rain picks up there is a huge storm
                    break;
                case 3:
                    Debug.Log("DAY 3 start");
                    StartCoroutine(TerrainController.Flatten(dayLengthInSeconds, true));
                    StartCoroutine(Dry(dayLengthInSeconds)); //day three the pond dries up
                    
                    break;
                case 4:
                    Debug.Log("DAY 4 start");
                    StartCoroutine(grow.Grow(dayLengthInSeconds, false));
                    break;
            }
                    //TODO: call on sound manager to start day sounds
                  
        }
    }

    IEnumerator Fill(float waterLevelMax,float duration)
    {
        float fillDuration = duration;
        isFilling = true; // Set the flag to true
        float startY = water.transform.position.y; // Get the starting y-position of the water
        float t = 0.0f; // Time counter

        while (t < fillDuration)
        { // Loop until duration is reached
            t += Time.deltaTime; // Increment the time counter
            float y = Mathf.Lerp(startY, waterLevelMax, t / fillDuration); // Calculate the new y-position of the water
            water.transform.position = new Vector3(transform.position.x, y, transform.position.z); // Update the position of the water
            yield return null; // Wait for the next frame
        }

        isFilling = false; // Set the flag to false
        //TODO: stop the water gurggle sound
        yield break; // End the coroutine
    }

    IEnumerator Dry(float duration)
    {
        float fillDuration = duration;
        float startY = water.transform.position.y; // Get the starting y-position of the water
        float t = 0.0f; // Time counter

        while (t < fillDuration)
        { // Loop until duration is reached
            t += Time.deltaTime; // Increment the time counter
            float y = Mathf.Lerp(startY, -.5f, t / fillDuration); // Calculate the new y-position of the water
            water.transform.position = new Vector3(transform.position.x, y, transform.position.z); // Update the position of the water
            yield return null; // Wait for the next frame
        }
        yield break; // End the coroutine
    }
}
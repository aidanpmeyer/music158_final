using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class grow : MonoBehaviour
{
    public GameObject[] plants;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // grow objects in object list
    public IEnumerator Grow(float duration,Boolean grow)
    {
        Debug.Log("growing");
        
        float t = 0.0f; // Time counter
        Vector3 startScale = new Vector3(0, 0, 0);
        Vector3 targetScale = new Vector3(1, 1, 1);
        while (t < duration)
        { // Loop until duration is reached
            Vector3 scale;
            t += Time.deltaTime; // Increment the time counter
            if (grow)
            {
                scale = Vector3.Lerp(startScale, targetScale, t / duration); //grow scale
            } else
            {
                scale = Vector3.Lerp(targetScale, startScale, t / duration); // shrink amout
            }
            
            for(int i = 0; i < plants.Length; i++)
            {
                
                plants[i].transform.localScale = scale; // Update the position of the water
            }
            
            yield return null; // Wait for the next frame
        }

        yield break; // End the coroutine
    }
}

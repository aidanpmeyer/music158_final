using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TerrainController : MonoBehaviour
{
    public float Duration = 2.0f;  // duration of the flattening animation in seconds
    public float HeightThreshold = 0.5f;  // height threshold below which terrain vertices will be flattened
    public AnimationCurve FlattenCurve = AnimationCurve.Linear(0, 1, 1, 0);  // curve defining the flattening animation

    private Terrain terrain;
    private float flattenStartTime;
    private float originalHeightScale;
    private float[,] originalHeights;

    private void Start()
    {
        // get the Terrain component attached to this GameObject
        terrain = GetComponent<Terrain>();

        // store the original height scale and heights of the terrain
        originalHeightScale = terrain.terrainData.heightmapScale.y;
        originalHeights = terrain.terrainData.GetHeights(0, 0, terrain.terrainData.heightmapResolution, terrain.terrainData.heightmapResolution);
    }

    public IEnumerator Flatten(float Dur, bool flat)
    {
        float flattenProgress = 0;
        flattenStartTime = Time.time;
        Debug.Log("growland");
        Duration = Dur;
        while (flattenProgress <  1)
        {
            Duration = Dur;
            flattenProgress = (Time.time - flattenStartTime) / Duration;
            float flattenAmount = FlattenCurve.Evaluate(flattenProgress);

            // modify the terrain heights based on the flatten amount
            float[,] heights = new float[terrain.terrainData.heightmapResolution, terrain.terrainData.heightmapResolution];
            for (int z = 0; z < terrain.terrainData.heightmapResolution; z++)
            {
                for (int x = 0; x < terrain.terrainData.heightmapResolution; x++)
                {
                    float height = originalHeights[z, x];
                    if (height < HeightThreshold)
                        if (flat)
                        {
                            height = Mathf.Lerp(0, height, flattenAmount);
                        } else
                        {
                            height = Mathf.Lerp(height, 0, flattenAmount);
                        }
                        
                    heights[z, x] = height;
                }
            }

            terrain.terrainData.SetHeights(0, 0, heights);

            // if the animation is complete, reset the terrain heights to their original values

            // yield control back to Unity for one frame
            yield return null;
        }
        if (true)
        {
            terrain.terrainData.SetHeights(0, 0, originalHeights);
        }
       
        yield return null;
    }

   
}
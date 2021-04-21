using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Util
{
    static int rightxbound = 30;
    static int leftxbound = -10;
    static int upybound = 20;
    static int downybound = -20;

    static public Vector3 GetRandomPosition(
        Vector3 startPosition, 
        float startRange = 7f, 
        float endRange = 11f,
        int paramrightxbound = 30,
        int paramleftxbound = -10,
        int paramupybound = 20,
        int paramdownybound = -20
    )
    {
        Vector3 spawnPosition = startPosition + GetRandomDir() * GetRandomDist(); 

        rightxbound = paramrightxbound;
        leftxbound = paramleftxbound;
        upybound = paramupybound;
        downybound = paramdownybound;

        // if the random position is outside the range of the map
        // generate a random position within the map
        if(
            spawnPosition.x >= rightxbound ||
            spawnPosition.x <= leftxbound ||
            spawnPosition.y >= upybound ||
            spawnPosition.y <= downybound
        )
        {
            spawnPosition.x = Random.Range(leftxbound + 5, rightxbound - 5);
            spawnPosition.y = Random.Range(downybound + 5, upybound - 5);
        }

        rightxbound = 0;

        return spawnPosition;
    }

    static public float GetRandomDist(float startRange = 3f, float endRange = 5f)
    {
        return Random.Range(startRange, endRange);
    }
    
    static public Vector3 GetRandomDir()
    {
        return new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }

    static public int GetRightXBound()
    {
        return rightxbound;
    }

    static public int GetLeftXBound()
    {
        return leftxbound;
    }

    static public int GetUpYBound()
    {
        return upybound;
    }

    static public int GetDownYBound()
    {
        return downybound;
    }

    static public IEnumerator FlashRenderer(SpriteRenderer toFlash, float flashTime, float flashSpeed, Color originalColor)
    {
        float flashingFor = 0;
        Color newColor = new Color32(255, 255, 255,255);
        Color flashColor = new Color32(255, 255, 255,255);

        while(flashingFor < flashTime)
        {
            toFlash.material.color = newColor;
            flashingFor += Time.deltaTime;
            yield return new WaitForSeconds(flashSpeed);
            flashingFor += flashSpeed;
            if(newColor == flashColor)
            {
                newColor = originalColor;
            }
            else
            {
                newColor = flashColor;
            }
        }
    }
}

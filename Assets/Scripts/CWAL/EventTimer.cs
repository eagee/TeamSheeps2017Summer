using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTimer : MonoBehaviour {

    public List<Event> events;
    private List<GameObject> observers;

    public float StartingWaterLevel = -12f;
    public float EndingWaterLevel = 2f;
    public float WaterLevelSpeed = 2f;

    private float m_waterLevel;
    private float m_currentSpeed;

    public float GetWaterLevel()
    {
        return m_waterLevel;
    }

    void Start()
    {
        observers = new List<GameObject>();
        m_waterLevel = StartingWaterLevel;
    }

    public void RegisterObserver(GameObject observer)
    {
        observers.Add(observer);
    }

    // Update is called once per frame
    void Update () {
        // Gradually increase our water level
        if(m_waterLevel < EndingWaterLevel)
        {
            float stageLength = (5f - StartingWaterLevel);
            float firstThird = (stageLength / 3f);
            float lastEigth = (stageLength / 8f) * 7f;
            float currentProgress = (m_waterLevel - StartingWaterLevel);
            float targetSpeed = WaterLevelSpeed;

            if(currentProgress > firstThird && currentProgress < lastEigth)
            {
                targetSpeed = WaterLevelSpeed / 2f;
            }
            else if(currentProgress > lastEigth)
            {
                targetSpeed = WaterLevelSpeed;
            }
            else if (currentProgress > stageLength)
            {
                targetSpeed = WaterLevelSpeed * 10f;
            }

            m_currentSpeed = Mathf.Lerp(m_currentSpeed, targetSpeed, Time.deltaTime);
            m_waterLevel += Time.deltaTime * m_currentSpeed;
        }
        
    }
}

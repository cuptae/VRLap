using System.Collections;
using UnityEngine;
using UnityEngine.UI; // UI 사용

public class TimeManager : MonoSingleton<TimeManager>
{
    public int hour = 0;
    public int day = 1;
    public float dayTime = 0f;
    public float dayDuration = 60.0f;
    public float nightTime = 0f;
    public float nightDuration = 60.0f;
    public Light sun;
    public Light roomLight;
    
    public bool isNight;
    
    public TextMesh hourText; // 시간을 표시할 UI 텍스트

    void Start()
    {
        StartCoroutine(UpdateTime());
    }

    IEnumerator UpdateTime()
    {
        while (true) 
        {
            // 낮 시간 (07:00 ~ 19:00)
            isNight = false;
            dayTime = 0f;
            while (dayTime < dayDuration)
            {
                dayTime += Time.deltaTime;
                hour = 7 + Mathf.FloorToInt((dayTime / dayDuration) * 12);
                
                UpdateHourText(); // UI 텍스트 업데이트

                float sunRotation = (dayTime / dayDuration) * 180f;
                sun.transform.rotation = Quaternion.Euler(sunRotation, -30, 0);

                if (hour >= 7 && hour < 14)  // 7시 ~ 14시: 밝아짐
                {
                    float t = (dayTime - (dayDuration * (0f / 12f))) / (dayDuration * (7f / 12f)); 
                    sun.intensity = Mathf.SmoothStep(0, 1, Mathf.Clamp01(t)); 
                }
                else if (hour >= 14 && hour < 19)  // 14시 ~ 19시: 어두워짐
                {
                    float t = (dayTime - (dayDuration * (7f / 12f))) / (dayDuration * (5f / 12f));
                    sun.intensity = Mathf.SmoothStep(1, 0, Mathf.Clamp01(t));
                }

                if(hour > 16)
                {
                    roomLight.gameObject.SetActive(true);
                }

                yield return null;
            }

            // 밤 시간 (19:00 ~ 07:00)
            isNight = true;
            sun.intensity = 0f;
            nightTime = 0f;
            while (nightTime < nightDuration)
            {
                nightTime += Time.deltaTime;
                hour = 19 + Mathf.FloorToInt((nightTime / nightDuration) * 12);

                if (hour > 22)
                {
                   roomLight.gameObject.SetActive(false); 
                }

                if (hour >= 24) hour -= 24; // 24시가 넘으면 0시로 변경

                UpdateHourText(); // UI 텍스트 업데이트

                yield return null;
            }
            day++; // 하루 증가
        }
    }

    void UpdateHourText()
    {
        if (hourText != null)
        {
            hourText.text = "시간 " + hour + ":00";
        }
    }
}

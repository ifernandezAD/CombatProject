using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager instance { get; private set; }

    [SerializeField] private float slowdownFactor = 0.05f;
    private float originalTimeScale;
    private float originalFixedDeltaTime;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        originalTimeScale = Time.timeScale;
        originalFixedDeltaTime = Time.fixedDeltaTime;
    }

    public void DoSlowMotion()
    {
        Time.timeScale = slowdownFactor;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
    }

    public void ResetTime()
    {
        Time.timeScale = originalTimeScale;
        Time.fixedDeltaTime = originalFixedDeltaTime;
    }
}

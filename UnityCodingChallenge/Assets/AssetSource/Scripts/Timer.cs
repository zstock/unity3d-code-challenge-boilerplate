using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer
{
    private bool _isRunning = false;
    private float _remainingTime;

    public void StartTimer(float startTime)
    {
        _remainingTime = startTime;
        _isRunning = true;
    }

    public void Tick()
    {
        if(_isRunning)
            _remainingTime -= Time.deltaTime;
    }

    public void Pause()
    {
        _isRunning = false;
    }

    public void Play()
    {
        _isRunning = true;
    }

    public float GetRemainingTime()
    {
        return _remainingTime;
    }

    public bool IsFinished()
    {
        return _remainingTime <= 0.0f;
    }
}

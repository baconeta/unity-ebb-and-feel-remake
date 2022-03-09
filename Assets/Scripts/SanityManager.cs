using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SanityManager : MonoBehaviour
{
    public float maxSanity = 100.0f;
    public float minSanity = 0;
    public float startingSanity = 75.0f;
    public float currentSanity;
    public float sanityDrainPerSecond = 1.0f;
    public float minDrainSanity = 25.0f;
    private bool _sanityDraining;
    public float highSanityMinimum = 75;
    public float lowSanityMaximum = 25;


    private float _currentTimer;

    public enum SanityLevel
    {
        High,
        Medium,
        Low
    }

    // Start is called before the first frame update
    private void Start()
    {
        // global sanity manager
        currentSanity = startingSanity;
        _currentTimer = 0.0f;
        _sanityDraining = currentSanity > minDrainSanity;
    }

    // Update is called once per frame
    private void Update()
    {
        // The sanity manager per tick needs to manage what the current sanity
        _currentTimer += Time.deltaTime;

        if (_currentTimer < 1.0f) return;

        _currentTimer = 0.0f;

        if (_sanityDraining)
        {
            AddSanity(-sanityDrainPerSecond);
            if (currentSanity < minDrainSanity)
            {
                currentSanity = minDrainSanity;
                _sanityDraining = false;
            }
        }
        else
        {
            if (currentSanity > minDrainSanity)
            {
                _sanityDraining = true;
            }
        }
    }

    public void AddSanity(float sanityToAdd)
    {
        currentSanity += sanityToAdd;
        if (currentSanity > maxSanity)
        {
            currentSanity = maxSanity;
        }

        if (currentSanity < minSanity)
        {
            currentSanity = minSanity;
        }

        if (currentSanity > minDrainSanity)
        {
            _sanityDraining = true;
        }
    }

    public float GetSanityValue()
    {
        return currentSanity;
    }

    public SanityLevel GetSanityLevel()
    {
        if (currentSanity >= highSanityMinimum)
        {
            return SanityLevel.High;
        }

        if (currentSanity <= lowSanityMaximum)
        {
            return SanityLevel.Low;
        }

        return SanityLevel.Medium;
    }
}
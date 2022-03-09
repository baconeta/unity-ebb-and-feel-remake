using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SanityManager : MonoBehaviour
{
    public float maxSanity = 100.0f;
    public float minSanity = 0;
    public float startingSanity = 75.0f;
    private float _currentSanity;
    public float sanityDrainPerSecond = 1.0f;
    public float minDrainSanity = 25.0f;
    private bool _sanityDraining;

    private float _currentTimer;

    // Start is called before the first frame update
    private void Start()
    {
        // global sanity manager
        _currentSanity = startingSanity;
        _currentTimer = 0.0f;
        _sanityDraining = _currentSanity > minDrainSanity;
    }

    // Update is called once per frame
    private void Update()
    {
        // The sanity manager per tick needs to manage what the current sanity
        _currentTimer += Time.deltaTime;

        if (_currentTimer < 1.0f) return;

        if (_sanityDraining)
        {
            _currentSanity -= sanityDrainPerSecond;
            if (_currentSanity > minDrainSanity)
            {
                _currentSanity = minDrainSanity;
                _sanityDraining = false;
            }
        }
    }

    public void AddSanity(float sanityToAdd)
    {
        _currentSanity += sanityToAdd;
        if (_currentSanity > maxSanity)
        {
            _currentSanity = maxSanity;
        }

        if (_currentSanity < minSanity)
        {
            _currentSanity = minSanity;
        }
    }

    public float GetSanity()
    {
        return _currentSanity;
    }
}
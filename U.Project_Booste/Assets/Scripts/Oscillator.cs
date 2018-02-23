﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{

    [SerializeField] Vector3 movementVector = new Vector3(10f, 10f, 10f);
    [SerializeField] float period = 2f;

    // todo remove from inspector later
    [Range(0, 1)] [SerializeField] float movementFactor; // 0 for not moved, 1 for fully moved.

    Vector3 startingPos;

    // Use this for initialization
    void Start()
    {
        startingPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Protects against divide by 0
        if (period <= Mathf.Epsilon) { return; }
        float cycles = Time.time / period;

        const float tau = Mathf.PI * 2;
        float rawSinWave = Mathf.Sin(cycles * tau * .75f);


        movementFactor = (rawSinWave / 2f) + .5f;
        Vector3 offset = movementFactor * movementVector;
        transform.position = startingPos + offset;
    }
}
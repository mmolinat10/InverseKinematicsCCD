﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring : MonoBehaviour {

    public Transform target; // rest
    private Transform localTransform;

    public Vector3 position;
    public Vector3 velocity;

    public float zeta = 0.9f; // damping ratio
    public float omega = 0.1f; // angular frequency

    // Use this for initialization
    void Start() {
        localTransform = GetComponent<Transform>();
        position = localTransform.position;
        velocity = Vector3.zero;
    }

    // Update is called once per frame
    void Update() {
        spring(Time.deltaTime);
        localTransform.position = position;
    }

    private void spring(float dt) {
        if (zeta >= 1)
            return;
        if (zeta < 0)
            zeta = 0;

        // note that since we're moving by dt x0 = position - target and v0 = velocity
        // the catch is we have to update velocity every call thereby also using the velocity function we derived

        Vector3 x0 = position - target.position;
        float omegaZeta = omega * zeta;
        float alpha = omega * Mathf.Sqrt(1.0f - zeta * zeta);
        float exp = Mathf.Exp(-dt * omegaZeta);
        float cos = Mathf.Cos(dt * alpha);
        float sin = Mathf.Sin(dt * alpha);
        Vector3 c2 = (velocity + x0 * omegaZeta) / alpha;

        position = target.position + exp * (x0 * cos + c2 * sin);
        velocity = -exp * ((x0 * omegaZeta - c2 * alpha) * cos + (x0 * alpha + c2 * omegaZeta) * sin);
    }
}
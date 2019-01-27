﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kayak : MonoBehaviour
{
    [SerializeField] float speed = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(-transform.right * Time.deltaTime * speed);
    }
}

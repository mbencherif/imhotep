﻿using UnityEngine;
using System.Collections;
using System;
using System.ComponentModel;

public class AnnotationPoint : MonoBehaviour {

    [DefaultValue("")]
    public string text { get; set; }
    [DefaultValue("")]
    public string creator { get; set; }
    public DateTime creationDate { get; set; }

    // Use this for initialization
    void Start () {
        creationDate = DateTime.Now;
	}

    // Update is called once per frame
    /*void Update () {
	
	}*/
}
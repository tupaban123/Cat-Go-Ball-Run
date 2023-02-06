using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Serialization;
using UnityEngine;

public class BigPlatform : Prop
{
    [SerializeField] private Transform platformTop;

    private Camera _cam;

    private void Awake()
    {
        _cam = Camera.main;
    }

    public bool IsUnderScreen()
    {
        if (platformTop == null)
            return false;

        var topPos = platformTop.position;

        var topScreenPos = _cam.WorldToScreenPoint(topPos);
        
        if(topScreenPos.y < -50)
        {
            return true;
        }

        return false;
    }
}

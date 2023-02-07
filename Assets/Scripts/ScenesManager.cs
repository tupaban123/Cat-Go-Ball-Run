using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    private void Awake()
    {
        Application.targetFrameRate = 60;
        Camera cam = Camera.main;

        cam.orthographicSize = 6 * (0.46171875f / cam.aspect);
    }
    
    public void GoToGame()
    {
        SceneManager.LoadScene(1);
    }
}

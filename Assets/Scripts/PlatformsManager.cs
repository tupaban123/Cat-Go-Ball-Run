using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlatformsManager : MonoBehaviour
{
    [Header("Prefabs")] 
    [SerializeField] private Platform platform;

    [Header("Components")]
    [SerializeField] private Transform platformsParent;
    
    [Header("Settings")] 
    [SerializeField] private int platformsCount;
    [SerializeField] private float platformsDistance;
    [SerializeField] private Vector2 startPlatformPos;
    [SerializeField] private float stepSize;

    private List<Platform> _platforms = new List<Platform>();
    
    private void Start()
    {
        GenerateMap();
    }

    private void Update()
    {
        List<Platform> platformsToPlaceToTop = new List<Platform>();
        foreach (var currentPlatform in _platforms)
        {
            currentPlatform.transform.position -= new Vector3(0, stepSize / 10, 0);
            if (currentPlatform.IsUnderScreen())
                platformsToPlaceToTop.Add(currentPlatform);
        }

        PlaceToTop(platformsToPlaceToTop);
    }

    private void GenerateMap()
    {
        for (int i = 0; i < platformsCount; i++)
        {
            var xPos = startPlatformPos.x;
            var yPos = startPlatformPos.y + (platformsDistance * i);

            var platformPos = new Vector2(xPos, yPos);

            var newPlatform = Instantiate(platform, platformsParent);
            newPlatform.Init(platformPos);
            
            _platforms.Add(newPlatform);
            
            GenerateBridge(newPlatform);
        }
    }

    private void GenerateBridge(Platform currentPlatform)
    {
        Bridge bridgeToSpawn = currentPlatform.ShortBridge;
            
        var chanceForLarge = Random.Range(0, 100);
            
        if (chanceForLarge > 50 && chanceForLarge % 2 == 0)
        {
            bridgeToSpawn = currentPlatform.LargeBridge;
        }
            
        var bridgePos = currentPlatform.GetPosForBridge();
            
        if(bridgeToSpawn.BridgeType == BridgeType.Large)
        {
            var incorrectBridgePos = bridgePos;
            bridgePos = new Vector2(0, incorrectBridgePos.y);
                
            bridgeToSpawn.InitZEulerAngle(Random.Range(-35, 35));
        }
            
        bridgeToSpawn.Init(bridgePos);
        currentPlatform.SetBridge(bridgeToSpawn);
        bridgeToSpawn.gameObject.SetActive(true);
    }

    private void PlaceToTop(List<Platform> platforms)
    {
        foreach (var platform in platforms)
        {
            var lastPlatformYPos = _platforms[_platforms.Count - 1].transform.position.y;

            var currentPlatformPos = platform.transform.position;
            var newPos = new Vector2(currentPlatformPos.x, lastPlatformYPos + platformsDistance);

            platform.transform.position = newPos;
         
            platform.RemoveBridge();
            GenerateBridge(platform);
            
            _platforms.Remove(platform);
            _platforms.Add(platform);
        }
    }
}

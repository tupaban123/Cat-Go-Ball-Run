using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using Random = UnityEngine.Random;
using Cysharp.Threading.Tasks;

public class PlatformsManager : MonoBehaviour
{
    [Header("Prefabs")] 
    [SerializeField] private Platform platform;

    [Header("Components")]
    [SerializeField] private BigPlatform platformsParent;
    
    [Header("Settings")] 
    [SerializeField] private int platformsCount;
    [SerializeField] private float platformsDistance;
    [SerializeField] private Transform platformSpawnPoint;
    [SerializeField] private float stepSize;

    private List<Platform> _platforms = new List<Platform>();

    private CancellationTokenSource _endMoveCancellationToken;
    
    private void Start()
    {
        _endMoveCancellationToken = new CancellationTokenSource();
        GenerateMap();
    }

    public void StartGame()
    {
        MoveAtStart();
    }

    private void GenerateMap()
    {
        for (int i = 0; i < platformsCount; i++)
        {
            var xPos = platformSpawnPoint.position.x;
            var yPos = platformSpawnPoint.position.y + (platformsDistance * i);

            var platformPos = new Vector3(xPos, yPos, 0);

            var newPlatform = Instantiate(platform, platformsParent.transform);
            newPlatform.Init(platformPos);
            
            _platforms.Add(newPlatform);
            newPlatform.isGivingMoney = i != 0;

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

    private async UniTask MoveAtStart()
    {
        while (!platformsParent.IsUnderScreen())
        {
            platformsParent.transform.position -= new Vector3(0, stepSize / 10, 0);
            await UniTask.NextFrame(cancellationToken: _endMoveCancellationToken.Token);
        }

        MovePlatforms();
    }

    private async UniTask MovePlatforms()
    {
        while(true)
        {
            List<Platform> platformsToPlaceToTop = new List<Platform>();
            foreach (var currentPlatform in _platforms)
            {
                currentPlatform.transform.position -= new Vector3(0, stepSize / 10, 0);
                if (currentPlatform.IsUnderScreen())
                    platformsToPlaceToTop.Add(currentPlatform);
            }

            PlaceToTop(platformsToPlaceToTop);
            await UniTask.NextFrame(cancellationToken: _endMoveCancellationToken.Token);
        }
    }

    public void StopMove() => _endMoveCancellationToken.Cancel();
}

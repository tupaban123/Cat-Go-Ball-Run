using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;
using Zenject;

public class Cat : MonoBehaviour
{
    [Header("AudioSources")]
    [SerializeField] private AudioSource coinAudioSource;
    [SerializeField] private AudioSource rollingAudioSource;
    [SerializeField] private AudioSource loseAudioSource;

    [Header("Settings")]
    [SerializeField] private float rayLength;
    [SerializeField] private float accelerationMultiplier;
    [SerializeField] private UnityEvent OnLose;

    [Header("Components")] 
    [SerializeField] private LosePanel losePanel;

    private Rigidbody2D _rigidbody;

    [SerializeField] private bool _isPlaying = false;

    private PlatformsManager _platformsManager;
    private Animator _animator;
    private ScoreManager _scoreManager;
    private LoseManager _loseManager;
    
    [Inject] private Pouch _pouch;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _platformsManager = FindObjectOfType<PlatformsManager>();
        _animator = GetComponent<Animator>();
        _scoreManager = FindObjectOfType<ScoreManager>();
        _loseManager = FindObjectOfType<LoseManager>();
    }

    private void FixedUpdate()
    {
        if (!_isPlaying)
            return;

        var acceleration = Input.acceleration;
        _rigidbody.velocity = new Vector2(acceleration.x * accelerationMultiplier, 0);

        if (!IsOnPlatform())
            Lose();
    }

    public void StartGame()
    {
        _animator.SetTrigger("OnRotate");
        _platformsManager.StartGame();
        rollingAudioSource.Play();
        _isPlaying = true;
    }

    private bool IsOnPlatform()
    {
        if (Physics.Raycast(transform.position, Vector3.forward, out RaycastHit hit, rayLength))
        {
            if (hit.collider.TryGetComponent(out Prop prop))
                return true;
        }
        
        return false;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.TryGetComponent(out Platform platform))
        {
            if (!platform.isGivingMoney)
                return;
            
            _scoreManager.AddScore();
            coinAudioSource.Play();
        }
    }

    public void StartLose() => Lose();
    
    private async void Lose()
    {
        _rigidbody.simulated = false;
        _platformsManager.StopMove();
        _isPlaying = false;
        rollingAudioSource.Stop();
        _animator.SetTrigger("OnLose"); 
        
        var loseClipLength = loseAudioSource.clip.length;
        loseAudioSource.Play();
        
        await UniTask.Delay(TimeSpan.FromSeconds(loseClipLength));
        
        _loseManager.AddLose();
        losePanel.Init(_scoreManager.currentScore);
        OnLose?.Invoke();
    }

    private void OnDrawGizmos()
    {   
        Gizmos.DrawRay(transform.position, Vector3.forward);   
    }
}

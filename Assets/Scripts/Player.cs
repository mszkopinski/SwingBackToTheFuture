using UnityEngine;
using System;

public class Player : MonoSingleton<Player> 
{
    [SerializeField] public float playerSitAssOffset = -8f;

    [Header("Debug")]
    [SerializeField] bool enablePlayerLogging = false;

    float playerReleasedLastYPosition = 0;
    public bool IsPlayerPlaced 
    {
        get { return isPlayerPlaced; }
        set 
        {
            if (isPlayerPlaced == value)
                return;

            isPlayerPlaced = value;

        }
    }
    public Vector3 PlayerPlacedPosition { get; private set; }
    public Animator PlayerAnimator { get; private set; }
    
    Quaternion defaultRotation;
    Vector3 defaultPosition;
    Rigidbody rb;
    bool isPlayerPlaced;

    public EventHandler PlayerInstantiated;

    void Awake()
    {
        PlayerAnimator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    void Start() 
    {
        defaultRotation = transform.rotation;
        defaultPosition = transform.position;
        OnPlayerInstatiated();
    }

    void Update()
    {
        if (playerReleasedLastYPosition != 0)
        {
            
        }
    }

    public void SetPlayerPlacement(Transform swingSitTransform)
    {
        if (enablePlayerLogging)
            Debug.Log("Setting Player Placement" + swingSitTransform.position);

        PlayerAnimator.SetBool("isLyingForward", false);
        PlayerAnimator.SetBool("isLyingBackward", false);
        PlayerAnimator.SetBool("isLaunched", false);

        rb.isKinematic = true;
        rb.useGravity = false;

        transform.parent = swingSitTransform;
        transform.position = swingSitTransform.position;
        ResetRotation();

        IsPlayerPlaced = true;
    }

    void Respawn()
    {
        ResetRotation();
    }   

    void ResetRotation()
    {
        transform.rotation = defaultRotation;
    }

    protected void OnPlayerInstatiated()
    {
        PlayerInstantiated?.Invoke(this, EventArgs.Empty);
    }
    
}
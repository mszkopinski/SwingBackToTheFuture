using UnityEngine;
using System;

public class Player : MonoSingleton<Player> 
{
    [SerializeField] public float playerSitAssOffset = -8f;

    [Header("Debug")]
    [SerializeField] bool enablePlayerLogging = false;

    public bool IsPlayerPlaced { get; set; } = false;
    public Vector3 PlayerPlacedPosition { get; private set; }
    public Animator PlayerAnimator { get; private set; }
    
    Quaternion defaultRotation;
    Rigidbody rb;

    public EventHandler PlayerInstantiated;

    void Awake()
    {
        PlayerAnimator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    void Start() 
    {
        defaultRotation = transform.rotation;
        OnPlayerInstatiated();
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
        transform.rotation = defaultRotation;

        IsPlayerPlaced = true;
    }

    protected void OnPlayerInstatiated()
    {
        PlayerInstantiated?.Invoke(this, EventArgs.Empty);
    }
    
}
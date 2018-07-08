using UnityEngine;
using System;

public class Player : MonoSingleton<Player> 
{
    [SerializeField] public float playerSitAssOffset = -8f;

    public bool IsPlayerPlaced { get; private set; } = false;
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

    void Update() 
    {
        if (!IsPlayerPlaced)
            return;

    }

    public void SetPlayerPlacement(Transform swingSitTransform)
    {
        PlayerAnimator.SetBool("isLyingForward", false);
        PlayerAnimator.SetBool("isLyingBackward", false);
        PlayerAnimator.SetBool("isLaunched", false);

        IsPlayerPlaced = true;
        rb.isKinematic = true;
        rb.useGravity = false;

        transform.parent = swingSitTransform;
        transform.position = swingSitTransform.position;
        transform.rotation = defaultRotation;
    }

    protected void OnPlayerInstatiated()
    {
        PlayerInstantiated?.Invoke(this, EventArgs.Empty);
    }
    
}
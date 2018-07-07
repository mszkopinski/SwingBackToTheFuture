using UnityEngine;
using System;

public class Player : MonoSingleton<Player> 
{
    [SerializeField] public float playerSitAssOffset = -8f;
    public EventHandler PlayerInstantiated;

    public bool IsPlayerPlaced;
    public Vector3 PlayerPlacedPosition { get; private set;}
    
    Quaternion defaultRotation;

    Rigidbody rb;

    void Awake()
    {
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
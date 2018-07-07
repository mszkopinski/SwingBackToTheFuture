using UnityEngine;
using System;

public class Player : MonoSingleton<Player> 
{
    public EventHandler PlayerInstantiated;

    void Start() 
    {
        OnPlayerInstatiated();
    }

    protected void OnPlayerInstatiated()
    {
        PlayerInstantiated?.Invoke(this, EventArgs.Empty);
    }
    
}
using System;
using UnityEngine;
using UnityEngine.UI;

public class StartGamePanel : MonoSingleton<StartGamePanel>
{
	[SerializeField] Button startGameButton;

	public EventHandler GameStarted;

	void Awake() 
	{
		Time.timeScale = 0f;
		startGameButton.onClick.AddListener(TogglePanelOn);
		startGameButton.GetComponent<Animator>().Play("startGameButtonIdle");
	}

	public void TogglePanelOn() 
	{
		Time.timeScale = 1f;
		gameObject.SetActive(false);
		GameStarted?.Invoke(null, EventArgs.Empty);
	}
}
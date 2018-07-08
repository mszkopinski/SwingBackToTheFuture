using UnityEngine;
using UnityEngine.UI;
using System;

public class GameUI : MonoSingleton<GameUI>
{
	[SerializeField] Canvas canvas;
	[SerializeField] Button swingButton;
	[SerializeField] Button jumpButton;

	public EventHandler SwingButtonPressed;
	public EventHandler JumpButtonPressed;

	void Start()
	{
		StartGamePanel.Instance.GameStarted += OnGameStarted;

		swingButton?.onClick.AddListener(OnSwingButtonPressed);
		jumpButton?.onClick.AddListener(OnJumpButtonPressed);

		Mediator.Instance.Subscribe(EventNames.SwingAngleStateChanged, (obj) => 
		{
			swingButton.interactable = (bool)obj;
		});
	}

	protected virtual void OnJumpButtonPressed()
	{
		JumpButtonPressed?.Invoke(null, EventArgs.Empty);
	}

	protected virtual void OnSwingButtonPressed()
	{
		SwingButtonPressed?.Invoke(null, EventArgs.Empty);
	}

	void OnGameStarted(object sender, EventArgs args)
	{
		canvas.enabled = true;
	}
}
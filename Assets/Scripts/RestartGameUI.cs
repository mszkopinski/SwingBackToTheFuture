using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RestartGameUI : MonoSingleton<RestartGameUI> 
{
	[SerializeField] GameObject restartGamePanel;
	[SerializeField] Button restartGameButton;

	void Start()
	{
		restartGameButton.onClick.AddListener(() => 
		{
			restartGamePanel.SetActive(false);		
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		});
	}

	public void ToggleOnRestartGamePanel()
	{
		restartGamePanel.SetActive(true);	
		GameUI.Instance.DisableGameUI();
		Time.timeScale = 1f;
	}
}
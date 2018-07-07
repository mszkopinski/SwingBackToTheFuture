using UnityEngine;

public class Swing : MonoBehaviour
{
	[SerializeField] Chain leftChain;
	[SerializeField] Chain rightChain;

	[Header("Swing")]
	[SerializeField] GameObject swingSit;
	[SerializeField] Transform leftSwingSitHook;
	[SerializeField] Transform rightSwingSitHook;

	[Header("Debug Trigger")]
	[SerializeField] bool isHooked = true;

	void Awake() 
	{
		if (leftChain == null || rightChain == null)
		{
			Debug.LogError($"No chains attached to {nameof(Swing)}");
			return; 
		}

		leftChain.ChainEndTransform = leftSwingSitHook;
		rightChain.ChainEndTransform = rightSwingSitHook;
	}


	public void PlacePlayer(Transform playerTransform)
	{

	}
}
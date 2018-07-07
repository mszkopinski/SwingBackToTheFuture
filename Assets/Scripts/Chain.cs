using UnityEngine;

public class Chain : MonoBehaviour 
{
	[SerializeField] public Transform ChainStart;
	[SerializeField] public Transform ChainEnd;

	Transform chainEndTransform = null;
	public Transform ChainEndTransform 
	{ 
		get { return chainEndTransform; } 
		set 
		{
			chainEndTransform = value;
			isChainEndAnimated = true;
		} 
	}

	bool isChainEndAnimated = false;

	void Update() 
	{
		if (!isChainEndAnimated)
			return;

		ChainEnd.position =  ChainEndTransform.position;
	}

} 

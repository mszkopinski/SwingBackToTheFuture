using UnityEngine;

public class Swing : MonoBehaviour
{
	[SerializeField] Chain leftChain;
	[SerializeField] Chain rightChain;

	[Header("Swing")]
	[SerializeField] GameObject swingSit;
	[SerializeField] float swingSitPushForce = 5f;
	[SerializeField] Transform leftSwingSitHook;
	[SerializeField] Transform rightSwingSitHook;
	[SerializeField] float minimumSwingPushAngle = 20f;
	[SerializeField] float maximumSwingPushAngle = 45f;

	[Header("Debug Trigger")]
	[SerializeField] public bool IsControlledSwing = false;
	[SerializeField] bool IsControlledInitialy = false;

	Rigidbody swingSitRigidbody;

	public bool IsInAngleToSwing { 
		get { return isInAngleToSwing; } 
		set 
		{ 
			if (isInAngleToSwing == value) 
				return;
				
			if (isSwingButtonRecentlyPressed)
				isSwingButtonRecentlyPressed = false;

			isInAngleToSwing = value;
			Mediator.Instance.SendNotifications(EventNames.SwingAngleStateChanged, isInAngleToSwing);
		}
	}
	bool isInAngleToSwing = false;
	bool isSwingButtonRecentlyPressed = false;

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

	void Start() 
	{
		swingSitRigidbody = swingSit?.GetComponent<Rigidbody>();
		GameUI.Instance.SwingButtonPressed += OnSwingButtonPressed;
	}

	void Update()
	{
		var angleToSwingSit = CalculateAngleToSwingSit();
		IsInAngleToSwing = (angleToSwingSit >= minimumSwingPushAngle && angleToSwingSit <= maximumSwingPushAngle && swingSit.transform.localPosition.y <= -2.5) || IsControlledInitialy;
	}

	void OnSwingButtonPressed(object sender, System.EventArgs args)
	{
		if (swingSitRigidbody == null || !IsControlledSwing)
			return;

		if (!isSwingButtonRecentlyPressed)
		{
			swingSitRigidbody.AddForce((swingSitRigidbody.velocity.x > 0 ? swingSit.transform.right : -swingSit.transform.right) * swingSitPushForce * Time.deltaTime);
			isSwingButtonRecentlyPressed = true;
		}

	}

	double CalculateAngleToSwingSit()
	{
		var swingPosition = swingSit.transform.position;
		var root = transform.position;

		return Vector3.Angle(new Vector3(root.x, root.y, 0f), new Vector3(swingPosition.x, swingPosition.y, 0f));
	}

	void OnTriggerEnter(Collider other) 
	{
		
	}
}
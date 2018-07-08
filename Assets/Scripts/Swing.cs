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
	[SerializeField] bool isHooked = true;
	[SerializeField] public bool IsControlledSwing = false;
	[SerializeField] bool IsControlledInitialy = false;

	Rigidbody swingSitRigidbody;

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
	}

	void Update()
	{
		var angleToSwingSit = CalculateAngleToSwingSit();

		if ((angleToSwingSit >= minimumSwingPushAngle && angleToSwingSit <= maximumSwingPushAngle) || IsControlledInitialy)
		{
			HandleSwingSitPush();
		}
	}

	void HandleSwingSitPush()
	{
		if (swingSitRigidbody == null || !IsControlledSwing)
			return;

		if (Input.GetKeyDown(KeyCode.RightArrow))
		{
			swingSitRigidbody.AddForce(swingSit.transform.right * swingSitPushForce * Time.deltaTime);
		}
		else if (Input.GetKeyDown(KeyCode.LeftArrow))
		{
			swingSitRigidbody.AddForce(-swingSit.transform.right * swingSitPushForce * Time.deltaTime);
		}
	}

	double CalculateAngleToSwingSit()
	{
		var swingPosition = swingSit.transform.position;
		var root = transform.position;

		return Vector3.Angle(new Vector3(root.x, root.y, 0f), new Vector3(swingPosition.x, swingPosition.y, 0f));
	}

	void OnTriggerEnter(Collider other) {
		
	}
}
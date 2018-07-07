using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoSingleton<CameraController> 
{
		[Header("Main Settings")]
        [SerializeField] Vector3 positionOffset = Vector3.zero;


        [Header("Speed Settings")]
        [SerializeField] float smoothSpeed = 0.0001f;
        [SerializeField] float rotationSmoothSpeed = 5f;
        [SerializeField] float rotationSpeed = 5f;


        [Header("Triggers")]
        [SerializeField] bool lookAtTarget = true;

        Transform targetTransform;
        Vector3 velocity = Vector3.zero;
        bool isRotating = false;
        Camera cam;

        void Awake()
        {
            cam = GetComponent<Camera>() ?? Camera.main;
			Player.Instance.PlayerInstantiated += OnPlayerInstantiated;
        }

        void OnPlayerInstantiated(object obj, System.EventArgs args)
        {
            var player = obj as Player;
            var cwanyComponent = player.gameObject.GetComponent(typeof(CwanySkrypt));

            targetTransform = cwanyComponent != null ? cwanyComponent.transform : player?.transform;
        }


        void LateUpdate()
        {
            if (AssertTarget())
            {
                var targetPos = targetTransform.position + positionOffset;
                cam.transform.position = Vector3.SmoothDamp(cam.transform.position, targetPos, ref velocity, isRotating ? rotationSmoothSpeed : smoothSpeed);


                if (lookAtTarget)
                    cam.transform.LookAt(targetTransform);
            }
        }


        bool AssertTarget() => targetTransform != null;
    }

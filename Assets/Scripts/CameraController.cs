using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CameraController : MonoSingleton<CameraController> 
{
		[Header("Main Settings")]
        [SerializeField] Vector3 positionOffset = Vector3.zero;


        [Header("Speed Settings")]
        [SerializeField] float smoothSpeed = 0.0001f;

        [Header("Triggers")]
        [SerializeField] bool lookAtTarget = true;

        Transform targetTransform;
        Vector3 velocity = Vector3.zero;
        Camera cam;

        void Awake()
        {
            Application.targetFrameRate = 30;
            cam = GetComponent<Camera>() ?? Camera.main;
			Player.Instance.PlayerInstantiated += OnPlayerInstantiated;
        }

        void Start()
        {
            StartGamePanel.Instance.GameStarted += OnGameStarted;
        }

        void OnGameStarted(object obj, EventArgs args)
        {
            GetComponent<Animator>().SetTrigger("isStarted");
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
                cam.transform.position = Vector3.SmoothDamp(cam.transform.position, targetPos, ref velocity, smoothSpeed);


                if (lookAtTarget)
                    cam.transform.LookAt(targetTransform);
            }
        }


        bool AssertTarget() => targetTransform != null;
    }

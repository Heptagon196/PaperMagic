using System;
using SaveData;
using UnityEngine;
using UnityEngine.Serialization;

namespace Controller
{
    public class CameraController : MonoBehaviour, ISaveDataProcesser
    {
        public static CameraController Instance;
        public PlayerMovementMode cameraMode;
        public Transform target;
        public Vector3 initialPosition;
        public Vector3 foresightDist = new Vector3(2, 0, 0);
        public float moveSpeed = 2;
        public float rotateSpeed = 2;
        public Vector3 modeSwitchOffset = new Vector3(0, 0, 0);
        public Quaternion modeSwitchRotate;
        public bool enableCameraMovement = true;
        public Vector3 cameraTargetPosition;
        public Quaternion cameraTargetRotation;
        private Vector3 _offset;
        private float PlayerMovingX => PlayerController.Instance.movingX;
        private float _direction = 1;
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            _offset = target.position - transform.position;
            _offset.x = 0;
        }
        private void LateUpdate()
        {
            if (!enableCameraMovement)
            {
                return;
            }
            if (PlayerController.Instance.IsGrounded)
            {
                _direction = PlayerMovingX switch
                {
                    > 0 => 1,
                    < 0 => -1,
                    _ => _direction
                };
            }

            cameraTargetPosition = target.position - _offset + _direction * foresightDist;
            transform.position = Vector3.Lerp(transform.position, cameraTargetPosition, Time.deltaTime * moveSpeed);
            transform.rotation = Quaternion.Lerp(transform.rotation, cameraTargetRotation, Time.deltaTime * rotateSpeed);
        }

        public void SaveDataTo(ref GameData gameData)
        {
            gameData.cameraPosition = cameraTargetPosition;
            gameData.cameraRotation = cameraTargetRotation;
        }
        public void LoadDataFrom(ref GameData gameData)
        {
            transform.position = gameData.cameraPosition;
            transform.rotation = gameData.cameraRotation;
            
            SetCameraMode(gameData.movementMode);
            cameraTargetPosition = transform.position;
            cameraTargetRotation = transform.rotation;
        }
        public void SetDefaultData(ref GameData gameData)
        {
            gameData.movementMode = PlayerMovementMode.Platform;
            gameData.cameraPosition = initialPosition;
            gameData.cameraRotation = transform.rotation;
        }
        public void SetCameraMode(PlayerMovementMode mode)
        {
            if (cameraMode == mode)
            {
                return;
            }
            cameraMode = mode;
            if (mode == PlayerMovementMode.Topdown)
            {
                cameraTargetRotation = modeSwitchRotate;
                _offset -= modeSwitchOffset;
            }
            else
            {
                cameraTargetRotation = Quaternion.identity;
                _offset += modeSwitchOffset;
            }
        }
    }
}

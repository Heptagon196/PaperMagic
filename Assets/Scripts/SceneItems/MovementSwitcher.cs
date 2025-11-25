using System;
using Controller;
using UnityEngine;

namespace SceneItems
{
    public class MovementSwitcher : MonoBehaviour
    {
        public PlayerMovementMode leftMovementMode;
        public PlayerMovementMode rightMovementMode;
        public Vector3 leftExitOffset;
        public Vector3 rightExitOffset;
        private Vector3 _startPos, _endPos;
        private bool _playerIsIn = false;
        private bool _leftIn = false;
        public void Update()
        {
            if (!_playerIsIn)
            {
                return;
            }
            var playerTransform = PlayerController.Instance.transform;
            var rate = (playerTransform.position.x - _startPos.x) / (_endPos.x - _startPos.x);
            var adjustPos = playerTransform.position;
            adjustPos.z = _startPos.z + (_endPos.z - _startPos.z) * rate;
            playerTransform.position = adjustPos;
        }
        public void OnTriggerEnter(Collider other)
        {
            if (other.gameObject == PlayerController.Instance.gameObject)
            {
                _startPos = other.bounds.ClosestPoint(other.transform.position);
                _playerIsIn = true;
                var width = PlayerController.Instance.GetComponent<CapsuleCollider>().radius;
                // 左侧进入
                if (_startPos.x < transform.position.x)
                {
                    _leftIn = true;
                    _endPos = transform.position + rightExitOffset + new Vector3(width, 0, 0);
                }
                // 右侧进入
                else
                {
                    _leftIn = false;
                    _endPos = transform.position + leftExitOffset - new Vector3(width, 0, 0);
                }
                PlayerController.Instance.SetMovingBackwards(_endPos.z > _startPos.z);
            }
        }
        public void OnTriggerExit(Collider other)
        {
            if (other.gameObject == PlayerController.Instance.gameObject)
            {
                _playerIsIn = false;
                var contactPoint = other.bounds.ClosestPoint(other.transform.position);
                var leftOut = contactPoint.x < transform.position.x;
                var playerTransform = PlayerController.Instance.transform;
                var adjustPos = playerTransform.position;
                adjustPos.z = leftOut == _leftIn ? _startPos.z : _endPos.z;
                playerTransform.position = adjustPos;
                
                var mode = leftOut ? leftMovementMode : rightMovementMode;
                PlayerController.Instance.SetMovementMode(mode);
                CameraController.Instance.SetCameraMode(mode);
            }
        }
    }
}
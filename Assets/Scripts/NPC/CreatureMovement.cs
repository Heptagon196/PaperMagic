using System;
using Controller;
using UnityEngine;

namespace NPC
{
    [Serializable]
    public enum CreatureAIStat
    {
        Idle = 0, // 不移动
        RandomMove = 1, // 随机移动
        Wandering = 2, // 游荡
        JumpTowards = 3, // 跳跃接近
        WalkTowards = 4, // 移动接近
        WalkAway = 5, // 远离
        Attack = 6, // 攻击
    }
    public class CreatureMovement : MonoBehaviour
    {
        public bool isGrounded;
        private SpriteRenderer _spriteRenderer;
        public CreatureAnimation creatureAnimation;
        public Vector3 Position => transform.position;
        private Rigidbody _rigidbody;
        public CreatureInfoBase CreatureInfo;
        private bool _inited = false;
        private int _playerLayer, _groundLayer, _hitTestLayer;
        private bool _savedVelocity = false;
        private Vector3 _velocity;
        public void SetCreature(CreatureInfoBase info)
        {
            _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            _rigidbody = GetComponent<Rigidbody>();
            creatureAnimation = GetComponent<CreatureAnimation>();
            CreatureInfo = info;
            _inited = true;
            _groundLayer = LayerMask.GetMask("Ground");
            CreatureInfo.OnStart(gameObject);
        }
        private void Update()
        {
            if (!_inited)
            {
                return;
            }
            // 俯视角时只允许友善npc行动
            if (PlayerController.Instance.movementMode == PlayerMovementMode.Topdown)
            {
                if (CreatureInfo.faction != CreatureFaction.Friendly)
                {
                    if (!_savedVelocity)
                    {
                        _savedVelocity = true;
                        _velocity = _rigidbody.velocity;
                    }
                    _rigidbody.velocity = Vector3.zero;
                    return;
                }
            }
            if (_savedVelocity)
            {
                _rigidbody.velocity = _velocity;
                _savedVelocity = false;
            }
            var pos = transform.position;
            pos.y = pos.y - CreatureInfo.height + 0.25f;
            isGrounded = Physics.BoxCast(pos, new Vector3(0.5f, 0.1f, 0.5f),
                Vector3.down, Quaternion.identity, 0.2f, _groundLayer);
            CreatureInfo.SetIsOnGround(isGrounded);
            CreatureInfo.OnUpdate();
            _spriteRenderer.flipX = _rigidbody.velocity.x < 0;
        }
    }
}
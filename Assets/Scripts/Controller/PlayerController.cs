using System;
using Equipment;
using NPC;
using SaveData;
using UI;
using UI.General;
using UI.Player;
using UnityEngine;

namespace Controller
{
    [Serializable]
    public enum PlayerMovementMode
    {
        Platform,
        Topdown,
    }
    public class PlayerController : CreatureBase, ISaveDataProcesser, IPropertyPercent
    {
        private static PlayerController _instance = null;
        public static PlayerController Instance => _instance;
        public static float CurrentZValue;
        private static readonly int AnimOnGround = Animator.StringToHash("onGround");
        private static readonly int AnimMoveSpeed = Animator.StringToHash("moveSpeed");
        private static readonly int AnimMovingBackwards = Animator.StringToHash("movingBackwards");
        private static readonly int AnimFlip = Animator.StringToHash("Flip");
        private bool CanMoveForward => !playerRigidbody.constraints.HasFlag(RigidbodyConstraints.FreezePositionZ);
        public Rigidbody playerRigidbody;
        public float moveSpeed, jumpForce;
        public LayerMask groundLayer;
        public Transform footPoint;
        public Vector3 rightDirection = Vector3.right;
        public Vector3 frontDirection = Vector3.forward;
        public Animator animator;
        public Animator flipAnimator;
        public SpriteRenderer spriteRenderer;
        public PlayerMovementMode movementMode = PlayerMovementMode.Platform;

        public bool IsGrounded
        {
            get => _isGrounded;
        }
        private bool _isGrounded = false;

        public int maxJumpCount = 2;

        public float movingX;

        private int _jumpCount = 0;
        private bool _movingBackwards = false;
        private readonly float _jumpResumeTime = 0.3f;
        private float _lastJumpTime;
        private Plane _xyPlane = new Plane(Vector3.forward, Vector3.zero);
        
        public void SetMovingBackwards(bool value)
        {
            animator.SetBool(AnimMovingBackwards, value);
        }
        public void SetMovementMode(PlayerMovementMode mode)
        {
            movementMode = mode;
            switch (mode)
            {
                case PlayerMovementMode.Platform:
                    SetMovingBackwards(false);
                    CurrentZValue = transform.position.z;
                    _xyPlane = new Plane(Vector3.forward, new Vector3(0, 0, CurrentZValue));
                    playerRigidbody.constraints =
                        RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ;
                    break;
                case PlayerMovementMode.Topdown:
                    playerRigidbody.constraints =
                        RigidbodyConstraints.FreezeRotation;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        public void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                PlayerPropertySlider.RegisterProperty(SliderPropertyType.Health, this);
                faction = CreatureFaction.Friendly;
                DontDestroyOnLoad(gameObject);
                return;
            }
            if (_instance != this)
            {
                Destroy(gameObject);
            }
        }
        public void Update()
        {
            if (UIFunctions.Instance.UIOpen)
            {
                return;
            }
            Vector2 input = new Vector2(
                Input.GetAxis("Horizontal"),
                CanMoveForward ? Input.GetAxis("Vertical") : 0
            ).normalized;
            var speed = rightDirection * (moveSpeed * input.x) + frontDirection * (moveSpeed * input.y);
            playerRigidbody.velocity = new Vector3(0, playerRigidbody.velocity.y, 0) + speed;
            var animMoveSpeed = playerRigidbody.velocity.magnitude;
            animator.SetFloat(AnimMoveSpeed, animMoveSpeed);
            
            movingX = input.x;

            bool bNeedFlip = false;

            if (input.y > Single.Epsilon && !_movingBackwards)
            {
                _movingBackwards = true;
                animator.SetBool(AnimMovingBackwards, true);
                bNeedFlip = true;
            }
            if (input.y < -Single.Epsilon && _movingBackwards)
            {
                _movingBackwards = false;
                animator.SetBool(AnimMovingBackwards, false);
                bNeedFlip = true;
            }
        
            if (input.x > Single.Epsilon && spriteRenderer.flipX)
            {
                spriteRenderer.flipX = false;
                bNeedFlip = true;
            }
            if (input.x < -Single.Epsilon && !spriteRenderer.flipX)
            {
                spriteRenderer.flipX = true;
                bNeedFlip = true;
            }


            if (bNeedFlip)
            {
                flipAnimator.SetTrigger(AnimFlip);
            }

            _isGrounded = Physics.BoxCast(footPoint.position, new Vector3(0.5f, 0.1f, 0.5f),
                Vector3.down, Quaternion.identity, 0.2f, groundLayer);
            animator.SetBool(AnimOnGround, _isGrounded);
            if (_isGrounded && (Time.time - _lastJumpTime > _jumpResumeTime))
            {
                _jumpCount = maxJumpCount;
            }
            if (_jumpCount > 0 && Input.GetButtonDown("Jump"))
            {
                _jumpCount--;
                var velocity = playerRigidbody.velocity;
                velocity.y = jumpForce;
                playerRigidbody.velocity = velocity;
                _lastJumpTime = Time.time;
            }
        }
        public Vector3 GetCastLocation()
        {
            return transform.position;
        }
        public Vector3 GetCastTowards()
        {
            if (Camera.main != null)
            {
                var mousePos = Input.mousePosition;
                var ray = Camera.main.ScreenPointToRay(mousePos);
                if (_xyPlane.Raycast(ray, out var hit))
                {
                    return ray.GetPoint(hit) - transform.position;
                }
            }
            return spriteRenderer.flipX ? Vector3.left : Vector3.right;
        }
        public void SaveDataTo(ref GameData gameData)
        {
            gameData.playerPosition = transform.position;
            gameData.maxHealth = maxHealthPoint;
            gameData.currentHealth = healthPoint;
            gameData.movementMode = movementMode;
        }
        public void LoadDataFrom(ref GameData gameData)
        {
            transform.position = gameData.playerPosition;
            maxHealthPoint = gameData.maxHealth;
            healthPoint = gameData.currentHealth;
            movementMode = gameData.movementMode;
            SetMovementMode(movementMode);
        }
        public void SetDefaultData(ref GameData gameData)
        {
            gameData.playerPosition = new Vector3(0, 2, 0);
            gameData.maxHealth = 100;
            gameData.currentHealth = 100;
            gameData.movementMode = PlayerMovementMode.Platform;
        }
        public float GetPropertyPercent(SliderPropertyType propertyType)
        {
            return healthPoint / maxHealthPoint;
        }
        public override void OnTakeDamage(float damage, CreatureBase source)
        {
            if (healthPoint <= 0)
            {
                UIFunctions.Instance.ShowLoseGame();
            }
        }
    }
}

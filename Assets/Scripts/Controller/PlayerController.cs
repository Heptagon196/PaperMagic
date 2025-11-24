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
    public class PlayerController : CreatureBase, ISaveDataProcesser, IPropertyPercent
    {
        private static PlayerController _instance = null;
        public static PlayerController Instance => _instance;
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
        private static readonly int AnimOnGround = Animator.StringToHash("onGround");
        private static readonly int AnimMoveSpeed = Animator.StringToHash("moveSpeed");
        private static readonly int AnimMovingBackwards = Animator.StringToHash("movingBackwards");
        private static readonly int AnimFlip = Animator.StringToHash("Flip");
        public Rigidbody playerRigidbody;
        public float moveSpeed, jumpForce;
        public LayerMask groundLayer;
        public Transform footPoint;
        public Vector3 rightDirection = Vector3.right;
        public Vector3 frontDirection = Vector3.forward;
        public bool canMoveForward = false;
    
        public Animator animator;
        public Animator flipAnimator;
        public SpriteRenderer spriteRenderer;

        public bool IsGrounded
        {
            get => _isGrounded;
        }
        private bool _isGrounded = false;

        public int maxJumpCount = 2;

        private int _jumpCount = 0;
        private bool _movingBackwards = false;
        private readonly float _jumpResumeTime = 0.3f;
        private float _lastJumpTime;
        private Plane _xyPlane = new Plane(Vector3.forward, Vector3.zero);
        private void Start()
        {
            // weapon.InitSpellTree();
        }

        void Update()
        {
            if (UIFunctions.Instance.UIOpen)
            {
                return;
            }
            Vector2 input = new Vector2(
                Input.GetAxis("Horizontal"),
                canMoveForward ? Input.GetAxis("Vertical") : 0
            ).normalized;
            var speed = rightDirection * (moveSpeed * input.x) + frontDirection * (moveSpeed * input.y);
            playerRigidbody.velocity = new Vector3(0, playerRigidbody.velocity.y, 0) + speed;
            var animMoveSpeed = playerRigidbody.velocity.magnitude;
            animator.SetFloat(AnimMoveSpeed, animMoveSpeed);

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
        }
        public void LoadDataFrom(ref GameData gameData)
        {
            transform.position = gameData.playerPosition;
            maxHealthPoint = gameData.maxHealth;
            healthPoint = gameData.currentHealth;
        }
        public void SetDefaultData(ref GameData gameData)
        {
            gameData.playerPosition = transform.position;
            gameData.maxHealth = 100;
            gameData.currentHealth = 100;
        }
        public float GetPropertyPercent(SliderPropertyType propertyType)
        {
            return healthPoint / maxHealthPoint;
        }
        public override void OnTakeDamage(float damage, CreatureBase source) {}
    }
}

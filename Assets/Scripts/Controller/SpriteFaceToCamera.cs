using System;
using UnityEngine;

namespace Controller
{
    public class SpriteFaceToCamera : MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;
        private Camera _camera;
        private void Start()
        {
            _camera = Camera.main;
        }
        private void Update()
        {
            transform.rotation = _camera.transform.rotation;
        }
    }
}
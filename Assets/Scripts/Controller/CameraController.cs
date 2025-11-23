using UnityEngine;

namespace Controller
{
    public class CameraController : MonoBehaviour
    {
        public Transform target;

        private Vector3 _offset;

        private void Start()
        {
            _offset = target.position - transform.position;
            _offset.x = 0;
        }

        // Update is called once per frame
        private void LateUpdate()
        {
            transform.position = target.position - _offset;
        }
    }
}

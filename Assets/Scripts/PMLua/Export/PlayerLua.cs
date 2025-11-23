using Controller;
using UnityEngine;
using XLua;

namespace PMLua.Export
{
    [LuaCallCSharp]
    public class PlayerLua
    {
        public GameObject GetObject()
        {
            return PlayerController.Instance.gameObject;
        }
        public Vector3 GetCastPosition()
        {
            return PlayerController.Instance.GetCastLocation();
        }
        public Vector3 GetPosition()
        {
            return PlayerController.Instance.transform.position;
        }
        public void SetPosition(Vector3 playerLocation)
        {
            PlayerController.Instance.transform.position = playerLocation;
        }
        public Vector3 GetVelocity()
        {
            return PlayerController.Instance.playerRigidbody.velocity;
        }
        public void SetVelocity(Vector3 playerVelocity)
        {
            PlayerController.Instance.playerRigidbody.velocity = playerVelocity;
        }
    }
}
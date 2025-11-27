using System;
using Controller;
using Quest;
using UI.General;
using UnityEngine;

namespace SceneItems
{
    public class QuestChecker : MonoBehaviour
    {
        public string questID;
        private float _lastTipTime = 0;
        private void LateUpdate()
        {
            if (QuestManager.GetCachedQuestStatus(questID) != QuestStatus.Completed)
            {
                var vec = PlayerController.Instance.playerRigidbody.velocity;
                if (!Mathf.Approximately(vec.x, 0))
                {
                    PlayerController.Instance.playerRigidbody.velocity = Vector3.zero;
                    if (Time.time - _lastTipTime > 3)
                    {
                        _lastTipTime = Time.time;
                        UIFunctions.Instance.ShowFloatTip("完成任务前无法移动");
                    }
                }
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }
}
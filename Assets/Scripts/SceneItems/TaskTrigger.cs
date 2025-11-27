using System;
using Controller;
using Quest;
using UnityEngine;

namespace SceneItems
{
    public class TaskTrigger : MonoBehaviour
    {
        public string triggerTask;
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject == PlayerController.Instance.gameObject)
            {
                if (!string.IsNullOrEmpty(triggerTask))
                {
                    QuestManager.AddQuest(triggerTask);
                    // QuestManager.SelectedQuest = triggerTask;
                }
                gameObject.SetActive(false);
            }
        }
    }
}
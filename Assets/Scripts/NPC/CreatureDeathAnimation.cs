using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

namespace NPC
{
    public class CreatureDeathAnimation : MonoBehaviour
    {
        private const string DeathAnimationStateName = "death";
        public void InitAnim(CreatureAnimation inCreatureAnimation)
        {
            transform.position = inCreatureAnimation.transform.position;
            var sprite = gameObject.AddComponent<SpriteRenderer>();
            var creatureAnimation = gameObject.AddComponent<CreatureAnimation>();
            creatureAnimation.loop = false;
            creatureAnimation.Animations = inCreatureAnimation.Animations;
            creatureAnimation.AnimationSwitchDuration = inCreatureAnimation.AnimationSwitchDuration;
            creatureAnimation.SetAnimStat(DeathAnimationStateName);
            float deathTime = 0;
            if (creatureAnimation.currentSprites != null)
            {
                deathTime = creatureAnimation.currentSprites.Count * creatureAnimation.switchDuration;
            }
            sprite.DOFade(0, deathTime).OnComplete(() =>
            {
                Destroy(gameObject);
            });
        }
    }
}
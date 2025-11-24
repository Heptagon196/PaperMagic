using System;
using System.Collections.Generic;
using Backpack;
using UnityEngine;

namespace NPC
{
    public class CreatureAnimation : MonoBehaviour
    {
        public float switchDuration = 0.2f;
        public Dictionary<string, List<Sprite>> Animations = new();
        public Dictionary<string, float> AnimationSwitchDuration = new();
        public string currentStage = "";
        public List<Sprite> currentSprites;
        public int currentAnimationFrame = 0;
        public bool loop = true;

        private float _lastSwitchTime = 0;
        private SpriteRenderer _spriteRenderer;
        private void Awake()
        {
            _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        }
        private void Update()
        {
            if (currentSprites == null || currentSprites.Count == 0)
            {
                return;
            }
            if (Time.time - _lastSwitchTime > switchDuration)
            {
                _lastSwitchTime = Time.time;
                _spriteRenderer.sprite = currentSprites[currentAnimationFrame];
                currentAnimationFrame++;
                if (currentAnimationFrame < currentSprites.Count)
                {
                    return;
                }
                if (loop)
                {
                    currentAnimationFrame -= currentSprites.Count;
                }
                else
                {
                    currentAnimationFrame--;
                }
            }
        }

        public void LoadSpriteSet(Dictionary<string, List<string>> spriteSetList,
            Dictionary<string, float> switchDurations)
        {
            AnimationSwitchDuration = switchDurations;
            _spriteRenderer.sprite = null;
            foreach (var spriteSet in spriteSetList)
            {
                LoadSpriteSetForStat(spriteSet.Key, spriteSet.Value);
            }
        }
        public void LoadSpriteSetForStat(string animStage, List<string> spritePathList)
        {
            Animations[animStage] = new(spritePathList.Count);
            var spriteSet = Animations[animStage];
            for (var idx = 0; idx < spritePathList.Count; idx++)
            {
                spriteSet.Add(null);
                var spriteIdx = idx;
                SpriteLoader.Instance.AsyncLoadSingeSprite(spritePathList[idx], sprite =>
                {
                    spriteSet[spriteIdx] = sprite;
                });
            }
        }
        public void SetAnimStat(string animStage)
        {
            currentAnimationFrame = 0;
            Animations.TryGetValue(animStage, out currentSprites);
            AnimationSwitchDuration.TryGetValue(animStage, out switchDuration);
            _lastSwitchTime = 0;
        }
    }
}
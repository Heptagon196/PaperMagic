using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace Backpack
{
    public delegate void OnSpriteLoaded(Sprite sprite);
    public class SpriteLoader : MonoBehaviour
    {
        public static SpriteLoader Instance;
        private const string SpriteDir = "GameData/Sprites";
        private static readonly Dictionary<string, Sprite> Sprites = new();
        private static readonly Dictionary<string, List<object>> WaitLoadList = new();
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            // StartCoroutine(LoadAllIcons());
        }
        public void AsyncLoadSingeSprite(string filePath, object targetObject)
        {
            StartCoroutine(_AsyncLoadSingleSprite(filePath, targetObject));
        }
        public void AsyncLoadSingeSprite(string filePath, OnSpriteLoaded callBack)
        {
            StartCoroutine(_AsyncLoadSingleSprite(filePath, callBack));
        }
        private static void OnSpriteLoaded(object targetObject, Sprite sprite)
        {
            switch (targetObject)
            {
                case Image image:
                    image.sprite = sprite;
                    break;
                case SpriteRenderer spriteRenderer:
                    spriteRenderer.sprite = sprite;
                    break;
                case OnSpriteLoaded onSpriteLoaded:
                    onSpriteLoaded.Invoke(sprite);
                    break;
            }
        }
        private IEnumerator _AsyncLoadSingleSprite(string iconPath, object targetObject)
        {
            if (Sprites.TryGetValue(iconPath, out var sprite))
            {
                OnSpriteLoaded(targetObject, sprite);
                yield break;
            }
            
            WaitLoadList.TryGetValue(iconPath, out var list);
            if (list == null)
            {
                WaitLoadList.Add(iconPath, new List<object>(new [] { targetObject }));
            }
            else
            {
                WaitLoadList[iconPath].Add(targetObject);
                yield break;
            }
            
            yield return AsyncLoadSprite(Path.Combine(SpriteDir, iconPath));
            Sprites.TryGetValue(iconPath, out sprite);
            if (sprite != null)
            {
                WaitLoadList.TryGetValue(iconPath, out list);
                if (list != null)
                {
                    foreach (var item in list)
                    {
                        OnSpriteLoaded(item, sprite);
                    }
                    WaitLoadList.Remove(iconPath);
                }
            }
        }
        private IEnumerator AsyncLoadSprite(string filePath)
        {
            var request = UnityWebRequestTexture.GetTexture(Path.GetFullPath(filePath));
            yield return request.SendWebRequest();
            if (request.result == UnityWebRequest.Result.Success)
            {
                var texture = DownloadHandlerTexture.GetContent(request);
                Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
                Sprites.Add(Path.GetRelativePath(SpriteDir, filePath).Replace('\\', '/'), sprite);
            }
        }
    }
}
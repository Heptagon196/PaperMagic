using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = System.Object;

namespace Controller
{
    public class EventParamBase
    {
        public readonly Enum EventType;
        public readonly GameObject Subject;
        public readonly string ObjectID;
        public EventParamBase(Enum eventType, GameObject subject = null, string objectID = null)
        {
            EventType = eventType;
            Subject = subject;
            ObjectID = objectID;
        }
    }
    public static class EventManager
    {
        private static readonly Dictionary<Enum, Dictionary<Object, List<Action<EventParamBase>>>> Events = new();
        public static void AddListener(Object self, Enum eventType, Action<EventParamBase> listener)
        {
            if (Events.TryGetValue(eventType, out var eventList))
            {
                eventList.TryAdd(self, new());
                eventList[self].Add(listener);
            }
            else
            {
                Events.Add(eventType, new ()
                {
                    { self, new() { listener } }
                });
            }
        }
        public static void RemoveListeners(Object self, Enum eventType)
        {
            if (Events.TryGetValue(eventType, out var objectsList))
            {
                objectsList.Remove(self);
            }
        }
        public static void RemoveListeners(Enum eventType)
        {
            Events.Remove(eventType);
        }
        public static void Broadcast(EventParamBase param)
        {
            if (Events.TryGetValue(param.EventType, out var eventList))
            {
                foreach (var subject in eventList.SelectMany(objects => objects.Value))
                {
                    subject?.Invoke(param);
                }
            }
        }
        public static void Broadcast(Enum eventType, GameObject subject = null, string objectID = null)
        {
            Broadcast(new EventParamBase(eventType, subject, objectID));
        }
    }
}
namespace Cheems
{
    using System.Collections.Generic;
    using UnityEngine.EventSystems;
    using UnityEngine.Events;

    public static class EventTriggerUtility
    {
        public static void AddEvent(EventTrigger eventTrigger, EventTriggerType eventType,
                                    UnityAction<BaseEventData> action)
        {
            EventTrigger.Entry entry = new EventTrigger.Entry
                                       {
                                           eventID = eventType
                                       };
            entry.callback.AddListener(action);
            eventTrigger.triggers.Add(entry);
        }

        public static void AddEvent(EventTrigger eventTrigger, EventTriggerType eventType, UnityAction action)
        {
            EventTrigger.Entry entry = new EventTrigger.Entry
                                       {
                                           eventID = eventType
                                       };
            entry.callback.AddListener((data) => action());
            eventTrigger.triggers.Add(entry);
        }

        public static void RemoveEvent(EventTrigger eventTrigger, EventTriggerType eventType,
                                       UnityAction<BaseEventData> action)
        {
            EventTrigger.Entry entry = eventTrigger.triggers.Find(e => e.eventID == eventType);
            if (entry != null)
            {
                entry.callback.RemoveListener(action);
                eventTrigger.triggers.Remove(entry);
            }
        }

        public static void RemoveEvent(EventTrigger eventTrigger, EventTriggerType eventType, UnityAction action)
        {
            EventTrigger.Entry entry = eventTrigger.triggers.Find(e => e.eventID == eventType);
            if (entry != null)
            {
                entry.callback.RemoveListener((data) => action());
                eventTrigger.triggers.Remove(entry);
            }
        }

        public static void ClearAllEvents(EventTrigger eventTrigger)
        {
            eventTrigger.triggers.Clear();
        }

        public static void RemoveAllEvents(EventTrigger eventTrigger, params EventTriggerType[] eventTypes)
        {
            List<EventTrigger.Entry> entries = new List<EventTrigger.Entry>();
            foreach (var eventType in eventTypes)
            {
                entries.AddRange(eventTrigger.triggers.FindAll(entry => entry.eventID == eventType));
            }

            foreach (var entry in entries)
            {
                entry.callback.RemoveAllListeners();
                eventTrigger.triggers.Remove(entry);
            }
        }
    }
}
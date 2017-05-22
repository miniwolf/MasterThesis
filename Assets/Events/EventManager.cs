using System.Collections.Generic;
using UnityEngine;

namespace Assets.Events {
    public enum Events {
        QuestStarted,
        Travelled,
        StartedTalking
    }

    public class EventManager : MonoBehaviour {
        private static readonly Dictionary<Events, List<EventHandler>> handlers
            = new Dictionary<Events, List<EventHandler>>();

        private void Awake() {
            handlers.Add(Events.QuestStarted, new List<EventHandler>());
            handlers.Add(Events.Travelled, new List<EventHandler>());
            handlers.Add(Events.StartedTalking, new List<EventHandler>());
        }

        /// <summary>
        /// Subscribing to an event will make the eventmanager call the handler when the event is
        /// invoked on the eventmanager. @see CallEvent(Events)
        /// </summary>
        /// <param name="events">Event that the handler is subscribing to</param>
        /// <param name="handler">Handler to be called. @See EventHandler</param>
        public static void SubscribeToEvent(Events events, EventHandler handler) {
            handlers[events].Add(handler);
        }

        /// <summary>
        /// Will invoke Action on every EventHandler subscribed to the Events argument.
        /// </summary>
        /// <param name="events">Events to invoke handlers on</param>
        public static void CallEvent(Events events) {
            List<EventHandler> eventHandlers;
            if (!handlers.TryGetValue(events, out eventHandlers)) {
                Debug.Log("Error: Remember to add the assigned handler to the scene: " + events);
                return;
            }
            foreach (var eventHandler in eventHandlers) {
                eventHandler.Action();
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Threading;
using Assets.Network.Client.Handlers.Container;
using Assets.Network.Shared.Messages;
using UnityEngine;

namespace Assets.Network.Client.Handlers {
    public class GeneralHandler : Handler {
        private static readonly Dictionary<Type, Handler> handlers = new Dictionary<Type, Handler>();
        private static readonly Container.Container container = new DefaultContainer();
        private readonly DefaultThreadHandler threadHandler;
        private readonly List<object> unhandledObjects = new List<object>();
        private readonly List<Thread> threads = new List<Thread>();
        private readonly Thread runnable;

        public GeneralHandler() {
            threadHandler = new DefaultThreadHandler(container, this);
            runnable = new Thread(() => threadHandler.Start());
            runnable.Start();
            container.SetThread(runnable);
        }

        public void RegisterHandler(Type type, Handler playerStateHandler) {
            handlers.Add(type, playerStateHandler);
            InputHandler.Register(type, container);
        }

        public void Handle(InGoingMessages obj) {
            var handler = handlers[obj.GetType()];

            if (handler == null) {
                Debug.LogError("Missing handler " + obj.GetType());
                unhandledObjects.Add(obj);
                return;
            }

            var thread = new Thread(() => {
                handler.Handle(obj);
            });

            thread.Start();
            threads.Add(thread);
        }

        public Thread GetThread() {
            return runnable;
        }
    }
}
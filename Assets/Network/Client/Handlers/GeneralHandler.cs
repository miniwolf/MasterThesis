using System;
using System.Collections.Generic;
using System.Threading;
using Assets.Network.Client;
using Network.Client.Handlers.Container;
using Network.Shared.Messages;
using UnityEngine;

namespace Network.Client.Handlers {
    public class GeneralHandler : Handler {
        private static readonly Dictionary<Type, Handler> handlers = new Dictionary<Type, Handler>();
        private static readonly Container.Container container = new DefaultContainer();
        private readonly List<object> unhandledObjects = new List<object>();
        private readonly List<Thread> threads = new List<Thread>();
        private readonly Thread runnable;

        public GeneralHandler() {
            runnable = new Thread(() => new DefaultThreadHandler(container, this).Start());
        }

        public static void RegisterHandler(Type type, Handler playerStateHandler) {
            handlers.Add(type, playerStateHandler);
            InputHandler.Register(type, container);
        }

        public void Handle(InGoingMessages obj) {
            var handler = GeneralHandler.handlers[obj.GetType()];

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
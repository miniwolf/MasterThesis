﻿using Network.Client;
using Network.Client.Handlers;
using UnityEngine;

namespace Assets.Network.Client {
    public class Client : MonoBehaviour {
        private Communication com;
        public Communication Communication {
            get { return com; }
        }

        public void Start() {
            com = new Communication("localhost", 8001);
            GeneralHandlerFactory.Construct();
        }
    }
}
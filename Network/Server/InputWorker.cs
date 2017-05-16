using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using Assets.Network.Shared;

namespace Network.Server {
    public class InputWorker : Worker {
        private readonly int ID;
        private readonly OutputWorker output;
        private readonly NetworkStream objIn;
        private readonly BinaryFormatter formatter = new BinaryFormatter();

        public InputWorker(int ID, TcpClient tcpClient, OutputWorker output) {
            this.ID = ID;
            this.output = output;
            objIn = tcpClient.GetStream();
        }

        public void Start() {
            try {
                while (running) {
                    var input = formatter.Deserialize(objIn);
                    HandleInput(input);
                }
            } catch (IOException) {
                Data.RemoveUser(ID);
                Console.Out.WriteLine("Logging out ID: " + ID);
                running = false;
            }
        }

        private void HandleInput(object input) {
            if (input == null) {
                return;
            }
            var s = input as string;
            if (s != null) {
                Console.Out.WriteLine(s);
            } else if (input is UpdateCount) {
                Console.Out.WriteLine((UpdateCount) input);
            } else if (input is Location) {
                HandleLocation(input);
                output.Response.Enqueue(new AllIsWell());
            } else if (input is Quest) {
                HandleQuest(input);
                output.Response.Enqueue(new AllIsWell());
            } else if (input is GetState) {
                var playerState = HandleGetState();
                output.Response.Enqueue(playerState);
            } else if (input is GetOtherStates) {
                var playerStates = HandleGetOtherStates();
                output.Response.Enqueue(playerStates);
            } else if (input is TalkingTo) {
                HandleTalking(input);
                output.Response.Enqueue(new AllIsWell());
            }
        }

        private void HandleLocation(object input) {
            var playerState = Data.GetUserState(ID);
            Console.Out.WriteLine("Location");
            playerState.Location = (Location) input;
            Data.UpdateState(ID, playerState);
        }

        private void HandleQuest(object input) {
            var playerState = Data.GetUserState(ID);
            Console.Out.WriteLine("Quest");
            playerState.Quest = (Quest) input;
            Data.UpdateState(ID, playerState);
        }

        private PlayerState HandleGetState() {
            var playerState = Data.GetUserState(ID);
            Console.Out.WriteLine("GetState");
            return playerState;
        }

        private IEnumerable<PlayerState> HandleGetOtherStates() {
            var playerStates = Data.GetAllBut(ID);
            Console.Out.WriteLine("GetOtherState");
            return playerStates;
        }

        private void HandleTalking(object input) {
            var playerState = Data.GetUserState(ID);
            Console.Out.WriteLine("Talking");
            playerState.Npc = ((TalkingTo) input).npc;
            Data.UpdateState(ID, playerState);
        }
    }

    public abstract class Worker {
        protected bool running = true;
    }
}
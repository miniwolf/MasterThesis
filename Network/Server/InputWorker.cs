using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Assets.Network.Shared;
using Assets.Network.Shared.Actions;
using Assets.Network.Shared.Messages;
using Xml2CSharp;

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
            } catch (SerializationException) {
            }
            Console.Out.WriteLine("Logging out ID: " + ID);
            Data.RemoveUser(ID);
            running = false;
        }

        private void HandleInput(object input) {
            if (input == null) {
                return;
            }
            var s = input as string;
            var to = input as GoingTo;
            if (to != null) {
                HandleLocation(to);
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
            } else if (input is HasChosen) {
                HandleChoices();
                Data.SendToAllOther(ID, new OtherHasChosen(((HasChosen) input).Choice));
                output.Response.Enqueue(new AllIsWell());
            } else if (input is StayResponse) {
                Data.SendToAllOther(ID, input);
                output.Response.Enqueue(new AllIsWell());
            } else if (input is StartedQuest) {
                HandleQuest(input);
                output.Response.Enqueue(new AllIsWell());
            } else {
                Console.Out.WriteLine("Does not understand: " + input);
            }
        }

        private void HandleChoices() {
            Data.GetAllBut(ID);
        }

        private void HandleLocation(object input) {
            var playerState = Data.GetUserState(ID);
            Console.Out.WriteLine("Location");
            playerState.Location = ((InGoingMessages<Location>) input).GetAccess().GetData();
            Data.UpdateState(ID, playerState);
        }

        private void HandleQuest(object input) {
            var playerState = Data.GetUserState(ID);
            Console.Out.WriteLine("Quest");
            playerState.Quest = ((InGoingMessages<Quest>) input).GetAccess().GetData();
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
            playerState.Npc = ((InGoingMessages<string>) input).GetAccess().GetData();
            Data.UpdateState(ID, playerState);
        }
    }

    public abstract class Worker {
        protected bool running = true;
    }
}
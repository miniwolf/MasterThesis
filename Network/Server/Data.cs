﻿using System.Collections.Generic;
using System.Linq;
using Network.Shared;

namespace Network.Server {
    public class Data {
        private static readonly Dictionary<int, OutputWorker> OUTPUT_WORKER_MAP =
            new Dictionary<int, OutputWorker>();

        private static readonly Dictionary<int, InputWorker> INPUT_WORKER_MAP =
            new Dictionary<int, InputWorker>();

        private static readonly Dictionary<int, PlayerState> PlayerStates =
            new Dictionary<int, PlayerState>();

        public static void AddUser(int ID, OutputWorker outputWorker, InputWorker inputWorker) {
            OUTPUT_WORKER_MAP.Add(ID, outputWorker);
            INPUT_WORKER_MAP.Add(ID, inputWorker);
        }

        public static void UpdateState(int ID, PlayerState state) {
            PlayerStates[ID] = state;
            foreach (var outWorker in OUTPUT_WORKER_MAP.Values) {
                if (outWorker.GetID() == ID) {
                    continue;
                }
                outWorker.Response.Enqueue(state);
            }
        }

        public static PlayerState GetUserState(int ID) {
            return PlayerStates[ID];
        }

        public static IEnumerable<PlayerState> GetAllBut(int ID) {
            return (from state in PlayerStates.Keys where state != ID select PlayerStates[state]).ToList();
        }

        public static void RemoveUser(int id) {
            OUTPUT_WORKER_MAP.Remove(id);
            INPUT_WORKER_MAP.Remove(id);
        }
    }
}

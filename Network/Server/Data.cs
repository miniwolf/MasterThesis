using System.Collections.Generic;
using System.Linq;
using Network.Server;
using Network.Shared;

namespace Assets.Network.Server {
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
            PlayerStates.Add(ID, state);
        }

        public static PlayerState GetUserState(int ID) {
            return PlayerStates[ID];
        }

        public static IEnumerable<PlayerState> GetAllBut(int ID) {
            return PlayerStates.Keys.Where(id => id != ID).Select(id => PlayerStates[id]);
        }
    }
}

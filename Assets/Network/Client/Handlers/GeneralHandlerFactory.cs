using Network.Shared;

namespace Network.Client.Handlers {
    public class GeneralHandlerFactory {
        public static void Construct() {
            GeneralHandler.RegisterHandler(typeof(PlayerState), new PlayerStateHandler());
        }
    }
}

using Assets.scripts;
using Network.Shared;

namespace Network.Client.Handlers {
    public class GeneralHandlerFactory {
        public static GeneralHandler handler = new GeneralHandler();

        public static void Construct(Player player) {
            handler.RegisterHandler(typeof(PlayerState), new PlayerStateHandler(player));
        }
    }
}

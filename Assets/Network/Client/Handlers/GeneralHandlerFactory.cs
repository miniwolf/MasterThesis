using Assets.Network.Shared;
using Assets.scripts;

namespace Assets.Network.Client.Handlers {
    public class GeneralHandlerFactory {
        public static GeneralHandler handler = new GeneralHandler();

        public static void Construct(Player player, Player me, GameStateManager manager) {
            handler.RegisterHandler(typeof(PlayerState), new PlayerStateHandler(player, me, manager));
        }
    }
}

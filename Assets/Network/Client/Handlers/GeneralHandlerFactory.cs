using Assets.Network.Shared;
using Assets.Network.Shared.Actions;
using Assets.scripts;

namespace Assets.Network.Client.Handlers {
    public class GeneralHandlerFactory {
        public static GeneralHandler handler = new GeneralHandler();

        public static void Construct(GameStateManager manager) {
            handler.RegisterHandler(typeof(PlayerState), new PlayerStateHandler(manager));
            handler.RegisterHandler(typeof(StayResponse), new StayResponseHandler());
            handler.RegisterHandler(typeof(OtherHasChosen), new OtherChosenHandler());
        }
    }
}

namespace Assets.Events {
    /// <summary>
    /// Together with EventManager events will be called from outside and all registered handlers
    /// will be notified. @See EventManager.SubscribeToEvent(EventManager.Events)
    /// </summary>
    public interface EventHandler {
        void Action();
    }
}
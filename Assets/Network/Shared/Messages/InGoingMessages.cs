﻿namespace Network.Shared.Messages {
    public interface InGoingMessages {
    }

    public interface InGoingMessages<T> : InGoingMessages {
        Access<T> GetAcces();
    }
}
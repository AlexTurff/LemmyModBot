﻿using LemmyModBot.RequestModels;

namespace LemmyModBot
{
    public interface IApiConnection
    {
        string Url { get; }

        void Login(string username, string password);
        TResponse? SendRequest<TRequest,TResponse>(TRequest request) where TRequest : RequestBase;
    }
}
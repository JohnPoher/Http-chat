using System;
using System.Threading.Tasks;

namespace ChatServer;

public interface IServer : IDisposable
{
    public Task StartAsync(string uri);
}
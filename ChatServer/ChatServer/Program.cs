using System.Threading.Tasks;
using ChatServer.Commands;
using ChatServer.Providers.Details;
using ChatServer.Providers.Manufacturers;

namespace ChatServer;

public static class Program
{
    private const string ServerUri = "http://127.0.0.1:8888/";

    public static async Task Main()
    {
        await CreateServer().StartAsync(ServerUri).ConfigureAwait(false);
    }

    private static IServer CreateServer()
    {
        return Locator.Current.Locate<IServer>();
    }
}
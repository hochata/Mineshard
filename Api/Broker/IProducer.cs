namespace Mineshard.Api.Broker;

public interface IProducer
{
    public void Send(Guid repoId);
}

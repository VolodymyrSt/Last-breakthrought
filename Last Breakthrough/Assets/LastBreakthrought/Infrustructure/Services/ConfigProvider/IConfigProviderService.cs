using LastBreakthrought.Configs.Game;
using LastBreakthrought.Configs.Player;

namespace LastBreakthrought.Infrustructure.Services.ConfigProvider
{
    public interface IConfigProviderService
    {
        PlayerConfigSO PlayerConfigSO { get; }
        GameConfigSO GameConfigSO { get; }
    }
}
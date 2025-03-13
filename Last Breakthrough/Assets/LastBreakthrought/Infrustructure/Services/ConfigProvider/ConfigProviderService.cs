using LastBreakthrought.Configs.Game;
using LastBreakthrought.Configs.Player;

namespace LastBreakthrought.Infrustructure.Services.ConfigProvider
{
    public class ConfigProviderService : IConfigProviderService
    {
        public PlayerConfigSO PlayerConfigSO { get; private set; }
        public GameConfigSO GameConfigSO { get; private set; }

        public ConfigProviderService(PlayerConfigSO playerConfigSO, GameConfigSO gameConfigSO)
        {
            PlayerConfigSO = playerConfigSO;
            GameConfigSO = gameConfigSO;
        }
    }
}

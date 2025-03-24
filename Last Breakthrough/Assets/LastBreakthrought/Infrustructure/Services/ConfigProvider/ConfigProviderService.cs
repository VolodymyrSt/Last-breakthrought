using LastBreakthrought.Configs.Enemy;
using LastBreakthrought.Configs.Game;
using LastBreakthrought.Configs.Player;

namespace LastBreakthrought.Infrustructure.Services.ConfigProvider
{
    public class ConfigProviderService : IConfigProviderService
    {
        public PlayerConfigSO PlayerConfigSO { get; private set; }
        public GameConfigSO GameConfigSO { get; private set; }
        public EnemyConfigHolderSO EnemyConfigHolderSO { get; private set; }

        public ConfigProviderService(PlayerConfigSO playerConfigSO, GameConfigSO gameConfigSO, EnemyConfigHolderSO enemyConfigHolderSO )
        {
            PlayerConfigSO = playerConfigSO;
            GameConfigSO = gameConfigSO;
            EnemyConfigHolderSO = enemyConfigHolderSO;
        }
    }
}

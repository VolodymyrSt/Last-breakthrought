using LastBreakthrought.Configs.Enemy;
using LastBreakthrought.Configs.Game;
using LastBreakthrought.Configs.Player;
using LastBreakthrought.Configs.Robot;

namespace LastBreakthrought.Infrustructure.Services.ConfigProvider
{
    public interface IConfigProviderService
    {
        PlayerConfigSO PlayerConfigSO { get; }
        GameConfigSO GameConfigSO { get; }
        EnemyConfigHolderSO EnemyConfigHolderSO { get; }
        RobotConfigHolderSO RobotConfigHolderSO { get; }
    }
}
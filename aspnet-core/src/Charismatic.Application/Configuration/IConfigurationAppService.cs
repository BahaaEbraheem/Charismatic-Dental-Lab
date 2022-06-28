using System.Threading.Tasks;
using Charismatic.Configuration.Dto;

namespace Charismatic.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}

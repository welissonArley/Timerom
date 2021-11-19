using System;
using System.Threading.Tasks;

namespace Timerom.App.Services.XamarinEssentials.Interface
{
    public interface ILauncher
    {
        Task OpenAsync(Uri uri);
    }
}

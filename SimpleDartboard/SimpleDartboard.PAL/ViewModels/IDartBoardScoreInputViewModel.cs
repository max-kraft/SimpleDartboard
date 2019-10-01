using System.Windows.Input;

namespace SimpleDartboard.PAL.ViewModels
{
    public interface IDartBoardScoreInputViewModel : IContentViewModel
    {
        ICommand AddScoreActionForSelectedPlayerCommand { get; set; }
    }
}
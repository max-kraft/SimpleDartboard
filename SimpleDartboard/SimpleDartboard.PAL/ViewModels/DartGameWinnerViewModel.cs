using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;
using SimpleDartboard.PAL.Core;
using SimpleDartboard.PAL.Models;

namespace SimpleDartboard.PAL.ViewModels
{
    public class DartGameWinnerViewModel : BaseViewModel, IDartGameWinnerViewModel
    {
        private IPlayerScoreBoardViewModel _playerScoreBoardViewModel;
        private DartGameSetting _dartGameSetting;
        public IPlayerScoreBoardViewModel PlayerScoreBoardViewModel
        {
            get => _playerScoreBoardViewModel;
            set
            {
                _playerScoreBoardViewModel = value;
                OnPropertyChanged("PlayerScoreBoardViewModel");
            }
        }

        public ICommand StartNewGameCommand { get; set; }

        public DartGameWinnerViewModel()
        {
            StartNewGameCommand = new RelayCommand(StartNewGame);
            _dartGameSetting = new DartGameSetting();
            Mediator.Register(MessageType.ShowWinner, ShowWinner);
            Mediator.Register(MessageType.StartGame, SaveDartGameSettings);
        }

        private void SaveDartGameSettings(object gameSetting)
        {
            _dartGameSetting = gameSetting as DartGameSetting;
        }

        private void ShowWinner(object playerToShow)
        {
            PlayerScoreBoardViewModel = playerToShow as IPlayerScoreBoardViewModel;
            if(PlayerScoreBoardViewModel == null) return;
            if (_dartGameSetting.PlayerOne.Name == PlayerScoreBoardViewModel.Name)
            {
                _dartGameSetting.PlayerOne.LegAmount = PlayerScoreBoardViewModel.Legs.Count;
                _dartGameSetting.PlayerOne.IsFirstSelected = false;
                _dartGameSetting.PlayerTwo.IsFirstSelected = true;
            }
            else
            {
                _dartGameSetting.PlayerTwo.LegAmount = PlayerScoreBoardViewModel.Legs.Count;
                _dartGameSetting.PlayerTwo.IsFirstSelected = false;
                _dartGameSetting.PlayerOne.IsFirstSelected = true;
            }
        }

        private void StartNewGame()
        {
            _dartGameSetting.PlayerOne.ScoreActions.Clear();
            _dartGameSetting.PlayerTwo.ScoreActions.Clear();
            Mediator.NotifyColleagues(MessageType.StartGame, _dartGameSetting);
        }
    }
}
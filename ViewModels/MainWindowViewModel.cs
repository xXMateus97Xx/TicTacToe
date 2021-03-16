using System.Collections.ObjectModel;
using ReactiveUI;

namespace TicTacToe.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        char _nextTurn;
        bool _hasFinished;

        public MainWindowViewModel()
        {
            _nextTurn = 'X';
            _symbols = new ObservableCollection<ObservableCollection<char?>>
            {
                new ObservableCollection<char?> { null, null, null },
                new ObservableCollection<char?> { null, null, null },
                new ObservableCollection<char?> { null, null, null }
            };
        }

        ObservableCollection<ObservableCollection<char?>> _symbols;
        public ObservableCollection<ObservableCollection<char?>> Symbols => _symbols;

        string _winnerMessage;
        public string  WinnerMessage
        {
            get => _winnerMessage;
            set => this.RaiseAndSetIfChanged(ref _winnerMessage, value);
        }

        public void ResetGame()
        {
            _nextTurn = 'X';
            _hasFinished = false;
            WinnerMessage = string.Empty;
            for (int i = 0; i < _symbols.Count; i++)
                for (int j = 0; j < _symbols[i].Count; j++)
                    _symbols[i][j] = null;
        }

        public void PlayTurn(string coordinate)
        {
            if (_hasFinished)
                return;

            var (x , y) = ParseCoordinate(coordinate);

            if (_symbols[x][y].HasValue)
                return;

            _symbols[x][y] = _nextTurn;

            if (_nextTurn == 'X')
                _nextTurn = 'O';
            else
                _nextTurn = 'X';

            CheckWinner();
        }

        void CheckWinner()
        {
            char? symbol = null;
        
            if (_symbols[0][0] != null)
            {
                if ((_symbols[0][0] == _symbols[0][1] && _symbols[0][0] == _symbols[0][2]) ||
                    (_symbols[0][0] == _symbols[1][0] && _symbols[0][0] == _symbols[2][0]) ||
                    (_symbols[0][0] == _symbols[1][1] && _symbols[0][0] == _symbols[2][2]))
                {
                    symbol = _symbols[0][0];
                }
            }

            if (symbol == null && _symbols[0][1] != null)
                if (_symbols[0][1] == _symbols[1][1] && _symbols[0][1] == _symbols[2][1])
                    symbol = _symbols[0][1];

            if (symbol == null && _symbols[0][2] != null)
                if (_symbols[0][2] == _symbols[1][2] && _symbols[0][2] == _symbols[2][2])
                    symbol = _symbols[0][2];

            if (symbol == null && _symbols[1][0] != null)
                if (_symbols[1][0] == _symbols[1][1] && _symbols[1][0] == _symbols[1][2])
                    symbol = _symbols[1][0];

            if (symbol == null && _symbols[2][0] != null)
            {
                if ((_symbols[2][0] == _symbols[2][1] && _symbols[2][0] == _symbols[2][2]) ||
                    (_symbols[2][0] == _symbols[1][1] && _symbols[2][0] == _symbols[0][2]))
                {
                    symbol = _symbols[2][0];
                }
            }

            if (symbol != null)
            {
                WinnerMessage = $"{symbol} ganhou!!!";
                _hasFinished = true;
                return;
            }

            var tie = true;
            for (int i = 0; i < _symbols.Count && tie; i++)
                for (int j = 0; j < _symbols[i].Count && tie; j++)
                    tie = _symbols[i][j] != null;

            if (tie)
            {
                WinnerMessage = "Empatou!!!";
                _hasFinished = true;
            }
        }

        (int x, int y) ParseCoordinate(string coordinate)
        {
            var x = coordinate[0] - '0';
            var y = coordinate[2] - '0';
            return(x, y);
        }
    }
}

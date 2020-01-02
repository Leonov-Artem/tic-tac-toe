using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TicTacToe
{
    enum PlayerType
    {
        User,
        Computer
    }

    public partial class Form1 : Form
    {
        const string USER = "X";
        const string COMPUTER = "O";
        const int FILD_SIZE = 5;
        const string NOTIFICATION = "Уведомление";

        PlayerType _currentPlayer;
        string[,] _playingField;

        public Form1()
        {
            InitializeComponent();
            this.ActiveControl = buttonStart;
            _currentPlayer = PlayerType.User;
            _playingField = new string[FILD_SIZE, FILD_SIZE];
        }

        private void Button_Click(object sender, EventArgs e)
        {
            this.ActiveControl = null;
            var pressedButton = sender as Button;
            pressedButton.Enabled = false;

            ChangeTextOfPressedButton(pressedButton);
            ChangePlayingField(pressedButton);

            VictoryCheck();
        }

        private void ButtonStart_Click(object sender, EventArgs e)
        {

        }

        private void ChangeTextOfPressedButton(Button pressedButton)
        {
            switch (_currentPlayer)
            {
                case PlayerType.User:
                    pressedButton.Text = USER;
                    _currentPlayer = PlayerType.Computer;
                    break;
                case PlayerType.Computer:
                    pressedButton.Text = COMPUTER;
                    _currentPlayer = PlayerType.User;
                    break;
            }
        }

        private Tuple<int, int> ComputeIndex(Button button)
        {
            int j = (button.TabIndex % FILD_SIZE);

            if (button.TabIndex >= 0 && button.TabIndex <= 4)
                return new Tuple<int, int>(0, j);

            else if (button.TabIndex >= 5 && button.TabIndex <= 9)
                return new Tuple<int, int>(1, j);

            else if (button.TabIndex >= 10 && button.TabIndex <= 14)
                return new Tuple<int, int>(2, j);

            else if (button.TabIndex >= 15 && button.TabIndex <= 19)
                return new Tuple<int, int>(3, j);

            else
                return new Tuple<int, int>(4, j);
        }

        private void ChangePlayingField(Button pressedButton)
        {
            var index = ComputeIndex(pressedButton);
            _playingField[index.Item1, index.Item2] = pressedButton.Text;
        }

        #region VictoryCheck

        private void VictoryCheck()
        {
            if (HorizontalVictory())
                MessageBox.Show("Победа по горизонтали!", NOTIFICATION);
            else if (VerticalVictory())
                MessageBox.Show("Победа по вертикали!", NOTIFICATION);
            else if (DiagonalVictory())
                MessageBox.Show("Победа по диагонали!", NOTIFICATION);
            else if (AllButtonsInactive())
                MessageBox.Show("Ничья!", NOTIFICATION);
        }

        private bool VerticalVictory()
        {
            for (int j = 0; j < FILD_SIZE; j++)
            {
                if (_playingField[0, j] != null &&
                    _playingField[0, j] == _playingField[1, j] &&
                    _playingField[0, j] == _playingField[2, j] &&
                    _playingField[0, j] == _playingField[3, j] &&
                    _playingField[0, j] == _playingField[4, j])
                    return true;
            }

            return false;
        }

        private bool HorizontalVictory()
        {
            for (int i = 0; i < FILD_SIZE; i++)
            {
                if (_playingField[i, 0] != null &&
                    _playingField[i, 0] == _playingField[i, 1] &&
                    _playingField[i, 0] == _playingField[i, 2] &&
                    _playingField[i, 0] == _playingField[i, 3] &&
                    _playingField[i, 0] == _playingField[i, 4])
                    return true;
            }

            return false;
        }

        private bool DiagonalVictory()
        {
            if (MainDiagonalVictory() || SideDiagonalVictory())
                return true;

            return false;
        }

        private bool MainDiagonalVictory()
        {
            if (_playingField[0, 0] != null &&
                _playingField[0, 0] == _playingField[1, 1] &&
                _playingField[0, 0] == _playingField[2, 2] &&
                _playingField[0, 0] == _playingField[3, 3] &&
                _playingField[0, 0] == _playingField[4, 4])
                return true;

            return false;
        }

        private bool SideDiagonalVictory()
        {
            if (_playingField[4, 0] != null &&
                _playingField[4, 0] == _playingField[3, 1] &&
                _playingField[4, 0] == _playingField[2, 2] &&
                _playingField[4, 0] == _playingField[1, 3] &&
                _playingField[4, 0] == _playingField[0, 4])
                return true;

            return false;
        }

        private bool AllButtonsInactive()
        {
            Control.ControlCollection buttons = groupBox1.Controls;

            foreach(var button in buttons)
            {
                var someButton = button as Button;
                if (someButton.Enabled)
                    return false;
            }

            return true;
        }

        #endregion
    }
}

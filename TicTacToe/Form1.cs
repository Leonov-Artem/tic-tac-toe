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

        PlayerType _currentPlayer;
        string[,] _playingField;

        public Form1()
        {
            InitializeComponent();
            this.ActiveControl = buttonStart;
            _currentPlayer = PlayerType.User;
            _playingField = new string[5, 5];
        }

        private void Button_Click(object sender, EventArgs e)
        {
            this.ActiveControl = null;
            var pressedButton = sender as Button;
            pressedButton.Enabled = false;

            ChangeTextOfPressedButton(pressedButton);
            ChangePlayingField(pressedButton);
        }

        private void ButtonStart_Click(object sender, EventArgs e)
        {

        }

        private void ChangePlayingField(Button pressedButton)
        {
            var index = ComputeIndex(pressedButton);
            _playingField[index.Item1, index.Item2] = pressedButton.Text;
        }

        private Tuple<int, int> ComputeIndex(Button button)
        {
            int j = (button.TabIndex % 5);
            int i = -1;

            if (button.TabIndex >= 0 && button.TabIndex <= 4)
                i = 0;
            else if (button.TabIndex >= 5 && button.TabIndex <= 9)
                i = 1;
            else if (button.TabIndex >= 10 && button.TabIndex <= 14)
                i = 2;
            else if (button.TabIndex >= 15 && button.TabIndex <= 19)
                i = 3;
            else
                i = 4;

            return new Tuple<int, int>(i, j);
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
    }
}

using CheckLibrary;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace CheckGame
{
    class ManeGridDrawer
    {
        public Game Game { private get; set; }
        private readonly Grid maneGrid;
        public ManeGridDrawer(Grid grid, Game game)
        {
            maneGrid = grid;
            Game = game;
        }

        public void OnGameOver()
        {
            if (Game.WhoWin() == WinColor.White)
            {
                MessageBox.Show($"Победа белых! Поздравляем, {Game.GetPlayerNames()[0]}");
            }
            else if (Game.WhoWin() == WinColor.Black)
            {
                MessageBox.Show($"Победа черных! Поздравляем, {Game.GetPlayerNames()[1]}");
            }
            ClearTextBlocks();
        }

        private void ClearTextBlocks()
        {
            foreach (UIElement element in maneGrid.Children)
            {
                if (element is TextBlock textblock)
                {
                    textblock.Text = "";
                }
            }
        }
        public void SetTextBlocksText()
        {
            foreach (UIElement element in maneGrid.Children)
            {
                if (element is TextBlock textblock)
                {
                    SetTextTo(textblock);
                }
            }
        }

        private void SetTextTo(TextBlock textblock)
        {
            if (textblock.Name == "whoGoTB")
            {
                SetWhoGoText(textblock);
            }
            if (textblock.Name == "player1TB")
            {
                SetPlayer1Name(textblock);
            }
            if (textblock.Name == "player2TB")
            {
                SetPlayer2Name(textblock);
            }
        }

        private void SetPlayer1Name(TextBlock textblock)
        {
            textblock.Text = Game.GetPlayerNames()[0];
        }
        private void SetPlayer2Name(TextBlock textblock)
        {
            textblock.Text = Game.GetPlayerNames()[1];
        }
        private void SetWhoGoText(TextBlock whoGoTB)
        {
            if (Game.WhoGo == WhoGo.White)
            {
                whoGoTB.Text = "ХОД БЕЛЫХ";
            }
            else
            {
                whoGoTB.Text = "ХОД ЧЕРНЫХ";
            }
        }
    }
}

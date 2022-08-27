using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ticTacToe {
    public partial class MainPage : ContentPage {

        private int turn = 0;
        public MainPage() {
            InitializeComponent();
        }

        private void ButtonClear_Clicked(object sender, EventArgs e) {
            LabelError.Text = "";
            LabelVictory.Text = "";
            this.ClearGrid();
        }

        private void Button_Clicked(object sender, EventArgs e) {
            Button button = (Button)sender;

            // Check if the button already was selected.
            if (button.Text != " ") {
                LabelError.Text = "Botão já selecionado.";
                return;
            }
            LabelError.Text = "";

            // Write in the button.
            switch (turn) {
                case 0:
                    button.Text = "X";
                    this.turn = 1;
                    break;
                case 1:
                    button.Text = "O";
                    this.turn = 0;
                    break;
            }

            // Check if someone has win
            string player = (turn == 0 ? "X" : "O");
            string victory = this.CheckVictory(player);

            if (victory == "") {
                return;
            }

            LabelVictory.Text = $"Vitória {victory} para o jogador [{player}]";
        }

        private string CheckVictory(string currentPlayer) {
            List<Button> buttons = this.GetButtons();

            // Check horizontals wins
            for (int i = 0; i < 9; i += 3) {
                if (buttons[i].Text == currentPlayer && buttons[i + 1].Text == currentPlayer && buttons[i + 2].Text == currentPlayer) {
                    return "horizontal";
                }
            }

            // Check verticals wins
            for (int i = 0; i < 3; i++) {
                if (buttons[i].Text == currentPlayer && buttons[i + 3].Text == currentPlayer && buttons[i + 6].Text == currentPlayer) {
                    return "vertical";
                }
            }

            // Check diagonals wins
            if (buttons[0].Text == currentPlayer && buttons[4].Text == currentPlayer && buttons[8].Text == currentPlayer) {
                return "diagonal";
            }
            if (buttons[2].Text == currentPlayer && buttons[4].Text == currentPlayer && buttons[6].Text == currentPlayer) {
                return "diagonal";
            }

            return "";
        }

        private List<Button> GetButtons() {
            List<Button> buttons = new List<Button>();

            foreach (View element in GridHash.Children.ToList<View>()) {
                buttons.Add((Button)element);
            }

            return buttons;
        }

        private void ClearGrid() {
            // Clear the old hash.
            foreach (Button button in (List<Button>)GridHash.Children) {
                button.Text = " ";
            }
        }
    }
}
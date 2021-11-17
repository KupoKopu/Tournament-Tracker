using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TrackerLibrary;
using TrackerLibrary.Models;

namespace TrackerUI
{
    public partial class TournamentViewer : Form
    {
        /// <summary>
        /// Global variable for access of all methods.
        /// </summary>
        private TournamentModel tournament;
        /// <summary>
        /// Contains round information <Matchup|Matchup|Matchup,Matchup|Matchup|Matchup>.
        /// </summary>
        BindingList<int> rounds = new BindingList<int>();
        /// <summary>
        /// Stores the list of Matchups related to the selected Round in roundDropDown.
        /// </summary>
        BindingList<MatchupModel> selectedMatchups = new BindingList<MatchupModel>();

        public TournamentViewer(TournamentModel tournamentModel)
        {
            InitializeComponent();

            tournament = tournamentModel;

            tournament.OnTournamentComplete += Tournament_OnTournamentComplete;

            LoadFormData();

            WireUpLists();

            LoadRounds();
        }

        private void Tournament_OnTournamentComplete(object sender, DateTime e)
        {
            this.Close();
        }

        /// <summary>
        /// Loads tournament data onto form.
        /// </summary>
        private void LoadFormData()
        {
            TournamentName.Text = tournament.TournamentName;
        }

        /// <summary>
        /// Wires up Rounds, Matchups and Teams information.
        /// </summary>
        private void WireUpLists()
        {
            // Load Rounds.
            //roundDropDown.DataSource = null;
            roundDropDown.DataSource = rounds;
            matchupListBox.DataSource = selectedMatchups;
            matchupListBox.DisplayMember = "DisplayName";
        }
        // temporary



        /// <summary>
        /// Loads Rounds information to roundDropDown.
        /// </summary>
        private void LoadRounds()
        {
            //rounds = new BindingList<int>();
            rounds.Clear();

            // Always will be 1 round in tournament.
            rounds.Add(1);
            // What round we are on right now.
            int currRound = 1;

            //Goes through list of list of Matchups in Tournament Rounds.
            foreach (List<MatchupModel> matchups in tournament.Rounds)
            {
                //Is first matchup in list round higher than current round we are on?
                if (matchups.First().MatchupRound > currRound)
                {
                    
                    currRound = matchups.First().MatchupRound;
                    // Adds int round from matchup to rounds. 1,<2>,<3>...
                    rounds.Add(currRound);
                    
                }
            }

            //WireUpLists();
            LoadMatchups(1);
        }

        /// <summary>
        /// Updates Matchups ListBox every time roundDropDown index changes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void roundDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadMatchups((int)roundDropDown.SelectedItem);
        }

        /// <summary>
        /// Method for updating Matchups ListBox.
        /// </summary>
        private void LoadMatchups(int round)
        {
            // Gets the current Round integer selected from roundDropDown.
            //int round = (int)roundDropDown.SelectedItem;

            // Loops through Lists of Lists of matchups
            foreach (List<MatchupModel> matchups in tournament.Rounds)
            {
                // Is the first Matchup in list equal to the selected round of roundDropDown?
                if (matchups.First().MatchupRound == round)
                {
                    selectedMatchups.Clear();
                    foreach (MatchupModel m in matchups)
                    {
                        if (m.Winner == null || !unplayedOnlyCheckbox.Checked)
                        {
                            selectedMatchups.Add(m); 
                        } 
                    }
                    // Put that list of Matchups in selectedMatchups for populating matchupsListBox.
                }
            }


            if (selectedMatchups.Count != 0)
            {
                loadMatchup(selectedMatchups.First()); 
            }


            DisplayMatchupInfo();
            //WireUpLists2();
        }

        private void DisplayMatchupInfo()
        {
            bool isVisible = (selectedMatchups.Count > 0);

            teamOneName.Visible = isVisible;
            teamOneScoreLabel.Visible = isVisible;
            teamOneScoreValue.Visible = isVisible;

            teamTwoName.Visible = isVisible;
            teamTwoScoreLabel.Visible = isVisible;
            teamTwoScoreValue.Visible = isVisible;

            versusLabel.Visible = isVisible;
            scoreButtom.Visible = isVisible;
        }

        /// <summary>
        /// Updates Team values every time matchupListBox index changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void matchupListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadMatchup((MatchupModel)matchupListBox.SelectedItem);
        }

        /// <summary>
        /// Method for updating Team values.
        /// </summary>
        private void loadMatchup(MatchupModel m)
        {
            //MatchupModel m = (MatchupModel)matchupListBox.SelectedItem;
            

            //Temporary fix, prevents null caused when clearing listbox to break program.
            if (m == null)
            {
                return; 
            }

            for (int i = 0; i < m.Entries.Count; i++)
            {
                if (i == 0)
                {
                    if (m.Entries[0].TeamCompeting != null)
                    {
                        teamOneName.Text = m.Entries[0].TeamCompeting.TeamName;
                        teamOneScoreValue.Text = m.Entries[0].Score.ToString();

                        teamTwoName.Text = "<bye>";
                        teamTwoScoreValue.Text = "";
                    }
                    else
                    {
                        teamOneName.Text = "Not Yet Set";
                        teamOneScoreValue.Text = "";
                    }


                }

                if (i == 1)
                {
                    if (m.Entries[1].TeamCompeting != null)
                    {
                        teamTwoName.Text = m.Entries[1].TeamCompeting.TeamName;
                        teamTwoScoreValue.Text = m.Entries[1].Score.ToString();
                    }
                    else
                    {
                        teamTwoName.Text = "Not Yet Set";
                        teamTwoScoreValue.Text = "";
                    }
                }

            }
        }

        private void unplayedOnlyCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            LoadMatchups((int)roundDropDown.SelectedItem);
        }

        private string ValidateData()
        {
            string output = "";

            double teamOneScore = 0;
            double teamTwoScore = 0;

            bool scoreOneValid = double.TryParse(teamOneScoreValue.Text, out teamOneScore);
            bool scoreTwoValid = double.TryParse(teamTwoScoreValue.Text, out teamTwoScore);

            if (!scoreOneValid || !scoreTwoValid)
            {
                output = "Enter a valid number into the matchup fields.";
            }
            else if (teamOneScore == 0 && teamTwoScore == 0)
            {
                output = "You did not enter a score in either team.";
            }
            else if (teamOneScore == teamTwoScore)
            {
                output = "Ties are not allowed in this application.";
            }
            return output;
        }

        private void scoreButtom_Click(object sender, EventArgs e)
        {
            

            string errorMessage = ValidateData();
            if (errorMessage.Length > 0)
            {
                MessageBox.Show(errorMessage);
                return;
            }

            MatchupModel m = (MatchupModel)matchupListBox.SelectedItem;
            double teamOneScore = 0;
            double teamTwoScore = 0;


            for (int i = 0; i < m.Entries.Count; i++)
            {
                if (i == 0)
                {
                    if (m.Entries[0].TeamCompeting != null)
                    {

                        bool ScoreValid = double.TryParse(teamOneScoreValue.Text, out teamOneScore);
                        if (ScoreValid)
                        {
                            m.Entries[0].Score = teamOneScore;
                        }
                        else
                        {
                            MessageBox.Show("Please enter a valid score.");
                            return;
                        }
                    }
                }

                if (i == 1)
                {
                    if (m.Entries[1].TeamCompeting != null)
                    {

                        bool ScoreValid = double.TryParse(teamTwoScoreValue.Text, out teamTwoScore);
                        if (ScoreValid)
                        {
                            m.Entries[1].Score = teamTwoScore;
                        }
                        else
                        {
                            MessageBox.Show("Please enter a valid score.");
                            return;
                        }
                    }
                }

            }

            try
            {
                TournamentLogic.UpdateTournamentResults(tournament);

            }
            catch (Exception ex)
            {
                MessageBox.Show($"The application had an error: {ex.Message}");

                throw;
            }
            LoadMatchups((int)roundDropDown.SelectedItem);

            

        }

    }
}

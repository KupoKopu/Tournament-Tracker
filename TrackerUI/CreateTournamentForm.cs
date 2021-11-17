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
    public partial class CreateTournamentForm : Form, IPrizeRequester, ITeamRequester
    {
        List<TeamModel> availableTeams = GlobalConfig.Connections.GetTeam_All();
        List<TeamModel> selectedTeams = new List<TeamModel>();
        List<PrizeModel> selectedPrizes = new List<PrizeModel>();
        public CreateTournamentForm()
        {
            InitializeComponent();

            InitializeLists();
        }
        private void CreateTournamentForm_Load(object sender, EventArgs e)
        {

        }

        private void WireUpLists()
        {
            // TODO - Crap solution, find better since pressing add again removes the "selected" person
            selectTeamDropBox.DataSource = null;
            selectTeamDropBox.DataSource = availableTeams;
            selectTeamDropBox.DisplayMember = "TeamName";

            tournamentTeamsListBox.DataSource = null;
            tournamentTeamsListBox.DataSource = selectedTeams;
            tournamentTeamsListBox.DisplayMember = "TeamName";

            prizesListBox.DataSource = null;
            prizesListBox.DataSource = selectedPrizes;
            prizesListBox.DisplayMember = "PlaceName";
        }

        private void InitializeLists()
        {
            selectTeamDropBox.DataSource = availableTeams;
            selectTeamDropBox.DisplayMember = "TeamName";

            tournamentTeamsListBox.DataSource = selectedTeams;
            tournamentTeamsListBox.DisplayMember = "TeamName";

            prizesListBox.DataSource = selectedPrizes;
            prizesListBox.DisplayMember = "PlaceName";
        }

        private void createPrizeButton_Click(object sender, EventArgs e)
        {
            //Call the createPrize form.
            CreatePrize frm = new CreatePrize(this);
            frm.Show();
        }

        public void PrizeComplete(PrizeModel model)
        {

            //Get back from the form a PrizeModel.
            //Take the PrizeModel and put it into our list of selected prizes.
            selectedPrizes.Add(model);
            WireUpLists();
        }

        private void addTeamButton_Click(object sender, EventArgs e)
        {
            //PersonModel p = (PersonModel)selectTeamMemberDropDown.SelectedItem;


            //if (p != null)
            //{
            //    availableTeamMembers.Remove(p);
            //    selectedTeamMembers.Add(p);

            //    WireUpLists();
            //}

            TeamModel t = (TeamModel)selectTeamDropBox.SelectedItem;

            if (t != null)
            {
                availableTeams.Remove(t);
                selectedTeams.Add(t);

                WireUpLists();
            }
        }

        public void teamComplete(TeamModel model)
        {
            selectedTeams.Add(model);
            WireUpLists();
        }

        private void createNewTeamLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            CreateTeam frm = new CreateTeam(this);
            frm.Show();
        }

        private void tournamentPlayerRemoveButton_Click(object sender, EventArgs e)
        {
            //PersonModel p = (PersonModel)teamMembersListBox.SelectedItem;


            //if (p != null)
            //{
            //    selectedTeamMembers.Remove(p);
            //    availableTeamMembers.Add(p);

            //    WireUpLists();
            //}

            TeamModel t = (TeamModel)tournamentTeamsListBox.SelectedItem;

            if (t != null)
            {
                selectedTeams.Remove(t);
                availableTeams.Add(t);
                WireUpLists();
            }
        }   

        private void prizesListRemoveButton_Click(object sender, EventArgs e)
        {
            PrizeModel p = (PrizeModel)prizesListBox.SelectedItem;

            if (p != null)
            {
                selectedPrizes.Remove(p);
                WireUpLists();
            }
        }

        private void createTournamentButton_Click(object sender, EventArgs e)
        {
            // Validate data.
            decimal fee = 0;

            bool feeAcceptable = decimal.TryParse(entryFeeValue.Text, out fee);

            if (!feeAcceptable)
            {
                MessageBox.Show(
                    "Enter a valid fee.", 
                    "Invalid Fee", 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Error);
                return;
            }

            // Create Tournament model.
            TournamentModel tm = new TournamentModel();

            tm.TournamentName = tournamentNameValue.Text;
            tm.EntryFees = fee;

            tm.Prizes = selectedPrizes;
            tm.EnteredTeams = selectedTeams;

            // TODO - Create our matchups.
            //Order our list randomly.
            //Check if it is big enough - if not, add in byes.
            //Create our first round of matchups.
            // Create every round after that - 8 - 4 - 2 - 1
            TournamentLogic.CreateRounds(tm);
            


            // Create Tournament entry.
            // Create all of the prizes entries.
            // Create all of team entries.
            GlobalConfig.Connections.CreateTournament(tm);

            tm.AlertUsersToNewRound();

            TournamentViewer frm = new TournamentViewer(tm);
            frm.Show();
            this.Close();
        }
    }
}

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
    public partial class TournamentDashboardForm : Form
    {
        List<TournamentModel> tournaments = GlobalConfig.Connections.GetTournament_All();
        public TournamentDashboardForm()
        {
            InitializeComponent();

            wireUpLists();
        }

        private void wireUpLists()
        {
            loadTournamentDropDown.DataSource = tournaments;
            loadTournamentDropDown.DisplayMember = "TournamentName";
        }

        private void createTournamentButton_Click(object sender, EventArgs e)
        {
            CreateTournamentForm frm = new CreateTournamentForm();
            frm.Show();  
        }

        private void loadTournamentButton_Click(object sender, EventArgs e)
        {
            TournamentModel tm = (TournamentModel)loadTournamentDropDown.SelectedItem;
            TournamentViewer frm = new TournamentViewer(tm);
            frm.Show();
        }
    }
}

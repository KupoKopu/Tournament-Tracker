using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerLibrary.Models
{
    public class TournamentModel
    {
        public event EventHandler<DateTime> OnTournamentComplete;

        /// <summary>
        /// Id to uniquely identify in data sources
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Name of tournament.
        /// </summary>
        public string TournamentName { get; set; }
        /// <summary>
        /// Entry fee to enter tournament.
        /// </summary>
        public decimal EntryFees { get; set; }
        /// <summary>
        /// Teams participating in tournament.
        /// </summary>
        public List<TeamModel> EnteredTeams { get; set; } = new List<TeamModel>();
        /// <summary>
        /// Prizes available in tournament
        /// </summary>
        public List<PrizeModel> Prizes { get; set; } = new List<PrizeModel>();
        /// <summary>
        /// List of list containing matchups
        /// </summary>
        public List<List<MatchupModel>> Rounds { get; set; } = new List<List<MatchupModel>>();

        public void CompleteTournament()
        {
            OnTournamentComplete?.Invoke(this, DateTime.Now);
        }
    }
}

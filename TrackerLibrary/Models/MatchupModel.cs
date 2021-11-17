using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerLibrary.Models
{
    public class MatchupModel
    {
        /// <summary>
        /// Id to uniquely identify in data sources
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// List of competing teams.
        /// </summary>
        public List<MatchupEntryModel> Entries { get; set; } = new List<MatchupEntryModel>();
        /// <summary>
        /// The ID from the database that will be used to look up the winner.
        /// </summary>
        public int WinnerId { get; set; }
        /// <summary>
        /// The winner of the matchup.
        /// </summary>
        public TeamModel Winner { get; set; }
        /// <summary>
        /// The round the matchup is in.
        /// </summary>
        public int MatchupRound { get; set; }

        public string DisplayName
        {
            get
            {
                string output = "";

                foreach (MatchupEntryModel me in Entries)
                {

                    if (me.TeamCompeting != null)
                    {
                        if (output.Length == 0)
                        {
                            output = me.TeamCompeting.TeamName;
                        }
                        else
                        {
                            output += $" vs. {me.TeamCompeting.TeamName }";
                        } 
                    }
                    else
                    {
                        output = "Matchup Not Yet Determined";
                        break;
                    }
                }

                return output;
            }

        }

    }
}

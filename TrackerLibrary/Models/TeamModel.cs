using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerLibrary.Models
{
    public class TeamModel
    {
        /// <summary>
        /// Id to uniquely identify in data sources.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// List of team members stored in PersonModel.
        /// </summary>
        public List<PersonModel> TeamMembers { get; set; } = new List<PersonModel>();
        /// <summary>
        /// Model's team name.
        /// </summary>
        public string TeamName { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerLibrary.Models
{
    public class PersonModel
    {
        /// <summary>
        /// Id to uniquely identify in data sources
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// First name of person.
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// Last name of person.
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// Email address of person.
        /// </summary>
        public string EmailAddress { get; set; }
        /// <summary>
        /// Cellphone number of person.
        /// </summary>
        public string CellPhoneNumber { get; set; }

        public string FullName
        {
            get 
            { 
                return $"{FirstName} {LastName}";
            }
        }


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerLibrary.Models
{
    public class PrizeModel
    {
        /// <summary>
        /// Id to uniquely identify in data sources
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Place number required to win prize.
        /// </summary>
        public int PlaceNumber { get; set; }
        /// <summary>
        /// Name for place to be called when won.
        /// </summary>
        public string PlaceName { get; set; }
        /// <summary>
        /// Prize amount that is won.
        /// </summary>
        public decimal PrizeAmount { get; set; }
        /// <summary>
        /// Percentage money of tournament revenue
        /// put as prize money.
        /// </summary>
        public double PrizePercentage { get; set; }

        public PrizeModel()
        {

        }

        public PrizeModel(string placeName, string placeNumber, string prizeAmount, string prizePercentage)
        {
            PlaceName = placeName;

            int placeNumberValue = 0;
            int.TryParse(placeNumber, out placeNumberValue);
            PlaceNumber = placeNumberValue;

            decimal prizeAmountValue = 0;
            decimal.TryParse(prizeAmount, out prizeAmountValue);
            PrizeAmount = prizeAmountValue;

            double prizePercentageValue = 0;
            double.TryParse(prizePercentage, out prizePercentageValue);
            PrizePercentage = prizePercentageValue;
        }
    }
}

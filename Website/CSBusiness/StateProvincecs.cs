
namespace CSBusiness
{
    public class StateProvince 
    {
         
        #region Properties
        /// <summary>
        /// Gets or sets the state/province identifier
        /// </summary>
        public int StateProvinceId { get; set; }

        /// <summary>
        /// Gets or sets the country identifier
        /// </summary>
        public int CountryId { get; set; }

        /// <summary>
        /// Gets or sets the name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the abbreviation
        /// </summary>
        public string Abbreviation { get; set; }

        /// <summary>
        /// Gets or sets the display order
        /// </summary>
        public int DisplayOrder { get; set; }

        public bool Visible { get; set; }

        #endregion

    }
}

using System;

namespace CSBusiness
{
    [Serializable]
    public class Country
    {
        public Country() {}
        public Country(int countryId, string countryName)
        {
            this.CountryId = countryId;
            this.Name = countryName;
        }
        public int CountryId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int OrderNo { get; set; }
        public bool Visible { get; set; }
    }
}

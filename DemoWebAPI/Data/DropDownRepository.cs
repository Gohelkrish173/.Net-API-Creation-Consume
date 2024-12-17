using DemoWebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace DemoWebAPI.Data
{
    public class DropDownRepository
    {
        private readonly IConfiguration _configuration;

        public DropDownRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        #region CountryDropDown
        public IEnumerable<CountryDropDownModel> GetCountryDropDown()
        {
            List<CountryDropDownModel> CDDM = new List<CountryDropDownModel>();
            using (SqlConnection conn = new SqlConnection(this._configuration.GetConnectionString("myConnection")))
            {
                SqlCommand cmd = new SqlCommand("PR_LOC_Country_SelectComboBox", conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read()) 
                {
                    CDDM.Add(new CountryDropDownModel
                    {
                        CountryID = Convert.ToInt32(reader["CountryID"]),
                        CountryName = reader["CountryName"].ToString()
                    });
                }
            }
            return CDDM;
        }
        #endregion

        #region StateDropDownByCountry
        public IEnumerable<StateDropDownModel> GetStateDropDownByCountry(int CountryID)
        {
            List<StateDropDownModel> SDDM = new List<StateDropDownModel>();
            using (SqlConnection conn = new SqlConnection(this._configuration.GetConnectionString("myConnection")))
            {
                SqlCommand cmd = new SqlCommand("PR_LOC_State_SelectComboBoxByCountryID", conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                conn.Open();
                cmd.Parameters.AddWithValue("CountryID", CountryID);

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    SDDM.Add(new StateDropDownModel
                    {
                        StateID = Convert.ToInt32(reader["StateID"]),
                        StateName = reader["StateName"].ToString()
                    });
                }
            }
            return SDDM;
        }
        #endregion
    }
}

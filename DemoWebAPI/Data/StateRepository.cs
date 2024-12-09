﻿using DemoWebAPI.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DemoWebAPI.Data
{
    public class StateRepository
    {
        private readonly IConfiguration _configuration;

        public StateRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        #region GetAllStates
        public IEnumerable<StateModel> GetAllStates()
        {
            List<StateModel> state = new List<StateModel> ();
            using (SqlConnection conn = new SqlConnection(this._configuration.GetConnectionString("myConnection")))
            {
                SqlCommand cmd = new SqlCommand("PR_LOC_State_SelectAll", conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read()) 
                {
                    state.Add(new StateModel
                    {
                        StateID = Convert.ToInt32(reader["StateID"]),
                        CountryID = Convert.ToInt32(reader["CountryID"]),
                        StateName = reader["StateName"].ToString(),
                        StateCode = reader["StateCode"].ToString(),
                    });
                }
                return state;
            }
        }
        #endregion

        #region InsertState
        public bool InsertState(StateModel stateModel)
        {
            using (SqlConnection conn = new SqlConnection(this._configuration.GetConnectionString("myConnection")))
            {
                SqlCommand cmd = new SqlCommand("PR_LOC_State_Insert", conn)
                {
                    CommandType = CommandType.StoredProcedure,
                };
                conn.Open();

                cmd.Parameters.AddWithValue("StateName", stateModel.StateName);
                cmd.Parameters.AddWithValue("StateCode", stateModel.StateCode);
                cmd.Parameters.AddWithValue("CountryID",Convert.ToInt32(stateModel.CountryID));

                int ReflectedMessage = cmd.ExecuteNonQuery();

                return ReflectedMessage > 0;

            }
        }
        #endregion

        #region UpdateState
        public bool UpdateState(StateModel stateModel)
        {
            using (SqlConnection conn = new SqlConnection(this._configuration.GetConnectionString("myConnection")))
            {
                SqlCommand cmd = new SqlCommand("PR_LOC_State_Update", conn)
                {
                    CommandType = CommandType.StoredProcedure,
                };
                conn.Open();

                cmd.Parameters.AddWithValue("StateID", Convert.ToInt32(stateModel.StateID));
                cmd.Parameters.AddWithValue("StateName", stateModel.StateName);
                cmd.Parameters.AddWithValue("StateCode", stateModel.StateCode);
                cmd.Parameters.AddWithValue("CountryID", Convert.ToInt32(stateModel.CountryID));

                int ReflectedMessage = cmd.ExecuteNonQuery();

                return ReflectedMessage > 0;

            }
        }
        #endregion

        #region DeleteCountry
        public bool DeleteState(int StateID)
        {
            using (SqlConnection conn = new SqlConnection(this._configuration.GetConnectionString("myConnection")))
            {
                SqlCommand cmd = new SqlCommand("PR_LOC_State_Delete", conn)
                {
                    CommandType = CommandType.StoredProcedure,
                };
                conn.Open();

                cmd.Parameters.AddWithValue("StateID", Convert.ToInt32(StateID));

                int ReflectedMessage = cmd.ExecuteNonQuery();

                return ReflectedMessage > 0;

            }
        }
        #endregion
    }
}

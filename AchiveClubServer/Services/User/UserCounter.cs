﻿using System.Collections.Generic;
using System.Linq;

using System.Data;
using System.Data.SqlClient;
using Dapper;

using AchiveClubServer.Data.DTO;

namespace AchiveClubServer.Services
{
    public class UserCounter
    {
        private string connectionString = null;
        public UserCounter(string connection)
        {
            connectionString = connection;
        }

        public int GetValue()
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return db.Query<int>("select Count(*) from Users").FirstOrDefault();
            }
        }
    }
}
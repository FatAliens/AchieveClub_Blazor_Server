﻿using AchieveClubServer.Data.DTO;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace AchieveClubServer.Services
{
    public class UsersMedalRepository : IUserMedalRepository
    {
        private string connectionString = null;
        public UsersMedalRepository(string connection)
        {
            connectionString = connection;
        }
        public bool Delete(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "DELETE FROM UserMedals WHERE Id = @id";
                try
                {
                    db.Execute(sqlQuery, new { id });
                }
                catch
                {
                    return false;
                }
                return true;
            }
        }

        public List<UserMedal> GetAll()
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return db.Query<UserMedal>("SELECT * FROM UserMedals").ToList();
            }
        }

        public UserMedal GetById(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return db.Query<UserMedal>("SELECT * FROM UserMedals WHERE Id = @id", new { id }).FirstOrDefault();
            }
        }

        public int Insert(UserMedal value)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery =
                    "INSERT INTO UserMedals(UserRefId, MedalRefId) " +
                    "VALUES(@UserRefId, @MedalRefId);" +
                    "SELECT CAST(SCOPE_IDENTITY() as int);";
                int? clubId = db.Query<int>(sqlQuery, value).FirstOrDefault();
                return clubId.Value;
            }
        }

        public bool Update(UserMedal value)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "UPDATE UserMedals SET UserRefId = @UserRefId, MedalRefId = @MedalRefId WHERE Id = @Id";
                try
                {
                    db.Execute(sqlQuery, value);
                }
                catch
                {
                    return false;
                }
                return true;
            }
        }
    }
}

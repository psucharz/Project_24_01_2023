﻿using Dapper;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Linq;

namespace Project_24_01_2023
{
    public class ProfilesDAO
    {
        public static List<ProfileCredentials> GetProfiles()
        {
            using (IDbConnection idbc = new SQLiteConnection(LoadConnectionString()))
            {
                var output = idbc.Query<ProfileCredentials>("SELECT * FROM Profile", new DynamicParameters());
                return output.ToList();
            }
        }

        public static void SetProfile(ProfileCredentials profile)
        {
            using (IDbConnection idbc = new SQLiteConnection(LoadConnectionString()))
            {
                idbc.Execute("INSERT INTO Profile (FirstName, LastName, ProfilePicture, Password, RecoveryQuestion, RecoveryAnswer, DateModified, LoginTimeout, FailedLoginAttempts) " +
                "VALUES (@FirstName, @LastName, @ProfilePicture, @Password, @RecoveryQuestion, @RecoveryAnswer, @DateModified, @LoginTimeout, @FailedLoginAttempts)",
                profile);
            }
        }

        public static void ModifyProfile(ProfileCredentials profile)
        {
            using (IDbConnection idbc = new SQLiteConnection(LoadConnectionString()))
            {
                idbc.Execute("UPDATE Profile SET FirstName = @FirstName, LastName = @LastName, ProfilePicture = @ProfilePicture," +
                "RecoveryQuestion = @RecoveryQuestion, RecoveryAnswer = @RecoveryAnswer, DateModified = @DateModified, LoginTimeout = @LoginTimeout, FailedLoginAttempts = @FailedLoginAttempts " +
                "WHERE Id = @Id",
                profile);
            }
        }

        public static void DeleteProfile(ProfileCredentials profile)
        {
            using (IDbConnection idbc = new SQLiteConnection(LoadConnectionString()))
            {
                idbc.Execute("DELETE FROM Profile WHERE Id = @Id", profile);
            }
        }

        private static string LoadConnectionString(string id = "PassManagerDB")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }

        public static void ModifyProfilePicture(ProfileCredentials pC, byte[] picture)
        {
            using (IDbConnection idbc = new SQLiteConnection(LoadConnectionString()))
            {
                idbc.Execute("UPDATE Profile SET ProfilePicture = @picture WHERE Id = @Id", new { picture, Id = pC.Id });
            }
        }
    }

    public class PasswordsDAO
    {

    }
}

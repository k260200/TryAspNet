using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

using Entities;
using Interfaces;

namespace DL
{
    public class DataBaseDL : IDataLayer
    {
        private readonly string _connectionString;

        public DataBaseDL(string connectionString)
        {
            _connectionString = connectionString;
        }

        public int AddReward(Reward reward)
        {
            Decimal id = 0;

            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("spUsersAndRewards_AddReward", connection);
                command.CommandType = CommandType.StoredProcedure;

                var rewardTitleParameter = command.Parameters.Add("Title", System.Data.SqlDbType.NVarChar).Value = reward.Title;
                var rewardDescriptionParameter = command.Parameters.Add("Description", System.Data.SqlDbType.NVarChar).Value = reward.Description;

                connection.Open();
                id = (decimal)command.ExecuteScalar();
            }

            return Decimal.ToInt32(id);
        }

        public int AddUser(User user)
        {
            Decimal id = 0;

            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("spUsersAndRewards_AddUser", connection);
                command.CommandType = CommandType.StoredProcedure;

                var userFirstNameParameter = command.Parameters.Add("FirstName", System.Data.SqlDbType.NVarChar).Value = user.FirstName;
                var userLastNameParameter = command.Parameters.Add("LastName", System.Data.SqlDbType.NVarChar).Value = user.LastName;
                var userBirthDateParameter = command.Parameters.Add("BirthDate", System.Data.SqlDbType.DateTime).Value = user.BirthDate;

                connection.Open();
                id = (decimal)command.ExecuteScalar();
            }

            return Decimal.ToInt32(id);
        }

        public void AddUserAndReward(int userId, int rewardId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("spUsersAndRewards_AwardUser", connection);
                command.CommandType = CommandType.StoredProcedure;

                var userIDParameter = command.Parameters.Add("UserID", System.Data.SqlDbType.Int).Value = userId;
                var rewardIDParameter = command.Parameters.Add("RewardID", System.Data.SqlDbType.Int).Value = rewardId;

                connection.Open();
                var reader = command.ExecuteNonQuery();
            }
        }

        public bool DeleteReward(int rewardID)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("spUsersAndRewards_DeleteReward", connection);
                command.CommandType = CommandType.StoredProcedure;

                var rewardIDParameter = command.Parameters.Add("RewardID", System.Data.SqlDbType.Int).Value = rewardID;

                connection.Open();
                command.ExecuteNonQuery();
            }

            return true;
        }

        public bool DeleteUser(int userID)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("spUsersAndRewards_DeleteUser", connection);
                command.CommandType = CommandType.StoredProcedure;

                var userIDParameter = command.Parameters.Add("UserID", System.Data.SqlDbType.Int).Value = userID;

                connection.Open();
                command.ExecuteNonQuery();
            }

            return true;
        }

        public bool DeleteUserAndReward(int userId, int rewardId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("spUsersAndRewards_TakeAwayRewardFromUser", connection);
                command.CommandType = CommandType.StoredProcedure;

                var userIDParameter = command.Parameters.Add("UserID", System.Data.SqlDbType.Int).Value = userId;
                var rewardIDParameter = command.Parameters.Add("RewardID", System.Data.SqlDbType.Int).Value = rewardId;

                connection.Open();
                command.ExecuteNonQuery();
            }

            return true;
        }

        public void EditReward(int rewardID, Reward newReward)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("spUsersAndRewards_EditReward", connection);
                command.CommandType = CommandType.StoredProcedure;

                var rewardIDParameter = command.Parameters.Add("RewardID", System.Data.SqlDbType.Int).Value = rewardID;
                var rewardTitleParameter = command.Parameters.Add("Title", System.Data.SqlDbType.NVarChar).Value = newReward.Title;
                var rewardDescriptionParameter = command.Parameters.Add("Description", System.Data.SqlDbType.NVarChar).Value = newReward.Description;

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void EditUser(int userID, User newUser)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("spUsersAndRewards_EditUser", connection);
                command.CommandType = CommandType.StoredProcedure;

                var userIDParameter = command.Parameters.Add("UserID", System.Data.SqlDbType.Int).Value = userID;
                var userFirstNameParameter = command.Parameters.Add("FirstName", System.Data.SqlDbType.NVarChar).Value = newUser.FirstName;
                var userLastNameParameter = command.Parameters.Add("LastName", System.Data.SqlDbType.NVarChar).Value = newUser.LastName;
                var userBirthDateParameter = command.Parameters.Add("BirthDate", System.Data.SqlDbType.DateTime).Value = newUser.BirthDate;

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public IEnumerable<User> GetAwardedUsers(int rewardID)
        {
            var awardedUsers = new List<User>();

            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("spUsersAndRewards_GetAllAwardedUsers", connection);
                command.CommandType = CommandType.StoredProcedure;

                var idParameter = command.Parameters.Add("RewardID", System.Data.SqlDbType.Int);
                idParameter.Value = rewardID;

                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var user = new User(
                        (int)reader[0],
                        (string)reader[1],
                        (string)reader[2],
                        (DateTime)reader[3]);

                    awardedUsers.Add(user);
                }
            }

            return awardedUsers;
        }

        public IEnumerable<Reward> GetRewards()
        {
            var rewards = new List<Reward>();

            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("SELECT RewardID, Title, Description FROM Rewards", connection);
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var reward = new Reward(
                        (int)reader[0],
                        (string)reader[1],
                        (string)reader[2]);

                    rewards.Add(reward);
                }
            }

            return rewards;
        }

        public IEnumerable<Reward> GetUserRewards(int userID)
        {
            var userRewards = new List<Reward>();

            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("spUsersAndRewards_GetAllUserRewards", connection);
                command.CommandType = CommandType.StoredProcedure;

                var idParameter = command.Parameters.Add("UserID", System.Data.SqlDbType.Int);
                idParameter.Value = userID;

                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var reward = new Reward(
                        (int)reader[0],
                        (string)reader[1],
                        (string)reader[2]);

                    userRewards.Add(reward);
                }
            }

            return userRewards;
        }

        public IEnumerable<User> GetUsers()
        {
            var users = new List<User>();

            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("SELECT UserID, FirstName, LastName, BirthDate FROM Users", connection);
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var user = new User(
                        (int)reader[0],
                        (string)reader[1],
                        (string)reader[2],
                        (DateTime)reader[3]);

                    users.Add(user);
                }
            }

            return users;
        }

        public IEnumerable<KeyValuePair<int, int>> GetUsersAndRewards()
        {
            var usersAndRewards = new List<KeyValuePair<int, int>>();

            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("SELECT UserID, RewardID FROM UsersAndRewards", connection);
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var pair = new KeyValuePair<int, int>(
                        (int)reader[0],
                        (int)reader[1]);

                    usersAndRewards.Add(pair);
                }
            }

            return usersAndRewards;
        }
    }
}

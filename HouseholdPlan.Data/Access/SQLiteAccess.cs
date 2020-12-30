using HouseholdPlan.Data.Constants;
using HouseholdPlan.Data.Entities.People;
using HouseholdPlan.Data.Entities.Work;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.IO;

namespace HouseholdPlan.Data.Access
{
    public class SQLiteAccess
    {
        public string DataBaseName { get; set; } = "HouseholdPlan.db";

        public SQLiteAccess()
        {
            CheckIsInit();
        }

        public void CheckIsInit()
        {
            if (!File.Exists(DataBaseName))
            {
                InitDataBase();
            }

        }

        public void InitDataBase()
        {
            CreateGlobalCounter();

            CreateUsersTable();

            CreateHistoryTasksTable();
            CreateHouseholdTasksTable();
            CreateProcessingTimesTable();
        }

        public int GetGlobalCounter()
        {
            int id = -1;

            using (var connection = GetConnection())
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = "SELECT CounterValue FROM Counters WHERE CounterName = 'GlobalCounter';";
                using var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    id = reader.GetInt32(0);
                }
            }

            IncrementCounter(CounterNames.GlobalCounter);

            return id;
        }

        public void IncrementCounter(CounterNames counterName)
        {
            RunCommand($"UPDATE Counters SET CounterValue = CounterValue + 1  WHERE  CounterName = '{counterName}';");
        }

        public int GetCounter(CounterNames counterName)
        {
            int id = -1;

            using (var connection = GetConnection())
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = $"SELECT CounterValue FROM Counters WHERE CounterName = '{counterName}';";
                using var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    id = reader.GetInt32(0);
                }
            }

            if (id == -1)
            {
                throw new Exception("Der counterName wurde nicht gefunden");
            }

            return id;
        }

        public void CreateGlobalCounter()
        {
            RunCommand($"DROP TABLE IF EXISTS {TableNames.Counters};");
            RunCommand($"CREATE TABLE {TableNames.Counters} (CounterName VARCHAR(50) NOT NULL, CounterValue INT NOT NULL,  PRIMARY KEY (CounterName));");
            RunCommand($"INSERT INTO {TableNames.Counters} (CounterName, CounterValue) VALUES ('GlobalCounter', 1);");
            RunCommand($"INSERT INTO {TableNames.Counters} (CounterName, CounterValue) VALUES ('{CounterNames.UserVersion}', 0);");
            RunCommand($"INSERT INTO {TableNames.Counters} (CounterName, CounterValue) VALUES ('{CounterNames.HistoryTasksVersion}', 0);");
            RunCommand($"INSERT INTO {TableNames.Counters} (CounterName, CounterValue) VALUES ('{CounterNames.HouseholdTasksVersion}', 0);");
            RunCommand($"INSERT INTO {TableNames.Counters} (CounterName, CounterValue) VALUES ('{CounterNames.ProcessingTimesVersion}', 0);");
        }

        public void CreateUsersTable()
        {
            if (GetCounter(CounterNames.UserVersion) == 0)
            {
                RunCommand($"DROP TABLE IF EXISTS {TableNames.Users};");
            }
            RunCommand($"CREATE TABLE {TableNames.Users} (Id INT NOT NULL, Name VARCHAR(150) NULL, PRIMARY KEY (Id));");
            IncrementCounter(CounterNames.UserVersion);
        }

        public void CreateHistoryTasksTable()
        {
            if (GetCounter(CounterNames.HistoryTasksVersion) == 0)
            {
                RunCommand($"DROP TABLE IF EXISTS {TableNames.HistoryTasks};");
            }
            RunCommand($"CREATE TABLE {TableNames.HistoryTasks} (Id INT NOT NULL, TaskId INT NOT NULL, EditorId INT NOT NULL, Date DATETIME NOT NULL, Status  INT NOT NULL, PRIMARY KEY (Id));");
            IncrementCounter(CounterNames.HistoryTasksVersion);
        }

        public void CreateHouseholdTasksTable()
        {
            if (GetCounter(CounterNames.HouseholdTasksVersion) == 0)
            {
                RunCommand($"DROP TABLE IF EXISTS {TableNames.HouseholdTasks};");
            }
            RunCommand($"CREATE TABLE {TableNames.HouseholdTasks} (Id INT NOT NULL, Title VARCHAR(350) NOT NULL, Description VARCHAR(1000), ProcessingTimeId INT NOT NULL, CreatorId  INT NOT NULL, PRIMARY KEY (Id));");
            IncrementCounter(CounterNames.HouseholdTasksVersion);
        }

        public void CreateProcessingTimesTable()
        {
            if (GetCounter(CounterNames.ProcessingTimesVersion) == 0)
            {
                RunCommand($"DROP TABLE IF EXISTS {TableNames.ProcessingTimes};");
            }

            RunCommand($"CREATE TABLE {TableNames.ProcessingTimes} (Id INT NOT NULL, Every INT NOT NULL, Replay INT NOT NULL, InitialDate DATETIME NOT NULL,  PRIMARY KEY (Id));");
            IncrementCounter(CounterNames.ProcessingTimesVersion);
        }

        #region CreateOrUpdate User

        public void CreateOrUpdate(UserEntity user)
        {
            if (user.Id == 0)
            {
                CreateUser(user);
            }
            else
            {
                UpdateUser(user);
            }
        }

        public void CreateUser(UserEntity user)
        {
            user.Id = GetGlobalCounter();
            int numberOfRow = RunCommand($"INSERT INTO {TableNames.Users} (Id, Name) VALUES ({user.Id}, '{user.Name}');");
            IsRowEffected(numberOfRow, "Beim Erstellen des Nutzers ist ein Fehler aufgetreten.");
        }

        public void UpdateUser(UserEntity user)
        {
            int numberOfRow = RunCommand($"UPDATE {TableNames.Users} SET Name = '{user.Name}' WHERE Id = {user.Id};");
            IsRowEffected(numberOfRow, "Beim Ändern des Nutzers ist ein Fehler aufgetreten.");
        }

        public List<UserEntity> GetUsers()
        {
            List<UserEntity> output = new List<UserEntity>();

            using (var connection = GetConnection())
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = $"SELECT Id, Name FROM {TableNames.Users};";
                using var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var id = reader.GetInt32(0);
                    var name = reader.GetString(1);

                    output.Add(new UserEntity { Id = id, Name = name });
                }
            }

            return output;
        }

        #endregion CreateOrUpdate User

        #region CreateOrUpdate HistoryTask

        public void CreateOrUpdate(HistoryTaskEntity task)
        {
            if (task.Id == 0)
            {
                CreateHistoryTask(task);
            }
            else
            {
                UpdateHistoryTask(task);
            }
        }

        public void CreateHistoryTask(HistoryTaskEntity task)
        {
            task.Id = GetGlobalCounter();
            int numberOfRow = RunCommand($"INSERT INTO {TableNames.HistoryTasks} (Id, TaskId, EditorId, Date, Status) VALUES ({task.Id}, {task.TaskId}, {task.EditorId}, '{task.Date}', {ProcessingStatusHelper.ProcessingStatusToId(task.Status)} );");
            IsRowEffected(numberOfRow, "Beim Erstellen des Nutzers ist ein Fehler aufgetreten.");
        }

        public void UpdateHistoryTask(HistoryTaskEntity task)
        {
            int numberOfRow = RunCommand($"UPDATE {TableNames.HistoryTasks} SET TaskId = '{task.TaskId}',EditorId = {task.EditorId}, Date ='{task.Date}', Status = {ProcessingStatusHelper.ProcessingStatusToId(task.Status)}  WHERE Id = {task.Id};");
            IsRowEffected(numberOfRow, "Beim Ändern des Nutzers ist ein Fehler aufgetreten.");
        }

        public List<HistoryTaskEntity> GetHistoryTasks()
        {
            List<HistoryTaskEntity> output = new List<HistoryTaskEntity>();

            using (var connection = GetConnection())
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = $"SELECT Id, TaskId, EditorId, Date, Status FROM {TableNames.HistoryTasks};";
                using var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var id = reader.GetInt32(0);
                    var taskId = reader.GetInt32(1);
                    var editorId = reader.GetInt32(2);
                    var date = reader.GetDateTime(3);
                    var status = reader.GetInt32(4);

                    output.Add(new HistoryTaskEntity
                    {
                        Id = id,
                        TaskId = taskId,
                        EditorId = editorId,
                        Date = date,
                        Status = ProcessingStatusHelper.IdToProcessingStatus(status)
                    });
                }
            }

            return output;
        }

        #endregion CreateOrUpdate HistoryTask

        #region CreateOrUpdate HouseholdTask

        public void CreateOrUpdate(HouseholdTaskEntity task)
        {
            if (task.Id == 0)
            {
                CreateHouseholdTask(task);
            }
            else
            {
                UpdateHouseholdTask(task);
            }
        }

        public void CreateHouseholdTask(HouseholdTaskEntity task)
        {
            task.Id = GetGlobalCounter();
            int numberOfRow = RunCommand($"INSERT INTO {TableNames.HouseholdTasks} (Id, Title, Description, ProcessingTimeId, CreatorId) VALUES ({task.Id}, '{task.Title}', '{task.Description}', {task.ProcessingTimeId}, {task.CreatorId} );");
            IsRowEffected(numberOfRow, "Beim Erstellen des Nutzers ist ein Fehler aufgetreten.");
        }

        public void UpdateHouseholdTask(HouseholdTaskEntity task)
        {
            int numberOfRow = RunCommand($"UPDATE {TableNames.HouseholdTasks} SET Title='{task.Title}', Description='{task.Description}', ProcessingTimeId={task.ProcessingTimeId}, CreatorId={task.CreatorId}  WHERE Id = {task.Id};");
            IsRowEffected(numberOfRow, "Beim Ändern des Nutzers ist ein Fehler aufgetreten.");
        }

        public List<HouseholdTaskEntity> GetHouseholdTasks()
        {
            List<HouseholdTaskEntity> output = new List<HouseholdTaskEntity>();

            using (var connection = GetConnection())
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = $"SELECT Id, Title, Description, ProcessingTimeId, CreatorId FROM {TableNames.HouseholdTasks};";
                using var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var id = reader.GetInt32(0);
                    var title = reader.GetString(1);
                    var description = reader.GetString(2);
                    var processingTimeId = reader.GetInt32(3);
                    var creatorId = reader.GetInt32(4);

                    output.Add(new HouseholdTaskEntity
                    {
                        Id = id,
                        Title = title,
                        Description = description,
                        ProcessingTimeId = processingTimeId,
                        CreatorId = creatorId
                    });
                }
            }

            return output;
        }

        #endregion CreateOrUpdate HouseholdTask

        #region CreateOrUpdate ProcessingTime

        public void CreateOrUpdate(ProcessingTimeEntity time)
        {
            if (time.Id == 0)
            {
                CreateProcessingTime(time);
            }
            else
            {
                UpdateProcessingTime(time);
            }
        }

        public void CreateProcessingTime(ProcessingTimeEntity task)
        {
            task.Id = GetGlobalCounter();
            int numberOfRow = RunCommand($"INSERT INTO {TableNames.ProcessingTimes} (Id, Every, Replay, InitialDate) VALUES ({task.Id}, {task.Every}, {ProcessingDateReplayHelper.ProcessingDateReplayToId(task.Replay)}, '{task.InitialDate}');");
            IsRowEffected(numberOfRow, "Beim Erstellen des Nutzers ist ein Fehler aufgetreten.");
        }

        public void UpdateProcessingTime(ProcessingTimeEntity task)
        {
            int numberOfRow = RunCommand($"UPDATE {TableNames.ProcessingTimes} SET Every = {task.Every}, Replay={ProcessingDateReplayHelper.ProcessingDateReplayToId(task.Replay)}, InitialDate='{task.InitialDate}' WHERE Id = {task.Id};");
            IsRowEffected(numberOfRow, "Beim Ändern des Nutzers ist ein Fehler aufgetreten.");
        }

        public List<ProcessingTimeEntity> GetProcessingTimes()
        {
            List<ProcessingTimeEntity> output = new List<ProcessingTimeEntity>();

            using (var connection = GetConnection())
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = $"SELECT Id, Every, Replay, InitialDate FROM {TableNames.ProcessingTimes};";
                using var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var id = reader.GetInt32(0);
                    var every = reader.GetInt32(1);
                    var replay = ProcessingDateReplayHelper.IdToProcessingDateReplay(reader.GetInt32(2));
                    var initialDate = DateTime.Parse(reader.GetString(3));

                    output.Add(new ProcessingTimeEntity
                    {
                        Id = id,
                        Every = every,
                        Replay = replay,
                        InitialDate = initialDate
                    });
                }
            }

            return output;
        }

        #endregion CreateOrUpdate ProcessingTime

        private static bool IsRowEffected(int actualRowsCount, string message)
        {
            return IsRowEffected(1, actualRowsCount, message);
        }

        private static bool IsRowEffected(int expected, int actualRowsCount, string message)
        {
            if (expected != actualRowsCount)
            {
                throw new Exception(message);
            }

            return true;
        }

        protected int RunCommand(string commandString)
        {
            int numberOfRow = 0;
            using (var connection = GetConnection())
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = commandString;
                numberOfRow = command.ExecuteNonQuery();
            }
            return numberOfRow;
        }

        protected int CreateCommand(string commandString, List<SqliteParameter> parameters)
        {
            int id = 0;
            using (var connection = GetConnection())
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = commandString;

                if (parameters != null)
                    foreach (var parameter in parameters)
                    {
                        command.Parameters.Add(parameter);
                    }

                using var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    id = reader.GetInt32(0);
                }
            }

            return id;
        }

        protected SqliteConnection GetConnection()
        {
            return new SqliteConnection($"Data Source={DataBaseName}");
        }

        //protected SqliteParameter GetParameter(string name, object value)
        //{
        //    //List<SqliteParameter> parameters = new List<SqliteParameter>();
        //    //parameters.Add(new SqliteParameter("$counterName", "GlobalCounter"));
        //    //parameters.Add(new SqliteParameter(name, value));

        //    return new SqliteParameter(name, value);
        //}
    }
}
using System;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Data.SqlClient;
using Dapper;
using System.Collections.Generic;
using System.Text;
using System.Linq;
namespace Splashify.Controllers

{
    public class SqliteDataAccess
    {
        public static List<Models.PersonModel> LoadPeople()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<Models.PersonModel>("SELECT * FROM Person", new DynamicParameters());
                return output.ToList();
            }


        }

        public static List<Models.ScoreModel> LoadFinalScore()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<Models.ScoreModel>("SELECT * FROM Person", new DynamicParameters());
                return output.ToList();
            }


        }

        public static void SavePerson(Models.PersonModel person)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("insert into Person(fname,lname) values(@fname, @lname)", person);
            }
        }

        private static string LoadConnectionString()
        {
            //add name="Default" connectionstring="Data source=./testdb.db;Version=3;" providerName="System.data.sqlClient"   

            return ConfigurationManager.ConnectionStrings["Default"].ConnectionString;
        }

        //Event
        //string EventNameField, string EventDateField, string EventGenderField, int Judge1Field, int Judge2Field, int Judge3Field)

        public static void SaveEvent(Models.EventModel e)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("insert into Event(name,date,gender,judge1ID,judge2ID,judge3ID) values(@name, @date, @gender, @judge1ID, @judge2ID, @judge3ID)", e);
            }
        }

    }

}
using System;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Data.SqlClient;
using Dapper;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Splashify.Models;

namespace Splashify.Controllers

{
    public class SqliteDataAccess
    {


        private static string LoadConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["Default"].ConnectionString;
        }

        //Person outdated - doesnt exist in db
        public static List<UserModel> LoadPeople()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<UserModel>("SELECT * FROM user", new DynamicParameters());
                return output.ToList();
            }
        }

        //outdated - doesnt exist in db
        public static List<ScoreModel> LoadFinalScore()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<Models.ScoreModel>("SELECT * FROM user", new DynamicParameters());
                return output.ToList();
            }


        }
        //outdated - doesnt exist in db
        public static void SavePerson(UserModel user)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("insert into User(fname,lname, email, password, birthdate, gender, role) values(@fname, @lname,@email, @password, @birthdate, @gender, 'default')", user);
            }
        }

        //Event
        public static void SaveEvent(EventModel eventObj)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("insert into Event(eventID,startdate,gender) values(@name, @startdate, @gender)", eventObj);
            }
        }


        public static List<EventModel> LoadEvent()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<EventModel>("SELECT * FROM event", new DynamicParameters());
                return output.ToList();
            }
        }

        //Login
        public static UserModel AuthorizeUser(UserModel user)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
               

                UserModel output = cnn.QuerySingleOrDefault<UserModel>("SELECT * FROM user WHERE email = @email", user);

                if (output == null)
                {
                    Console.WriteLine("NULL!");
                }
                else
                {
                    Console.WriteLine(output.birthdate);

                }
                return output;
            }
        }


        //Checks if user exists already
        public static UserModel UserExist(UserModel user)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                /* does 2 queries for email and birthdate*/
                /* depending on match, returns obj*/
                /* if no match, returns null*/

                UserModel output1 = cnn.QuerySingleOrDefault<UserModel>("SELECT * FROM user WHERE email = @email", user);
                UserModel output2 = cnn.QuerySingleOrDefault<UserModel>("SELECT * FROM user WHERE birthdate = @birthdate", user);
                UserModel output3 = new UserModel();

                if (output1 == null && output2 != null)
                {
                    output2.fname = "birthdate duplicate";
                    return output2;
                }
                else if (output1 != null && output2 == null)
                {
                    output1.fname = "email duplicate";
                    return output1;

                }
                else if (output1 != null && output2 != null)
                {
                    output3.fname = "both duplicate";
                    return output3;

                }
                else
                {
                    return null;
                }

            }
        }

        public static void RoleApplication(UserModel user)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                //fetches the users data again in order to have a userID for the application query
                UserModel userRefresh = cnn.QuerySingleOrDefault<UserModel>("SELECT * FROM user WHERE email = @email", user);
                userRefresh.role = user.role;
                cnn.Execute("insert into roleapplication(userID, role) values(@userID, @role)", user);
            }
        }



        //Generic test
        public static T SingleObject<T>(T obj, string table, string column)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                
                T output = cnn.QuerySingleOrDefault<T>("SELECT * FROM " + table + " WHERE " + column + " = @" + column, obj);
       
                return output;
            }
        }

    }
}
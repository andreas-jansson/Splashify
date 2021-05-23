using Dapper;
using Splashify.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Linq;

namespace Splashify.Controllers

{
    public class SqliteDataAccess
    {


        private static string LoadConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["Default"].ConnectionString;
        }

       
        public static List<UserModel> LoadPeople()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<UserModel>("SELECT * FROM user", new DynamicParameters());
                return output.ToList();
            }
        }

       
        public static List<ScoreModel> LoadFinalScore()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<ScoreModel>("SELECT * FROM user", new DynamicParameters());
                return output.ToList();
            }


        }

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
                cnn.Execute("insert into Event(eventID,startdate,gender, eventtype) values(@eventID, @startdate, @gender, @eventtype)", eventObj);
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

        public static string GetNextEventDate()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                EventModel output = cnn.Query<EventModel>("SELECT * FROM event WHERE startdate > DATE('now') ORDER BY startdate ASC LIMIT 1", new DynamicParameters()).First();
                return output.startdate.ToString();
            }
        }

        //Search
        public static List<SearchModel> LoadSearch(SearchModel user)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<SearchModel>("", new DynamicParameters());
                if (user.Value == 1)
                {
                    Console.WriteLine("value = 1");
                    if (user.SearchField != null) 
                    {
                        output = cnn.Query<SearchModel>("select fname, lname, eventid, jumpnr, finalscore " +
                            "from jump JOIN (competitor JOIN user ON user.userID = competitor.userID) ON jump.competitorID = competitor.competitorID " +
                            "where fname = '" + user.SearchField + "'", new DynamicParameters());
                    }
                    else 
                    {
                        output = cnn.Query<SearchModel>("select fname, lname, eventid, jumpnr, finalscore " +
                            "from jump JOIN (competitor JOIN user ON user.userID = competitor.userID) " +
                            "ON jump.competitorID = competitor.competitorID ", new DynamicParameters());
                    }
                    return output.ToList();
                }
                else if (user.Value == 2)
                {
                    Console.WriteLine("value = 2");
                    if (user.SearchField != null)
                    {
                        output = cnn.Query<SearchModel>("select fname, lname, event.eventid, startdate " +
                            "from user JOIN (judge JOIN (eventjudge JOIN event ON event.eventID = eventjudge.eventID) " +
                            "ON judge.judgeID = eventjudge.judgeID) ON user.userID = judge.userID " +
                            "where fname = '" + user.SearchField + "'", new DynamicParameters());
                    }
                    else 
                    {
                        output = cnn.Query<SearchModel>("select fname, lname, event.eventid, startdate " +
                            "from user JOIN (judge JOIN (eventjudge JOIN event ON event.eventid = eventjudge.eventid) " +
                            "ON judge.judgeid = eventjudge.judgeid) ON user.userid = judge.userid ", new DynamicParameters());
                    }
                    return output.ToList();
                }
                else if (user.Value == 3)
                {
                    Console.WriteLine("value = 3");
                    if (user.SearchField != null) 
                    {
                        output = cnn.Query<SearchModel>("SELECT event.eventid, startdate, competitorid, jumpnr, finalscore " +
                            "FROM Jump JOIN event ON jump.eventid = event.eventid " +
                            "WHERE event.eventid ='" + user.SearchField + "'", new DynamicParameters());
                    }
                    else 
                    {
                        output = cnn.Query<SearchModel>("SELECT event.eventid, startdate, competitorid, jumpnr, finalscore " +
                        "FROM Jump JOIN event ON jump.eventid = event.eventid ", new DynamicParameters());
                    }
                    return output.ToList();
                }
                else
                {
                    Console.WriteLine("Error");
                    return null;
                }
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


        //Checks if user exists already - registration
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
                cnn.Execute("insert into roleapplication(userID, role) values(@userID, @role)", userRefresh);
            }
        }




        //Generic returns 1 object
        public static T SingleObject<T>(T obj, string query)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                
                T output = cnn.QuerySingleOrDefault<T>(query, obj);
                
                return output;
            }
        }

        //Generic returns 1 string
        public static string SingleObjectString<T>(T obj, string table, string column, string returncolumn)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {

                var output = cnn.QueryFirstOrDefault<string>("SELECT " + returncolumn + " FROM " + table + " WHERE " + column + " = @" + column, obj);
                return output;
            }
        }
        
        //Generic returns list - otestad
        public static List <T>LoadManyObjects<T>(T obj, string query)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {


                var output = cnn.Query<T>(query, obj);
                return output.ToList();

            }
        }


        // Complex Jumps for judge overview
        public static List<EventJumpModel>LoadEventJumps(EventJumpModel eventjump, string query)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {

                var output = cnn.Query<EventJumpModel>(query, eventjump);
                return output.ToList();

            }
        }

        //Role Applications - info under managment
        public static List<RoleApplicationModel> LoadRoleApplication()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<RoleApplicationModel>("select u.userID, u.fname, u.lname, ra.role from user as u join roleapplication as ra on u.userID = ra.userID; ", new DynamicParameters());
                return output.ToList();
            }
        }


        //Event Applications - info under managment
        public static List<EventApplicationModel> LoadEventApplication()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<EventApplicationModel>("select ea.ID, " +
                    "ea.eventID, ea.clubID, c.clubname, e.gender, e.startdate " +
                    "from club as c inner join eventapplication as ea on " +
                    "c.clubID = ea.clubID inner join event as e on " +
                    "ea.eventID = e.eventID group by ea.eventID, " +
                    "ea.clubID;", new DynamicParameters());

                return output.ToList();
            }
        }


        //Role Application - Denyed
        public static void DenyRole(RoleApplicationModel applicant)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("delete from roleapplication where userID = @userID", applicant);
            }
        }
        //Role Application - Approved
        public static void ApproveRole(RoleApplicationModel applicant)
        {

           

            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("update user set role = @role where userID = @userID", applicant);
            }
            DenyRole(applicant);

        }

        //Event Application - Approved
        public static void ApproveEvent(EventApplicationModel applicant)
        {
            Console.WriteLine("event id:: " + applicant.eventID);
            Console.WriteLine("club id:: " + applicant.clubID);

            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("insert into eventclub(eventID, clubID) values(@eventID, @clubId)", applicant);
            }
            DenyEvent(applicant);

        }


        //Event Application - Denyed
        public static void DenyEvent(EventApplicationModel applicant)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("delete from eventapplication where ID = @ID", applicant);
            }
        }


        //Generic saves list
        public static void SaveManyObjects<T>(List<T> obj, string query)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                foreach(var item in obj)
                {

                    cnn.Execute(query, item);

                }

            }
        }
        

        //Generic save object
        public static void SaveSingleObject<T>(T obj, string query)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute( query, obj);
            }
        }

        //Generic Application - Denied
        public static void DenyApplication<T>(T obj, string query)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute(query, obj);
            }
        }

   
    }
}
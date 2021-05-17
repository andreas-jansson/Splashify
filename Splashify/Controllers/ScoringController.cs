using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Splashify.Models;


namespace Splashify.Controllers
{
    public class ScoringController : Controller
    {
        List<EventJumpModel> scoreList = new List<EventJumpModel>();


        public ScoringController()
        {

        }

        public ActionResult SetScore(int ContestantIDField, string JumpTypeField, float ScoreField)
        {

            //get upcoming event info based on judgeID/userID

            //get the first jumpnumber that hasnt been scored by said judge for CompetitorID

            //Assign score to that competitor

            //Trigger check for finalscore

       


            string query_next_event = "select e.eventID, j.judgeID, e.startdate " +
            "from event as e " +
            "inner join eventjudge as ej on e.eventID = ej.eventID " +
            "inner join judge as j on j.judgeID = ej.judgeID " +
            "inner join user as u on u.userID = j.userID " +
            "where u.userID=@userID and startdate >= date('now') " +
            "order by startdate LIMIT 1";




            ScoreModel eventinfo = new ScoreModel();
            eventinfo.userID = (int)HttpContext.Session.GetInt32("UserID");
            Console.WriteLine("userID: " + eventinfo.userID);
            eventinfo =SqliteDataAccess.SingleObject(eventinfo, query_next_event);
            Console.WriteLine("eventinfo: " + eventinfo.eventID + " " + eventinfo.judgeID);
            var eventID = eventinfo.eventID;
            var judgeID = eventinfo.judgeID;
            float height = eventinfo.height;
            int jumpID=0;



            //Only allows scoring for an event if the eventdate is today
            DateTime thedate = eventinfo.startdate;
            DateTime today = DateTime.Today;
            //if (today == thedate)
            if (true)
            {
                Console.WriteLine("IF! today: " + today + " startdate: " + thedate);

            }
            else
            {
                Console.WriteLine("Not Today! today: " + today + " startdate: " + eventinfo.startdate);
                return RedirectToAction("Scoring", "Home");

            }




            //Fetches latest jump nr if a score exists
            //if null assumse current jumpnr is 1
            //else take the number and add 1 to it if it isnt already max number
            string query_next_jump ="select j.jumpnr, j.jumpID from score as s inner join jump as j on j.jumpID = s.jumpID where eventID = @eventID and judgeID = @judgeID order by jumpnr desc LIMIT 1";
            eventinfo = SqliteDataAccess.SingleObject(eventinfo, query_next_jump);
            //Console.WriteLine("current jump: " + eventinfo.jumpnr);

            string query_score = "";
            string query_jump_type = "update jump set jumptype = @jumptype where jumpID=@jumpID";




            //Seperates groupnr from style in jumptype. eg 101A -> groupnr: 101 style: A
            int size = JumpTypeField.Length - 1;
            string groupstr = "";
            int i = 0;
            int groupnr;
            char style = new char();
            foreach (char x in JumpTypeField)
            {
                if (x == 48 || x == 49 || x == 50 || x == 51 || x == 52 || x == 53 || x == 54 || x == 55 || x == 56 || x == 57)
                {
                    groupstr += x; 
                }
                else
                {
                    style = x;
                }
                i++;
            }

            Console.Write(groupstr);
            Console.Write(" - " + style);
            Console.WriteLine("");
            Console.WriteLine("grpstr: " + groupstr);

            groupnr = Int32.Parse(groupstr);

            Console.WriteLine("groupnr: " + groupnr);

            //Get jumptypeID
            
            JumpTypeModel jump = new JumpTypeModel();
            jump.groupnr = groupnr;
            jump.style = style;
            jump.height = height;
            string query_dd = "select * from jumptype where groupnr = 101 and height = 3 and style = 'A'";
            jump=SqliteDataAccess.SingleObject(jump, query_dd);
            int ID = jump.ID;
            var dd = jump.value;


            if (eventinfo == null)
                {
                    ScoreModel score_edge = new ScoreModel();
                    //insert score for jumpnr 1
                    Console.WriteLine("jump is null, assuming score is for jumpnr1 ");
                    //need to find out jumpid
                    score_edge.competitorID = ContestantIDField;
                    score_edge.eventID = eventID;
                    Console.WriteLine("eventID: " + score_edge.eventID + " judgeID: " + score_edge.judgeID + "competitorID " + score_edge.competitorID);
                    string query_get_jumpid = " select jumpID, eventID from jump where eventID = @eventID and competitorID = @competitorID order by jumpnr asc limit 1";
                    score_edge = SqliteDataAccess.SingleObject(score_edge, query_get_jumpid);
                    score_edge.competitorID = ContestantIDField;
                    score_edge.eventID = eventID;
                    score_edge.jumpnr = 1;
                    score_edge.judgeID = judgeID;
                    score_edge.score = ScoreField;
                    score_edge.jumptype = ID;
                    jumpID = score_edge.jumpID;
                    query_score = "insert into score(jumpID, judgeID, score) values(@jumpID, @judgeID, @score)";
                    SqliteDataAccess.SaveSingleObject(score_edge, query_score);
                    Console.WriteLine("jmptype: " + score_edge.jumptype + " competitorID: " + score_edge.competitorID + " eventID: " + score_edge.eventID);
                    SqliteDataAccess.SaveSingleObject(score_edge, query_jump_type);



                }
            else if (eventinfo.jumpnr != 5)
            {
                query_score = "insert into score(jumpID, judgeID, score) values(@jumpID, @judgeID, @score)";
                Console.WriteLine("jump is: " + eventinfo.jumpnr);
                eventinfo.jumpnr++;
                eventinfo.jumpID++;
                eventinfo.score = ScoreField;
                eventinfo.eventID = eventID;
                eventinfo.judgeID = judgeID;
                eventinfo.jumptype = ID;
                eventinfo.competitorID = ContestantIDField;
                jumpID = eventinfo.jumpID;
                Console.WriteLine("Inserting to score: " + eventinfo.jumpID + " " + eventinfo.judgeID + " " + eventinfo.score);
                Console.WriteLine("next is jump: " + eventinfo.jumpnr);
                SqliteDataAccess.SaveSingleObject(eventinfo, query_score);
                Console.WriteLine("jmptype: " + eventinfo.jumptype + " competitorID: " + eventinfo.competitorID + " eventID: " + eventinfo.eventID);
                SqliteDataAccess.SaveSingleObject(eventinfo, query_jump_type);


                //insert score for jumpnr
            }
            else if (eventinfo.jumpnr == 5)
            {
                Console.WriteLine("Error you have already scored all jumps for this competitor" + eventinfo.jumpnr);


            }

            FinalScore(jumpID, groupnr, style, height, dd);

            return RedirectToAction("Scoring", "Home");

        }




        public void FinalScore(int jumpID, int groupnr, char style, float height, float dd)
        {
            Console.WriteLine("Finalscore - jump ID: " + jumpID);

            string query = "update jump set finalscore = @finalscore where jumpID=@jumpID";
            ScoreModel obj = new ScoreModel();
            obj.jumpID = jumpID;
            int count = Int32.Parse(SqliteDataAccess.SingleObjectString(obj, "score", "jumpID" ,"count(jumpID)"));
            Console.WriteLine("Score count: " + count);

            if(count == 3)
            {
                Console.WriteLine("All scores are submitted");
                //fetch all three scores:
                //discard highest and lowest
                //muliply by 3
                //multiply by (degree of difficulty)
    
                //Fetch degree of difficulty
                Console.WriteLine("degree of difficulty: " + dd);

                //Fetch all judge scores
                string query_all_scores = "select * from score where jumpID = @jumpID";
                EventJumpModel jumpscores = new EventJumpModel();
                jumpscores.jumpID=jumpID;
                scoreList = SqliteDataAccess.LoadManyObjects(jumpscores, query_all_scores);
                foreach(var c in scoreList)
                {
                    Console.WriteLine("C: " + c.score);
                }

                int i, j;
                //bubbelsort the three scores
                for(i = 0; i < 2; i++)
                {
                    for (j = 0; j < 2 - i; j++)
                    {
                        if (scoreList[j].score > scoreList[j + 1].score)
                        {
                            var temp = scoreList[j + 1];
                            scoreList[j + 1] = scoreList[i];
                            scoreList[j] = temp;
                        }
                    }
                }
                var median_score = scoreList[1].score;
                Console.WriteLine("Median score: " + median_score);

                //Calcualtes finalscore
                var final_score = (median_score * 3) * dd;
                Console.WriteLine("Final score: " + final_score);

                EventJumpModel finalScore = new EventJumpModel();
                finalScore.finalscore = final_score;
                finalScore.jumpID = jumpID; 

                var query_final = "update jump set finalscore = @finalscore where jumpID=@jumpID";

                SqliteDataAccess.SaveSingleObject(finalScore, query_final);

            }
            else
            {
                //skip
            }
        }
    }
}

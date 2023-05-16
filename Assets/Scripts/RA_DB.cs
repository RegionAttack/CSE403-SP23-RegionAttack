using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using UnityEngine.TestTools;
//using Mono.Data.Sqlite;
using MySql.Data.MySqlClient;

public class RA_DB  // : MonoBehaviour
{

    private string globalConnectionString2 = "server=ls-ce5399038c51311cbf1d9d5bfdef049493963c8d.cafcpcqvxqv2.us-west-2.rds.amazonaws.com;user=dbmaster1;database=Database-1;port=3306;password=dbmaster1;";
    private string globalConnectionString3 = "server=ls-ce5399038c51311cbf1d9d5bfdef049493963c8d.cafcpcqvxqv2.us-west-2.rds.amazonaws.com;user=dbmaster1;database=Database-1;password=dbmaster1;";

    private string globalConnectionString = "server=ls-ce5399038c51311cbf1d9d5bfdef049493963c8d.cafcpcqvxqv2.us-west-2.rds.amazonaws.com;uid=dbmaster1;pwd=dbmaster1;database=Database-1";


    public string testConnection()
    {
        string connectionString = globalConnectionString;
        MySqlConnection connection = new MySqlConnection(connectionString);

        try
        {
            connection.Open();
            Console.WriteLine("Connection opened successfully!");

        }
        catch (MySqlException ex)
        {
            Console.WriteLine("Error: " + ex.Message);
            return "connection failed";
        }
        finally
        {
            connection.Close();
            Console.WriteLine("Connection closed.");

        }
        return "connection successful"; //occurs if the connection opens, and is then closed

    }

    //rn just create the Friends Table
    public string createTables()
    {
        string ret = "";
        string connStr = globalConnectionString;
        MySqlConnection conn = new MySqlConnection(connStr);
        try
        {
            Console.WriteLine("Connecting to MySQL...");
            conn.Open();

            // possibly remove the primary key restraint
            string sql2 = "CREATE TABLE IF NOT EXISTS FRIENDS (user1 varchar(20), user2 varchar(20), PRIMARY KEY(user1, user2))";
            MySqlCommand cmd2 = new MySqlCommand(sql2, conn);
            cmd2.ExecuteNonQuery();

            string sql3 = "CREATE TABLE IF NOT EXISTS USERS (name varchar(20), pwd varchar(20), onlineStatus int, totalWins int, loggedIn int, inGame int, PRIMARY KEY(name))";
            MySqlCommand cmd3 = new MySqlCommand(sql3, conn);
            cmd3.ExecuteNonQuery();

            string sql4 = "CREATE TABLE IF NOT EXISTS GAMES (gameID int, user1 varchar(20), user2 varchar(20), moveNum int, currentPlayer varchar(20), placedNum int, locationX int, locationY int, gameEnded int, PRIMARY KEY(gameID))";
            MySqlCommand cmd4 = new MySqlCommand(sql4, conn);
            cmd4.ExecuteNonQuery();


            ret = "worked";

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            ret = "failed";
        }

        conn.Close();
        Console.WriteLine("Done.");
        Console.WriteLine(ret);
        return ret;


    }

    public int addUser(string username, string password) {  
        //make sure the username and password are not too long
        if (username.Length > 19 || password.Length > 19) {
            return -2;
        }
        
        string ret = "";
        string connStr = globalConnectionString;
        MySqlConnection conn = new MySqlConnection(connStr);
        try
        {
            Console.WriteLine("Connecting to MySQL...");
            conn.Open();

            //string sql = "SELECT Name, HeadOfState FROM Country WHERE Continent='Oceania'";
            string sql = "SELECT name FROM USERS WHERE name=" + username + "";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader read = cmd.ExecuteReader(); //for some reason the cmd.ExecuteNonQuery() is returning an int
            if (read.HasRows == true) {
                conn.Close();
                return -1; //duplicate exists
            }

            string sql2 = "INSERT INTO USERS (name, pwd, onlineStatus, totalWins, loggedIn, inGame) VALUES (" + username + ", " + password + ", 0,0,0,0) ";
            MySqlCommand cmd2 = new MySqlCommand(sql2, conn);
            cmd2.ExecuteNonQuery(); //for some reason the cmd.ExecuteNonQuery() is returning an int

            ret = "worked";

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            ret = "failed";
            return 0;
        }

        conn.Close();
        Console.WriteLine("Done.");
        Console.WriteLine(ret);
        return 1;//indicates that it worked




    }

    public int deleteUser(string username) {
        if (username.Length > 19) {
            return -2;
        }
        string ret = "";
        string connStr = globalConnectionString;
        MySqlConnection conn = new MySqlConnection(connStr);
        try
        {
            Console.WriteLine("Connecting to MySQL...");
            conn.Open();


            string sql2 = "DELETE FROM USERS where name="+username+"";
            MySqlCommand cmd2 = new MySqlCommand(sql2, conn);
            cmd2.ExecuteNonQuery(); //for some reason the cmd.ExecuteNonQuery() is returning an int

            ret = "worked";

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            ret = "failed";
        }

        conn.Close();
        Console.WriteLine("Done.");
        Console.WriteLine(ret);
        return 1;//indicates that it worked

    }

    public int logIn(string username, string password) {
        if (username.Length > 19 || password.Length > 19)
        {
            return -2;
        }

        //should check if same pwd, then update

        int ret = 0;
        string connStr = globalConnectionString;
        MySqlConnection conn = new MySqlConnection(connStr);
        try
        {
            Console.WriteLine("Connecting to MySQL...");
            conn.Open();

            /*
            string sql1 = "SELECT FROM USERS where name=" + username + " and pwd=" + password + "";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader read = cmd.ExecuteReader(); //for some reason the cmd.ExecuteNonQuery() is returning an int
            int count = 0;
            try {
                while (read.Read()) {
                    count = 1;
                }
            }
            if (count = 0) {
                conn.Close();
                return -1; //indicates the user didn't exist
            }
            */

            //if the user/pwd combo exists, log them in.
            string sql2 = "UPDATE USERS SET loggedIn=1 WHERE name=" + username + " and pwd=" + password + "";
            
            MySqlCommand cmd2 = new MySqlCommand(sql2, conn);
            int changes = cmd2.ExecuteNonQuery(); //for some reason the cmd.ExecuteNonQuery() is returning an int. 
            if (changes > 0) {
                ret = 1;
            }
            

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            ret = -1;
        }

        conn.Close();
        Console.WriteLine("Done.");
        Console.WriteLine(ret);
        return ret;//1 means loggedIn, 0 means pwd or username is wrong.

    }

    public int logOut(string username, string password) {
        if (username.Length > 19 || password.Length > 19)
        {
            return -2;
        }
        int ret = 0;
        string connStr = globalConnectionString;
        MySqlConnection conn = new MySqlConnection(connStr);
        try
        {
            Console.WriteLine("Connecting to MySQL...");
            conn.Open();

            

            //if the user/pwd combo exists, log them in.
            string sql2 = "UPDATE USERS SET loggedIn=0 WHERE name=" + username + " and pwd=" + password + "";

            MySqlCommand cmd2 = new MySqlCommand(sql2, conn);
            int changes = cmd2.ExecuteNonQuery(); //for some reason the cmd.ExecuteNonQuery() is returning an int
            if (changes > 0) {
                ret = 1;
            }
            

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            ret = -1;
        }

        conn.Close();
        Console.WriteLine("Done.");
        Console.WriteLine(ret);
        return ret;//returns 0 if not logged out, if logged out is successful return 1. -1 means error


    }
    
    public int addFriend(string thisPlayer, string p2) {
        int ret = 0;
        string connStr = globalConnectionString;
        MySqlConnection conn = new MySqlConnection(connStr);
        try
        {
            Console.WriteLine("Connecting to MySQL...");
            conn.Open();

            
            string sql1 = "SELECT FROM USERS where name=" + thisPlayer + "";
            MySqlCommand cmd = new MySqlCommand(sql1, conn);
            MySqlDataReader read = cmd.ExecuteReader(); //for some reason the cmd.ExecuteNonQuery() is returning an int
            int count = 0;
            try {
                while (read.Read()) {
                    count = 1;
                }
            }
            finally
            {
                read.Close();
            }

            if (count == 0) {
                
                conn.Close();
                return -1; //indicates the user didn't exist
            }

            
            string sql2 = "SELECT FROM USERS where name=" + p2 + "";
            MySqlCommand cmd2 = new MySqlCommand(sql2, conn);
            MySqlDataReader read2 = cmd.ExecuteReader(); //for some reason the cmd.ExecuteNonQuery() is returning an int
            int count2 = 0;
            try {
                while (read2.Read()) {
                    count2 = 1;
                }
            }
            finally {
                read2.Close();
            }
            
            if (count2 == 0) {
                conn.Close();
                return -1; //indicates the user didn't exist
            }
            



            //adding the friend relations
            string sql3 = "INSERT INTO FRIENDS (user1, user2) VALUES (" + thisPlayer + ", " + p2 + ")";
            MySqlCommand cmd3 = new MySqlCommand(sql3, conn);
            cmd3.ExecuteNonQuery(); //for some reason the cmd.ExecuteNonQuery() is returning an int
            ret = 1;

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            ret = -1;
        }

        conn.Close();
        Console.WriteLine("Done.");
        Console.WriteLine(ret);
        return ret;

    }

    public int deleteFriend(string thisPlayer, string Friendname) {
        int ret = 0;
        string connStr = globalConnectionString;
        MySqlConnection conn = new MySqlConnection(connStr);
        try
        {
            Console.WriteLine("Connecting to MySQL...");
            conn.Open();

            //string sql = "SELECT Name, HeadOfState FROM Country WHERE Continent='Oceania'";
            string sql = "DELETE FROM FRIENDS WHERE (user1=" + thisPlayer + " and user2=" + Friendname + ") or (user1=" + Friendname + " or user2=" + thisPlayer + ")";

            MySqlCommand cmd = new MySqlCommand(sql, conn);
            int changes = cmd.ExecuteNonQuery(); //for some reason the cmd.ExecuteNonQuery() is returning an int
            if (changes > 0)
            {
                ret = 1;
            }
            

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            ret = -1;
        }

        conn.Close();
        Console.WriteLine("Done.");
        Console.WriteLine(ret);
        return ret;

    }


    /*
        this is method is not used in the game functionality, but is primarily used by the test code to reset the AWS MySql database 
        so that testing will return the same result each time, rather than having duplicate values in the database after running the test once
        resulting in the database needing to get manually reset for accurate testing.
    */
    public int clearTables() 
    {

        
        int ret = 0;
        string connStr = globalConnectionString;
        MySqlConnection conn = new MySqlConnection(connStr);
        try
        {
            Console.WriteLine("Connecting to MySQL...");
            conn.Open();

            //string sql = "SELECT Name, HeadOfState FROM Country WHERE Continent='Oceania'";
            string sql = "DELETE FROM FRIENDS";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.ExecuteNonQuery(); //return type is number of rows effected by command, 

            string sql2 = "DELETE FROM USERS";
            MySqlCommand cmd2 = new MySqlCommand(sql2, conn);
            cmd2.ExecuteNonQuery();

            string sql3 = "DELETE FROM GAMES";
            MySqlCommand cmd3 = new MySqlCommand(sql3, conn);
            cmd3.ExecuteNonQuery();



            ret = 1;

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            ret = -1;
        }

        conn.Close();
        Console.WriteLine("Done.");
        Console.WriteLine(ret);
        return ret;

    }

    //win is either 0 or 1. number of games played will be incremented by 1
    public int UpdateScoreCount(string username, int win) {
        return 1;

    }


    /*
     * below is the code for online game management. The issue with the code below is that no locking is implemented because for some reason
     * locking tables is not transaction safe. apparently there doesn't exist transaction safe protocol for this kind of connection.
     * verified through MySql Debugger testing and google searching
    */


    //must call this method before CreateOnlineGame() so that player matching can be done
    public int PlayOnlineGame(string username, string password) {

        int ret = 0;
        string connStr = globalConnectionString;
        MySqlConnection conn = new MySqlConnection(connStr);
        try
        {
            Console.WriteLine("Connecting to MySQL...");
            conn.Open();

            //if the user/pwd combo exists, log them in.
            string sql2 = "UPDATE USERS SET onlineStatus=1 WHERE name=" + username + " and pwd=" + password + "";

            MySqlCommand cmd2 = new MySqlCommand(sql2, conn);
            int changes = cmd2.ExecuteNonQuery(); //for some reason the cmd.ExecuteNonQuery() is returning an int. 
            if (changes > 0)
            {
                ret = 1;
            }


        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            ret = -1;
        }

        conn.Close();
        Console.WriteLine("Done.");
        Console.WriteLine(ret);
        return ret;//1 means loggedIn, 0 means pwd or username is wrong.
    }

    //must call this method before CreateOnlineGame() so that player matching can be done
    public int NotPlayOnlineGame(string username, string password)
    {

        int ret = 0;
        string connStr = globalConnectionString;
        MySqlConnection conn = new MySqlConnection(connStr);
        try
        {
            Console.WriteLine("Connecting to MySQL...");
            conn.Open();

            //if the user/pwd combo exists, log them in.
            string sql2 = "UPDATE USERS SET onlineStatus=0 WHERE name=" + username + " and pwd=" + password + "";

            MySqlCommand cmd2 = new MySqlCommand(sql2, conn);
            int changes = cmd2.ExecuteNonQuery(); //for some reason the cmd.ExecuteNonQuery() is returning an int. 
            if (changes > 0)
            {
                ret = 1;
            }


        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            ret = -1;
        }

        conn.Close();
        Console.WriteLine("Done.");
        Console.WriteLine(ret);
        return ret;//1 means loggedIn, 0 means pwd or username is wrong.
    }


    /*
    public int CreateOnlineGame(string username) {
        //see if game already exists, if not then find another player and create a game based on a new gameID number
        //then return the gameID number.

        MySqlConnection myConnection = new MySqlConnection(globalConnectionString);
        myConnection.Open();

        MySqlCommand myCommand = myConnection.CreateCommand();
        MySqlTransaction myTrans;

        // Start a local transaction
        myTrans = myConnection.BeginTransaction();
        // Must assign both transaction object and connection
        // to Command object for a pending local transaction
        myCommand.Connection = myConnection;
        myCommand.Transaction = myTrans;

        try
        {
            myCommand.CommandText = "insert into Test (id, desc) VALUES (100, 'Description')";
            myCommand.ExecuteNonQuery();
            myCommand.CommandText = "insert into Test (id, desc) VALUES (101, 'Description')";
            myCommand.ExecuteNonQuery();
            myTrans.Commit();
            Console.WriteLine("Both records are written to database.");
        }
        catch (Exception e)
        {
            try
            {
                myTrans.Rollback();
            }
            catch (SqlException ex)
            {
                if (myTrans.Connection != null)
                {
                    Console.WriteLine("An exception of type " + ex.GetType() +
                    " was encountered while attempting to roll back the transaction.");
                }
            }

            Console.WriteLine("An exception of type " + e.GetType() +
            " was encountered while inserting the data.");
            Console.WriteLine("Neither record was written to database.");
        }
        finally
        {
            myConnection.Close();
        }



    }

    
    public int GetOnlineGameCurrentMove() { 
    }

    public int GiveOnlineGameCurrentMove() { 
    }

    public int EndOnlineGame(int game_id) { 
    }
    */

}

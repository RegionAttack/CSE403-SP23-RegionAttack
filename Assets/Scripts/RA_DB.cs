using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using UnityEngine.TestTools;
//using Mono.Data.Sqlite;
using MySql.Data.MySqlClient;

public class RA_DB  // : MonoBehaviour
{

    private string connectionString = "server=ls-ce5399038c51311cbf1d9d5bfdef049493963c8d.cafcpcqvxqv2.us-west-2.rds.amazonaws.com;user=dbmaster1;database=Database-1;port=3306;password=dbmaster1;";
    
    
    public string testConnection()
    {
        string connectionString = connectionString;
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
        string connStr = connectionString;
        MySqlConnection conn = new MySqlConnection(connStr);
        try
        {
            Console.WriteLine("Connecting to MySQL...");
            conn.Open();

            //string sql = "SELECT Name, HeadOfState FROM Country WHERE Continent='Oceania'";
            string sql2 = "CREATE TABLE FRIENDS (user1 varchar(20), user2 varchar(20))";
            MySqlCommand cmd = new MySqlCommand(sql2, conn);
            MySqlDataReader rdr = cmd.ExecuteNonQuery();
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

    public string addFriend(string p1, string p2) {
        string ret = "";
        string connStr = connectionString;
        MySqlConnection conn = new MySqlConnection(connStr);
        try
        {
            Console.WriteLine("Connecting to MySQL...");
            conn.Open();

            //string sql = "SELECT Name, HeadOfState FROM Country WHERE Continent='Oceania'";
            string sql = "INSERT INTO FRIENDS (user1, user2) VALUES (bob, joe)";
            
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader rdr = cmd.ExecuteNonQuery();
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

    /*
        this is method is not used in the game functionality, but is primarily used by the test code to reset the AWS MySql database 
        so that testing will return the same result each time, rather than having duplicate values in the database after running the test once
        resulting in the database needing to get manually reset for accurate testing.
    */
    public string clearTables() 
    {

        
        string ret = "";
        string connStr = connectinString;
        MySqlConnection conn = new MySqlConnection(connStr);
        try
        {
            Console.WriteLine("Connecting to MySQL...");
            conn.Open();

            //string sql = "SELECT Name, HeadOfState FROM Country WHERE Continent='Oceania'";
            string sql = "DELETE FROM FRIENDS";


            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader rdr = cmd.ExecuteNonQuery();
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
}

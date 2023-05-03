using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using NUnit.Framework;
using UnityEngine.TestTools;
using Mono.Data.Sqlite;

public class RA_DB : MonoBehaviour
{
    private string dbName = "URI=file:Inventory.db";
    // Start is called before the first frame update
    void Start() //added the public, originally just "void Start()"
    {
        //this method can have a bunch of stuff, like create tables and read/write
        //createDB();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private int createDB()
    {
        int tablesMade = 0;
        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();
            //create USERS table. holds login and basic information
            using (var command = connection.CreateCommand())
            {
                //1 table for each player info
                command.CommandText = "CREATE TABLE IF NOT EXISTS USERS (name VARCHAR(20), pwd VARCHAR(20), onlineStatus INT, totalWins INT, totalGames INT);"; //need a semi-colon outside and inside the quotations
                command.ExecuteNonQuery();
                tablesMade += 1;

            }
            connection.Close();
        }
        //create GAME table. used to maintain games that are being played between remote players.
        using (var command2 = connection.CreateCommand())
        {
            //1 table for each player info
            command2.CommandText = "CREATE TABLE IF NOT EXISTS GAMES (gameID INT, user1 VARCHAR(20), user2 VARCHAR(20), moveNum INTEGER, currentPlayer VARCHAR(20), lastNum INTEGER, placement INTEGER);"; //need a semi-colon outside and inside the quotations
            command2.ExecuteNonQuery();
            tablesMade += 1;

        }
        connection.Close();

        //create FRIEND table. friends list
        using (var command3 = connection.CreateCommand())
        {
            //1 table for each player info
            command3.CommandText = "CREATE TABLE IF NOT EXISTS FRIEND (user1 VARCHAR(20), user2 VARCHAR(20));"; //need a semi-colon outside and inside the quotations
            command3.ExecuteNonQuery();
            tablesMade += 1;

        }
        connection.Close();

        return tablesMade;

    }

    public int CreateUser(string name, string pwd)
    {
        //add a brand new player to the database, check first if same user name is there

        bool userExists = true; //rewrite to use integers, for various failure modes. transmit data back to client
        int t = 0;

        using (var command = connection.CreateCommand())
        {
            //name VARCHAR(20), pwd VARCHAR(20), onlineStatus INT, totalWins INT, totalGames INT
            command.CommandText = "SELECT * FROM USERS WHERE name='"+name+"';"; //need a semi-colon outside and inside the quotations
            using (IDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                    if (reader["name"] == name)
                    {
                        userExists = false;
                        t = 1;
                    }
                reader.Close();
            }

        }
        connection.Close();

        //1 indicates that the user already exists
        if (t == 1)
        {
            return t;
        }


        if (userExists)
        {
            using (var command = connection.CreateCommand())
            {
                //name VARCHAR(20), pwd VARCHAR(20), onlineStatus INT, totalWins INT, totalGames INT
                command.CommandText = "INSERT INTO USERS (name, pwd, onlineStatus, totalWins, totalGames) VALUES ('" + name + "','" + pwd + "', 0, 0, 0 );"; //need a semi-colon outside and inside the quotations
                command.ExecuteNonQuery();

            }
            connection.Close();
            t = 2;

        }

        //returns true if the method just added the new user. returns false if the user already existed
        //return 2 indicates that the user previous did not exist, but now exists
        return t;

    }

    public void login()
    {


    }

    public void logout() { }

    public int addFriend(string mainPlayer, string friendName)
    {
        //check if mainPlayer and friend both exist

        int status = 0;
        bool main = false;
        bool friend = false;
        using (var command = connection.CreateCommand())
        {
            command.CommandText = "SELECT name FROM USERS;";
            using (IDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                    if (reader["name"] == mainPlayer)
                    {
                        mainPlayer = true;
                        status += 1;
                    }
                if (reader["name"] == friendName)
                {
                    friend = true;
                    status += 1;
                }
                reader.Close();
            }
        }
        connection.Close();
        if (status < 2)
        {
            return status;
        }
        if (main && friend && status == 2)
        {
            using (var command3 = connection.CreateCOmmand())
            {
                command3.CommandText = "SELECT * FROM FRIEND WHERE user1 = '"+mainPlayer+"';";
                using (IDataReader reader = command3.ExecuteReader())
                {
                    while (reader.Read())
                        if (reader["user2"] == friendName)
                        {
                            status += 1;
                        }
                    reader.Close();
                }
            }
            //if status=3, then the tuple exists. if status=2, the tuple doesnt exist
            if (status == 2)
            {
                using (var command2 = connection.CreateCOmmand())
                {
                    command2.CommandText = "INSERT INTO FRIEND (user1, user2) VALUES ('" + mainPlayer + "','" + friendName + "');";
                    command2.ExecuteNonQuery();
                    status += 2;
                }
            }
        }

        //add mainPlayer<->friend connection
        return status;

    }

    public bool startOnlinePlay(string name)
    {

        bool done = false;

        using (var command = connection.CreateCommand())
        {
            //name VARCHAR(20), pwd VARCHAR(20), onlineStatus INT, totalWins INT, totalGames INT
            command.CommandText = "UPDATE USERS SET onlineStatus = 1 WHERE name='"+name+"';"; //need a semi-colon outside and inside the quotations
            command.ExecuteNonQuery();
            done = true;

        }
        connection.Close();

        return done;

    }

    public bool endOnlinePlay(string name)
    {

        bool done = false;

        using (var command = connection.CreateCommand())
        {
            //name VARCHAR(20), pwd VARCHAR(20), onlineStatus INT, totalWins INT, totalGames INT
            command.CommandText = "UPDATE USERS SET onlineStatus = 0 WHERE name='"+name+"';"; //need a semi-colon outside and inside the quotations
            command.ExecuteNonQuery();
            done = true;

        }
        connection.Close();

        return done;

    }

}

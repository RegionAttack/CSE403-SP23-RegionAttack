using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
//using MySql.Data.MySqlClient;

public class DBTests
{
    // A Test  as an ordinary method

    [Test]
    public void initializeDatabase()
    {
        RA_DB Db = new RA_DB();

        Assert.AreEqual("successful connection", Db.testConnection());
        Assert.AreEqual("successful connection", Db.testConnection());

    }
    /*
    [Test]
    public void addUsers()
    {
        RA_DB Db = new RA_DB();
        //make sure usernames/pwd that are too long are caught
        Assert.AreEqual(-2, Db.addUser("abcdabcdabcdabcdabcdabcd", "abcdabcdabcdabcdabcdabcd"));
        Assert.AreEqual(-2, Db.addUser("abcdabcdabcdabcdabcdabcd", "abcdabcd"));
        Assert.AreEqual(-2, Db.addUser("abcdabcdabcda", "abcdabcdabcdabcdabcdabcd"));
        
        //add unique users
        Assert.AreEqual(1, Db.addUser("bill1", "bill1"));
        Assert.AreEqual(1, Db.addUser("bill2", "bill2"));
        Assert.AreEqual(1, Db.addUser("bill3", "bill3"));

        //add duplicate users with same/dif passwords
        Assert.AreEqual(-1, Db.addUser("bill1", "bill1"));
        Assert.AreEqual(-1, Db.addUser("bill1", "bill2"));
        Assert.AreEqual(-1, Db.addUser("bill2", "bill1"));

        //reset database
        Assert.AreEqual(1, Db.clearTables());
    }

    [Test]
    public void LoggingInOut() {
        RA_DB Db = new RA_DB();
        
        //adding users
        Assert.AreEqual(1, Db.addUser("bill1", "bill1"));
        Assert.AreEqual(1, Db.addUser("bill2", "bill2"));
        Assert.AreEqual(1, Db.addUser("bill3", "bill3"));

        //logging in bad users
        //make sure usernames/pwd that are too long are caught
        Assert.AreEqual(-2, Db.logIn("abcdabcdabcdabcdabcdabcd", "abcdabcdabcdabcdabcdabcd"));
        Assert.AreEqual(-2, Db.logIn("abcdabcdabcdabcdabcdabcd", "abcdabcd"));
        Assert.AreEqual(-2, Db.logIn("abcdabcdabcda", "abcdabcdabcdabcdabcdabcd"));


        //logging in non-existant users
        Assert.AreEqual(0, Db.addUser("jo", "jo"));

        //logging in valid users
        Assert.AreEqual(1, Db.logIn("bill1", "bill1"));
        Assert.AreEqual(1, Db.logIn("bill2", "bill2"));

        //logging out valid users
        Assert.AreEqual(1, Db.logOut("bill1", "bill1"));
        Assert.AreEqual(1, Db.logOut("bill2", "bill2"));

        //reset database
        Assert.AreEqual(1, Db.clearTables());


    }

    [Test]
    public void AddFriendTest()
    {
        RA_DB Db = new RA_DB();

        //adding users
        Assert.AreEqual(1, Db.addUser("bill1", "bill1"));
        Assert.AreEqual(1, Db.addUser("bill2", "bill2"));
        Assert.AreEqual(1, Db.addUser("bill3", "bill3"));

        //adding friend relations
        Assert.AreEqual(1, Db.addFriend("bill1", "bill2"));
        Assert.AreEqual(1, Db.addFriend("bill2", "bill3"));

        //adding non-existant people
        Assert.AreEqual(0, Db.deleteFriend("bill1", "bill6"));
        Assert.AreEqual(0, Db.deleteFriend("bill5", "bill1"));

        //reset database
        Assert.AreEqual(1, Db.clearTables());
    }


    [Test]
    public void RemoveFriendTest()
    {
        RA_DB Db = new RA_DB();

        //adding users
        Assert.AreEqual(1, Db.addUser("bill1", "bill1"));
        Assert.AreEqual(1, Db.addUser("bill2", "bill2"));
        Assert.AreEqual(1, Db.addUser("bill3", "bill3"));

        //adding friend relations
        Assert.AreEqual(1, Db.addFriend("bill1", "bill2"));
        Assert.AreEqual(1, Db.addFriend("bill2", "bill3"));

        //removing friend relations
        Assert.AreEqual(1, Db.deleteFriend("bill1", "bill2"));
        Assert.AreEqual(1, Db.deleteFriend("bill2", "bill3"));

        //removing non-existant relations
        Assert.AreEqual(0, Db.deleteFriend("bill5", "bill6"));

        //reset database
        Assert.AreEqual(1, Db.clearTables());
    }

    [Test]
    public void onlineStatusTest() {
        RA_DB Db = new RA_DB();

        //adding users
        Assert.AreEqual(1, Db.addUser("bill1", "bill1"));
        Assert.AreEqual(1, Db.addUser("bill2", "bill2"));

        //going offline, when never online
        Assert.AreEqual(0, Db.NotPlayOnlineGame("bill1", "bill1"));

        //modifying online status
        Assert.AreEqual(1, Db.PlayOnlineGame("bill1", "bill1"));
        Assert.AreEqual(1, Db.PlayOnlineGame("bill2", "bill2"));

        //duplicate online status
        Assert.AreEqual(0, Db.PlayOnlineGame("bill1", "bill1"));

        //going offline
        Assert.AreEqual(1, Db.NotPlayOnlineGame("bill1", "bill1"));
        Assert.AreEqual(0, Db.NotPlayOnlineGame("bill1", "bill1"));
    }

    [Test]
    public void addScoreTest()
    {
        RA_DB Db = new RA_DB();

        //adding users
        Assert.AreEqual(1, Db.addUser("bill1", "bill1"));
        Assert.AreEqual(1, Db.addUser("bill2", "bill2"));

    }
    */
}

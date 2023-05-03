using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class DBTests
{


    // A Test  as an ordinary method

    [Test]
    public void initializeDatabase()
    {
        RA_DB Db = new sql_test1_sri();

        Assert.AreEqual(3, Db.CreateDB());
        Assert.AreEqual(3, Db.CreateDB());


    }

    [Test]
    public void addUsers()
    {
        RA_DB Db = new sql_test1_sri();

        //create Database
        Assert.AreEqual(3, Db.CreateDB());

        //Add Users
        Assert.AreEqual(2, Db.CreateUser("bob1", "pwd1"));
        Assert.AreEqual(2, Db.CreateUser("bob2", "pwd2"));
        Assert.AreEqual(2, Db.CreateUser("bob3", "pwd3"));

        //add duplicate users
        Assert.AreEqual(1, Db.CreateUser("bob3", "pwd3")); //dup user, same pwd
        Assert.AreEqual(1, Db.CreateUser("bob3", "pwd1")); //dup user, dif pwd
    }

    [Test]
    public void addFriends()
    {
        RA_DB Db = new sql_test1_sri();

        //create Database
        Assert.AreEqual(3, Db.CreateDB());

        //Add Users
        Assert.AreEqual(2, Db.CreateUser("bob1", "pwd1"));
        Assert.AreEqual(2, Db.CreateUser("bob2", "pwd2"));
        Assert.AreEqual(2, Db.CreateUser("bob3", "pwd3"));

        //Create Friend Relations
        Assert.AreEqual(1, Db.addFriends("bob1", "NotExisting"));
        Assert.AreEqual(1, Db.addFriends("NotExisting", "bob2"));
        Assert.AreEqual(0, Db.addFriends("NotExisting", "NotExisting"));
        Assert.AreEqual(4, Db.addFriends("bob1", "bob2"));
        Assert.AreEqual(4, Db.addFriends("bob1", "bob3"));
        Assert.AreEqual(3, Db.addFriends("bob1", "bob2"));//duplicate friend relation
        Assert.AreEqual(4, Db.addFriends("bob2", "bob1"));//adding relation in opposite direction
    }


    [Test]
    public void modifyOnlineStatus()
    {
        RA_DB Db = new sql_test1_sri();

        //create Database
        Assert.AreEqual(3, Db.CreateDB());

        //Add Users
        Assert.AreEqual(2, Db.CreateUser("bob1", "pwd1"));
        Assert.AreEqual(2, Db.CreateUser("bob2", "pwd2"));
        Assert.AreEqual(2, Db.CreateUser("bob3", "pwd3"));

        //Create Friend Relations
        Assert.AreEqual(1, Db.addFriends("bob1", "NotExisting"));
        Assert.AreEqual(1, Db.addFriends("NotExisting", "bob2"));
        Assert.AreEqual(0, Db.addFriends("NotExisting", "NotExisting"));
        Assert.AreEqual(4, Db.addFriends("bob1", "bob2"));
        Assert.AreEqual(4, Db.addFriends("bob1", "bob3"));
        Assert.AreEqual(3, Db.addFriends("bob1", "bob2"));//duplicate friend relation
        Assert.AreEqual(4, Db.addFriends("bob2", "bob1"));//adding relation in opposite direction
    }

}
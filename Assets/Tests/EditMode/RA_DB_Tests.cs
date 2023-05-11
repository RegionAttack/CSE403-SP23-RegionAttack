using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using MySql.Data.MySqlClient;

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

    [Test]
    public void creatingTables()
    {
        RA_DB Db = new RA_DB();

        Assert.AreEqual("worked", Db.createTables());
        //Assert.AreEqual("successful connection", Db.testConnection());

    }

    [Test]
    public void addFriendTest()
    {
        RA_DB Db = new RA_DB();

        Assert.AreEqual("successful connection", Db.addFriend());
        Db.clearTables();


    }

}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Library.Models;
using System.Collections.Generic;
using System;

namespace Library.Tests
{
  [TestClass]
  public class UserTest : IDisposable
  {
    public void Dispose()
    {
      User.ClearAll();
    }
    public UserTest()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889; database=library_tests;";
    }
    [TestMethod]
    public void UserConstructor_CreatesInstanceOfUser()
    {
      User newUser = new User("Sam Mark", "1 User Street", "555-555-5555", 1);
      Assert.AreEqual(typeof(User), newUser.GetType());

    }

    [TestMethod]
    public void GetUserName_GetsUserName_String()
    {
      string name = "Sam Mark";
      User newUser = new User(name, "test", "test", 1);
      string result = newUser.GetUserName();
      Assert.AreEqual(name, result);
    }

    [TestMethod]
    public void SetUserName_SetsUserName_String()
    {
      string username = "Sam Mark";
      User newUser = new User(username, "test", "test", 1);
      string updatedUserName = "Mark Sam";
      newUser.SetUserName(updatedUserName);
      string result = newUser.GetUserName();
      Assert.AreEqual(updatedUserName, result);
    }
    [TestMethod]
    public void SetUserAddress_SetsUserAddress_String()
    {
      string address = "123 street";
      User newUser = new User("test", address, "test", 1);
      string updatedAddress = "999 street";
      newUser.SetUserAddress(updatedAddress);
      string result = newUser.GetUserAddress();
      Assert.AreEqual(updatedAddress, result);
    }

    [TestMethod]
    public void GetUserAddress_GetsUsersAddress_String()
    {
      string address = "123 street";
      User newUser = new User("test", address, "test", 1);
      string result = newUser.GetUserAddress();
      Assert.AreEqual(address, result);
    }

    [TestMethod]
    public void GetUserPhone_GetsUserPhone_String()
    {
      string phone = "555-555-5555";
      User newUser = new User("test", "test", phone, 1);
      string result = newUser.GetUserPhone();
      Assert.AreEqual(phone, result);
    }

    [TestMethod]
    public void SetUserPhone_SetsUserPhone_String()
    {
      string phone = "555-555-5555";
      User newUser = new User("test", "test", phone, 1);
      string updatedPhone = "111-111-1111";
      newUser.SetUserPhone(updatedPhone);
      string result = newUser.GetUserPhone();
      Assert.AreEqual(updatedPhone, result);
    }
    [TestMethod]
    public void GetUserId_GetsUserId_Int()
    {
      int id = 1;
      User newUser = new User("test", "test", "test", id);
      int result = newUser.GetUserId();
      Assert.AreEqual(id, result);
    }
    [TestMethod]
    public void Save_SavesToDatabase_User()
    {
      User testUser = new User("test", "test", "test");
      testUser.Save();
      List<User> result = User.GetAll();
      List<User> testList = new List<User>{testUser};
      CollectionAssert.AreEqual(testList, result);
    }

    [TestMethod]
    public void GetAll_ReturnsUsers_UserList()
    {
      User newUser1 = new User("test", "test", "test");
      newUser1.Save();
      User newUser2 = new User("test", "test", "test");
      newUser2.Save();
      List<User> newList = new List<User> {newUser1, newUser2};
      List<User> result = User.GetAll();
      CollectionAssert.AreEqual(newList, result);
    }

  }
}

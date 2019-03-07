using Microsoft.VisualStudio.TestTools.UnitTesting;
using Library.Models;
using System.Collections.Generic;
using System;

namespace Library.Tests
{
  [TestClass]
  public class PatronTest : IDisposable
  {
    public void Dispose()
    {
      Patron.ClearAll();
    }
    public PatronTest()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889; database=library_tests;";
    }
    [TestMethod]
    public void PatronConstructor_CreatesInstanceOfPatron()
    {
      Patron newPatron = new Patron("Sam Mark", "1 Patron Street", "555-555-5555", 1);
      Assert.AreEqual(typeof(Patron), newPatron.GetType());

    }

    [TestMethod]
    public void GetPatronName_GetsPatronName_String()
    {
      string name = "Sam Mark";
      Patron newPatron = new Patron(name, "test", "test", 1);
      string result = newPatron.GetPatronName();
      Assert.AreEqual(name, result);
    }

    [TestMethod]
    public void SetPatronName_SetsPatronName_String()
    {
      string patronname = "Sam Mark";
      Patron newPatron = new Patron(patronname, "test", "test", 1);
      string updatedPatronName = "Mark Sam";
      newPatron.SetPatronName(updatedPatronName);
      string result = newPatron.GetPatronName();
      Assert.AreEqual(updatedPatronName, result);
    }
    [TestMethod]
    public void SetPatronAddress_SetsPatronAddress_String()
    {
      string address = "123 street";
      Patron newPatron = new Patron("test", address, "test", 1);
      string updatedAddress = "999 street";
      newPatron.SetPatronAddress(updatedAddress);
      string result = newPatron.GetPatronAddress();
      Assert.AreEqual(updatedAddress, result);
    }

    [TestMethod]
    public void GetPatronAddress_GetsPatronsAddress_String()
    {
      string address = "123 street";
      Patron newPatron = new Patron("test", address, "test", 1);
      string result = newPatron.GetPatronAddress();
      Assert.AreEqual(address, result);
    }

    [TestMethod]
    public void GetPatronPhone_GetsPatronPhone_String()
    {
      string phone = "555-555-5555";
      Patron newPatron = new Patron("test", "test", phone, 1);
      string result = newPatron.GetPatronPhone();
      Assert.AreEqual(phone, result);
    }

    [TestMethod]
    public void SetPatronPhone_SetsPatronPhone_String()
    {
      string phone = "555-555-5555";
      Patron newPatron = new Patron("test", "test", phone, 1);
      string updatedPhone = "111-111-1111";
      newPatron.SetPatronPhone(updatedPhone);
      string result = newPatron.GetPatronPhone();
      Assert.AreEqual(updatedPhone, result);
    }
    [TestMethod]
    public void GetPatronId_GetsPatronId_Int()
    {
      int id = 1;
      Patron newPatron = new Patron("test", "test", "test", id);
      int result = newPatron.GetPatronId();
      Assert.AreEqual(id, result);
    }
    [TestMethod]
    public void Save_SavesToDatabase_Patron()
    {
      Patron testPatron = new Patron("test", "test", "test");
      testPatron.Save();
      List<Patron> result = Patron.GetAll();
      List<Patron> testList = new List<Patron>{testPatron};
      CollectionAssert.AreEqual(testList, result);
    }

    [TestMethod]
    public void GetAll_ReturnsPatrons_PatronList()
    {
      Patron newPatron1 = new Patron("test", "test", "test");
      newPatron1.Save();
      Patron newPatron2 = new Patron("test", "test", "test");
      newPatron2.Save();
      List<Patron> newList = new List<Patron> {newPatron1, newPatron2};
      List<Patron> result = Patron.GetAll();
      CollectionAssert.AreEqual(newList, result);
    }

    [TestMethod]
    public void Find_ReturnsCorrectPatronFromDatabase_Patron()
    {
      Patron testPatron = new Patron("test", "test", "test");
      testPatron.Save();

      //Act
      Patron foundPatron= Patron.Find(testPatron.GetPatronId());

      //Assert
      Assert.AreEqual(testPatron, foundPatron);
    }
  }
}




    // [TestMethod]
    // public void Delete_DeletesPatronFromDatabase_PatronList()
    // {
    //   Patron testPatron = new Patron("test", "test" "test")
    //   testPatron.Save()
    // }
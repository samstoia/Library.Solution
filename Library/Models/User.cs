using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;

namespace Library.Models
{
  public class User
  {
    private string _user_name;
    private string _address;
    private string _phone;
    private int _id;

    public User(string user_name, string address, string phone, int id = 0)
    {
      _user_name = user_name;
      _address = address;
      _phone = phone;
      _id = id;
    }

    public static void ClearAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM users;";
      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public string GetUserName()
    {
      return _user_name;
    }

    public void SetUserName(string newUser_name)
    {
      _user_name = newUser_name;
    }

    public string GetUserAddress()
    {
      return _address;
    }

    public void SetUserAddress(string newAddress)
    {
      _address = newAddress;
    }

    public string GetUserPhone()
    {
      return _phone;
    }

    public void SetUserPhone(string newPhone)
    {
      _phone = newPhone;
    }
    public int GetUserId()
    {
      return _id;
    }
    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO users (user_name, address, phone) VALUES (@user_name, @address, @phone);";
      MySqlParameter user_name = new MySqlParameter();
      user_name.ParameterName = "user_name";
      user_name.Value = this._user_name;
      cmd.Parameters.Add(user_name);
      MySqlParameter address = new MySqlParameter();
      address.ParameterName = "address";
      address.Value = this._address;
      cmd.Parameters.Add(address);
      MySqlParameter phone = new MySqlParameter();
      phone.ParameterName = "phone";
      phone.Value = this._phone;
      cmd.Parameters.Add(phone);
      cmd.ExecuteNonQuery();
      _id = (int) cmd.LastInsertedId;
      if ( conn != null )
      {
        conn.Dispose();
      }
    }

    public static List<User> GetAll()
    {
      List<User> allUsers = new List<User> { };
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM users;";
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        string user_name = rdr.GetString(0);
        string address = rdr.GetString(1);
        string phone = rdr.GetString(2);
        int id = rdr.GetInt32(3);
        User newUser = new User(user_name, address, phone, id);
        allUsers.Add(newUser);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allUsers;
    }
    public override bool Equals(System.Object otherUser)
    {
      if (!(otherUser is User))
      {
        return false;
      }
      else
      {
        User newUser = (User) otherUser;
        bool userNameEquality = (this.GetUserName() == newUser.GetUserName());
        bool userAddressEquality = (this.GetUserAddress() == newUser.GetUserAddress());
        bool userPhoneEquality = (this.GetUserPhone() == newUser.GetUserPhone());
        bool UserIdEquality = (this.GetUserId() == newUser.GetUserId());
        return (userNameEquality && userAddressEquality && userPhoneEquality && UserIdEquality);
      }
    }


    public static User Find(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM users WHERE id = (@searchId);";
      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@searchId";
      searchId.Value = id;
      cmd.Parameters.Add(searchId);
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      int userId = 0;
      string user_name = "";
      string address = "";
      string phone = "";
      while(rdr.Read())
      {
        user_name = rdr.GetString(0);
        address = rdr.GetString(1);
        phone = rdr.GetString(2);
        userId = rdr.GetInt32(3);
      }
      User newUser = new User(user_name, address, phone, userId);
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return newUser;
    }
    public bool Delete()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM users WHERE id = @userId;";
      MySqlParameter userIdParameter = new MySqlParameter();
      userIdParameter.ParameterName = "@userId";
      userIdParameter.Value = this.GetUserId();
      cmd.Parameters.Add(userIdParameter);
      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return true;
    }

  }
}

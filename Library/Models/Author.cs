using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;

namespace Library.Models
{
  public class Author
  {
    private string _author_name;
    private int _authorId;

    public Author(string author_name, int authorId = 0)
    {
      _author_name = author_name;
      _authorId = authorId;
    }

    public static void ClearAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM authors;";
      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public string GetAuthorName()
    {
      return _author_name;
    }

    public int GetAuthorId()
    {
      return _authorId;
    }

    public void SetAuthorName(string newAuthor_name)
    {
      _author_name = newAuthor_name;
    }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO authors (author_name) VALUES (@author_name);";
      MySqlParameter author_name = new MySqlParameter();
      author_name.ParameterName = "@author_name";
      author_name.Value = this._author_name;
      cmd.Parameters.Add(author_name);
      cmd.ExecuteNonQuery();
      _authorId = (int) cmd.LastInsertedId;
      conn.Close();
      if ( conn != null )
      {
        conn.Dispose();
      }
    }

    public static List<Author> GetAll()
    {
      List<Author> allAuthors = new List<Author> { };
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM authors;";
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        string author_name = rdr.GetString(0);
        int author_id = rdr.GetInt32(1);
        Author newAuthor = new Author(author_name, author_id);
        allAuthors.Add(newAuthor);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allAuthors;
    }

    public override bool Equals(System.Object otherAuthor)
    {
      if (!(otherAuthor is Author))
      {
        return false;
      }
      else
      {
        Author newAuthor = (Author) otherAuthor;
        bool authorIdEquality = (this.GetAuthorId() == newAuthor.GetAuthorId());
        bool authorNameEquality = (this.GetAuthorName() == newAuthor.GetAuthorName());
        return (authorIdEquality && authorNameEquality);
      }
    }
    public static Author Find(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM authors WHERE id = (@searchId);";
      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@searchId";
      searchId.Value = id;
      cmd.Parameters.Add(searchId);
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      int authorId = 0;
      string author_name = "";
      while(rdr.Read())
      {
        author_name = rdr.GetString(0);
        authorId = rdr.GetInt32(1);
      }
      Author newAuthor = new Author(author_name, authorId);
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return newAuthor;
    }



  }
}

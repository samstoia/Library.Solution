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

    public void AddBook(Book newBook)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO authors_books (book_id, author_id) VALUES (@bookId, @authorId);";
      MySqlParameter bookIdParameter = new MySqlParameter();
      bookIdParameter.ParameterName = "@bookId";
      bookIdParameter.Value = newBook.GetBookId();
      cmd.Parameters.Add(bookIdParameter);
      MySqlParameter authorIdParameter = new MySqlParameter();
      authorIdParameter.ParameterName = "@authorId";
      authorIdParameter.Value = this._authorId;
      cmd.Parameters.Add(authorIdParameter);
      cmd.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public List<Book> GetBooks()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      // joining 3 tables authors books authors_books
      cmd.CommandText = @"SELECT books.* FROM authors
        JOIN authors_books ON (authors.id = authors_books.author_id)
        JOIN books ON (authors_books.book_id = books.id)
        WHERE authors.id = @authorId;";
      MySqlParameter authorIdParameter = new MySqlParameter();
      authorIdParameter.ParameterName = "@authorId";
      authorIdParameter.Value = _authorId;
      cmd.Parameters.Add(authorIdParameter);
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      List<Book> bookList = new List<Book>{};
      while(rdr.Read())
      {
        string bookTitle = rdr.GetString(0);
        int bookId = rdr.GetInt32(1);
        Book newBook = new Book(bookTitle, bookId);
        bookList.Add(newBook);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return bookList;
    }

    public void Edit(string newName)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"UPDATE authors SET author_name = @authorName WHERE id = @authorId;";
      MySqlParameter authorNameParameter = new MySqlParameter();
      authorNameParameter.ParameterName = "@authorName";
      authorNameParameter.Value = newName;
      cmd.Parameters.Add(authorNameParameter);
      MySqlParameter authorIdParameter = new MySqlParameter();
      authorIdParameter.ParameterName = "@authorId";
      authorIdParameter.Value = this._authorId;
      cmd.Parameters.Add(authorIdParameter);
      cmd.ExecuteNonQuery();
      _author_name = newName;
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public void Delete()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM authors_books WHERE author_id = @authorId; DELETE FROM authors WHERE id = @authorId;";
      MySqlParameter authorIdParameter = new MySqlParameter();
      authorIdParameter.ParameterName = "@authorId";
      authorIdParameter.Value = this._authorId;
      cmd.Parameters.Add(authorIdParameter);
      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
  }
}

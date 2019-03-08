using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;

namespace Library.Models
{
  public class Book
  {
    private string _title;
    private int _bookId;

    public Book(string title, int bookId = 0)
    {
      _title = title;
      _bookId = bookId;
    }

    public static void ClearAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM books;";
      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public string GetTitle()
    {
      return _title;
    }

    public void SetTitle(string newTitle)
    {
      _title = newTitle;
    }

    public int GetBookId()
    {
      return _bookId;
    }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO books (title) VALUES (@title);";
      MySqlParameter title = new MySqlParameter();
      title.ParameterName = "@title";
      title.Value = this._title;
      cmd.Parameters.Add(title);
      cmd.ExecuteNonQuery();
      _bookId = (int) cmd.LastInsertedId;
      conn.Close();
      if ( conn != null )
      {
        conn.Dispose();
      }
    }

    public static List<Book> GetAll()
    {
      List<Book> allBooks = new List<Book> { };
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM books;";
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        string title = rdr.GetString(0);
        int id = rdr.GetInt32(1);
        Book newBook = new Book(title, id);
        allBooks.Add(newBook);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allBooks;
    }

    public override bool Equals(System.Object otherBook)
    {
      if (!(otherBook is Book))
      {
        return false;
      }
      else
      {
        Book newBook = (Book) otherBook;
        bool bookIdEquality = (this.GetBookId() == newBook.GetBookId());
        bool bookNameEquality = (this.GetTitle() == newBook.GetTitle());
        return (bookIdEquality && bookNameEquality);
      }
    }
    public static Book Find(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM books where id = (@searchId);";
      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@searchId";
      searchId.Value = id;
      cmd.Parameters.Add(searchId);
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      int bookId = 0;
      string title = "";
      while (rdr.Read())
      {
        title = rdr.GetString(0);
        bookId = rdr.GetInt32(1);
      }
      Book newBook = new Book(title, bookId);
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return newBook;

    }

    public void Delete()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM authors_books WHERE book_id = @bookId; DELETE FROM books WHERE id = @bookId;";
      MySqlParameter bookIdParameter = new MySqlParameter();
      bookIdParameter.ParameterName = "@bookId";
      bookIdParameter.Value = this._bookId;
      cmd.Parameters.Add(bookIdParameter);
      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public void Edit(string newTitle)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"UPDATE books SET title = @bookTitle WHERE id = @bookId;";
      MySqlParameter bookTitleParameter = new MySqlParameter();
      bookTitleParameter.ParameterName = "@bookTitle";
      bookTitleParameter.Value = newTitle;
      cmd.Parameters.Add(bookTitleParameter);
      MySqlParameter bookIdParameter = new MySqlParameter();
      bookIdParameter.ParameterName = "@bookId";
      bookIdParameter.Value = this._bookId;
      cmd.Parameters.Add(bookIdParameter);
      cmd.ExecuteNonQuery();
      _title = newTitle;
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public void AddAuthor(Author newAuthor)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO authors_books (book_id, author_id) VALUES (@bookId, @authorId);";
      MySqlParameter bookIdParameter = new MySqlParameter();
      bookIdParameter.ParameterName = "@bookId";
      bookIdParameter.Value = _bookId;
      cmd.Parameters.Add(bookIdParameter);
      MySqlParameter authorIdParameter = new MySqlParameter();
      authorIdParameter.ParameterName = "@authorId";
      authorIdParameter.Value = newAuthor.GetAuthorId();
      cmd.Parameters.Add(authorIdParameter);
      cmd.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public List<Author> GetAuthors()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      // joining 3 tables authors books authors_books
      cmd.CommandText = @"SELECT authors.* FROM authors
        JOIN authors_books ON (authors.id = authors_books.author_id)
        JOIN books ON (authors_books.book_id = books.id)
        WHERE books.id = @bookId;";
      MySqlParameter bookIdParameter = new MySqlParameter();
      bookIdParameter.ParameterName = "@bookId";
      bookIdParameter.Value = _bookId;
      cmd.Parameters.Add(bookIdParameter);
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      List<Author> authorList = new List<Author>{};
      while(rdr.Read())
      {
        string authorName = rdr.GetString(0);
        int authorId = rdr.GetInt32(1);
        Author newAuthor = new Author(authorName, authorId);
        authorList.Add(newAuthor);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return authorList;
    }

    public static List<Book> GetAvailable()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT books.* FROM books WHERE book_id IN (SELECT book_id FROM copies WHERE patron_id IS NULL);";
      List<Book> allBooks = new List<Book>{};
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      int bookId = 0;
      string bookTitle = "";
      while (rdr.Read())
      {
        bookTitle = rdr.GetString(0);
        bookId = rdr.GetInt32(1);
        Book newBook = new Book(bookTitle, bookId);
        allBooks.Add(newBook);
      }

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }

      return allBooks;
    }
  }
}

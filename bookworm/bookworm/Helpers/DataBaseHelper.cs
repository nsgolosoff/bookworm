using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using bookworm.Entities;
using System.Threading;

namespace bookworm.Helpers
{
    public class DataBaseHelper
    {
        public static void LoadDataFromCSV(string filePath)
        {
            using (var db = new BookWormContext())
            {
                using (var reader = new StreamReader(filePath))
                {
                    while (!reader.EndOfStream)
                    {
                        try
                        {
                            var values = reader.ReadLine().Split(';');
                            var book = new Book();
                            book.ISBN = values[4].Trim();
                            book.Autor = values[0].Trim();
                            book.Title = values[1].Trim();
                            book.Price = double.Parse(values[2]);
                            book.Count = double.Parse(values[3]);
                            var dbBook = db.Books.FirstOrDefault(b =>
                                b.ISBN.Equals(book.ISBN));
                            if (dbBook == null)
                            {
                                db.Books.Add(book);
                            }
                            else
                            {
                                dbBook.Title = book.Title;
                                dbBook.Autor = book.Autor;
                                dbBook.Price = book.Price;
                                dbBook.Count += book.Count;
                            }

                        }
                        catch
                        {
                            continue;
                        }
                    }
                }
                db.SaveChanges();
            }
        }

        public static List<Book> GetBooks(Func<Book, bool> condition = null)
        {

            using (var db = new BookWormContext())
            {
                if (condition == null)
                    return db.Books.ToList();
                return db.Books.Where(condition).ToList();
            }
        }


        public static int RegisterOrder(Book book, string name, string phone, string address)
        {
            using (var db = new BookWormContext())
            {
                var client = db.Clients.FirstOrDefault((c) => c.Phone.Equals(phone));
                if (client == null)
                {
                    client = new Client { Name = name, Phone = phone, Address = address };
                    db.Clients.Add(client);
                }
                var dbBook = db.Books.FirstOrDefault((b) => b.Id == book.Id) ?? book;
                dbBook.Count--;
                var order = new Order()
                {
                    Book = dbBook,
                    Client = client
                };
                db.Orders.Add(order);
                db.SaveChanges();
                return order.Id;
            }
        }

    }
}


using WebApp.Data.Models;

namespace WebApp.Data

{
    
    public class DataSource
    {
        private static DataSource instance;
        private DataSource()
        {
        }
        public static DataSource GetInstance()
        {
            if (instance == null)
            {
                instance = new DataSource();
            }
            return instance;
        }
        public List<Author> _authors = new List<Author>();
        public List<Book> _books = new List<Book>();
        public List<Publisher> _publishers = new List<Publisher>();
    }
}

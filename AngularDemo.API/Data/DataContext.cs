using AngularDemo.API.Models;
using Microsoft.EntityFrameworkCore;

namespace AngularDemo.API.Data
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions<DataContext> options):base(options){ }
        public DbSet<WebUser> WebUser {get;set;}
    }
}
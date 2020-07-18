using AngularDemo.API.Properties.Models;
using Microsoft.EntityFrameworkCore;

namespace AngularDemo.API.Controllers.Data
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions<DataContext> options):base(options){ }
        public DbSet<WebUser> WebUser {get;set;}
    }
}
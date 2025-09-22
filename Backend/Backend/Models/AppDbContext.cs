using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Backend.Models;

// Db context is like a gateway to connect to the database which is coming from the entity framework core
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }

    //dbset represents a table of the database Person is like the table heading name {People} is the database table name
    public DbSet<Student> Students { get; set; }

}

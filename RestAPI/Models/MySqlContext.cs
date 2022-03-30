﻿using Microsoft.EntityFrameworkCore;

namespace RestAPI.Models
{
    public class MyDbContext : DbContext
    {
        public MyDbContext() { }
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { }

        public DbSet<Person> Persons { get; set; }
    }
}
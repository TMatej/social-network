﻿using DataAccessLayer.Entity;
using Microsoft.EntityFrameworkCore;
using System;

namespace PizzaShopDAL.Data
{
    public static class DataInitializer
    {
        //Specifying IDs is mandatory if seeding db through OnModelCreating method
        public static void Seed(this ModelBuilder modelBuilder)
        {
            /*modelBuilder.Entity<Shape>().HasData(triangle);
            modelBuilder.Entity<Ingredient>().HasData(ham);*/

            modelBuilder.Entity<User>().HasData
            (
                new User
                {
                    UserId = 1,
                    Username = "lokomotivatomas123",
                    PrimaryEmail = "cokoloko@gmail.com",
                    PasswordHash = "0123456789abcde0"
                }
            );
            modelBuilder.Entity<Profile>().HasData
            (
                new Profile
                {
                    Id = 1,
                    CreatedAt = DateTime.Now,
                    OwnerId = 1
                }
            );
        }
    }
}
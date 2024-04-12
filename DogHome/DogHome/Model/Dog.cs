﻿using DogHome.Repositories;
namespace DogHome.Model
{
    public class Dog : IEntityKey
    {
        public int Id { get; set; }
        public int Age { get; set; }
        public string Name { get; set; }
        public string? ImagePath { get; set; }
        public double Price { get; set; }
        public string? Gender { get; set; }
        public string? GroomingNeeds { get; set; }
        public int SellerId { get; set; }
        public string Size { get; set; }
        public int BreedId { get; set; }
    }
}

﻿namespace LibroMind_BE.Services.Models
{
    public class AddressGetDTO
    {
        public int Id { get; set; }

        public string Street { get; set; } = null!;

        public int Number { get; set; }

        public string? Building { get; set; }

        public int? Floor { get; set; }

        public int? Apartment { get; set; }

        public string City { get; set; } = null!;

        public string County { get; set; } = null!;

        public string Country { get; set; } = null!;

        public string? Map { get; set; }

    }
}
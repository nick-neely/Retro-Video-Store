﻿namespace RetroVideoStore.Models
{
    public class OrderDetail
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int MovieId { get; set; }
        public int Quantity { get; set; }

        public Order Order { get; set; }
        public Movie Movie { get; set; }
    }
}
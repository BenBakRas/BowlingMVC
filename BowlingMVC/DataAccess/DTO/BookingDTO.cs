using ShModel;
using DataAccess;
using DataAccess.DTO;
using DataAccess.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;



namespace DataAccess.DTO
{
    public class BookingDTO
    {
        public int Id { get; set; }
        public DateTime StartDateTime { get; set; }
        public TimeSpan HoursToPlay { get; set; }


        public int NoOfPlayers { get; set; }
        public Customer Customer { get; set; }
        public Lane Lane { get; set; }
        public Price Price { get; set; }

        public BookingDTO(int inId, DateTime inStartDateTime, TimeSpan inHoursToPlay, int inNoOfPlayers, Customer customer, Lane lane, Price price)
        {
            Id = inId;
            StartDateTime = inStartDateTime;
            HoursToPlay = inHoursToPlay;
            NoOfPlayers = inNoOfPlayers;
            Customer = customer;
            Lane = lane;
            Price = price;
        }
    }
}
using ShModel;
using BowlingMVC.DataAccess;
using BowlingMVC.DataAccess;
using BowlingMVC.DataAccess.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;



    namespace DataAccessDTO
    {
        public class DataAccessBooking : ICrudService<BookingDTO>
        {
            private SqlConnectionStringBuilder conStr;
            private ICrudService<CustomersDTO> _iCustomer;
            private ICrudService<PriceDTO> _iPrice;

            public DataAccessBooking()
            {
                conStr = DbConnection.GetConnectionStringBuilder();

                // Create a logger instance using NullLoggerFactory
                ILogger<DataAccessCustomers> dataAccessCustomerLogger = NullLoggerFactory.Instance.CreateLogger<DataAccessCustomers>();

                _iCustomers = new DataAccessCustomers(dataAccessCustomerLogger);
                _iPrice = new DataAccessPrice();
            }

            public int Create(BookingDTO bookingDto)
            {
                using (SqlConnection connection = DbConnection.GetConnection())
                {
                    connection.Open();

                    using (SqlCommand cmd = new SqlCommand("INSERT INTO Booking (StartDateTime, HoursToPlay, NoOfPlayers, CustomerID, LaneID, PriceID) OUTPUT INSERTED.ID VALUES (@StartDateTime, @HoursToPlay, @NoOfPlayers, @CustomerID, @LaneID, @PriceID)", connection))
                    {
                        cmd.Parameters.AddWithValue("@StartDateTime", bookingDto.StartDateTime);
                        cmd.Parameters.AddWithValue("@HoursToPlay", bookingDto.HoursToPlay.TotalHours);
                        cmd.Parameters.AddWithValue("@NoOfPlayers", bookingDto.NoOfPlayers);
                        cmd.Parameters.AddWithValue("@CustomerID", bookingDto.Customers.Id);
                        cmd.Parameters.AddWithValue("@LaneID", bookingDto.Lane.Id);
                        cmd.Parameters.AddWithValue("@PriceID", bookingDto.Price.Id);

                        return (int)cmd.ExecuteScalar();
                    }
                }
            }

            // ... rest of the methods ...

            private BookingDTO BuildDtoObject(SqlDataReader reader)
            {
                int id = reader.GetInt32(reader.GetOrdinal("Id"));
                DateTime startDateTime = reader.GetDateTime(reader.GetOrdinal("StartDateTime"));
                TimeSpan hoursToPlay = TimeSpan.FromHours(reader.GetDouble(reader.GetOrdinal("HoursToPlay")));
                int noOfPlayers = reader.GetInt32(reader.GetOrdinal("NoOfPlayers"));
                int customerId = reader.GetInt32(reader.GetOrdinal("CustomerID"));
                int laneId = reader.GetInt32(reader.GetOrdinal("LaneID"));
                int priceId = reader.GetInt32(reader.GetOrdinal("PriceID"));

                CustomersDTO customersDTO = _iCustomer.GetById(customerId);
                PriceDTO priceDTO = _iPrice.GetById(priceId);

                BookingDTO bookingDto = new BookingDTO
                {
                    Id = id,
                    StartDateTime = startDateTime,
                    HoursToPlay = hoursToPlay,
                    NoOfPlayers = noOfPlayers,
                    Customers = new Customers
                    {
                        Id = customersDTO.Id,
                        FirstName = customersDTO.FirstName,
                        LastName = customersDTO.LastName,
                        Email = customersDTO.Email,
                        Phone = customersDTO.Phone
                    },
                    Lane = new Lane
                    {
                        Id = laneId,
                        // Add additional properties for Lane here as needed
                    },
                    Price = new Price
                    {
                        Id = priceId,
                        // Add additional properties for Price here as needed
                    }
                };

                return bookingDto;
            }
        }
    }
}


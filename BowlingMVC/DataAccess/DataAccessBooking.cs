using DataAccess.DTO;
using Microsoft.Data.SqlClient;
using DataAccess.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using ShModel;
using System.Numerics;

namespace DataAccess
{
    public class DataAccessBooking : ICrudService<BookingDTO>
    {
        private SqlConnectionStringBuilder conStr;
        private ICrudService<CustomerDTO> _iCustomer;
        private ICrudService<PriceDTO> _iPrice;
        private ICrudService<LaneDTO> _iLane;
        public DataAccessBooking()
        {
            conStr = DbConnection.GetConnectionStringBuilder();

            // Create a logger instance using NullLoggerFactory
            ILogger<DataAccessCustomer> dataAccessCustomerLogger = NullLoggerFactory.Instance.CreateLogger<DataAccessCustomer>();

            _iCustomer = new DataAccessCustomer(dataAccessCustomerLogger);
            _iPrice = new DataAccessPrice();
        }

        public int Create(BookingDTO bookingDto)
        {
            using (SqlConnection connection = DbConnection.GetConnection())
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand("INSERT INTO Booking (HoursToPlay, StartDateTime, NoOfPlayers, CustomerID) OUTPUT INSERTED.ID VALUES (@HoursToPlay, @StartDateTime, @NoOfPlayers, @CustomerID)", connection))
                {
                    cmd.Parameters.AddWithValue("@HoursToPlay", bookingDto.HoursToPlay.TotalHours);
                    cmd.Parameters.AddWithValue("@StartDateTime", bookingDto.StartDateTime);
                    cmd.Parameters.AddWithValue("@NoOfPlayers", bookingDto.NoOfPlayers);
                    cmd.Parameters.AddWithValue("@CustomerID", bookingDto.Customer.Id);

                    return (int)cmd.ExecuteScalar();
                }
            }
        }

        public bool DeleteById(int id)
        {
            using (SqlConnection connection = DbConnection.GetConnection())
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand("DELETE FROM Booking WHERE Id = @Id", connection))
                {
                    cmd.Parameters.AddWithValue("@Id", id);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public IEnumerable<BookingDTO> GetAll()
        {
            using (SqlConnection connection = DbConnection.GetConnection())
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Booking", connection))
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    return BuildDtoObjects(reader);
                }
            }
        }

        public BookingDTO GetById(int id)
        {
            using (SqlConnection connection = DbConnection.GetConnection())
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Booking WHERE Id = @Id", connection))
                {
                    cmd.Parameters.AddWithValue("@Id", id);

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        return BuildDtoObject(reader);
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

        public bool Update(BookingDTO bookingDto)
        {
            using (SqlConnection connection = DbConnection.GetConnection())
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand("UPDATE Booking SET HoursToPlay = @HoursToPlay, StartDateTime = @StartDateTime, NoOfPlayers = @NoOfPlayers, CustomerID = @CustomerID WHERE Id = @Id", connection))
                {
                    cmd.Parameters.AddWithValue("@Id", bookingDto.Id);
                    cmd.Parameters.AddWithValue("@HoursToPlay", bookingDto.HoursToPlay.TotalHours);
                    cmd.Parameters.AddWithValue("@StartDateTime", bookingDto.StartDateTime);
                    cmd.Parameters.AddWithValue("@NoOfPlayers", bookingDto.NoOfPlayers);
                    cmd.Parameters.AddWithValue("@CustomerId", bookingDto.Customer.Id);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        private IEnumerable<BookingDTO> BuildDtoObjects(SqlDataReader reader)
        {
            List<BookingDTO> bookingDtos = new List<BookingDTO>();
            while (reader.Read())
            {
                BookingDTO bookingDto = BuildDtoObject(reader);
                bookingDtos.Add(bookingDto);
            }
            return bookingDtos;
        }


        private BookingDTO BuildDtoObject(SqlDataReader reader)
        {
            int id = reader.GetInt32(reader.GetOrdinal("Id"));
            int hoursToPlay = reader.GetInt32(reader.GetOrdinal("HoursToPlay"));
            DateTime startDateTime = reader.GetDateTime(reader.GetOrdinal("StartDateTime"));
            int noOfPlayers = reader.GetInt32(reader.GetOrdinal("NoOfPlayers"));
            int customerId = reader.GetInt32(reader.GetOrdinal("CustomerID"));

            CustomerDTO customerDTO = _iCustomer.GetById(customerId);

            // Create a new Customer object
            Customer customer = new Customer(customerDTO.Id, customerDTO.FirstName, customerDTO.LastName, customerDTO.Email, customerDTO.Phone);

            // TODO: Retrieve the Lane and Price objects or create them
            // Assuming that you have DataAccessLane and DataAccessPrice classes and their respective GetById methods,
            // you can retrieve the LaneDTO and PriceDTO objects like this:

            int laneId = reader.GetInt32(reader.GetOrdinal("LaneID"));
            LaneDTO laneDTO = _iLane.GetById(laneId);
            int priceId = reader.GetInt32(reader.GetOrdinal("PriceID"));
            PriceDTO priceDTO = _iPrice.GetById(priceId);

            // Create Lane and Price objects using the retrieved LaneDTO and PriceDTO
            Lane lane = new Lane(laneDTO.Id, laneDTO.LaneNumber);
            Price price = new Price(priceDTO.Id, priceDTO.NormalPrice, priceDTO.SpecialPrice, priceDTO.Weekday);

            // Create a new BookingDTO using the constructor
            BookingDTO bookingDto = new BookingDTO(id, startDateTime, TimeSpan.FromHours(hoursToPlay), noOfPlayers, customer, lane, price);

            return bookingDto;
        }

        /*public List<BookingDTO> GetBookingsForDate(DateTime date)
        {
            using (SqlConnection connection = DbConnection.GetConnection())
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Booking WHERE CAST(StartDateTime AS DATE) = @Date", connection))
                {
                    cmd.Parameters.AddWithValue("@Date", date.Date);

                    SqlDataReader reader = cmd.ExecuteReader();
                    return BuildDtoObjects(reader).ToList();*/
    }
}        
    

    

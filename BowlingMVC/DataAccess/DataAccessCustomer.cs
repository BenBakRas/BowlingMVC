using DataAccess.Interfaces;
using DataAccess.DTO;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace DataAccess
{
    public class DataAccessCustomer : ICrudService<CustomerDTO>
    {
        private SqlConnectionStringBuilder conStr;
        private readonly ILogger<DataAccessCustomer> _logger;

        public DataAccessCustomer(ILogger<DataAccessCustomer> logger)
        {
            _logger = logger;
            conStr = DbConnection.GetConnectionStringBuilder();
        }

        public int Create(CustomerDTO customerDto)
        {
            int customerId = -1;
            SqlConnection con = new(conStr.ConnectionString);

            string cmdTextCustomer = "INSERT INTO Customer (FirstName, LastName, Email, Phone) OUTPUT INSERTED.ID VALUES (@Fname, @Lname, @Email, @Phone)";
            SqlCommand cmdCustomer = new(cmdTextCustomer, con);

            cmdCustomer.Parameters.AddWithValue("@Fname", customerDto.FirstName);
            cmdCustomer.Parameters.AddWithValue("@Lname", customerDto.LastName);
            cmdCustomer.Parameters.AddWithValue("@Email", customerDto.Email);
            cmdCustomer.Parameters.AddWithValue("@Phone", customerDto.Phone);

            try
            {
                con.Open();
                customerId = (int)cmdCustomer.ExecuteScalar();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in Create method: {ex.Message}");
                throw;
            }
            finally
            {
                con.Close();
            }

            return customerId;
        }

        public CustomerDTO GetById(int id)
        {
            CustomerDTO customer = null;
            SqlConnection con = new(conStr.ConnectionString);
            SqlCommand cmd = new("SELECT * FROM Customer WHERE Id = @Id", con);
            cmd.Parameters.AddWithValue("@Id", id);

            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    customer = new CustomerDTO(
                        reader.GetInt32(reader.GetOrdinal("Id")),
                        reader.GetString(reader.GetOrdinal("FirstName")),
                        reader.GetString(reader.GetOrdinal("LastName")),
                        reader.GetString(reader.GetOrdinal("Email")),
                        reader.GetString(reader.GetOrdinal("Phone"))
                    );
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in GetById method: {ex.Message}");
                throw;
            }
            finally
            {
                con.Close();
            }

            return customer;
        }

        public IEnumerable<CustomerDTO> GetAll()
        {
            List<CustomerDTO> customers = new List<CustomerDTO>();
            SqlConnection con = new(conStr.ConnectionString);
            SqlCommand cmd = new("SELECT * FROM Customer", con);

            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    CustomerDTO customer = new CustomerDTO(
                        reader.GetInt32(reader.GetOrdinal("Id")),
                        reader.GetString(reader.GetOrdinal("FirstName")),
                        reader.GetString(reader.GetOrdinal("LastName")),
                        reader.GetString(reader.GetOrdinal("Email")),
                        reader.GetString(reader.GetOrdinal("Phone"))
                    );
                    customers.Add(customer);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in GetAll method: {ex.Message}");
                throw;
            }
            finally
            {
                con.Close();
            }

            return customers;
        }

        public bool Update(CustomerDTO customerDto)
        {
            bool result;
            SqlConnection con = new(conStr.ConnectionString);
            SqlCommand cmd = new("UPDATE Customer SET FirstName = @Fname, LastName = @Lname, Email = @Email, Phone = @Phone WHERE Id = @Id", con);
            cmd.Parameters.AddWithValue("@Fname", customerDto.FirstName);
            cmd.Parameters.AddWithValue("@Lname", customerDto.LastName);
            cmd.Parameters.AddWithValue("@Email", customerDto.Email);
            cmd.Parameters.AddWithValue("@Phone", customerDto.Phone);
            cmd.Parameters.AddWithValue("@Id", customerDto.Id);
            try
            {
                con.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                result = rowsAffected == 1;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in Update method: {ex.Message}");
                throw;
            }
            finally
            {
                con.Close();
            }

            return result;
        }

        public bool DeleteById(int id)
        {
            bool result;
            SqlConnection con = new(conStr.ConnectionString);
            SqlCommand cmd = new("DELETE FROM Customer WHERE Id = @Id", con);
            cmd.Parameters.AddWithValue("@Id", id);

            try
            {
                con.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                result = rowsAffected == 1;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in DeleteById method: {ex.Message}");
                throw;
            }
            finally
            {
                con.Close();
            }

            return result;
        }
    }
}
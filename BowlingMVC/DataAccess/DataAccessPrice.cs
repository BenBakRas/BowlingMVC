using DataAccess.Interfaces;
using DataAccess.DTO;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;

namespace DataAccess
{
    public class DataAccessPrice : ICrudService<PriceDTO>
    {
        private SqlConnectionStringBuilder conStr;

        public DataAccessPrice()
        {
            conStr = DbConnection.GetConnectionStringBuilder();
        }

        public int Create(PriceDTO entity)
        {
            SqlConnection con = new(conStr.ConnectionString);
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = "INSERT INTO Price (NormalPrice, SpecialPrice, Weekday) output INSERTED.ID VALUES (@normalPrice, @specialPrice, @weekday)";
            cmd.Parameters.AddWithValue("normalPrice", entity.NormalPrice);
            cmd.Parameters.AddWithValue("specialPrice", entity.SpecialPrice);
            cmd.Parameters.AddWithValue("weekday", entity.Weekday);

            int createdId = (int)cmd.ExecuteScalar();

            return createdId;
        }

        public bool DeleteById(int id)
        {
            SqlConnection con = new(conStr.ConnectionString);

            con.Open();

            SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = "DELETE FROM Price WHERE Id = @id";
            cmd.Parameters.AddWithValue("id", id);

            int rowsAffected = cmd.ExecuteNonQuery();

            return rowsAffected == 1;
        }

        public IEnumerable<PriceDTO> GetAll()
        {
            SqlConnection con = new(conStr.ConnectionString);
            con.Open();

            List<PriceDTO> list = null;

            SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = "SELECT * FROM Price";
            SqlDataReader reader = cmd.ExecuteReader();

            list = BuildObjects(reader);
            con.Close();

            return list;
        }

        public PriceDTO GetById(int id)
        {
            PriceDTO priceDto = null;

            SqlConnection con = new(conStr.ConnectionString);
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = "SELECT * FROM Price WHERE Id = @Id";
            cmd.Parameters.AddWithValue("@Id", id);
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read()) priceDto = BuildObject(reader);

            return priceDto;
        }

        public bool Update(PriceDTO entity)
        {
            SqlConnection con = new(conStr.ConnectionString);
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = "UPDATE Price SET NormalPrice = @normalPrice, SpecialPrice = @specialPrice, Weekday = @weekday WHERE Id = @id";
            cmd.Parameters.AddWithValue("normalPrice", entity.NormalPrice);
            cmd.Parameters.AddWithValue("specialPrice", entity.SpecialPrice);
            cmd.Parameters.AddWithValue("weekday", entity.Weekday);
            cmd.Parameters.AddWithValue("id", entity.Id);
            int rowsAffected = cmd.ExecuteNonQuery();

            return rowsAffected == 1;
        }

        private List<PriceDTO> BuildObjects(SqlDataReader reader)
        {
            List<PriceDTO> prices = new();
            try
            {
                while (reader.Read())
                {
                    PriceDTO priceDto = BuildObject(reader);
                    prices.Add(priceDto);
                }
            }
            catch (SqlException)
            {
                //TODO Handle exception
                throw;
            }
            return prices;
        }

        private PriceDTO BuildObject(SqlDataReader reader)
        {
            PriceDTO priceDto = null;

            try
            {
                priceDto = new PriceDTO(
                    reader.GetInt32(reader.GetOrdinal("Id")),
                    reader.GetDouble(reader.GetOrdinal("NormalPrice")),
                    reader.GetDouble(reader.GetOrdinal("SpecialPrice")),
                    reader.GetString(reader.GetOrdinal("Weekday"))
                );
            }
            catch (Exception)
            {
                // TODO Handle exception
            }
            return priceDto;
        }
    }
}
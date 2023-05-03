using DataAccessDTO.Interfaces;
using DataAccessDTO.DTO;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;

namespace DataAccessDTO
{
    public class DataAccessLane : ICrudService<LaneDTO>
    {
        private SqlConnectionStringBuilder conStr;
        public DataAccessLane()
        {
            conStr = DbConnection.GetConnectionStringBuilder();
        }

        public int Create(LaneDTO laneDto)
        {
            SqlConnection con = new(conStr.ConnectionString);
            con.Open();

            SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = "INSERT INTO Lane (Id, LaneNumber) OUTPUT INSERTED.ID VALUES (@Id, @LaneNumber)";
            cmd.Parameters.AddWithValue("@Id", laneDto.Id);
            cmd.Parameters.AddWithValue("@LaneNumber", laneDto.LaneNumber);

            int createdId = (int)cmd.ExecuteScalar();
            con.Close();

            return createdId;
        }

        public bool DeleteById(int id)
        {
            SqlConnection con = new(conStr.ConnectionString);
            con.Open();

            SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = "DELETE FROM Lane WHERE Id = @id";
            cmd.Parameters.AddWithValue("@id", id);

            int rowsAffected = cmd.ExecuteNonQuery();
            con.Close();

            return rowsAffected == 1;
        }

        public IEnumerable<LaneDTO> GetAll()
        {
            SqlConnection con = new(conStr.ConnectionString);
            con.Open();

            List<LaneDTO> list = new List<LaneDTO>();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = "SELECT * FROM Lane";
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                LaneDTO laneDto = new LaneDTO()
                {
                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                    LaneNumber = reader.GetInt32(reader.GetOrdinal("LaneNumber"))
                };
                list.Add(laneDto);
            }
            con.Close();
            return list;
        }

        public LaneDTO GetById(int id)
        {
            LaneDTO laneDto = null;
            SqlConnection con = new(conStr.ConnectionString);
            con.Open();

            SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = "SELECT * FROM Lane WHERE Id = @id";
            cmd.Parameters.AddWithValue("@id", id);
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                laneDto = new LaneDTO()
                {
                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                    LaneNumber = reader.GetInt32(reader.GetOrdinal("LaneNumber"))
                };
            }
            con.Close();
            return laneDto;
        }

        public bool Update(LaneDTO laneDto)
        {
            SqlConnection con = new(conStr.ConnectionString);
            con.Open();

            SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = "UPDATE Lane SET LaneNumber = @LaneNumber WHERE Id = @Id";
            cmd.Parameters.AddWithValue("@LaneNumber", laneDto.LaneNumber);
            cmd.Parameters.AddWithValue("@Id", laneDto.Id);

            int rowsAffected = cmd.ExecuteNonQuery();
            con.Close();

            return rowsAffected == 1;
        }
    }
}
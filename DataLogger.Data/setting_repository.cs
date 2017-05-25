using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using Npgsql;
using DataLogger.Entities;

namespace DataLogger.Data
{
    public class setting_repository: NpgsqlDBConnection
    {     
        #region Public procedure

        /// <summary>
        /// add new
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int add(ref setting obj)
        {
            using (NpgsqlDBConnection db = new NpgsqlDBConnection())
            {
                try
                {
                    int ID = -1;

                    if (db.open_connection())
                    {
                        string sql_command = "INSERT INTO settings ( setting_key, " +
                                            " setting_value, setting_type, note)" +
                                            " VALUES (:setting_key, " +
                                            " :setting_value, :setting_type, :note)";
                        sql_command += " RETURNING id;";

                        using (NpgsqlCommand cmd = db._conn.CreateCommand())
                        {   
                            cmd.CommandText = sql_command;

                            cmd.Parameters.Add(":setting_key", NpgsqlTypes.NpgsqlDbType.Varchar).Value = obj.setting_key;
                            cmd.Parameters.Add(":setting_value", NpgsqlTypes.NpgsqlDbType.Varchar).Value = obj.setting_value;
                            cmd.Parameters.Add(":setting_type", NpgsqlTypes.NpgsqlDbType.Varchar).Value = obj.setting_type;
                            cmd.Parameters.Add(":note", NpgsqlTypes.NpgsqlDbType.Varchar).Value = obj.note;                            

                            ID = Convert.ToInt32(cmd.ExecuteScalar());
                            obj.id = ID;

                            db.close_connection();
                            return ID;
                        }
                    }
                    else
                    {
                        db.close_connection();
                        return -1;
                    }
                }
                catch
                {
                    if (db != null)
                    {
                        db.close_connection();
                    }
                    return -1;
                }
                finally
                { db.close_connection(); }
            }
        }
        /// <summary>
        /// update
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int update(ref setting obj)
        {
            using (NpgsqlDBConnection db = new NpgsqlDBConnection())
            {
                try
                {

                    if (db.open_connection())
                    {
                        string sql_command = "UPDATE settings set  " +
                                            " setting_key = :setting_key, setting_value =:setting_value, " +
                                            " setting_type =:setting_type, " +
                                            " note = :note " +
                                            " where id = :id";

                        using (NpgsqlCommand cmd = db._conn.CreateCommand())
                        {                            
                            cmd.CommandText = sql_command;

                            cmd.Parameters.Add(":setting_key", NpgsqlTypes.NpgsqlDbType.Varchar).Value = obj.setting_key;
                            cmd.Parameters.Add(":setting_value", NpgsqlTypes.NpgsqlDbType.Varchar).Value = obj.setting_value;
                            cmd.Parameters.Add(":setting_type", NpgsqlTypes.NpgsqlDbType.Varchar).Value = obj.setting_type;
                            cmd.Parameters.Add(":note", NpgsqlTypes.NpgsqlDbType.Varchar).Value = obj.note;
                            cmd.Parameters.Add(":id", NpgsqlTypes.NpgsqlDbType.Integer).Value = obj.id;

                            cmd.ExecuteNonQuery();

                            db.close_connection();
                            return obj.id;
                        }
                    }
                    else
                    {
                        db.close_connection();
                        return -1;
                    }
                }
                catch
                {
                    if (db != null)
                    {
                        db.close_connection();
                    }
                    return -1;
                }
            }
        }


        /// <summary>
        /// delete
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool delete(int id)
        {
            using (NpgsqlDBConnection db = new NpgsqlDBConnection())
            {
                try
                {
                    bool result = false;

                    if (db.open_connection())
                    {
                        string sql_command = "DELETE from settings where id = " + id;

                        using (NpgsqlCommand cmd = db._conn.CreateCommand())
                        {                            
                            cmd.CommandText = sql_command;
                            result = cmd.ExecuteNonQuery() > 0;
                            db.close_connection();
                            return true;
                        }
                    }
                    else
                    {
                        db.close_connection();
                        return result;
                    }
                }
                catch
                {
                    if (db != null)
                    {
                        db.close_connection();
                    }
                    return false;
                }
                finally
                { db.close_connection(); }
            }
        }

        /// <summary>
        /// Get all
        /// </summary>
        /// <returns></returns>
        public IEnumerable<setting> get_all()
        {
            List<setting> listUser = new List<setting>();
            using (NpgsqlDBConnection db = new NpgsqlDBConnection())
            {
                try
                {   
                    if (db.open_connection())
                    {
                        string sql_command = "SELECT * FROM settings";
                        using (NpgsqlCommand cmd = db._conn.CreateCommand())
                        {                            
                            cmd.CommandText = sql_command;
                            NpgsqlDataReader reader = cmd.ExecuteReader();
                            while (reader.Read())
                            {
                                setting obj = new setting();
                                obj = (setting)_get_info(reader);
                                listUser.Add(obj);
                            }
                            reader.Close();
                            db.close_connection();
                            return listUser;
                        }
                    }
                    else
                    {
                        db.close_connection();
                        return null;
                    }
                }
                catch
                {
                    if (db != null)
                    {
                        db.close_connection();
                    }
                    return null;
                }
                finally
                { db.close_connection(); }
            }
        }

        /// <summary>
        /// get info by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public setting get_info_by_id(int id)
        {
            using (NpgsqlDBConnection db = new NpgsqlDBConnection())
            {
                try
                {

                    setting obj = null;
                    if (db.open_connection())
                    {
                        string sql_command = "SELECT * FROM settings WHERE id = " + id;
                        sql_command += " LIMIT 1";
                        using (NpgsqlCommand cmd = db._conn.CreateCommand())
                        {                            
                            cmd.CommandText = sql_command;

                            NpgsqlDataReader reader = cmd.ExecuteReader();

                            while (reader.Read())
                            {
                                obj = new setting();
                                obj = (setting)_get_info(reader);
                                break;
                            }
                            reader.Close();
                            db.close_connection();
                            return obj;
                        }
                    }
                    else
                    {
                        db.close_connection();
                        return null;
                    }
                }
                catch
                {
                    if (db != null)
                    {
                        db.close_connection();
                    }
                    return null;
                }
                finally
                { db.close_connection(); }
            }
        }
        
        #endregion Public procedure

        #region private procedure

        private setting _get_info(NpgsqlDataReader dataReader)
        {
            setting obj = new setting();
            try
            {
                if (!DBNull.Value.Equals(dataReader["id"]))
                    obj.id = Convert.ToInt32(dataReader["id"].ToString().Trim());
                else
                    obj.id = 0;
                if (!DBNull.Value.Equals(dataReader["setting_key"]))
                    obj.setting_key = dataReader["setting_key"].ToString().Trim();
                else
                    obj.setting_key = "";
                if (!DBNull.Value.Equals(dataReader["setting_value"]))
                    obj.setting_value = dataReader["setting_value"].ToString().Trim();
                else
                    obj.setting_value = "";
                if (!DBNull.Value.Equals(dataReader["setting_type"]))
                    obj.setting_type = dataReader["setting_type"].ToString().Trim();
                else
                    obj.setting_type = "";
               
                if (!DBNull.Value.Equals(dataReader["note"]))
                    obj.note = dataReader["note"].ToString().Trim();
                else
                    obj.note = "";               
            }
            catch (Exception ex)
            {                
                throw ex;             
            }
            return obj;
        }

        #endregion private procedure
    }
}
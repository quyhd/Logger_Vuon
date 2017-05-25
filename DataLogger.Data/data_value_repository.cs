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
    public class data_value_repository : NpgsqlDBConnection
    {
        #region Public procedure

        /// <summary>
        /// add new
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int add(ref data_value obj)
        {
            using (NpgsqlDBConnection db = new NpgsqlDBConnection())
            {
                try
                {
                    Int32 ID = -1;

                    if (db.open_connection())
                    {
                        string sql_command = "INSERT INTO data_values (MPS_pH, MPS_pH_status, MPS_EC, MPS_EC_status, " +
                                            " MPS_DO, MPS_DO_status, MPS_Turbidity, MPS_Turbidity_status, " +
                                            " MPS_ORP, MPS_ORP_status, MPS_SS, MPS_SS_status, MPS_Temp, MPS_Temp_status, " +
                                            " TN, TN_status, TP, TP_status, TOC, TOC_status, " +
                                            " module_Power, module_UPS, module_Door, module_Fire, module_Flow, " +
                                            " module_PumpLAM, module_PumpLRS, module_PumpLFLT, module_PumpRAM, module_PumpRRS, module_PumpRFLT, " +
                                            " module_air1, module_air2, module_cleaning, " +
                                            " module_Temperature, module_Humidity, " +
                                            " stored_date, stored_hour, stored_minute, MPS_status,pumping_system_status, station_status, " +
                                            " refrigeration_temperature, bottle_position, door_status, equipment_status, " +
                                            " created)" +
                                            " VALUES (:MPS_pH, :MPS_pH_status, :MPS_EC, :MPS_EC_status, " +
                                            " :MPS_DO,:MPS_DO_status, :MPS_Turbidity, :MPS_Turbidity_status, " +
                                            " :MPS_ORP, :MPS_ORP_status, :MPS_SS, :MPS_SS_status, :MPS_Temp, :MPS_Temp_status, " +
                                            " :TN, :TN_status, :TP, :TP_status, :TOC, :TOC_status, " +
                                            " :module_Power, :module_UPS, :module_Door, :module_Fire, :module_Flow, " +
                                            " :module_PumpLAM,:module_PumpLRS,:module_PumpLFLT,:module_PumpRAM,:module_PumpRRS,:module_PumpRFLT, " +
                                            " :module_air1, :module_air2, :module_cleaning, " +
                                            " :module_Temperature, :module_Humidity," +
                                            " :stored_date, :stored_hour, :stored_minute, :MPS_status, :pumping_system_status, :station_status, " +
                                            " :refrigeration_temperature, :bottle_position, :door_status, :equipment_status, " +
                                            " :created)";                        
                        sql_command += " RETURNING id;";

                        using (NpgsqlCommand cmd = db._conn.CreateCommand())
                        {
                            cmd.CommandText = sql_command;

                            cmd.Parameters.Add(":MPS_pH", NpgsqlTypes.NpgsqlDbType.Double).Value = obj.MPS_pH;
                            cmd.Parameters.Add(":MPS_pH_status", NpgsqlTypes.NpgsqlDbType.Integer).Value = obj.MPS_pH_status;
                            cmd.Parameters.Add(":MPS_EC", NpgsqlTypes.NpgsqlDbType.Double).Value = obj.MPS_EC;
                            cmd.Parameters.Add(":MPS_EC_status", NpgsqlTypes.NpgsqlDbType.Integer).Value = obj.MPS_EC_status;
                            cmd.Parameters.Add(":MPS_DO", NpgsqlTypes.NpgsqlDbType.Double).Value = obj.MPS_DO;
                            cmd.Parameters.Add(":MPS_DO_status", NpgsqlTypes.NpgsqlDbType.Integer).Value = obj.MPS_DO_status;
                            cmd.Parameters.Add(":MPS_Turbidity", NpgsqlTypes.NpgsqlDbType.Double).Value = obj.MPS_Turbidity;
                            cmd.Parameters.Add(":MPS_Turbidity_status", NpgsqlTypes.NpgsqlDbType.Integer).Value = obj.MPS_Turbidity_status;
                            cmd.Parameters.Add(":MPS_ORP", NpgsqlTypes.NpgsqlDbType.Double).Value = obj.MPS_ORP;
                            cmd.Parameters.Add(":MPS_ORP_status", NpgsqlTypes.NpgsqlDbType.Integer).Value = obj.MPS_ORP_status;
                            cmd.Parameters.Add(":MPS_SS", NpgsqlTypes.NpgsqlDbType.Double).Value = obj.MPS_SS;
                            cmd.Parameters.Add(":MPS_SS_status", NpgsqlTypes.NpgsqlDbType.Integer).Value = obj.MPS_SS_status;
                            cmd.Parameters.Add(":MPS_Temp", NpgsqlTypes.NpgsqlDbType.Double).Value = obj.MPS_Temp;
                            cmd.Parameters.Add(":MPS_Temp_status", NpgsqlTypes.NpgsqlDbType.Integer).Value = obj.MPS_Temp_status;
                            cmd.Parameters.Add(":TN", NpgsqlTypes.NpgsqlDbType.Double).Value = obj.TN;
                            cmd.Parameters.Add(":TN_status", NpgsqlTypes.NpgsqlDbType.Integer).Value = obj.TN_status;
                            cmd.Parameters.Add(":TP", NpgsqlTypes.NpgsqlDbType.Double).Value = obj.TP;
                            cmd.Parameters.Add(":TP_status", NpgsqlTypes.NpgsqlDbType.Integer).Value = obj.TP_status;
                            cmd.Parameters.Add(":TOC", NpgsqlTypes.NpgsqlDbType.Double).Value = obj.TOC;
                            cmd.Parameters.Add(":TOC_status", NpgsqlTypes.NpgsqlDbType.Integer).Value = obj.TOC_status;

                            cmd.Parameters.Add(":module_Power", NpgsqlTypes.NpgsqlDbType.Integer).Value = obj.module_Power;
                            cmd.Parameters.Add(":module_UPS", NpgsqlTypes.NpgsqlDbType.Integer).Value = obj.module_UPS;
                            cmd.Parameters.Add(":module_Door", NpgsqlTypes.NpgsqlDbType.Integer).Value = obj.module_Door;
                            cmd.Parameters.Add(":module_Fire", NpgsqlTypes.NpgsqlDbType.Integer).Value = obj.module_Fire;
                            cmd.Parameters.Add(":module_Flow", NpgsqlTypes.NpgsqlDbType.Integer).Value = obj.module_Flow;
                            cmd.Parameters.Add(":module_PumpLAM", NpgsqlTypes.NpgsqlDbType.Integer).Value = obj.module_PumpLAM;

                            cmd.Parameters.Add(":module_PumpLRS", NpgsqlTypes.NpgsqlDbType.Integer).Value = obj.module_PumpLRS;
                            cmd.Parameters.Add(":module_PumpLFLT", NpgsqlTypes.NpgsqlDbType.Integer).Value = obj.module_PumpLFLT;
                            cmd.Parameters.Add(":module_PumpRAM", NpgsqlTypes.NpgsqlDbType.Integer).Value = obj.module_PumpRAM;
                            cmd.Parameters.Add(":module_PumpRRS", NpgsqlTypes.NpgsqlDbType.Integer).Value = obj.module_PumpRRS;
                            cmd.Parameters.Add(":module_PumpRFLT", NpgsqlTypes.NpgsqlDbType.Integer).Value = obj.module_PumpRFLT;
                            cmd.Parameters.Add(":module_air1", NpgsqlTypes.NpgsqlDbType.Integer).Value = obj.module_air1;
                            cmd.Parameters.Add(":module_air2", NpgsqlTypes.NpgsqlDbType.Integer).Value = obj.module_air2;
                            cmd.Parameters.Add(":module_cleaning", NpgsqlTypes.NpgsqlDbType.Integer).Value = obj.module_cleaning;

                            cmd.Parameters.Add(":module_Temperature", NpgsqlTypes.NpgsqlDbType.Double).Value = obj.module_Temperature;
                            cmd.Parameters.Add(":module_Humidity", NpgsqlTypes.NpgsqlDbType.Double).Value = obj.module_Humidity;

                            cmd.Parameters.Add(":stored_date", NpgsqlTypes.NpgsqlDbType.Date).Value = obj.stored_date;
                            cmd.Parameters.Add(":stored_hour", NpgsqlTypes.NpgsqlDbType.Integer).Value = obj.stored_hour;
                            cmd.Parameters.Add(":stored_minute", NpgsqlTypes.NpgsqlDbType.Integer).Value = obj.stored_minute;
                            cmd.Parameters.Add(":MPS_status", NpgsqlTypes.NpgsqlDbType.Integer).Value = obj.MPS_status;
                            cmd.Parameters.Add(":pumping_system_status", NpgsqlTypes.NpgsqlDbType.Integer).Value = obj.pumping_system_status;
                            cmd.Parameters.Add(":station_status", NpgsqlTypes.NpgsqlDbType.Integer).Value = obj.station_status;

                            cmd.Parameters.Add(":refrigeration_temperature", NpgsqlTypes.NpgsqlDbType.Double).Value = obj.refrigeration_temperature;
                            cmd.Parameters.Add(":bottle_position", NpgsqlTypes.NpgsqlDbType.Integer).Value = obj.bottle_position;
                            cmd.Parameters.Add(":door_status", NpgsqlTypes.NpgsqlDbType.Integer).Value = obj.door_status;
                            cmd.Parameters.Add(":equipment_status", NpgsqlTypes.NpgsqlDbType.Integer).Value = obj.equipment_status;
                            cmd.Parameters.Add(":created", NpgsqlTypes.NpgsqlDbType.Timestamp).Value = obj.created;
                            //cmd.ExecuteNonQuery();
                            ID = (Int32)cmd.ExecuteScalar();
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
                catch(Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                    if (db != null)
                    {
                        db.close_connection();
                    }
                    return -1;
                }
                finally
                {
                    db.close_connection();
                }
            }
        }
        ///// <summary>
        ///// update
        ///// </summary>
        ///// <param name="obj"></param>
        ///// <returns></returns>
        //public int update(ref data_value obj)
        //{
        //    using (NpgsqlDBConnection db = new NpgsqlDBConnection())
        //    {
        //        try
        //        {

        //            if (db.open_connection())
        //            {
        //                string sql_command = "UPDATE data_values set  " +
        //                                    " data_value_key = :data_value_key, data_value_value =:data_value_value, " +
        //                                    " data_value_type =:data_value_type, " +
        //                                    " note = :note " +
        //                                    " where id = :id";

        //                using (NpgsqlCommand cmd = db._conn.CreateCommand())
        //                {                            
        //                    cmd.CommandText = sql_command;

        //                    cmd.Parameters.Add(new NpgsqlParameter(":data_value_key", obj.data_value_key));
        //                    cmd.Parameters.Add(new NpgsqlParameter(":data_value_value", obj.data_value_value));
        //                    cmd.Parameters.Add(new NpgsqlParameter(":data_value_type", obj.data_value_type));
        //                    cmd.Parameters.Add(new NpgsqlParameter(":note", obj.note));
        //                    cmd.Parameters.Add(new NpgsqlParameter(":id", obj.id));

        //                    cmd.ExecuteNonQuery();

        //                    db.close_connection();
        //                    return obj.id;
        //                }
        //            }
        //            else
        //            {
        //                db.close_connection();
        //                return -1;
        //            }
        //        }
        //        catch
        //        {
        //            if (db != null)
        //            {
        //                db.close_connection();
        //            }
        //            return -1;
        //        }
        //    }
        //}


        ///// <summary>
        ///// delete
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //public bool delete(int id)
        //{
        //    using (NpgsqlDBConnection db = new NpgsqlDBConnection())
        //    {
        //        try
        //        {
        //            bool result = false;

        //            if (db.open_connection())
        //            {
        //                string sql_command = "DELETE from data_values where id = " + id;

        //                using (NpgsqlCommand cmd = db._conn.CreateCommand())
        //                {                            
        //                    cmd.CommandText = sql_command;
        //                    result = cmd.ExecuteNonQuery() > 0;
        //                    db.close_connection();
        //                    return true;
        //                }
        //            }
        //            else
        //            {
        //                db.close_connection();
        //                return result;
        //            }
        //        }
        //        catch
        //        {
        //            if (db != null)
        //            {
        //                db.close_connection();
        //            }
        //            return false;
        //        }
        //        finally
        //        { db.close_connection(); }
        //    }
        //}

        /// <summary>
        /// Get all
        /// </summary>
        /// <returns></returns>
        public IEnumerable<data_value> get_all()
        {
            List<data_value> listUser = new List<data_value>();
            using (NpgsqlDBConnection db = new NpgsqlDBConnection())
            {
                try
                {
                    if (db.open_connection())
                    {
                        string sql_command = "SELECT * FROM data_values";
                        using (NpgsqlCommand cmd = db._conn.CreateCommand())
                        {
                            cmd.CommandText = sql_command;
                            NpgsqlDataReader reader = cmd.ExecuteReader();
                            while (reader.Read())
                            {
                                data_value obj = new data_value();
                                obj = (data_value)_get_info(reader);
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
        public data_value get_info_by_id(int id)
        {
            using (NpgsqlDBConnection db = new NpgsqlDBConnection())
            {
                try
                {

                    data_value obj = null;
                    if (db.open_connection())
                    {
                        string sql_command = "SELECT * FROM data_values WHERE id = " + id;
                        sql_command += " LIMIT 1";
                        using (NpgsqlCommand cmd = db._conn.CreateCommand())
                        {
                            cmd.CommandText = sql_command;

                            NpgsqlDataReader reader = cmd.ExecuteReader();

                            while (reader.Read())
                            {
                                obj = new data_value();
                                obj = (data_value)_get_info(reader);
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

        private data_value _get_info(NpgsqlDataReader dataReader)
        {
            data_value obj = new data_value();
            try
            {
                if (!DBNull.Value.Equals(dataReader["id"]))
                    obj.id = Convert.ToInt32(dataReader["id"].ToString().Trim());
                else
                    obj.id = 0;
                if (!DBNull.Value.Equals(dataReader["MPS_pH"]))
                    obj.MPS_pH = Convert.ToDouble(dataReader["MPS_pH"].ToString().Trim());
                else
                    obj.MPS_pH = 0;
                if (!DBNull.Value.Equals(dataReader["MPS_pH_status"]))
                    obj.MPS_pH_status = Convert.ToInt32(dataReader["MPS_pH_status"].ToString().Trim());
                else
                    obj.MPS_pH_status = 0;

                if (!DBNull.Value.Equals(dataReader["MPS_EC"]))
                    obj.MPS_EC = Convert.ToDouble(dataReader["MPS_EC"].ToString().Trim());
                else
                    obj.MPS_EC = 0;
                if (!DBNull.Value.Equals(dataReader["MPS_EC_status"]))
                    obj.MPS_EC_status = Convert.ToInt32(dataReader["MPS_EC_status"].ToString().Trim());
                else
                    obj.MPS_EC_status = 0;

                if (!DBNull.Value.Equals(dataReader["MPS_DO"]))
                    obj.MPS_DO = Convert.ToDouble(dataReader["MPS_DO"].ToString().Trim());
                else
                    obj.MPS_DO = 0;
                if (!DBNull.Value.Equals(dataReader["MPS_DO_status"]))
                    obj.MPS_DO_status = Convert.ToInt32(dataReader["MPS_DO_status"].ToString().Trim());
                else
                    obj.MPS_DO_status = 0;

                if (!DBNull.Value.Equals(dataReader["MPS_Turbidity"]))
                    obj.MPS_Turbidity = Convert.ToDouble(dataReader["MPS_Turbidity"].ToString().Trim());
                else
                    obj.MPS_Turbidity = 0;
                if (!DBNull.Value.Equals(dataReader["MPS_Turbidity_status"]))
                    obj.MPS_Turbidity_status = Convert.ToInt32(dataReader["MPS_Turbidity_status"].ToString().Trim());
                else
                    obj.MPS_Turbidity_status = 0;

                if (!DBNull.Value.Equals(dataReader["MPS_ORP"]))
                    obj.MPS_ORP = Convert.ToDouble(dataReader["MPS_ORP"].ToString().Trim());
                else
                    obj.MPS_ORP = 0;
                if (!DBNull.Value.Equals(dataReader["MPS_ORP_status"]))
                    obj.MPS_ORP_status = Convert.ToInt32(dataReader["MPS_ORP_status"].ToString().Trim());
                else
                    obj.MPS_ORP_status = 0;

                //if (!DBNull.Value.Equals(dataReader["MPS_SS"]))
                //    obj.MPS_SS = Convert.ToDouble(dataReader["MPS_SS"].ToString().Trim());
                //else
                //    obj.MPS_SS = 0;

                if (!DBNull.Value.Equals(dataReader["MPS_SS_status"]))
                    obj.MPS_SS_status = Convert.ToInt32(dataReader["MPS_SS_status"].ToString().Trim());
                else
                    obj.MPS_SS_status = 0;

                if (!DBNull.Value.Equals(dataReader["MPS_Temp"]))
                    obj.MPS_Temp = Convert.ToDouble(dataReader["MPS_Temp"].ToString().Trim());
                else
                    obj.MPS_Temp = 0;
                if (!DBNull.Value.Equals(dataReader["MPS_Temp_status"]))
                    obj.MPS_Temp_status = Convert.ToInt32(dataReader["MPS_Temp_status"].ToString().Trim());
                else
                    obj.MPS_Temp_status = 0;

                if (!DBNull.Value.Equals(dataReader["TN"]))
                    obj.TN = Convert.ToDouble(dataReader["TN"].ToString().Trim());
                else
                    obj.TN = 0;
                if (!DBNull.Value.Equals(dataReader["TN_status"]))
                    obj.TN_status = Convert.ToInt32(dataReader["TN_status"].ToString().Trim());
                else
                    obj.TN_status = 0;

                if (!DBNull.Value.Equals(dataReader["TP"]))
                    obj.TP = Convert.ToDouble(dataReader["TP"].ToString().Trim());
                else
                    obj.TP = 0;
                if (!DBNull.Value.Equals(dataReader["TP_status"]))
                    obj.TP_status = Convert.ToInt32(dataReader["TP_status"].ToString().Trim());
                else
                    obj.TP_status = 0;

                if (!DBNull.Value.Equals(dataReader["TOC"]))
                    obj.TOC = Convert.ToDouble(dataReader["TOC"].ToString().Trim());
                else
                    obj.TOC = 0;
                if (!DBNull.Value.Equals(dataReader["TOC_status"]))
                    obj.TOC_status = Convert.ToInt32(dataReader["TOC_status"].ToString().Trim());
                else
                    obj.TOC_status = 0;

                if (!DBNull.Value.Equals(dataReader["module_Power"]))
                    obj.module_Power = Convert.ToInt32(dataReader["module_Power"].ToString().Trim());
                else
                    obj.module_Power = 0;

                if (!DBNull.Value.Equals(dataReader["module_UPS"]))
                    obj.module_UPS = Convert.ToInt32(dataReader["module_UPS"].ToString().Trim());
                else
                    obj.module_UPS = 0;

                if (!DBNull.Value.Equals(dataReader["module_Door"]))
                    obj.module_Door = Convert.ToInt32(dataReader["module_Door"].ToString().Trim());
                else
                    obj.module_Door = 0;

                if (!DBNull.Value.Equals(dataReader["module_Fire"]))
                    obj.module_Fire = Convert.ToInt32(dataReader["module_Fire"].ToString().Trim());
                else
                    obj.module_Fire = 0;

                if (!DBNull.Value.Equals(dataReader["module_Flow"]))
                    obj.module_Flow = Convert.ToInt32(dataReader["module_Flow"].ToString().Trim());
                else
                    obj.module_Flow = 0;

                if (!DBNull.Value.Equals(dataReader["module_PumpLAM"]))
                    obj.module_PumpLAM = Convert.ToInt32(dataReader["module_PumpLAM"].ToString().Trim());
                else
                    obj.module_PumpLAM = 0;

                if (!DBNull.Value.Equals(dataReader["module_PumpLRS"]))
                    obj.module_PumpLRS = Convert.ToInt32(dataReader["module_PumpLRS"].ToString().Trim());
                else
                    obj.module_PumpLRS = 0;

                if (!DBNull.Value.Equals(dataReader["module_PumpLFLT"]))
                    obj.module_PumpLFLT = Convert.ToInt32(dataReader["module_PumpLFLT"].ToString().Trim());
                else
                    obj.module_PumpLFLT = 0;

                if (!DBNull.Value.Equals(dataReader["module_PumpRAM"]))
                    obj.module_PumpRAM = Convert.ToInt32(dataReader["module_PumpRAM"].ToString().Trim());
                else
                    obj.module_PumpRAM = 0;

                if (!DBNull.Value.Equals(dataReader["module_PumpRRS"]))
                    obj.module_PumpRRS = Convert.ToInt32(dataReader["module_PumpRRS"].ToString().Trim());
                else
                    obj.module_PumpRRS = 0;

                if (!DBNull.Value.Equals(dataReader["module_PumpRFLT"]))
                    obj.module_PumpRFLT = Convert.ToInt32(dataReader["module_PumpRFLT"].ToString().Trim());
                else
                    obj.module_PumpRFLT = 0;

                if (!DBNull.Value.Equals(dataReader["module_air1"]))
                    obj.module_air1 = Convert.ToInt32(dataReader["module_air1"].ToString().Trim());
                else
                    obj.module_air1 = 0;

                if (!DBNull.Value.Equals(dataReader["module_air2"]))
                    obj.module_air2 = Convert.ToInt32(dataReader["module_air2"].ToString().Trim());
                else
                    obj.module_air2 = 0;

                if (!DBNull.Value.Equals(dataReader["module_cleaning"]))
                    obj.module_cleaning = Convert.ToInt32(dataReader["module_cleaning"].ToString().Trim());
                else
                    obj.module_cleaning = 0;

                if (!DBNull.Value.Equals(dataReader["module_Temperature"]))
                    obj.module_Temperature = Convert.ToDouble(dataReader["module_Temperature"].ToString().Trim());
                else
                    obj.module_Temperature = 0;

                if (!DBNull.Value.Equals(dataReader["module_Humidity"]))
                    obj.module_Humidity = Convert.ToDouble(dataReader["module_Humidity"].ToString().Trim());
                else
                    obj.module_Humidity = 0;

                if (!DBNull.Value.Equals(dataReader["pumping_system_status"]))
                    obj.pumping_system_status = Convert.ToInt32(dataReader["pumping_system_status"].ToString().Trim());
                else
                    obj.pumping_system_status = 0;
                if (!DBNull.Value.Equals(dataReader["station_status"]))
                    obj.station_status = Convert.ToInt32(dataReader["station_status"].ToString().Trim());
                else
                    obj.station_status = 0;

                if (!DBNull.Value.Equals(dataReader["stored_date"]))
                    obj.stored_date = Convert.ToDateTime(dataReader["stored_date"].ToString().Trim());
                else
                    obj.stored_date = DateTime.Now;
                if (!DBNull.Value.Equals(dataReader["stored_hour"]))
                    obj.stored_hour = Convert.ToInt32(dataReader["stored_hour"].ToString().Trim());
                else
                    obj.stored_hour = 0;
                if (!DBNull.Value.Equals(dataReader["stored_minute"]))
                    obj.stored_minute = Convert.ToInt32(dataReader["stored_minute"].ToString().Trim());
                else
                    obj.stored_minute = 0;
                if (!DBNull.Value.Equals(dataReader["MPS_status"]))
                    obj.MPS_status = Convert.ToInt32(dataReader["MPS_status"].ToString().Trim());
                else
                    obj.MPS_status = 0;
                if (!DBNull.Value.Equals(dataReader["refrigeration_temperature"]))
                    obj.refrigeration_temperature = Convert.ToDouble(dataReader["refrigeration_temperature"].ToString().Trim());
                else
                    obj.refrigeration_temperature = 0;

                if (!DBNull.Value.Equals(dataReader["bottle_position"]))
                    obj.bottle_position = Convert.ToInt32(dataReader["bottle_position"].ToString().Trim());
                else
                    obj.bottle_position = 0;
                if (!DBNull.Value.Equals(dataReader["equipment_status"]))
                    obj.equipment_status = Convert.ToInt32(dataReader["equipment_status"].ToString().Trim());
                else
                    obj.equipment_status = 0;
                if (!DBNull.Value.Equals(dataReader["door_status"]))
                    obj.door_status = Convert.ToInt32(dataReader["door_status"].ToString().Trim());
                else
                    obj.door_status = 0;
                if (!DBNull.Value.Equals(dataReader["created"]))
                    obj.created = Convert.ToDateTime(dataReader["created"].ToString().Trim());
                else
                    obj.created = DateTime.Now;

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
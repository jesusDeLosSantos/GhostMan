﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Entities;

namespace DAL.query
{
    public class clsMapQueryDAL
    {
        /// <summary>
        ///     <header>public static List<clsMap> getListOfMapsDAL()</header>
        ///     <description> This method calls the database and returns a list of clsMap</description>
        ///     <precondition> None </precondition>
        ///     <postcondition> returns List<clsMap> maps to the BL </postcondition>
        /// </summary>
        /// <returns>returns List<clsMap> maps</returns>
        public static List<clsMap> getListOfMapsDAL()
        {
            conecction.clsConnection myConnection = new conecction.clsConnection();
            List<clsMap> maps = new List<clsMap>();
            SqlConnection connection = null;
            SqlCommand myCommand = new SqlCommand();
            SqlDataReader myReader = null;
            clsMap oMap;

            try
            {
                connection = myConnection.getConnection();
                myCommand.CommandText = "SELECT * FROM Map";
                myCommand.Connection = connection;
                myReader = myCommand.ExecuteReader();
                if (myReader.HasRows)
                {
                    while (myReader.Read())
                    {
                        oMap = new clsMap();
                        oMap.Id = (int)myReader["id"];
                        oMap.Nick = (String)myReader["nick"];
                        oMap.Size = (int)myReader["size"];
                        oMap.CommunityMap = (bool)myReader["communityMap"];
                        maps.Add(oMap);
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                myReader.Close();
                myConnection.closeConnection(ref connection);
            }
            return maps;
        }
        /// <summary>
        ///     <header>public static List<clsMap> getListOfCustomMapsDAL()</header>
        ///     <description> This method calls the database and returns a list of custom maps</description>
        ///     <precondition> None </precondition>
        ///     <postcondition> returns List<clsMap> customMaps to the BL </postcondition>
        /// </summary>
        /// <returns>returns List<clsMap> customMaps</returns>
        public static List<clsMap> getListOfCustomMapsDAL()
        {
            conecction.clsConnection myConnection = new conecction.clsConnection();
            List<clsMap> customMaps = new List<clsMap>();
            SqlConnection connection = null;
            SqlCommand myCommand = new SqlCommand();
            SqlDataReader myReader = null;
            clsMap oCustomMap;

            try
            {
                connection = myConnection.getConnection();
                myCommand.CommandText = "SELECT * FROM Maps WHERE communityMap = 1";
                myCommand.Connection = connection;
                myReader = myCommand.ExecuteReader();
                if (myReader.HasRows)
                {
                    while (myReader.Read())
                    {
                        oCustomMap = new clsMap();
                        oCustomMap.Id = (int)myReader["id"];
                        oCustomMap.Nick = (String)myReader["nick"];
                        oCustomMap.Size = (int)myReader["size"];
                        oCustomMap.CommunityMap = (bool)myReader["communityMap"];
                        customMaps.Add(oCustomMap);
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                myReader.Close();
                myConnection.closeConnection(ref connection);
            }
            return customMaps;
        }
        /// <summary>
        ///     <header>public static List<clsMap> getListOfCustomMapsDAL()</header>
        ///     <description> This method calls the database and returns a list of custom maps</description>
        ///     <precondition> None </precondition>
        ///     <postcondition> returns List<clsMap> customMaps to the BL </postcondition>
        /// </summary>
        /// <returns>returns List<clsMap> customMaps</returns>
        public static List<clsMap> getEspecificNumbersCustomMapsDAL(int number, string condicionBetween)
        {
            conecction.clsConnection myConnection = new conecction.clsConnection();
            List<clsMap> customMaps = new List<clsMap>();
            SqlConnection connection = null;
            SqlCommand myCommand = new SqlCommand();
            SqlDataReader myReader = null;
            clsMap oCustomMap;

            try
            {
                connection = myConnection.getConnection();
                myCommand.CommandText = $"SELECT M.id,M.nick,M.size,M.communityMap FROM(SELECT ROW_NUMBER() OVER(ORDER BY(select NULL)) AS rowNum, *FROM GM_Maps) AS M WHERE communityMap = 1 AND M.rowNum BETWEEN {condicionBetween}";
                myCommand.Parameters.Add("@NumeroElementos", sqlDbType: System.Data.SqlDbType.Int).Value = number;
                myCommand.Connection = connection;
                myReader = myCommand.ExecuteReader();
                if (myReader.HasRows)
                {
                    while (myReader.Read())
                    {
                        oCustomMap = new clsMap();
                        oCustomMap.Id = (int)myReader["id"];
                        oCustomMap.Nick = (String)myReader["nick"];
                        oCustomMap.Size = (int)myReader["size"];
                        oCustomMap.CommunityMap = (bool)myReader["communityMap"];
                        customMaps.Add(oCustomMap);
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                myReader.Close();
                myConnection.closeConnection(ref connection);
            }
            return customMaps;
        }

        /// <summary>
        ///     <header>public static List<clsMap> getListOfDefaultMapsDAL()</header>
        ///     <description> This method calls the database and returns a list of default maps</description>
        ///     <precondition> None </precondition>
        ///     <postcondition> returns List<clsMap> defaultMaps to the BL </postcondition>
        /// </summary>
        /// <returns>returns List<clsMap> defaultMaps</returns>
        public static List<clsMap> getListOfDefaultMapsDAL()
        {
            conecction.clsConnection myConnection = new conecction.clsConnection();
            List<clsMap> defaultMaps = new List<clsMap>();
            SqlConnection connection = null;
            SqlCommand myCommand = new SqlCommand();
            SqlDataReader myReader = null;
            clsMap oDefaultMap;

            try
            {
                connection = myConnection.getConnection();
                myCommand.CommandText = "SELECT * FROM Map WHERE communityMap = 0";
                myCommand.Connection = connection;
                myReader = myCommand.ExecuteReader();
                if (myReader.HasRows)
                {
                    while (myReader.Read())
                    {
                        oDefaultMap = new clsMap();
                        oDefaultMap.Id = (int)myReader["id"];
                        oDefaultMap.Nick = (String)myReader["nick"];
                        oDefaultMap.Size = (int)myReader["size"];
                        oDefaultMap.CommunityMap = (bool)myReader["communityMap"];
                        defaultMaps.Add(oDefaultMap);
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                myReader.Close();
                myConnection.closeConnection(ref connection);
            }
            return defaultMaps;
        }
        /// <summary>
        ///     <header>public static int getLastMapDAL()</header>
        ///     <description> This method calls the database and returns the id of the last map</description>
        ///     <precondition> None </precondition>
        ///     <postcondition> returns an id to the BL </postcondition>
        /// </summary>
        /// <returns>returns int idMap</returns>
        public static int getLastMapDAL()
        {
            conecction.clsConnection myConnection = new conecction.clsConnection();
            SqlConnection connection = null;
            SqlCommand myCommand = new SqlCommand();
            SqlDataReader myReader = null;
            int idMap = -1;

            try
            {
                connection = myConnection.getConnection();
                myCommand.CommandText = "SELECT TOP 1 * FROM GM_Maps ORDER BY id DESC";
                myCommand.Connection = connection;
                myReader = myCommand.ExecuteReader();
                if (myReader.HasRows)
                {
                    idMap = (int)myReader["id"];
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                myReader.Close();
                myConnection.closeConnection(ref connection);
            }
            return idMap;
        }
    }
}

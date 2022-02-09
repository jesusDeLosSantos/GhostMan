using System;
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
<<<<<<< HEAD
                        oMap.CommunityMap = (int)myReader["communityMap"];
=======
                        oMap.CommunityMap = (bool)myReader["communityMap"];
>>>>>>> Alex
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
<<<<<<< HEAD
                myCommand.CommandText = "SELECT * FROM Map WHERE communityMap = 1";
=======
                myCommand.CommandText = "SELECT * FROM Maps WHERE communityMap = 1";
>>>>>>> Alex
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
<<<<<<< HEAD
                        oCustomMap.CommunityMap = (int)myReader["communityMap"];
=======
                        oCustomMap.CommunityMap = (bool)myReader["communityMap"];
>>>>>>> Alex
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
<<<<<<< HEAD
                        oDefaultMap.CommunityMap = (int)myReader["communityMap"];
=======
                        oDefaultMap.CommunityMap = (bool)myReader["communityMap"];
>>>>>>> Alex
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
    }
}

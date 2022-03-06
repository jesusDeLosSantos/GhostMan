using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Entities;

namespace DAL.query
{
    public class clsElementMapQueryDAL
    {
        /// <summary>
        ///     <header>public static List<clsElementMap> getListOfElementMapDAL()</header>
        ///     <description> This method calls the database and returns a list of clsElementMap</description>
        ///     <precondition> None </precondition>
        ///     <postcondition> returns List<clsElementMap> elementsMap to the BL </postcondition>
        /// </summary>
        /// <returns>returns List<clsElementMap> elementsMap</returns>
        public static List<clsElementMap> getListOfElementMapDAL()
        {
            conecction.clsConnection myConnection = new conecction.clsConnection();
            List<clsElementMap> elementsMap = new List<clsElementMap>();
            SqlConnection connection = null;
            SqlCommand myCommand = new SqlCommand();
            SqlDataReader myReader = null;
            clsElementMap oElementMap;

            try
            {
                connection = myConnection.getConnection();
                myCommand.CommandText = "SELECT * FROM GM_ElementMaps";
                myCommand.Connection = connection;
                myReader = myCommand.ExecuteReader();
                if (myReader.HasRows)
                {
                    while (myReader.Read())
                    {
                        oElementMap = new clsElementMap();
                        oElementMap.IdMap = (int)myReader["idMap"];
                        oElementMap.IdElement = (int)myReader["idElement"];
                        oElementMap.AxisX = (short)myReader["axisX"];
                        oElementMap.AxisY = (short)myReader["axisY"];
                        elementsMap.Add(oElementMap);
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
            return elementsMap;
        }
        /// <summary>
        ///     <header>public static List<clsElementMap> getElementMapOfDefaultHardMap()</header>
        ///     <description> This method calls the database and returns a list of clsElementMap of the default hard map</description>
        ///     <precondition> None </precondition>
        ///     <postcondition> returns List<clsElementMap> defaultHardElementsMap to the BL </postcondition>
        /// </summary>
        /// <returns>returns List<clsElementMap> defaultHardElementsMap</returns>
        public static List<clsElementMap> getElementMapOfDefaultHardMap()
        {
            conecction.clsConnection myConnection = new conecction.clsConnection();
            List<clsElementMap> defaultHardElementsMap = new List<clsElementMap>();
            SqlConnection connection = null;
            SqlCommand myCommand = new SqlCommand();
            SqlDataReader myReader = null;
            clsElementMap oElementMap;

            try
            {
                connection = myConnection.getConnection();
                myCommand.CommandText = "SELECT GMEM.idMap, GMEM.idElement, GMEM.axisX, GMEM.axisY FROM GM_ElementMaps AS GMEM INNER JOIN GM_Maps AS GMM ON GMEM.idMap = GMM.id WHERE GMM.nick = 'default' AND GMM.mapName='Hard'";
                myCommand.Connection = connection;
                myReader = myCommand.ExecuteReader();
                if (myReader.HasRows)
                {
                    while (myReader.Read())
                    {
                        oElementMap = new clsElementMap();
                        oElementMap.IdMap = (int)myReader["idMap"];
                        oElementMap.IdElement = (int)myReader["idElement"];
                        oElementMap.AxisX = (short)myReader["axisX"];
                        oElementMap.AxisY = (short)myReader["axisY"];
                        defaultHardElementsMap.Add(oElementMap);
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
            return defaultHardElementsMap;
        }
    }
}

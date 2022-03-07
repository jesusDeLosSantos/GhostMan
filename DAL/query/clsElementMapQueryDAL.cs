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
        ///     <header>public static List<clsElementMap> getElementMapOfDefaultMap(int id)</header>
        ///     <description> This method calls the database and returns a list of clsElementMap of the default map that you have selected via the id in parameters</description>
        ///     <precondition> the int id must be between 1-3 depending on which map u want to get elements from:
        ///     1.-Easy
        ///     2.-Medium
        ///     3.-Hard
        /// </precondition>
        ///     <postcondition> returns List<clsElementMap> defaultElementsMap to the BL </postcondition>
        /// </summary>
        /// <returns>returns List<clsElementMap> defaultElementsMap</returns>
        public static List<clsElementMap> getElementMapOfDefaultMap(int id)
        {
            conecction.clsConnection myConnection = new conecction.clsConnection();
            List<clsElementMap> defaultElementsMap = new List<clsElementMap>();
            SqlConnection connection = null;
            SqlCommand myCommand = new SqlCommand();
            SqlDataReader myReader = null;
            clsElementMap oElementMap;

            try
            {
                connection = myConnection.getConnection();
                switch (id)
                {
                    case 1:
                        myCommand.CommandText = "SELECT GMEM.idMap, GMEM.idElement, GMEM.axisX, GMEM.axisY FROM GM_ElementMaps AS GMEM INNER JOIN GM_Maps AS GMM ON GMEM.idMap = GMM.id WHERE GMM.nick = 'default' AND GMM.mapName='Easy'";

                        break;
                    case 2:
                        myCommand.CommandText = "SELECT GMEM.idMap, GMEM.idElement, GMEM.axisX, GMEM.axisY FROM GM_ElementMaps AS GMEM INNER JOIN GM_Maps AS GMM ON GMEM.idMap = GMM.id WHERE GMM.nick = 'default' AND GMM.mapName='Medium'";

                        break;
                    case 3:
                        myCommand.CommandText = "SELECT GMEM.idMap, GMEM.idElement, GMEM.axisX, GMEM.axisY FROM GM_ElementMaps AS GMEM INNER JOIN GM_Maps AS GMM ON GMEM.idMap = GMM.id WHERE GMM.nick = 'default' AND GMM.mapName='Hard'";
                        break;
                }
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
                        defaultElementsMap.Add(oElementMap);
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
            return defaultElementsMap;
        }
    }
}

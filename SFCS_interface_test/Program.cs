using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeneralLinkSql;
using GeneralLinkSqlLocal;
using CyBLE_MTK_Application;
using System.Net;


namespace SFCS_interface_test
{
    public enum SFCS_enum
    {
        STE,
        FTE,
        Local
    }
    class Program
    {
        

        static void Main(string[] args)
        {
            string MsgPermissonCheck = "";
            string SerialNumber = "BCP9149AT0220351D2AK";
            string Model = SerialNumber.Substring(0, 9);
            string WorkerID = "op001";
            string Station = "MTK";

            Console.WriteLine("<<<<This is Demo for shopfloor interface at Sigmation.>>>>");
            SFCS_SIGMA sFCS_SIGMA = new SFCS_SIGMA();


            Console.WriteLine(string.Format("Insert DUT and Scan in barcode {0}", SerialNumber));
            Console.WriteLine(string.Format("Automatically retrieve model name from SerialNumber: {0}", Model));
            Console.WriteLine(string.Format("Automatically retrieve WorkerID: {0}", WorkerID));
            Console.WriteLine(string.Format("Automatically retrieve Station: {0}", Station));

            Console.WriteLine(string.Format("Sending {0} to shopfloor for permission check...", SerialNumber));

            Console.WriteLine("");
            Console.WriteLine("");


            try
            {
                MsgPermissonCheck = sFCS_SIGMA.PermissonCheck(SerialNumber, Model, WorkerID, Station);

                Console.WriteLine(string.Format("Returned Message for PermissonCheck: " + MsgPermissonCheck));

                if (MsgPermissonCheck.ToUpper().Contains("FAIL"))
                {
                    Console.WriteLine("Failed to do the permission check.");
                    Console.WriteLine("Test Program End.");
                    return;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("Exception of Returned Message for PermissonCheck: " + ex.Message));
                return;
            }

            Console.WriteLine("");
            Console.WriteLine("Programming and Testing is running...");
            Console.WriteLine("Test finished.");
            Console.WriteLine("");


            string TesterID = Dns.GetHostName();
            UInt16 errorcode = 0x0000;
            string SocketId = "1";
            string TestResult = "PASS";
            string TestStation = Station;
            string MFI_ID = "";

            try
            {
                bool result = sFCS_SIGMA.UploadTestResult
                    (SerialNumber, Model, TesterID, errorcode, SocketId,
                    TestResult, TestStation, MFI_ID);

                if (result)
                {
                    Console.WriteLine("SFCS Upload: {0} {1} {2} {3} {4} {5} {6}", 
                        SerialNumber,Model,TesterID,errorcode.ToString("X4"),SocketId,TestResult,TestStation);
                    Console.WriteLine("sFCS_SIGMA.UploadTestResult is successfully.");
                }
                else
                {
                    Console.WriteLine("sFCS_SIGMA.UploadTestResult is failure.");

                }
            }
            catch (Exception)
            {

                throw;
            }



            Console.ReadKey();
        }
    }
}

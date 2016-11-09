using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DotNetNuke;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Security;
using DotNetNuke.Entities.Users;
using DotNetNuke.Security.Membership;
using DotNetNuke.Services.Log.EventLog;
using DotNetNuke.Services.Mail;
using System.Data;
using System.Data.SqlClient;
using System.Xml;
using DotNetNuke.Services.Scheduling;
using System.IO;
using System.Net;
using System.Collections;



namespace qXMLFeedReader.DotNetNuke
{
    class ScheduledTaskExample : SchedulerClient
    {
        public ScheduledTaskExample(ScheduleHistoryItem objScheduleHistoryItem)
            : base()
        {
            ScheduleHistoryItem = objScheduleHistoryItem;
        }

        public override void DoWork()
        {
            try
            {
                
                //get the first xml feed
                XmlTextReader reader =
                    new XmlTextReader("https://beheer.ingoedebanen.nl/export/all/097b591799edc67a");

              
                DataSet ds = new DataSet();

                //string VacIDForDebug;

                ds.ReadXml(reader); //put the first xml in a dataset
               

                // create tables from the data
                DataTable dt = ds.Tables[1];
               
                // get the connection string from web.config
                string strConnectionString = ConfigurationManager.AppSettings["SiteSqlServer"];
                // get the web site root folder from the web.config
                string rootPath = ConfigurationManager.AppSettings["WebSitePath"];

                // create connection obj
                SqlConnection connection = new SqlConnection(strConnectionString);

                // Delete all ads that have transactionID = XMLFeed
                // delete the existing XML Feed data

                // the name of the delete stored proc
                string strUPD = "omv_delete_feed_ads";

                // create an update command object
                System.Data.SqlClient.SqlCommand cmdUpdate = null;

                // set command type to stored proc
                cmdUpdate = new SqlCommand(strUPD, connection);
                cmdUpdate.CommandType = CommandType.StoredProcedure;

                // add parameter for transaction ID
                cmdUpdate.Parameters.AddWithValue("@TransactionID", "XMLFeedBeheer");

                connection.Open();
                cmdUpdate.ExecuteNonQuery();
                connection.Close();
                
                // use an array list to get a short array of the logo paths in the feed as we loop it
                ArrayList FeedLogos = new ArrayList();

                // Loop the new feed data
                foreach (DataRow row in dt.Rows)
                {
                    string vacatureid = (string)row["vacatureid"];
                    string ShortTitle = (string)row["functienaam"];
                    string ActivateDate = (string)row["gemaakt"];
                    string CompanyName = (string)row["contact_bedrijfsnaam"];
                    string CompanyWebSiteURL = (string)row["vacature_url"];
                    string City = (string)row["locatie"];
                    string ContactName = (string)row["contact_naam"];
                    string ContactPhone = (string)row["contact_telefoon"];
                    string ContactEmail = (string)row["contact_email"];
                    string Description = (string)row["functieomschrijving"];
                    string Organization = (string)row["Organisatie"];
                    string Region = (string)row["Regio"];
                    string AdTypeOfWork = (string)row["Dienstverband"];
                    string AdHours = (string)row["Contracttype"];
                    string SalaryMIN = (string)row["Salaris_min"];
                    string SalaryMAX = (string)row["Salaris_max"];
                    string SalaryInfo = "";
                    string AdPeriod = (string)row["Salaris_periode"];
                    string AdExperience = (string)row["Werkervaring"];
                    string AdCareerlevel = (string)row["Carriere_niveau"];
                    string AdEducation = (string)row["Opleidingsniveau"];
                    string keyword1 = (string)row["Trefwoord"];
                    string LastUpdated = (string)row["gemaakt"];
                    string DEOI = (string)row["werkgever"];
                    string functieeisen = (string)row["functieeisen"];
                    string bedrijfsomschrijving = (string)row["bedrijfsomschrijving"];
                    string arbeidsvoorwaarden = (string)row["arbeidsvoorwaarden"];
                    string sollicitatieprocedure = (string)row["sollicitatieprocedure"];
                    string logo = (string)row["logo"];

                    // put the file path in the array
                    if (string.IsNullOrEmpty(logo) == true)
                    {
                  
                    }
                    else
                    {
                        FeedLogos.Add(logo);     
                    }

                   

                    // Insert the row into the table
                    // the name of the delete stored proc
                    string strINSERT = "omv_XML_Feed_Ads_INSERT";

                    // create an update command object
                    System.Data.SqlClient.SqlCommand cmdUpdate2 = null;

                    // set command type to stored proc
                    cmdUpdate2 = new SqlCommand(strINSERT, connection);
                    cmdUpdate2.CommandType = CommandType.StoredProcedure;

                    // add parameters with specified data types and sizes
                    cmdUpdate2.Parameters.Add("@TransactionID",SqlDbType.VarChar,100);
                    cmdUpdate2.Parameters.Add("@Organization", SqlDbType.VarChar, 200);
                    cmdUpdate2.Parameters.Add("@Region", SqlDbType.VarChar, 200);
                    cmdUpdate2.Parameters.Add("@ShortTitle", SqlDbType.VarChar, 100);
                    cmdUpdate2.Parameters.Add("@Description", SqlDbType.NVarChar, -1);
                    cmdUpdate2.Parameters.Add("@City", SqlDbType.NVarChar, 50);
                    cmdUpdate2.Parameters.Add("@TypeOfWork", SqlDbType.VarChar, 200);
                    cmdUpdate2.Parameters.Add("@Hours", SqlDbType.VarChar, 200);
                    cmdUpdate2.Parameters.Add("@SalaryMIN", SqlDbType.VarChar, 200);
                    cmdUpdate2.Parameters.Add("@SalaryMAX", SqlDbType.VarChar, 200);
                    cmdUpdate2.Parameters.Add("@Period", SqlDbType.VarChar, 200);
                    cmdUpdate2.Parameters.Add("@SalaryInfo", SqlDbType.NVarChar, 200);
                    cmdUpdate2.Parameters.Add("@Experience", SqlDbType.VarChar, 200);
                    cmdUpdate2.Parameters.Add("@CareerLevel", SqlDbType.VarChar, 200);
                    cmdUpdate2.Parameters.Add("@Education", SqlDbType.VarChar, 200);
                    cmdUpdate2.Parameters.Add("@CompanyName", SqlDbType.NVarChar, 100);
                    cmdUpdate2.Parameters.Add("@CompanyWebSiteURL", SqlDbType.NVarChar, 500);
                    cmdUpdate2.Parameters.Add("@keyword1", SqlDbType.NVarChar, 100);
                    cmdUpdate2.Parameters.Add("@ContactName", SqlDbType.NVarChar, 50);
                    cmdUpdate2.Parameters.Add("@ContactEmail", SqlDbType.NVarChar, 80);
                    cmdUpdate2.Parameters.Add("@ContactPhone", SqlDbType.NVarChar, 20);
                    cmdUpdate2.Parameters.Add("@ActivateDate", SqlDbType.DateTime);
                    cmdUpdate2.Parameters.Add("@LastUpdated", SqlDbType.DateTime);
                    cmdUpdate2.Parameters.Add("@DEOI", SqlDbType.VarChar, 50);
                    cmdUpdate2.Parameters.Add("@functieeisen", SqlDbType.NVarChar, -1);
                    cmdUpdate2.Parameters.Add("@bedrijfsomschrijving", SqlDbType.NVarChar, -1);
                    cmdUpdate2.Parameters.Add("@arbeidsvoorwaarden", SqlDbType.NVarChar, -1);
                    cmdUpdate2.Parameters.Add("@sollicitatieprocedure", SqlDbType.NVarChar, -1);
                    cmdUpdate2.Parameters.Add("@logo", SqlDbType.NVarChar, 500);

                    // now add the data
                    cmdUpdate2.Parameters["@TransactionID"].Value = "XMLFeedBeheer";
                    cmdUpdate2.Parameters["@Organization"].Value = Organization;
                    cmdUpdate2.Parameters["@Region"].Value= Region;
                    cmdUpdate2.Parameters["@ShortTitle"].Value= ShortTitle;
                    cmdUpdate2.Parameters["@Description"].Value= Description;
                    cmdUpdate2.Parameters["@City"].Value= City;
                    cmdUpdate2.Parameters["@TypeOfWork"].Value= AdTypeOfWork;
                    cmdUpdate2.Parameters["@Hours"].Value= AdHours;
                    cmdUpdate2.Parameters["@SalaryMIN"].Value= SalaryMIN;
                    cmdUpdate2.Parameters["@SalaryMAX"].Value= SalaryMIN;
                    cmdUpdate2.Parameters["@Period"].Value= AdPeriod;
                    cmdUpdate2.Parameters["@SalaryInfo"].Value= SalaryInfo;
                    cmdUpdate2.Parameters["@Experience"].Value= AdExperience;
                    cmdUpdate2.Parameters["@CareerLevel"].Value= AdCareerlevel;
                    cmdUpdate2.Parameters["@Education"].Value= AdEducation;
                    cmdUpdate2.Parameters["@CompanyName"].Value= CompanyName;
                    cmdUpdate2.Parameters["@CompanyWebSiteURL"].Value= CompanyWebSiteURL;
                    cmdUpdate2.Parameters["@keyword1"].Value= keyword1;
                    cmdUpdate2.Parameters["@ContactName"].Value= ContactName;
                    cmdUpdate2.Parameters["@ContactEmail"].Value= ContactEmail;
                    cmdUpdate2.Parameters["@ContactPhone"].Value= ContactPhone;
                    cmdUpdate2.Parameters["@ActivateDate"].Value= ActivateDate;
                    cmdUpdate2.Parameters["@LastUpdated"].Value= LastUpdated;
                    cmdUpdate2.Parameters["@DEOI"].Value= DEOI;
                    cmdUpdate2.Parameters["@functieeisen"].Value= functieeisen;
                    cmdUpdate2.Parameters["@bedrijfsomschrijving"].Value= bedrijfsomschrijving;
                    cmdUpdate2.Parameters["@arbeidsvoorwaarden"].Value= arbeidsvoorwaarden;
                    cmdUpdate2.Parameters["@sollicitatieprocedure"].Value= sollicitatieprocedure;
                    cmdUpdate2.Parameters["@logo"].Value= logo;

                    //open the connection
                    connection.Open();
                    // execute the query
                    cmdUpdate2.ExecuteNonQuery();
                    // close the connection
                    connection.Close();

                    // process the image files if there is data
                    if (!string.IsNullOrEmpty(logo) == true)
                    { // check if the file exists, if not then download it.

                        // get the virtual path to the site
                        string Path = rootPath;  //Server.MapPath("~");
                        string MappedPath = Path.Replace("\\", "/");
                        string FullPath = MappedPath + logo;

                        if (!File.Exists(FullPath))  // if the file doesn't exist
                        {
                            // get just the folder name
                            string logopath = logo.Substring(0, logo.LastIndexOf("/") + 1);
                            MappedPath = MappedPath + logopath; //check if the folder exists (create it if not)
                            if (!Directory.Exists(MappedPath))
                            Directory.CreateDirectory(MappedPath);

                            // get the file and save it
                            WebClient fileReader = new WebClient();
                            string imageAddress = "https://beheer.ingoedebanen.nl" + logo;
                            fileReader.DownloadFile(imageAddress,FullPath);
                        }
                     
                    }
                        
                }

                // this is where we check the files on disk to see if they are in the feed.
                // if NOT then delete from disk
                string mPath = rootPath; //Server.MapPath("~");
                int y = mPath.Length;
                string FolderPath = rootPath + "\\logo"; //Server.MapPath("~") + "\\logo";
                string CleanPath=FolderPath.Replace("\\","/");
                string[] files = Directory.GetFiles(FolderPath,"*.*",SearchOption.AllDirectories);
                string[] folders = Directory.GetDirectories(FolderPath, "*.*", SearchOption.TopDirectoryOnly);


                // loop the files
                foreach (string file in files)
                {
                    // we need the logo folder path and file name to compare with the feed data
                    string FilePath = file.Substring(y, file.Length-y);  // get the file and folder path
                    FilePath=FilePath.Replace("\\", "/");
                    int logoIndex = FeedLogos.IndexOf(FilePath);  // if the file doesn't exist then Index is -1
                    if(logoIndex==-1)
                    {
                        File.Delete(file);
                    }

                }

                // loop the folders and delete any empty ones
                foreach (string folder in folders)
                {
                    bool IsEmptyDirectory = (Directory.GetFiles(folder).Length == 0);
                    if (IsEmptyDirectory)
                        Directory.Delete(folder);
                }


                // report success to the scheduler framework
                ScheduleHistoryItem.Succeeded = true;

            }
            catch (Exception exc)
            {
                ScheduleHistoryItem.Succeeded = false;
                ScheduleHistoryItem.AddLogNote("EXCEPTION: " + exc.ToString());
                Errored(ref exc);
                Exceptions.LogException(exc);
            }

           
        }
       
    }
       
}
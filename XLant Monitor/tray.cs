using System;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using XLant;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Xml.Linq;
using System.Data;
using FileHelpers;
 
namespace MyTrayApp
{
    [DelimitedRecord(",")]
    public class VCrecord
    {
        //has to be a string to cope with headers, converted to int at runtime
        public string FileId;
        public string IndexData;
    }


    public class SysTrayApp : Form
    {
        [STAThread]
        public static void Main()
        {
            Application.Run(new SysTrayApp());
        }
 
        private NotifyIcon  trayIcon;
        private ContextMenu trayMenu;
        public List<string> toDos;
        XLMain.Staff user;
        string toBeIndex;
 
        public SysTrayApp()
        {
            // Create a simple tray menu with only one item.
            trayMenu = new ContextMenu();

            trayMenu.MenuItems.Add("Check", RefreshCheck);
            trayMenu.MenuItems.Add("TimeSheet Helper", LaunchTS);
            trayMenu.MenuItems.Add("Exit", OnExit);
 
            // Create a tray icon. In this example we use a
            // standard system icon for simplicity, but you
            // can of course use your own custom icon too.
            trayIcon      = new NotifyIcon();
            trayIcon.Text = "XLant Monitor";
            trayIcon.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
 
            // Add menu to tray icon and show it.
            trayIcon.ContextMenu = trayMenu;
            trayIcon.Visible     = true;
            trayIcon.BalloonTipTitle = "Xlant Monitor";

            //Collect data for later
            try
            {
                user = XLMain.Staff.StaffFromUser(Environment.UserName);   
                //discover the to be actioned by index
                XDocument settingsDoc = XLtools.settingsDoc;
                //query the setting files and try to find a match
                XElement setting = (from index in settingsDoc.Descendants("Indexes")
                                    select index).FirstOrDefault();
                foreach (XElement xIndex in setting.Descendants("Index"))
                {
                    if (xIndex.AttributeValueNull("Type") == "ToBe")
                    {
                        toBeIndex = xIndex.Value;
                    }
                }
            }
            catch (Exception e)
            {
                XLtools.LogException("XLant Monitor", e.ToString());
                MessageBox.Show("Application Failed to collect standing data");
            }
            Pause();
        }
        
        private void PeriodicCheck()
        {
            try
            {
                if (user.name != null && toBeIndex != null)
                {
                    int i = CheckToDos(user.name);
                    if (i != 0)
                    {
                        if (i == 1)
                        {
                            trayIcon.BalloonTipText = user.name + " you have a new item in your To Do's";
                        }
                        else
                        {
                            trayIcon.BalloonTipText = user.name + " you have " + i + " new items in your To Do's";
                        }
                        trayIcon.ShowBalloonTip(5000);
                    }
                }
                else
                {
                    MessageBox.Show("unable to load core data");
                }
            }
            catch (Exception e)
            {
                XLtools.LogException("XLant Monitor", e.ToString());
            }
        }
        
        public async Task Pause()
		{
            while (true)
            {
                await Task.Delay(30000);
                PeriodicCheck();
            }
		}
        
        private int CheckToDos(string name)
        {
            try
            {
                int noOfItems;
                //Run query against database
                //select * index01 from indextable where index05=name
                List<string> newToDos = new List<string>();
                string str = "select fileid from VCFileIndexView where index" + toBeIndex + " ='" + user.name + "'";
                DataTable xlReader = XLSQL.ReturnTable(str);
                if (xlReader.Rows.Count != 0)
                {
                    foreach (DataRow row in xlReader.Rows)
                    {
                        newToDos.Add(row["fileID"].ToString());
                    }
                }
                //compare list
                List<string> newItems = null;
                //handle there being nothing on the first run
                if (toDos == null)
                {
                    newItems = newToDos;
                }
                else
                {
                    newItems = newToDos.Except(toDos).ToList();
                }

                noOfItems = newItems.Count();
                //then set the todos to your new list for the next comparison
                toDos = newToDos;
                return noOfItems;
            }
            catch (Exception e)
            {
                XLtools.LogException("XLant Monitor", e.ToString());
                return 0;
            }
        }

        private void LaunchTS(object sender, EventArgs e)
        {
            XLant_Monitor.TimesheetHelper tsHelper = new XLant_Monitor.TimesheetHelper();
            tsHelper.Show();
        }

        protected override void OnLoad(EventArgs e)
        {
            Visible       = false; // Hide form window.
            ShowInTaskbar = false; // Remove from taskbar.
            this.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath); //set icon
            base.OnLoad(e);
        }
 
        private void OnExit(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void RefreshCheck(object sender, EventArgs e)
        {
            toDos = null;
            PeriodicCheck();
        }
 
        protected override void Dispose(bool isDisposing)
        {
            if (isDisposing)
            {
                // Release the icon resource.
                trayIcon.Dispose();
            }
 
            base.Dispose(isDisposing);
        }
    }
}
using System;
using System.Diagnostics;
using System.Windows.Forms;
using System.IO;

using WiimoteLib;

namespace HeadTracking
{
    public partial class HeadTracking : Form
    {
        public static Wiimote wm = new Wiimote();
        private WiimoteControl wii;        

        #region Initialization

        public static string FilePath = Directory.GetCurrentDirectory();
        public static string Game = "";
        public static string AppName = "";
        public static string WDirectory = "";        

        private static Process proc = null;
        
        public static bool Start = false;
        public static Timer tmrTrack;
        private int tic = 0;

        public static Moves moves = new Moves();

        public HeadTracking()
        {
            InitializeComponent();
            wii = new WiimoteControl();
            SuspendLayout();

            tmrTrack = new Timer();
            tmrTrack.Interval = 1000;
            tmrTrack.Tick += new EventHandler(timer_Tick);  

            Controls.Add(wii);
        }

        private void wm_WiimoteChanged(object sender, WiimoteChangedEventArgs args)
        {
            wii.UpdateState(args);
        }

        private void HeadTracking_Load_1(object sender, EventArgs e)
        {
            try
            {
                wii.Wiimote = wm;
                wm.Connect();
                wm.SetReportType(InputReport.IRAccel, true);
                wm.SetLEDs(false, false, false, false);

                string filename = "log_.txt";

                if (!File.Exists(filename))
                    WiimoteControl.stage = "Learning";
                else
                {
                    WiimoteControl.stage = "Practice";
                    Practice.ReadLog();
                    btnLearn.Text = "Start Practice";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception: " + ex.Message);
                Application.Exit();
            }

            pbBattery.Value = (wm.WiimoteState.Battery > 0xc8 ? 0xc8 : (int)wm.WiimoteState.Battery);
            lblBattery.Text = String.Format("{0:0.00}", wm.WiimoteState.Battery / pbBattery.Maximum * 100) + "%";
        }

        private void SelectFile(string name, string AppName)
        {
            OpenFileDialog op;
            DialogResult d;
            String path = "";
            String filter = "";

            if (AppName.Contains(".swf"))
                filter = "Swf files (*.swf)|*.swf|All files (*.*)|*.*";
            else
                filter = "Exe files (*.exe)|*.exe|All files (*.*)|*.*";

            do
            {
                op = new OpenFileDialog();
                op.Title = "Select the executable for " + name;
                op.Filter = filter;
                op.InitialDirectory = @"E:\";
                d = op.ShowDialog();
                if (d == DialogResult.OK)
                    path = op.FileName;
            }
            while (!path.Contains(AppName));

            int l = path.Length - AppName.Length;
            WDirectory = path.Substring(0, l);
        }

        private void cboWindows_SelectedIndexChanged(object sender, EventArgs e)
        {
            string name = cboWindows.Text;
            AppName = "";
            WDirectory = "";

            switch (name)
            {
                case "Tetris":
                    Game = "Tetris";
                    AppName = "tetris.swf";
                    SelectFile(name, AppName);
                    break;

                case "Pacman":
                    Game = "Pacman";
                    AppName = "pacman.swf";
                    SelectFile(name, AppName);
                    break;

                case "Need For Speed Underground 2":
                    Game = "Cars";
                    AppName = "speed2.exe";
                    SelectFile(name, AppName);
                    break;

                case "Quake III Arena":
                    Game = "Shooter";
                    AppName = "quake3.exe";
                    SelectFile(name, AppName);
                    break;

                default: break;
            }

            string filename = FilePath + "\\log_" + Game + ".txt";

            if (!File.Exists(filename))
            {
                WiimoteControl.stage = "Learning";
                btnLearn.Text = "Start Learning";
            }
            else
            {
                WiimoteControl.stage = "Practice";
                Practice.ReadLog();
                btnLearn.Text = "Start Practice";
            }
        }
        #endregion

        #region Learn&Practice

        private void btnLearn_Click_1(object sender, EventArgs e)
        {
            wm.WiimoteChanged += wm_WiimoteChanged;

            Console.WriteLine("Press F2 to start head tracking!");
            if (Game!= "") Console.WriteLine("Game: " + Game);
            
            if (AppName != "")
            {
                if (Game == "Pacman" || Game == "Tetris")
                    Process.Start(AppName);
                else
                {
                    proc = new Process();
                    proc.EnableRaisingEvents = true;
                    proc.StartInfo.WorkingDirectory = WDirectory;
                    proc.StartInfo.CreateNoWindow = false;
                    proc.StartInfo.FileName = AppName;
                    proc.Start();
                }
            }
            
            this.WindowState = FormWindowState.Minimized;
            KeyListener.Start();
        }

        public static void StartHeadTracking()
        {
            KeyListener.Stop();
            Console.WriteLine("HeadTracking started!");

            if (WiimoteControl.stage == "Learning")
            {
                switch (Game)
                {
                    case "Shooter": MouseListener.Start(); break;                    
                    default: KeyListener.Start(); break;
                }                
            }
            else
            {
                switch(Game)
                {
                    case "Pacman": break;
                    case "Tetris": break;
                    default: KeySender.Start(); break;
                }
            }

            tmrTrack.Start();
        }

        void timer_Tick(object sender, EventArgs e)
        {
            tic++;
            if (tic == 5 * 60)
            {
                Start = false;
                tmrTrack.Stop();

                if (proc != null)
                    proc.CloseMainWindow();
                else
                {
                    Process[] myProcesses;
                    myProcesses = Process.GetProcesses();

                    if (Game == "Pacman")
                    {                        
                        foreach (Process myProcess in myProcesses)
                        {
                            if (myProcess.MainWindowTitle.Contains("pacman"))
                                myProcess.CloseMainWindow();
                        }
                    }
                    else
                        if (Game == "Tetris")
                        {
                            foreach (Process myProcess in myProcesses)
                            {
                                if (myProcess.MainWindowTitle.Contains("tetris"))
                                    myProcess.CloseMainWindow();
                            }
                        }
                }

                Terminate();
            }
        } 

        #endregion       

        public void Terminate()
        {
            Start = false;

            cboWindows.SelectedIndex = -1;
            Console.WriteLine("HeadTracking stoped!");
            
            if (WiimoteControl.stage == "Learning")
            {
                switch (Game)
                {
                    case "Shooter": MouseListener.Stop(); break;
                    default: KeyListener.Stop(); break;
                }
                Learning.WriteLog();
            }

            Game = "";
            AppName = "";
            WDirectory = "";
            proc = null;
            tic = 0;

            WiimoteControl.Restart();
            this.WindowState = FormWindowState.Normal;
        }

        public static void DC()
        {
            wm.Disconnect();                                   
            Application.Exit();
        }

        private void HeadTracking_FormClosing(object sender, FormClosingEventArgs e)
        {
            DC();
        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PokemonGo.RocketAPI;

using GoBot.Logic;
using System.Threading;
using PokemonGo.RocketAPI.GeneratedCode;
using GoBot.Utils;

namespace GoBot
{
    public partial class FrmMain : Form
    {
        public BotInstance bot;
        public EventReceiver rec;
        public FrmMain()
        {
            InitializeComponent();
            rec = new EventReceiver();
            cbAuthType.SelectedIndex = 0;
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            Logger.SetLogger(new UserLogger.EventLogger(LogLevel.Info));

            LoadPokemon(clbCatch);
            LoadPokemon(clbEvolve);
            LoadPokemon(clbTransfer);
            LoadPokemon(clbBerries);
            rec.Set();

            GoBot.Utils.Events.OnMessageReceived += Events_OnMessageReceived;

            LoadSettings();
            
        }
        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveSettings();
            // I do this because it may not exit or a thread may be open still...
            Environment.Exit(0);
        }
        private void LoadSettings()
        {
            try
            {
                UserSettings.GoogleRefreshToken = Properties.Settings.Default.GoogleAuthValue;
                txtUser.Text = Properties.Settings.Default.Username;
                txtPass.Text = Properties.Settings.Default.Password;

                txtLat.Text = Properties.Settings.Default.Lat.ToString();
                txtLng.Text = Properties.Settings.Default.Lng.ToString();
                txtAltitude.Text = Properties.Settings.Default.Altitude.ToString();

                cbAuthType.SelectedIndex = Properties.Settings.Default.AuthType == "Ptc" ? 0 : 1;

                Logger.Write($"Google Token: {UserSettings.GoogleRefreshToken}");
            }
            catch (Exception ex)
            {
                MsgError(ex.ToString());
            }
        }
        private void SaveSettings()
        {
            try
            {
                Properties.Settings.Default.GoogleAuthValue = string.IsNullOrEmpty(UserSettings.GoogleRefreshToken) ? "" : UserSettings.GoogleRefreshToken;
                Properties.Settings.Default.Username = txtUser.Text;
                Properties.Settings.Default.Password = txtPass.Text;
                Properties.Settings.Default.Lat = txtLat.Text.ToDouble();
                Properties.Settings.Default.Lng = txtLng.Text.ToDouble();
                Properties.Settings.Default.Altitude = txtAltitude.Text.ToInt();
                Properties.Settings.Default.Save();
            }
            catch (Exception ex)
            {
                MsgError($"Exception: {ex}");
            }

        }
        private void Events_OnMessageReceived(object sender, Utils.LogReceivedArgs e)
        {
            try
            {  
                if (!this.IsHandleCreated)
                    return;

                this.Invoke((MethodInvoker)delegate ()
                {
                    // clear log
                    if (lvWalkLog.Items.Count > 300)
                        lvWalkLog.Items.Clear();

                    ListViewItem lvi = new ListViewItem($"[{ DateTime.Now.ToString("HH:mm:ss")}]");
                    lvi.SubItems.Add(e.Message);


                    lvWalkLog.Items.Add(lvi);


                });
            }
            catch (Exception ex)
            {

            }
        }



        private void MsgInfo(string msg)
        {
            MessageBox.Show(msg, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void MsgError(string msg)
        {
            MessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private void LoadPokemon(CheckedListBox clb)
        {
            foreach (PokemonId pid in Enum.GetValues(typeof(PokemonId)))
            {
                clb.Items.Add(pid.ToString());
            }

            clb.Sorted = true;
            clb.Sorted = false;
            clb.Items.Insert(0, "All");
        }
        private List<PokemonId> GetInverseList(CheckedListBox clb)
        {
            List<PokemonId> pids = new List<PokemonId>();
            pids.AddRange(Enum.GetValues(typeof(PokemonId)).Cast<PokemonId>().ToList());

            foreach (string str in clb.CheckedItems)
            {
                pids.Remove((PokemonId)Enum.Parse(typeof(PokemonId), str));
            }
            return pids;
        }
        private List<PokemonId> GetList(CheckedListBox clb)
        {
            List<PokemonId> pids = new List<PokemonId>();


            foreach (string str in clb.CheckedItems)
            {
                if (str == "All")
                {
                    pids.AddRange(Enum.GetValues(typeof(PokemonId)).Cast<PokemonId>().ToList());
                    break;
                }
                else
                {
                    pids.Add((PokemonId)Enum.Parse(typeof(PokemonId), str));
                }

            }
            return pids;
        }
        private void btnStart_Click(object sender, EventArgs e)
        {
            UserSettings.KeepCP = (txtBelowCp.Text).ToInt();
            UserSettings.EvolveOverCP = (txtEvolveCp.Text).ToInt(); 
            UserSettings.CatchOverCP = (txtCatchCp.Text).ToInt(); 

            UserSettings.KeepIV = (txtTransferIV.Text).ToInt(); 
            UserSettings.CatchOverIV = (txtCatchIV.Text).ToInt(); 
            UserSettings.EvolveOverIV = (txtEvolveIV.Text).ToInt(); 

            UserSettings.Username = txtUser.Text;
            UserSettings.Password = txtPass.Text;
            UserSettings.Auth = cbAuthType.SelectedIndex == 0 ? PokemonGo.RocketAPI.Enums.AuthType.Ptc : PokemonGo.RocketAPI.Enums.AuthType.Google;
            UserSettings.UseBerries = clbBerries.CheckedItems.Count > 0;
            UserSettings.WalkingSpeed = 20;
            UserSettings.StartLat = (txtLat.Text).ToDouble();
            UserSettings.StartLng = (txtLng.Text).ToDouble();
            UserSettings.CatchPokemon = chkCatchPokes.Checked;
            UserSettings.GetForts = chkGetForts.Checked;

            UserSettings.TopX = (txtTopX.Text).ToInt();

            UserSettings.Altitude = (txtAltitude.Text).ToDouble();

            Settings settings = new Settings();


            settings.recycleSettings.Add((txtPB.Text).ToInt());
            settings.recycleSettings.Add((txtGPB.Text).ToInt());
            settings.recycleSettings.Add((txtUPB.Text).ToInt());
            settings.recycleSettings.Add((txtP.Text).ToInt());
            settings.recycleSettings.Add((txtSP.Text).ToInt());
            settings.recycleSettings.Add((txtHP.Text).ToInt());
            settings.recycleSettings.Add((txtMP.Text).ToInt());
            settings.recycleSettings.Add((txtRevive.Text).ToInt());
            settings.recycleSettings.Add((txtMaxRevive.Text).ToInt());
            settings.recycleSettings.Add((txtRB.Text).ToInt());

            bot = new BotInstance(settings);

            bot.CatchList = chkInverseCatch.Checked ? GetInverseList(clbCatch) : GetList(clbCatch);
            bot.EvolveList = chkInverseEvolve.Checked ? GetInverseList(clbEvolve) : GetList(clbEvolve);
            bot.TransferList = chkInverseTransfer.Checked ? GetInverseList(clbTransfer) : GetList(clbTransfer);
            bot.BerryList = chkInverseBerries.Checked ? GetInverseList(clbBerries) : GetList(clbBerries);


            rec.bot = bot;
            bot.Execute();

            tStats.Start();
            btnStart.Enabled = false;
            btnStart.Scheme = cButton.Schemes.Green;

            btnStop.Enabled = true;
            btnStop.Scheme = cButton.Schemes.Red;
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            Logger.Write("Stop button hit!");
            bot.Stop();
            tStats.Stop();
            btnStart.Enabled = true;
            btnStart.Scheme = cButton.Schemes.Green;

            btnStop.Enabled = false;
            btnStop.Scheme = cButton.Schemes.Red;
        }

        private void tStats_Tick(object sender, EventArgs e)
        {

            if (bot == null || !bot.running)
                return;
            try
            {
                lblFound.Text = Statistics.PokemonFound;
                lblTransferred.Text = Statistics.PokemonTransferred;
                lblStardust.Text = Statistics.Stardust;
                lblXP.Text = Statistics.ExperiencePerHour;
                lblCurLevel.Text = Statistics.PlayerLevel;
                lblLevelUp.Text = Statistics.LevelUp;
                lblRequiredXP.Text = Statistics.RequiredXP;
                //lblDump.Text = bot._stats.ToString();
            }
            catch (Exception ex)
            {
                Logger.Write($"Stat Timer Error: {ex}");
            }
}

        private void btnResetSettings_Click(object sender, EventArgs e)
        {
            try
            {
                Properties.Settings.Default.Reset();
                Properties.Settings.Default.Save();

                Properties.Settings.Default.GoogleAuthValue = "";
                Properties.Settings.Default.Username = "Username";
                Properties.Settings.Default.Password = "Password";
                Properties.Settings.Default.Lat = 0;
                Properties.Settings.Default.Lng = 0;
                Properties.Settings.Default.Altitude = 0;

                Properties.Settings.Default.Save();
            }
            catch (Exception ex)
            {
                MsgError(ex.ToString());
            }
        }

        private async void btnEvolve_Click(object sender, EventArgs e)
        {
            if (bot == null || !bot.running)
            {
                MsgError("You cannot override when the bot is not running...");
                return;
            }

            await bot.EvolveAllPokemonWithEnoughCandy();
        }

        private async void btnTransfer_Click(object sender, EventArgs e)
        {
            if (bot == null || !bot.running)
            {
                MsgError("You cannot override when the bot is not running...");
                return;
            }

            await bot.TransferDuplicatePokemon(UserSettings.KeepCP, false);
        }
    }
}

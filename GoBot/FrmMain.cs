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
using AllEnum;
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
            UserSettings.GoogleRefreshToken = Properties.Settings.Default.GoogleAuthValue;
            txtUser.Text = Properties.Settings.Default.Username;
            txtPass.Text = Properties.Settings.Default.Password;

            txtLat.Text = Properties.Settings.Default.Lat.ToString();
            txtLng.Text = Properties.Settings.Default.Lng.ToString();
            txtAltitude.Text = Properties.Settings.Default.Altitude.ToString();

            cbAuthType.SelectedIndex = Properties.Settings.Default.AuthType == "Ptc" ?  0 : 1;

        }
        private void SaveSettings()
        {
            Properties.Settings.Default.GoogleAuthValue = UserSettings.GoogleRefreshToken;
            Properties.Settings.Default.Username = txtUser.Text;
            Properties.Settings.Default.Password = txtPass.Text;
            Properties.Settings.Default.Lat = GetDouble(txtLat.Text);
            Properties.Settings.Default.Lng = GetDouble(txtLng.Text);
            Properties.Settings.Default.Altitude = GetInt(txtAltitude.Text);
            Properties.Settings.Default.Save();

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

                    ListViewItem lvi = new ListViewItem(e.Sender);
                    lvi.SubItems.Add(e.Message);


                    lvWalkLog.Items.Add(lvi);


                });
            }
            catch (Exception ex)
            {

            }
        }

        private int GetInt(string str)
        {
            int o = 0;

            if (int.TryParse(str, out o))
            {
                return o;
            }
            return o;
        }
        private double GetDouble(string str)
        {
            double o = 0;

            if (double.TryParse(str, out o))
            {
                return o;
            }
            return o;
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
            UserSettings.KeepCP = GetInt(txtBelowCp.Text);
            UserSettings.EvolveOverCP = GetInt(txtEvolveCp.Text);
            UserSettings.CatchOverCP = GetInt(txtCatchCp.Text);

            UserSettings.KeepIV = GetInt(txtTransferIV.Text);
            UserSettings.CatchOverIV = GetInt(txtCatchIV.Text);
            UserSettings.EvolveOverIV = GetInt(txtEvolveIV.Text);

            UserSettings.Username = txtUser.Text;
            UserSettings.Password = txtPass.Text;
            UserSettings.Auth = cbAuthType.SelectedIndex == 0 ? PokemonGo.RocketAPI.Enums.AuthType.Ptc : PokemonGo.RocketAPI.Enums.AuthType.Google;
            UserSettings.UseBerries = clbBerries.CheckedItems.Count > 0;
            UserSettings.WalkingSpeed = 20;
            UserSettings.StartLat = GetDouble(txtLat.Text);
            UserSettings.StartLng = GetDouble(txtLng.Text);
            UserSettings.CatchPokemon = chkCatchPokes.Checked;
            UserSettings.GetForts = chkGetForts.Checked;

            UserSettings.Altitude = GetDouble(txtAltitude.Text);

            Settings settings = new Settings();


            settings.recycleSettings.Add(GetInt(txtPB.Text));
            settings.recycleSettings.Add(GetInt(txtGPB.Text));
            settings.recycleSettings.Add(GetInt(txtUPB.Text));
            settings.recycleSettings.Add(GetInt(txtP.Text));
            settings.recycleSettings.Add(GetInt(txtSP.Text));
            settings.recycleSettings.Add(GetInt(txtHP.Text));
            settings.recycleSettings.Add(GetInt(txtMP.Text));
            settings.recycleSettings.Add(GetInt(txtRevive.Text));
            settings.recycleSettings.Add(GetInt(txtMaxRevive.Text));
            settings.recycleSettings.Add(GetInt(txtRB.Text));

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

                lblDump.Text = bot._stats.ToString();
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
            }
            catch (Exception ex)
            {
                MsgError(ex.ToString());
            }
        }
    }
}

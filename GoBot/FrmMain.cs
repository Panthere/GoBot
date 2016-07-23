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
using PokemonGo.RocketAPI.Enums;
using System.Globalization;

namespace GoBot
{
    public partial class FrmMain : Form
    {
        private Sorter itemSorter;
        private Sorter pokemonSorter;
        public BotInstance bot;
        public EventReceiver rec;
        public FrmMain()
        {
            InitializeComponent();
            rec = new EventReceiver();
            cbAuthType.SelectedIndex = 0;
            itemSorter = new Sorter();
            pokemonSorter = new Sorter();
            lvBalls.ListViewItemSorter = itemSorter;
            lvPokemon.ListViewItemSorter = pokemonSorter;

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

            foreach (ColumnHeader ch in lvBalls.Columns)
            {
                string appendText = "< ";
                ch.Text = appendText + ch.Text;
            }
            foreach (ColumnHeader ch in lvPokemon.Columns)
            {
                string appendText = "< ";
                ch.Text = appendText + ch.Text;
            }

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

                txtGoogleAuth.Text = UserSettings.GoogleRefreshToken;

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

        private void lvBalls_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            itemSorter.Column = e.Column;
            // Reverse the current sort direction for this column.
            string appendText = "< ";
            if (itemSorter.Order == SortOrder.Ascending)
            {
                itemSorter.Order = SortOrder.Descending;
                appendText = "> ";

            }
            else
            {
                itemSorter.Order = SortOrder.Ascending;
            }

            lvBalls.Columns[e.Column].Text = appendText + lvBalls.Columns[e.Column].Text.Substring(2);

            // Perform the sort with these new sort options.
            lvBalls.Sort();
        }

        private void lvPokemon_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            pokemonSorter.Column = e.Column;
            // Reverse the current sort direction for this column.
            string appendText = "< ";
            if (pokemonSorter.Order == SortOrder.Ascending)
            {
                pokemonSorter.Order = SortOrder.Descending;
                appendText = "> ";

            }
            else
            {
                pokemonSorter.Order = SortOrder.Ascending;
            }

            lvPokemon.Columns[e.Column].Text = appendText + lvPokemon.Columns[e.Column].Text.Substring(2);

            // Perform the sort with these new sort options.
            lvPokemon.Sort();
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

            UserSettings.Teleport = chkTeleport.Checked;

            UserSettings.UseDelays = !chkNoDelay.Checked;

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

        private async void tStats_Tick(object sender, EventArgs e)
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
                lblRuntime.Text = Statistics.ProgramRuntime;
                lblPokemonPerHour.Text = Statistics.PokemonPerHour;

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

            if (!(MessageBox.Show("The override for evolve will evolve pokemon (in your inventory only) in the above CP and IV setting based on an OR statement. Filter lists WILL NOT APPLY.\r\n\r\nThis means that if you have 50 CP and 90% IV then it will evolve pokemon (with candy) that are OVER 49 CP OR that are greater than 89% IV.\r\n\r\nContinue?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes))
                return;

            await bot.OverrideEvolve(txtOverrideCP.Text.ToInt(), txtOverrideIV.Text.ToInt());
        }

        private async void btnTransfer_Click(object sender, EventArgs e)
        {
            if (bot == null || !bot.running)
            {
                MsgError("You cannot override when the bot is not running...");
                return;
            }

            if (!(MessageBox.Show("The override for transfer will transfer pokemon (in your inventory only) in the above CP and IV setting based on an OR statement. This will TRANSFER EVERY POKEMON (REGARDLESS IF DUPE OR NOT). Filter lists WILL NOT APPLY.\r\n\r\nThis means that if you have 50 CP and 90% IV then it will transfer pokemon that are 49 CP or below OR that are less than 89% IV.\r\n\r\nContinue?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes))
                return;

            if (txtOverrideCP.Text.ToInt() == 0 || txtOverrideIV.Text.ToInt() == 0)
            {
                if (!(MessageBox.Show("Warning, the IV and CP to override with have been set to 0. This will TRANSFER ALL POKEMON, IS THIS CORRECT?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes))
                    return;
            }


            await bot.OverrideTransfer(txtOverrideCP.Text.ToInt(), txtOverrideIV.Text.ToInt());
            //await bot.TransferDuplicatePokemon(UserSettings.KeepCP, false);
        }

        private void cbAuthType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbAuthType.SelectedIndex == 0)
            {
                txtUser.Visible = true;
                txtPass.Visible = true;
                txtGoogleAuth.Visible = false;
            }
            else
            {
                txtUser.Visible = false;
                txtPass.Visible = false;
                txtGoogleAuth.Visible = true;
            }
        }

        private async void refreshBallsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (bot == null || !bot.running)
            {
                MsgError("You cannot refresh when the bot is not running...");
                return;
            }
            try
            {
                lvBalls.Items.Clear();

                var items = await bot._inventory.GetItems();
                var balls = items.Where(i => ((MiscEnums.Item)i.Item_ == MiscEnums.Item.ITEM_POKE_BALL
                                          || (MiscEnums.Item)i.Item_ == MiscEnums.Item.ITEM_GREAT_BALL
                                          || (MiscEnums.Item)i.Item_ == MiscEnums.Item.ITEM_ULTRA_BALL
                                          || (MiscEnums.Item)i.Item_ == MiscEnums.Item.ITEM_MASTER_BALL) && i.Count > 0).GroupBy(i => ((MiscEnums.Item)i.Item_)).ToList();

                var pokeBalls = items.Where(i => (MiscEnums.Item)i.Item_ == MiscEnums.Item.ITEM_POKE_BALL).ToList();
                var greatBalls = items.Where(i => (MiscEnums.Item)i.Item_ == MiscEnums.Item.ITEM_GREAT_BALL).ToList();
                var ultraBalls = items.Where(i => (MiscEnums.Item)i.Item_ == MiscEnums.Item.ITEM_ULTRA_BALL).ToList();
                var masterBalls = items.Where(i => (MiscEnums.Item)i.Item_ == MiscEnums.Item.ITEM_MASTER_BALL).ToList();


                if (pokeBalls.Count > 0)
                    lvBalls.Items.Add(new ListViewItem(new[] { "Poke Balls", pokeBalls[0].Count.ToString() }));
                if (greatBalls.Count > 0)
                    lvBalls.Items.Add(new ListViewItem(new[] { "Great Balls", greatBalls[0].Count.ToString() }));
                if (ultraBalls.Count > 0)
                    lvBalls.Items.Add(new ListViewItem(new[] { "Ultra Balls", ultraBalls[0].Count.ToString() }));
                if (masterBalls.Count > 0)
                    lvBalls.Items.Add(new ListViewItem(new[] { "Master Balls", masterBalls[0].Count.ToString() }));
            }
            catch (Exception ex)
            {
                MsgError(ex.ToString());
            }
        }

        private async void refreshPokemonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (bot == null || !bot.running)
            {
                MsgError("You cannot refresh when the bot is not running...");
                return;
            }
            var items = await bot._inventory.GetItems();

        }

        private async void refreshAllItemsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (bot == null || !bot.running)
            {
                MsgError("You cannot refresh when the bot is not running...");
                return;
            }
            try
            {
                lvBalls.Items.Clear();
                var items = await bot._inventory.GetItems();
                foreach (var item in items)
                {
                    if (item.Count == 0)
                        continue;
                    try
                    {
                        MiscEnums.Item itemName = (MiscEnums.Item)item.Item_;
                        ListViewItem lvi = new ListViewItem(Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(itemName.ToString().Replace("ITEM_", "").Replace("_", " ").ToLower()));
                        lvi.SubItems.Add(item.Count.ToString());
                        lvBalls.Items.Add(lvi);
                    }
                    catch (InvalidCastException)
                    {

                    }
                }
            }
            catch (Exception ex)
            {
                MsgError(ex.ToString());
            }
}

        private async void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (bot == null || !bot.running)
            {
                MsgError("You cannot refresh when the bot is not running...");
                return;
            }
            try
            {
                lvPokemon.Items.Clear();

                var pokemons = await bot._inventory.GetPokemons();

                foreach (var pokemon in pokemons)
                {
                    ListViewItem lvi = new ListViewItem(pokemon.PokemonId.ToString());
                    lvi.SubItems.Add(pokemon.Cp.ToString());
                    lvi.SubItems.Add(BotInstance.CalculatePokemonPerfection(pokemon).ToString("0.00"));
                    lvi.SubItems.Add(pokemon.Stamina.ToString());
                    lvi.SubItems.Add(pokemon.StaminaMax.ToString());
                    lvi.SubItems.Add(pokemon.IndividualAttack.ToString());
                    lvPokemon.Items.Add(lvi);
                }
            }
            catch (Exception ex)
            {
                MsgError(ex.ToString());
            }

        }
    }
}

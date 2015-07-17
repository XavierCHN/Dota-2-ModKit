namespace Dota2ModKit
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.generateAddonLangsBtn = new MetroFramework.Controls.MetroButton();
			this.addonTile = new MetroFramework.Controls.MetroTile();
			this.metroContextMenu1 = new MetroFramework.Controls.MetroContextMenu(this.components);
			this.changePictureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.deleteAddonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.tabControl = new MetroFramework.Controls.MetroTabControl();
			this.toolsTab = new MetroFramework.Controls.MetroTabPage();
			this.findSoundNameBtn = new MetroFramework.Controls.MetroButton();
			this.decompileVtexButton = new MetroFramework.Controls.MetroButton();
			this.compileVtexButton = new MetroFramework.Controls.MetroButton();
			this.combineKVBtn = new MetroFramework.Controls.MetroButton();
			this.particleDesignBtn = new MetroFramework.Controls.MetroButton();
			this.helpTab = new MetroFramework.Controls.MetroTabPage();
			this.metroLink12 = new MetroFramework.Controls.MetroLink();
			this.metroLink11 = new MetroFramework.Controls.MetroLink();
			this.metroLink10 = new MetroFramework.Controls.MetroLink();
			this.metroLink9 = new MetroFramework.Controls.MetroLink();
			this.metroLink8 = new MetroFramework.Controls.MetroLink();
			this.metroLink7 = new MetroFramework.Controls.MetroLink();
			this.metroLink6 = new MetroFramework.Controls.MetroLink();
			this.goBtn = new MetroFramework.Controls.MetroButton();
			this.jsRadioButton = new MetroFramework.Controls.MetroRadioButton();
			this.textRadioButton = new MetroFramework.Controls.MetroRadioButton();
			this.luaRadioBtn = new MetroFramework.Controls.MetroRadioButton();
			this.addonNameLabel = new MetroFramework.Controls.MetroLabel();
			this.githubTextBox = new MetroFramework.Controls.MetroTextBox();
			this.metroLink5 = new MetroFramework.Controls.MetroLink();
			this.metroLink4 = new MetroFramework.Controls.MetroLink();
			this.metroLink3 = new MetroFramework.Controls.MetroLink();
			this.metroLink2 = new MetroFramework.Controls.MetroLink();
			this.metroLink1 = new MetroFramework.Controls.MetroLink();
			this.versionLabel = new MetroFramework.Controls.MetroLabel();
			this.notificationLabel = new MetroFramework.Controls.MetroLabel();
			this.metroRadioButton1 = new MetroFramework.Controls.MetroRadioButton();
			this.vscriptsTile = new MetroFramework.Controls.MetroTile();
			this.contentTile = new MetroFramework.Controls.MetroTile();
			this.metroTile3 = new MetroFramework.Controls.MetroTile();
			this.steamTile = new MetroFramework.Controls.MetroTile();
			this.metroToolTip1 = new MetroFramework.Components.MetroToolTip();
			this.metroTile1 = new MetroFramework.Controls.MetroTile();
			this.optionsBtn = new MetroFramework.Controls.MetroTile();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.metroContextMenu1.SuspendLayout();
			this.tabControl.SuspendLayout();
			this.toolsTab.SuspendLayout();
			this.helpTab.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.SuspendLayout();
			// 
			// generateAddonLangsBtn
			// 
			this.generateAddonLangsBtn.FontSize = MetroFramework.MetroButtonSize.Medium;
			this.generateAddonLangsBtn.Location = new System.Drawing.Point(130, 6);
			this.generateAddonLangsBtn.Margin = new System.Windows.Forms.Padding(4);
			this.generateAddonLangsBtn.Name = "generateAddonLangsBtn";
			this.generateAddonLangsBtn.Size = new System.Drawing.Size(125, 33);
			this.generateAddonLangsBtn.TabIndex = 1;
			this.generateAddonLangsBtn.TabStop = false;
			this.generateAddonLangsBtn.Text = "Generate Tooltips";
			this.generateAddonLangsBtn.Theme = MetroFramework.MetroThemeStyle.Dark;
			this.metroToolTip1.SetToolTip(this.generateAddonLangsBtn, "Generates tooltips from the scripts/npc files of this addon,\r\nwhich you can easil" +
        "y transfer over to addon_language.txt\r\nfiles.");
			this.generateAddonLangsBtn.UseSelectable = true;
			this.generateAddonLangsBtn.Click += new System.EventHandler(this.generateAddonLangsBtn_Click);
			// 
			// addonTile
			// 
			this.addonTile.ActiveControl = null;
			this.addonTile.ContextMenuStrip = this.metroContextMenu1;
			this.addonTile.Location = new System.Drawing.Point(9, 57);
			this.addonTile.Name = "addonTile";
			this.addonTile.Size = new System.Drawing.Size(136, 136);
			this.addonTile.Style = MetroFramework.MetroColorStyle.Green;
			this.addonTile.TabIndex = 0;
			this.addonTile.Text = "AddonName";
			this.addonTile.Theme = MetroFramework.MetroThemeStyle.Dark;
			this.metroToolTip1.SetToolTip(this.addonTile, "Left-click to change current addon. Right-click for\r\nmore options.");
			this.addonTile.UseSelectable = true;
			this.addonTile.UseTileImage = true;
			this.addonTile.Click += new System.EventHandler(this.addonTile_Click);
			// 
			// metroContextMenu1
			// 
			this.metroContextMenu1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.changePictureToolStripMenuItem,
            this.optionsToolStripMenuItem,
            this.deleteAddonToolStripMenuItem});
			this.metroContextMenu1.Name = "metroContextMenu1";
			this.metroContextMenu1.Size = new System.Drawing.Size(156, 70);
			// 
			// changePictureToolStripMenuItem
			// 
			this.changePictureToolStripMenuItem.Enabled = false;
			this.changePictureToolStripMenuItem.Name = "changePictureToolStripMenuItem";
			this.changePictureToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
			this.changePictureToolStripMenuItem.Text = "Change Picture";
			this.changePictureToolStripMenuItem.Click += new System.EventHandler(this.changePictureToolStripMenuItem_Click);
			// 
			// optionsToolStripMenuItem
			// 
			this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
			this.optionsToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
			this.optionsToolStripMenuItem.Text = "Options";
			this.optionsToolStripMenuItem.Click += new System.EventHandler(this.optionsToolStripMenuItem_Click);
			// 
			// deleteAddonToolStripMenuItem
			// 
			this.deleteAddonToolStripMenuItem.Name = "deleteAddonToolStripMenuItem";
			this.deleteAddonToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
			this.deleteAddonToolStripMenuItem.Text = "Delete Addon";
			this.deleteAddonToolStripMenuItem.Click += new System.EventHandler(this.deleteAddonBtn_Click);
			// 
			// tabControl
			// 
			this.tabControl.Controls.Add(this.toolsTab);
			this.tabControl.Controls.Add(this.helpTab);
			this.tabControl.FontSize = MetroFramework.MetroTabControlSize.Tall;
			this.tabControl.FontWeight = MetroFramework.MetroTabControlWeight.Regular;
			this.tabControl.ItemSize = new System.Drawing.Size(10, 34);
			this.tabControl.Location = new System.Drawing.Point(180, 39);
			this.tabControl.Name = "tabControl";
			this.tabControl.SelectedIndex = 0;
			this.tabControl.Size = new System.Drawing.Size(266, 225);
			this.tabControl.TabIndex = 2;
			this.tabControl.Theme = MetroFramework.MetroThemeStyle.Dark;
			this.tabControl.UseSelectable = true;
			// 
			// toolsTab
			// 
			this.toolsTab.Controls.Add(this.findSoundNameBtn);
			this.toolsTab.Controls.Add(this.decompileVtexButton);
			this.toolsTab.Controls.Add(this.compileVtexButton);
			this.toolsTab.Controls.Add(this.combineKVBtn);
			this.toolsTab.Controls.Add(this.particleDesignBtn);
			this.toolsTab.Controls.Add(this.generateAddonLangsBtn);
			this.toolsTab.HorizontalScrollbarBarColor = true;
			this.toolsTab.HorizontalScrollbarHighlightOnWheel = false;
			this.toolsTab.HorizontalScrollbarSize = 1;
			this.toolsTab.Location = new System.Drawing.Point(4, 38);
			this.toolsTab.Name = "toolsTab";
			this.toolsTab.Size = new System.Drawing.Size(258, 183);
			this.toolsTab.TabIndex = 0;
			this.toolsTab.Text = "Tools";
			this.toolsTab.Theme = MetroFramework.MetroThemeStyle.Dark;
			this.toolsTab.VerticalScrollbarBarColor = true;
			this.toolsTab.VerticalScrollbarHighlightOnWheel = false;
			this.toolsTab.VerticalScrollbarSize = 2;
			// 
			// findSoundNameBtn
			// 
			this.findSoundNameBtn.FontSize = MetroFramework.MetroButtonSize.Medium;
			this.findSoundNameBtn.Location = new System.Drawing.Point(0, 82);
			this.findSoundNameBtn.Margin = new System.Windows.Forms.Padding(4);
			this.findSoundNameBtn.Name = "findSoundNameBtn";
			this.findSoundNameBtn.Size = new System.Drawing.Size(125, 33);
			this.findSoundNameBtn.TabIndex = 6;
			this.findSoundNameBtn.TabStop = false;
			this.findSoundNameBtn.Text = "Find SoundName";
			this.findSoundNameBtn.Theme = MetroFramework.MetroThemeStyle.Dark;
			this.metroToolTip1.SetToolTip(this.findSoundNameBtn, "Decompiles .vtex_c files to .tga files");
			this.findSoundNameBtn.UseSelectable = true;
			this.findSoundNameBtn.Click += new System.EventHandler(this.findSoundNameBtn_Click);
			// 
			// decompileVtexButton
			// 
			this.decompileVtexButton.FontSize = MetroFramework.MetroButtonSize.Medium;
			this.decompileVtexButton.Location = new System.Drawing.Point(130, 82);
			this.decompileVtexButton.Margin = new System.Windows.Forms.Padding(4);
			this.decompileVtexButton.Name = "decompileVtexButton";
			this.decompileVtexButton.Size = new System.Drawing.Size(125, 33);
			this.decompileVtexButton.TabIndex = 5;
			this.decompileVtexButton.TabStop = false;
			this.decompileVtexButton.Text = ".vtex_c -> .tga";
			this.decompileVtexButton.Theme = MetroFramework.MetroThemeStyle.Dark;
			this.metroToolTip1.SetToolTip(this.decompileVtexButton, "Decompiles .vtex_c files to .tga files");
			this.decompileVtexButton.UseSelectable = true;
			this.decompileVtexButton.Click += new System.EventHandler(this.decompileVtexButton_Click);
			// 
			// compileVtexButton
			// 
			this.compileVtexButton.FontSize = MetroFramework.MetroButtonSize.Medium;
			this.compileVtexButton.Location = new System.Drawing.Point(130, 44);
			this.compileVtexButton.Margin = new System.Windows.Forms.Padding(4);
			this.compileVtexButton.Name = "compileVtexButton";
			this.compileVtexButton.Size = new System.Drawing.Size(125, 33);
			this.compileVtexButton.TabIndex = 4;
			this.compileVtexButton.TabStop = false;
			this.compileVtexButton.Text = ".tga -> .vtex_c";
			this.compileVtexButton.Theme = MetroFramework.MetroThemeStyle.Dark;
			this.metroToolTip1.SetToolTip(this.compileVtexButton, "Compiles .tga files to .vtex, and auto-creates the .vtex_c\'s");
			this.compileVtexButton.UseSelectable = true;
			this.compileVtexButton.Click += new System.EventHandler(this.compileVtexButton_Click);
			// 
			// combineKVBtn
			// 
			this.combineKVBtn.FontSize = MetroFramework.MetroButtonSize.Medium;
			this.combineKVBtn.Location = new System.Drawing.Point(0, 6);
			this.combineKVBtn.Margin = new System.Windows.Forms.Padding(4);
			this.combineKVBtn.Name = "combineKVBtn";
			this.combineKVBtn.Size = new System.Drawing.Size(125, 33);
			this.combineKVBtn.TabIndex = 3;
			this.combineKVBtn.TabStop = false;
			this.combineKVBtn.Text = "Combine KV Files";
			this.combineKVBtn.Theme = MetroFramework.MetroThemeStyle.Dark;
			this.metroToolTip1.SetToolTip(this.combineKVBtn, "Combines KV files in the scripts/npc directory of this\r\naddon. Prompts to break u" +
        "p the KV files if not done so\r\nalready.");
			this.combineKVBtn.UseSelectable = true;
			this.combineKVBtn.Click += new System.EventHandler(this.combineKVBtn_Click);
			// 
			// particleDesignBtn
			// 
			this.particleDesignBtn.FontSize = MetroFramework.MetroButtonSize.Medium;
			this.particleDesignBtn.Location = new System.Drawing.Point(0, 44);
			this.particleDesignBtn.Margin = new System.Windows.Forms.Padding(4);
			this.particleDesignBtn.Name = "particleDesignBtn";
			this.particleDesignBtn.Size = new System.Drawing.Size(125, 33);
			this.particleDesignBtn.TabIndex = 2;
			this.particleDesignBtn.TabStop = false;
			this.particleDesignBtn.Text = "Particle Designer";
			this.particleDesignBtn.Theme = MetroFramework.MetroThemeStyle.Dark;
			this.metroToolTip1.SetToolTip(this.particleDesignBtn, "Provides options to bulk modify the color, size, etc of\r\nselected particles.");
			this.particleDesignBtn.UseSelectable = true;
			this.particleDesignBtn.Click += new System.EventHandler(this.particleDesignBtn_Click);
			// 
			// helpTab
			// 
			this.helpTab.Controls.Add(this.metroLink12);
			this.helpTab.Controls.Add(this.metroLink11);
			this.helpTab.Controls.Add(this.metroLink10);
			this.helpTab.Controls.Add(this.metroLink9);
			this.helpTab.Controls.Add(this.metroLink8);
			this.helpTab.Controls.Add(this.metroLink7);
			this.helpTab.Controls.Add(this.metroLink6);
			this.helpTab.Controls.Add(this.goBtn);
			this.helpTab.Controls.Add(this.jsRadioButton);
			this.helpTab.Controls.Add(this.textRadioButton);
			this.helpTab.Controls.Add(this.luaRadioBtn);
			this.helpTab.Controls.Add(this.addonNameLabel);
			this.helpTab.Controls.Add(this.githubTextBox);
			this.helpTab.Controls.Add(this.metroLink5);
			this.helpTab.Controls.Add(this.metroLink4);
			this.helpTab.Controls.Add(this.metroLink3);
			this.helpTab.Controls.Add(this.metroLink2);
			this.helpTab.Controls.Add(this.metroLink1);
			this.helpTab.HorizontalScrollbarBarColor = true;
			this.helpTab.HorizontalScrollbarHighlightOnWheel = false;
			this.helpTab.HorizontalScrollbarSize = 1;
			this.helpTab.Location = new System.Drawing.Point(4, 38);
			this.helpTab.Name = "helpTab";
			this.helpTab.Size = new System.Drawing.Size(258, 183);
			this.helpTab.TabIndex = 3;
			this.helpTab.Text = "Help";
			this.helpTab.Theme = MetroFramework.MetroThemeStyle.Dark;
			this.helpTab.VerticalScrollbarBarColor = true;
			this.helpTab.VerticalScrollbarHighlightOnWheel = false;
			this.helpTab.VerticalScrollbarSize = 2;
			// 
			// metroLink12
			// 
			this.metroLink12.Location = new System.Drawing.Point(92, 115);
			this.metroLink12.Name = "metroLink12";
			this.metroLink12.Size = new System.Drawing.Size(56, 23);
			this.metroLink12.TabIndex = 19;
			this.metroLink12.Text = "dev.dota";
			this.metroLink12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.metroLink12.Theme = MetroFramework.MetroThemeStyle.Dark;
			this.metroLink12.UseSelectable = true;
			this.metroLink12.Click += new System.EventHandler(this.onLink_Click);
			// 
			// metroLink11
			// 
			this.metroLink11.Location = new System.Drawing.Point(11, 115);
			this.metroLink11.Name = "metroLink11";
			this.metroLink11.Size = new System.Drawing.Size(75, 23);
			this.metroLink11.TabIndex = 18;
			this.metroLink11.Text = "SpellLibrary";
			this.metroLink11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.metroLink11.Theme = MetroFramework.MetroThemeStyle.Dark;
			this.metroLink11.UseSelectable = true;
			this.metroLink11.Click += new System.EventHandler(this.onLink_Click);
			// 
			// metroLink10
			// 
			this.metroLink10.Location = new System.Drawing.Point(192, 57);
			this.metroLink10.Name = "metroLink10";
			this.metroLink10.Size = new System.Drawing.Size(34, 23);
			this.metroLink10.TabIndex = 17;
			this.metroLink10.Text = "VPK";
			this.metroLink10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.metroLink10.Theme = MetroFramework.MetroThemeStyle.Dark;
			this.metroLink10.UseSelectable = true;
			this.metroLink10.Click += new System.EventHandler(this.onLink_Click);
			// 
			// metroLink9
			// 
			this.metroLink9.Location = new System.Drawing.Point(78, 86);
			this.metroLink9.Name = "metroLink9";
			this.metroLink9.Size = new System.Drawing.Size(83, 23);
			this.metroLink9.TabIndex = 16;
			this.metroLink9.Text = "Ability Names";
			this.metroLink9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.metroLink9.Theme = MetroFramework.MetroThemeStyle.Dark;
			this.metroLink9.UseSelectable = true;
			this.metroLink9.Click += new System.EventHandler(this.onLink_Click);
			// 
			// metroLink8
			// 
			this.metroLink8.Location = new System.Drawing.Point(65, 57);
			this.metroLink8.Name = "metroLink8";
			this.metroLink8.Size = new System.Drawing.Size(86, 23);
			this.metroLink8.TabIndex = 15;
			this.metroLink8.Text = "Panorama API";
			this.metroLink8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.metroLink8.Theme = MetroFramework.MetroThemeStyle.Dark;
			this.metroLink8.UseSelectable = true;
			this.metroLink8.Click += new System.EventHandler(this.onLink_Click);
			// 
			// metroLink7
			// 
			this.metroLink7.Location = new System.Drawing.Point(11, 57);
			this.metroLink7.Name = "metroLink7";
			this.metroLink7.Size = new System.Drawing.Size(48, 23);
			this.metroLink7.TabIndex = 14;
			this.metroLink7.Text = "Lua API";
			this.metroLink7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.metroLink7.Theme = MetroFramework.MetroThemeStyle.Dark;
			this.metroLink7.UseSelectable = true;
			this.metroLink7.Click += new System.EventHandler(this.onLink_Click);
			// 
			// metroLink6
			// 
			this.metroLink6.Location = new System.Drawing.Point(167, 86);
			this.metroLink6.Name = "metroLink6";
			this.metroLink6.Size = new System.Drawing.Size(83, 23);
			this.metroLink6.TabIndex = 13;
			this.metroLink6.Text = "Lua Modifiers";
			this.metroLink6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.metroLink6.Theme = MetroFramework.MetroThemeStyle.Dark;
			this.metroLink6.UseSelectable = true;
			this.metroLink6.Click += new System.EventHandler(this.onLink_Click);
			// 
			// goBtn
			// 
			this.goBtn.Location = new System.Drawing.Point(230, 6);
			this.goBtn.Name = "goBtn";
			this.goBtn.Size = new System.Drawing.Size(28, 23);
			this.goBtn.TabIndex = 7;
			this.goBtn.Text = "GO";
			this.goBtn.Theme = MetroFramework.MetroThemeStyle.Dark;
			this.goBtn.UseSelectable = true;
			this.goBtn.Click += new System.EventHandler(this.goBtn_Click);
			// 
			// jsRadioButton
			// 
			this.jsRadioButton.AutoSize = true;
			this.jsRadioButton.Location = new System.Drawing.Point(145, 35);
			this.jsRadioButton.Name = "jsRadioButton";
			this.jsRadioButton.Size = new System.Drawing.Size(33, 15);
			this.jsRadioButton.TabIndex = 12;
			this.jsRadioButton.Text = "JS";
			this.jsRadioButton.TextAlign = System.Drawing.ContentAlignment.TopLeft;
			this.jsRadioButton.Theme = MetroFramework.MetroThemeStyle.Dark;
			this.jsRadioButton.UseSelectable = true;
			// 
			// textRadioButton
			// 
			this.textRadioButton.AutoSize = true;
			this.textRadioButton.Location = new System.Drawing.Point(102, 35);
			this.textRadioButton.Name = "textRadioButton";
			this.textRadioButton.Size = new System.Drawing.Size(37, 15);
			this.textRadioButton.TabIndex = 11;
			this.textRadioButton.Text = "KV";
			this.textRadioButton.TextAlign = System.Drawing.ContentAlignment.TopLeft;
			this.textRadioButton.Theme = MetroFramework.MetroThemeStyle.Dark;
			this.textRadioButton.UseSelectable = true;
			// 
			// luaRadioBtn
			// 
			this.luaRadioBtn.AutoSize = true;
			this.luaRadioBtn.Location = new System.Drawing.Point(54, 35);
			this.luaRadioBtn.Name = "luaRadioBtn";
			this.luaRadioBtn.Size = new System.Drawing.Size(42, 15);
			this.luaRadioBtn.TabIndex = 10;
			this.luaRadioBtn.Text = "Lua";
			this.luaRadioBtn.TextAlign = System.Drawing.ContentAlignment.TopLeft;
			this.luaRadioBtn.Theme = MetroFramework.MetroThemeStyle.Dark;
			this.luaRadioBtn.UseSelectable = true;
			// 
			// addonNameLabel
			// 
			this.addonNameLabel.AutoSize = true;
			this.addonNameLabel.FontWeight = MetroFramework.MetroLabelWeight.Regular;
			this.addonNameLabel.Location = new System.Drawing.Point(0, 6);
			this.addonNameLabel.Name = "addonNameLabel";
			this.addonNameLabel.Size = new System.Drawing.Size(56, 19);
			this.addonNameLabel.Style = MetroFramework.MetroColorStyle.Blue;
			this.addonNameLabel.TabIndex = 9;
			this.addonNameLabel.Text = "GitHub:";
			this.addonNameLabel.Theme = MetroFramework.MetroThemeStyle.Dark;
			// 
			// githubTextBox
			// 
			this.githubTextBox.Lines = new string[0];
			this.githubTextBox.Location = new System.Drawing.Point(56, 6);
			this.githubTextBox.MaxLength = 32767;
			this.githubTextBox.Name = "githubTextBox";
			this.githubTextBox.PasswordChar = '\0';
			this.githubTextBox.ScrollBars = System.Windows.Forms.ScrollBars.None;
			this.githubTextBox.SelectedText = "";
			this.githubTextBox.Size = new System.Drawing.Size(171, 23);
			this.githubTextBox.TabIndex = 7;
			this.githubTextBox.Theme = MetroFramework.MetroThemeStyle.Dark;
			this.githubTextBox.UseSelectable = true;
			// 
			// metroLink5
			// 
			this.metroLink5.Location = new System.Drawing.Point(11, 144);
			this.metroLink5.Name = "metroLink5";
			this.metroLink5.Size = new System.Drawing.Size(85, 23);
			this.metroLink5.TabIndex = 6;
			this.metroLink5.Text = "GetDotaStats";
			this.metroLink5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.metroLink5.Theme = MetroFramework.MetroThemeStyle.Dark;
			this.metroLink5.UseSelectable = true;
			this.metroLink5.Click += new System.EventHandler(this.onLink_Click);
			// 
			// metroLink4
			// 
			this.metroLink4.Location = new System.Drawing.Point(157, 57);
			this.metroLink4.Name = "metroLink4";
			this.metroLink4.Size = new System.Drawing.Size(29, 23);
			this.metroLink4.TabIndex = 5;
			this.metroLink4.Text = "IRC";
			this.metroLink4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.metroLink4.Theme = MetroFramework.MetroThemeStyle.Dark;
			this.metroLink4.UseSelectable = true;
			this.metroLink4.Click += new System.EventHandler(this.onLink_Click);
			// 
			// metroLink3
			// 
			this.metroLink3.Location = new System.Drawing.Point(154, 115);
			this.metroLink3.Name = "metroLink3";
			this.metroLink3.Size = new System.Drawing.Size(104, 23);
			this.metroLink3.TabIndex = 4;
			this.metroLink3.Text = "r/Dota2Modding";
			this.metroLink3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.metroLink3.Theme = MetroFramework.MetroThemeStyle.Dark;
			this.metroLink3.UseSelectable = true;
			this.metroLink3.Click += new System.EventHandler(this.onLink_Click);
			// 
			// metroLink2
			// 
			this.metroLink2.Location = new System.Drawing.Point(102, 144);
			this.metroLink2.Name = "metroLink2";
			this.metroLink2.Size = new System.Drawing.Size(64, 23);
			this.metroLink2.TabIndex = 3;
			this.metroLink2.Text = "Workshop";
			this.metroLink2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.metroLink2.Theme = MetroFramework.MetroThemeStyle.Dark;
			this.metroLink2.UseSelectable = true;
			this.metroLink2.Click += new System.EventHandler(this.onLink_Click);
			// 
			// metroLink1
			// 
			this.metroLink1.Location = new System.Drawing.Point(11, 86);
			this.metroLink1.Name = "metroLink1";
			this.metroLink1.Size = new System.Drawing.Size(61, 23);
			this.metroLink1.TabIndex = 2;
			this.metroLink1.Text = "ModDota";
			this.metroLink1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.metroLink1.Theme = MetroFramework.MetroThemeStyle.Dark;
			this.metroLink1.UseSelectable = true;
			this.metroLink1.Click += new System.EventHandler(this.onLink_Click);
			// 
			// versionLabel
			// 
			this.versionLabel.AutoSize = true;
			this.versionLabel.Location = new System.Drawing.Point(402, 277);
			this.versionLabel.Name = "versionLabel";
			this.versionLabel.Size = new System.Drawing.Size(49, 19);
			this.versionLabel.TabIndex = 4;
			this.versionLabel.Text = "v2.x.x.x";
			this.versionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.versionLabel.Theme = MetroFramework.MetroThemeStyle.Dark;
			// 
			// notificationLabel
			// 
			this.notificationLabel.AutoSize = true;
			this.notificationLabel.FontSize = MetroFramework.MetroLabelSize.Tall;
			this.notificationLabel.FontWeight = MetroFramework.MetroLabelWeight.Regular;
			this.notificationLabel.ForeColor = System.Drawing.Color.Maroon;
			this.notificationLabel.Location = new System.Drawing.Point(-4, 273);
			this.notificationLabel.Name = "notificationLabel";
			this.notificationLabel.Size = new System.Drawing.Size(142, 25);
			this.notificationLabel.Style = MetroFramework.MetroColorStyle.Green;
			this.notificationLabel.TabIndex = 5;
			this.notificationLabel.Text = "notificationLabel";
			this.notificationLabel.Theme = MetroFramework.MetroThemeStyle.Dark;
			this.notificationLabel.UseStyleColors = true;
			// 
			// metroRadioButton1
			// 
			this.metroRadioButton1.AutoSize = true;
			this.metroRadioButton1.Location = new System.Drawing.Point(6, 5);
			this.metroRadioButton1.Name = "metroRadioButton1";
			this.metroRadioButton1.Size = new System.Drawing.Size(127, 15);
			this.metroRadioButton1.TabIndex = 6;
			this.metroRadioButton1.Text = "metroRadioButton1";
			this.metroRadioButton1.UseSelectable = true;
			this.metroRadioButton1.Visible = false;
			// 
			// vscriptsTile
			// 
			this.vscriptsTile.ActiveControl = null;
			this.vscriptsTile.Location = new System.Drawing.Point(149, 57);
			this.vscriptsTile.Name = "vscriptsTile";
			this.vscriptsTile.Size = new System.Drawing.Size(29, 29);
			this.vscriptsTile.Style = MetroFramework.MetroColorStyle.Blue;
			this.vscriptsTile.TabIndex = 7;
			this.vscriptsTile.Text = "G";
			this.vscriptsTile.Theme = MetroFramework.MetroThemeStyle.Dark;
			this.metroToolTip1.SetToolTip(this.vscriptsTile, "Open the game directory of this addon.");
			this.vscriptsTile.UseSelectable = true;
			this.vscriptsTile.UseTileImage = true;
			this.vscriptsTile.Click += new System.EventHandler(this.shortcutTile_Click);
			// 
			// contentTile
			// 
			this.contentTile.ActiveControl = null;
			this.contentTile.Location = new System.Drawing.Point(149, 90);
			this.contentTile.Name = "contentTile";
			this.contentTile.Size = new System.Drawing.Size(29, 29);
			this.contentTile.Style = MetroFramework.MetroColorStyle.Orange;
			this.contentTile.TabIndex = 10;
			this.contentTile.Text = "C";
			this.contentTile.Theme = MetroFramework.MetroThemeStyle.Dark;
			this.metroToolTip1.SetToolTip(this.contentTile, "Open the content directory of this addon.");
			this.contentTile.UseSelectable = true;
			this.contentTile.UseTileImage = true;
			this.contentTile.Click += new System.EventHandler(this.shortcutTile_Click);
			// 
			// metroTile3
			// 
			this.metroTile3.ActiveControl = null;
			this.metroTile3.Location = new System.Drawing.Point(9, 197);
			this.metroTile3.Name = "metroTile3";
			this.metroTile3.Size = new System.Drawing.Size(37, 27);
			this.metroTile3.Style = MetroFramework.MetroColorStyle.Teal;
			this.metroTile3.TabIndex = 13;
			this.metroTile3.Text = "S1V";
			this.metroTile3.Theme = MetroFramework.MetroThemeStyle.Dark;
			this.metroToolTip1.SetToolTip(this.metroTile3, "Open the Source 1 VPK (requires GCFScape)");
			this.metroTile3.UseSelectable = true;
			this.metroTile3.UseTileImage = true;
			this.metroTile3.Click += new System.EventHandler(this.shortcutTile_Click);
			// 
			// steamTile
			// 
			this.steamTile.ActiveControl = null;
			this.steamTile.Location = new System.Drawing.Point(149, 123);
			this.steamTile.Name = "steamTile";
			this.steamTile.Size = new System.Drawing.Size(29, 29);
			this.steamTile.Style = MetroFramework.MetroColorStyle.Green;
			this.steamTile.TabIndex = 14;
			this.steamTile.Theme = MetroFramework.MetroThemeStyle.Dark;
			this.metroToolTip1.SetToolTip(this.steamTile, "Open the steam workshop page for this addon.");
			this.steamTile.UseSelectable = true;
			this.steamTile.UseTileImage = true;
			this.steamTile.Click += new System.EventHandler(this.workshopPageBtn_Click);
			// 
			// metroToolTip1
			// 
			this.metroToolTip1.Style = MetroFramework.MetroColorStyle.Blue;
			this.metroToolTip1.StyleManager = null;
			this.metroToolTip1.Theme = MetroFramework.MetroThemeStyle.Light;
			// 
			// metroTile1
			// 
			this.metroTile1.ActiveControl = null;
			this.metroTile1.Location = new System.Drawing.Point(51, 197);
			this.metroTile1.Name = "metroTile1";
			this.metroTile1.Size = new System.Drawing.Size(37, 27);
			this.metroTile1.Style = MetroFramework.MetroColorStyle.Red;
			this.metroTile1.TabIndex = 16;
			this.metroTile1.Text = "S2V";
			this.metroTile1.Theme = MetroFramework.MetroThemeStyle.Dark;
			this.metroToolTip1.SetToolTip(this.metroTile1, "Open the Source 2 VPK (requires GCFScape)");
			this.metroTile1.UseSelectable = true;
			this.metroTile1.UseTileImage = true;
			this.metroTile1.Click += new System.EventHandler(this.shortcutTile_Click);
			// 
			// optionsBtn
			// 
			this.optionsBtn.ActiveControl = null;
			this.optionsBtn.Location = new System.Drawing.Point(92, 197);
			this.optionsBtn.Name = "optionsBtn";
			this.optionsBtn.Size = new System.Drawing.Size(29, 27);
			this.optionsBtn.Style = MetroFramework.MetroColorStyle.Yellow;
			this.optionsBtn.TabIndex = 18;
			this.optionsBtn.Text = "O";
			this.optionsBtn.Theme = MetroFramework.MetroThemeStyle.Dark;
			this.metroToolTip1.SetToolTip(this.optionsBtn, "Open Options page");
			this.optionsBtn.UseSelectable = true;
			this.optionsBtn.UseTileImage = true;
			this.optionsBtn.Click += new System.EventHandler(this.optionsBtn_Click);
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = global::Dota2ModKit.Properties.Resources.d2modkit;
			this.pictureBox1.Location = new System.Drawing.Point(178, 15);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(32, 32);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pictureBox1.TabIndex = 7;
			this.pictureBox1.TabStop = false;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(450, 295);
			this.Controls.Add(this.pictureBox1);
			this.Controls.Add(this.optionsBtn);
			this.Controls.Add(this.metroTile1);
			this.Controls.Add(this.steamTile);
			this.Controls.Add(this.metroTile3);
			this.Controls.Add(this.contentTile);
			this.Controls.Add(this.vscriptsTile);
			this.Controls.Add(this.versionLabel);
			this.Controls.Add(this.metroRadioButton1);
			this.Controls.Add(this.notificationLabel);
			this.Controls.Add(this.tabControl);
			this.Controls.Add(this.addonTile);
			this.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Margin = new System.Windows.Forms.Padding(4);
			this.MaximizeBox = false;
			this.Name = "MainForm";
			this.Padding = new System.Windows.Forms.Padding(27, 92, 27, 31);
			this.Resizable = false;
			this.ShadowType = MetroFramework.Forms.MetroFormShadowType.DropShadow;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.Text = "Dota 2 ModKit";
			this.Theme = MetroFramework.MetroThemeStyle.Dark;
			this.Load += new System.EventHandler(this.Form1_Load);
			this.metroContextMenu1.ResumeLayout(false);
			this.tabControl.ResumeLayout(false);
			this.toolsTab.ResumeLayout(false);
			this.helpTab.ResumeLayout(false);
			this.helpTab.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroButton generateAddonLangsBtn;
        private MetroFramework.Controls.MetroTile addonTile;
		private MetroFramework.Controls.MetroTabControl tabControl;
		private MetroFramework.Controls.MetroTabPage toolsTab;
		private MetroFramework.Controls.MetroButton particleDesignBtn;
		private MetroFramework.Controls.MetroButton combineKVBtn;
		private MetroFramework.Controls.MetroLabel versionLabel;
		private MetroFramework.Controls.MetroLabel notificationLabel;
		private MetroFramework.Controls.MetroRadioButton metroRadioButton1;
		private MetroFramework.Controls.MetroTabPage helpTab;
		private MetroFramework.Controls.MetroLink metroLink5;
		private MetroFramework.Controls.MetroLink metroLink4;
		private MetroFramework.Controls.MetroLink metroLink3;
		private MetroFramework.Controls.MetroLink metroLink2;
		private MetroFramework.Controls.MetroLink metroLink1;
		private MetroFramework.Controls.MetroTextBox githubTextBox;
		private MetroFramework.Controls.MetroLabel addonNameLabel;
		private MetroFramework.Controls.MetroRadioButton textRadioButton;
		private MetroFramework.Controls.MetroRadioButton luaRadioBtn;
		private MetroFramework.Controls.MetroRadioButton jsRadioButton;
		private MetroFramework.Controls.MetroButton goBtn;
		private MetroFramework.Controls.MetroLink metroLink8;
		private MetroFramework.Controls.MetroLink metroLink7;
		private MetroFramework.Controls.MetroLink metroLink6;
		private MetroFramework.Controls.MetroLink metroLink9;
		private MetroFramework.Controls.MetroLink metroLink10;
		private MetroFramework.Controls.MetroLink metroLink11;
		private MetroFramework.Controls.MetroTile vscriptsTile;
		private MetroFramework.Controls.MetroTile contentTile;
		private MetroFramework.Controls.MetroTile metroTile3;
		private MetroFramework.Controls.MetroTile steamTile;
		private MetroFramework.Controls.MetroLink metroLink12;
		private MetroFramework.Components.MetroToolTip metroToolTip1;
		private MetroFramework.Controls.MetroContextMenu metroContextMenu1;
		private System.Windows.Forms.ToolStripMenuItem changePictureToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem deleteAddonToolStripMenuItem;
		private MetroFramework.Controls.MetroTile metroTile1;
		private MetroFramework.Controls.MetroButton decompileVtexButton;
		private MetroFramework.Controls.MetroButton compileVtexButton;
		private MetroFramework.Controls.MetroTile optionsBtn;
		private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
		private MetroFramework.Controls.MetroButton findSoundNameBtn;
		private System.Windows.Forms.PictureBox pictureBox1;
	}
}


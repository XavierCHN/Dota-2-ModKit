using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using KVLib;
using System.Drawing;
using System.IO;

namespace D2ModKit.KVEditor
{
    public partial class SingleKVItemView : UserControl
    {
        private string currentAddon;

        public SingleKVItemView()
        {
            InitializeComponent();
        }

        private void getImage(KeyValue kv, KVType type,out PictureBox pic)
        {
            int width = 64;
            int height = 64;
            pic = new PictureBox();
            pic.Width = width;
            pic.Height = height;

            pic.ErrorImage = Image.FromFile(Application.StartupPath + "\\error.png");

            string typePath = "";
            string fileName = "";
            if (type == KVType.Ability || type == KVType.Item)
            {
                typePath = "spellicons";
                foreach (KeyValue _kv in kv.Children)
                    if (_kv.Key == "AbilityTextureName")
                    {
                        fileName = _kv.GetString();
                        if (type == KVType.Item)
                        {
                            height = 32;
                            width = 62;
                            typePath = "items";
                            if (fileName.StartsWith("item_"))
                                fileName = fileName.Remove(0, 5);// remove "item_" from start pos    
                        }
                    }
            }
            if (type == KVType.Hero)
            {
                height = 36;
                typePath = "heroes";
                foreach (KeyValue _kv in kv.Children)
                    if (_kv.Key == "override_hero")
                    {
                        fileName = _kv.GetString();
                        if (type == KVType.Item)
                            if (fileName.StartsWith("item_"))
                                fileName = fileName.Remove(0, 5);// remove "item_" from start pos    
                    }
            }
            if (type == KVType.Unit)
                typePath = "invalid_thisisaverylongspecfiedcodethatnobodywillnamehisfolderlikethishahahahamysteamidis86815341xDcanyourememberthis";

            string imageFilePath = Path.Combine(Properties.Settings.Default.UGCPath, "game", currentAddon, "resources", "flash3", "images", typePath, fileName + ".png");
            if (File.Exists(imageFilePath))
                pic.Image = Image.FromFile(imageFilePath);
            else
                if (IsVPKImage(fileName, type))
                    pic.Image = Image.FromFile(Application.StartupPath + "\\vpk.png");
        }

        private bool IsVPKImage(string name, KVType type)
        {
            string built_in_file_name = Application.StartupPath + "Resources\\";
            if (type == KVType.Ability) built_in_file_name += "Built_In_Engiene_Ability_Names.txt";
            if (type == KVType.Unit) built_in_file_name += "Built_In_Engiene_Unit_Names.txt";
            if (type == KVType.Hero) built_in_file_name += "Built_In_Engiene_Hero_Names.txt";
            if (type == KVType.Item) built_in_file_name += "Built_In_Engiene_Item_Names.txt";
            
            string[] sts = File.ReadAllLines(built_in_file_name);

            for(int i = 0; i < sts.Length;i++)
                if (name == sts[i].Trim())
                    return true;
            return false;
        }

        public SingleKVItemView(KeyValue kv, KVType type)
        {
            currentAddon = Properties.Settings.Default.CurrAddon;
            PictureBox pic = new PictureBox();
            getImage(kv,type,out pic);
            pic.Left = 5;
            pic.Top = 3;
            this.Controls.Add(pic);
        }
        private void SingleKVItemView_Load(object sender, EventArgs e)
        {

        }
    }
}

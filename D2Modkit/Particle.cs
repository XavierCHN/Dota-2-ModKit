using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D2ModKit
{
    public class Particle
    {
        private string[] particleArr;

        public string[] ParticleArr
        {
            get { return particleArr; }
            set { particleArr = value; }
        }

        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private string path;

        public string Path
        {
            get { return path; }
            set 
            {
                path = value;
                name = path.Substring(path.LastIndexOf('\\') + 1);
            }
        }

        public Particle(string path)
        {
            Path = path;
            ParticleArr = System.IO.File.ReadAllLines(path);
        }

        public bool copyToFolder(string folderName)
        {
            try
            {
                System.IO.File.Copy(Path, folderName);
            }
            catch (IOException overwriteException)
            {
                return false;
            }
            Path = folderName + name;
            return true;
        }

        public bool fixChildRefs(string newFolder)
        {
            bool fix = false;
            for (int j = 0; j < ParticleArr.Length; j++)
            {
                string l = ParticleArr[j];
                if (l.Contains("string m_ChildRef = "))
                {
                    fix = true;
                    string relativeFolderPath = getRelativeFolderPath(newFolder);
                    string child = l.Substring(l.LastIndexOf('/') + 1);
                    string newRef = "string m_ChildRef = \"" + relativeFolderPath + child + "\n";
                    ParticleArr[j] = newRef;
                }
            }
            if (fix)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool fixChildRefs(string newFolder, string oldbase, string newbase)
        {
            bool fix = false;
            for (int j = 0; j < ParticleArr.Length; j++)
            {
                string l = ParticleArr[j];
                if (l.Contains("string m_ChildRef = "))
                {
                    if (l.Contains(oldbase))
                    {
                        l = l.Replace(oldbase, newbase);
                    }
                    fix = true;
                    string relativeFolderPath = getRelativeFolderPath(newFolder);
                    string child = l.Substring(l.LastIndexOf('/') + 1);
                    string newRef = "string m_ChildRef = \"" + relativeFolderPath + child + "\n";
                    ParticleArr[j] = newRef;
                }
            }
            if (fix)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public string getRelativeFolderPath(string newFolder)
        {
            string[] pathArr = newFolder.Split('\\');
            string relFolderPath = "";
            bool start = false;
            for (int k = 0; k < pathArr.Length; k++)
            {
                if (pathArr[k] == "particles")
                {
                    start = true;
                }

                if (start)
                {
                    relFolderPath += pathArr[k] + "/";
                }
            }
            return relFolderPath;
        }

        public void changeColor(string[] rgb)
        {
            for (int i = 0; i < ParticleArr.Count(); i++)
            {
                string l = ParticleArr[i];
                if (l.Contains("ColorMin") || l.Contains("ColorMax") || l.Contains("ConstantColor") || l.Contains("TintMin")
                    || l.Contains("TintMax"))
                {
                    string part1 = l.Substring(0, l.LastIndexOf('=') + 2);
                    string part2 = l.Substring(l.LastIndexOf('=') + 2);
                    part2 = part2.Replace("(", "");
                    part2 = part2.Replace(")", "");
                    char[] dels = {',',' '};
                    string[] nums = part2.Split(dels);
                    string lastNum = nums[3];
                    string newPart2 = "(" + " " + rgb[0] + ", " + rgb[1] + ", " + rgb[2] + ", " + lastNum + " )";
                    ParticleArr[i] = part1 + newPart2;
                }
            }
        }
        public override string ToString()
        {
            string str = "";
            for (int i = 0; i < ParticleArr.Count(); i++)
            {
                str += ParticleArr.ElementAt(i) + "\n";
            }
            return str;
        }

    }
}

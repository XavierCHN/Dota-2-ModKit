using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

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

        private string relativeFolderPath;

        public string RelativeFolderPath
        {
            get { return relativeFolderPath; }
            set { relativeFolderPath = value; }
        }

        public Particle(string path)
        {
            Path = path;
            ParticleArr = File.ReadAllLines(path);
        }

        public bool copyToFolder(string folderName)
        {
            try
            {
                File.Copy(Path, folderName);
            }
                // overwrite exception.
            catch (IOException)
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
                    relativeFolderPath = getRelativeFolderPath(newFolder);
                    string child = l.Substring(l.LastIndexOf('/') + 1);
                    string newRef = "string m_ChildRef = \"" + relativeFolderPath + child + "\n";
                    ParticleArr[j] = newRef;
                }
            }
            if (fix)
            {
                return true;
            }
            return false;
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
                    relativeFolderPath = getRelativeFolderPath(newFolder);
                    string child = l.Substring(l.LastIndexOf('/') + 1);
                    string newRef = "string m_ChildRef = \"" + relativeFolderPath + child + "\n";
                    ParticleArr[j] = newRef;
                }
            }
            if (fix)
            {
                return true;
            }
            return false;
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

        public string getRelativePath()
        {
            //string newFolder = Path.Substring(Path.LastIndexOf('\\') + 1);
            string[] pathArr = path.Split('\\');
            string relFolderPath = "";
            bool start = false;
            // skip the last element because it's always a /
            for (int k = 0; k < pathArr.Length; k++)
            {

                if (pathArr[k] == "particles")
                {
                    start = true;
                }

                if (start)
                {
                    relFolderPath += pathArr[k];
                    if (k != pathArr.Length - 1)
                    {
                        relFolderPath += "/";
                    }
                }
            }
            return relFolderPath;
        }

        public void changeColor(string[] rgb)
        {
            for (int i = 0; i < ParticleArr.Count(); i++)
            {
                string l = ParticleArr[i];
                if (l.Contains("ColorMin") || l.Contains("ColorMax") || l.Contains("ConstantColor") ||
                    l.Contains("ColorScale") ||
                    l.Contains("ColorFade") ||
                    l.Contains("TintMin") ||
                    l.Contains("TintMax")
                    )
                {
                    string part1 = l.Substring(0, l.LastIndexOf('=') + 2);
                    string part2 = l.Substring(l.LastIndexOf('=') + 2);
                    part2 = part2.Replace("(", "");
                    part2 = part2.Replace(")", "");
                    char[] dels = {',', ' '};
                    string[] nums = part2.Split(dels);
                    string lastNum = nums[3];
                    string newPart2 = "(" + " " + rgb[0] + ", " + rgb[1] + ", " + rgb[2] + ", " + lastNum + " )";
                    ParticleArr[i] = part1 + newPart2;
                }
            }
        }

        public void resize(int percentage)
        {
            for (int i = 0; i < ParticleArr.Count(); i++)
            {
                string l = ParticleArr[i];
                if (l.Contains("Radius") && l.Contains("="))
                {
                    string part1 = l.Substring(0, l.LastIndexOf('=') + 2);
                    string part2 = l.Substring(l.LastIndexOf('=') + 2);

                    double d = Double.Parse(part2);
                    if (Double.IsNaN(d))
                    {
                    }
                    else
                    {
                        // modify floats differently than ints.
                        if (l.Contains("m_f"))
                        {
                            double newVal = d + (percentage/100.0)*Math.Abs(d);
                            ParticleArr[i] = part1 + newVal;
                        }
                        else if (l.Contains("m_n"))
                        {
                            int _d = Convert.ToInt32(d);
                            int newVal = _d + (percentage/100)*Math.Abs(_d);
                            ParticleArr[i] = part1 + newVal;
                        }
                    }
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

        public void diff(Particle p2)
        {
            string[] arr2 = p2.ParticleArr;
            Debug.WriteLine("Did not find these in " + Name);
            for (int j = 0; j < arr2.Count(); j++)
            {
                string line = arr2[j];
                line = line.Trim();

                // check if this line is just blank. Also ignore child reference lines.
                if (line == "" || line.Contains("string m_ChildRef = "))
                {
                    continue;
                }

                if (!containsTrimmed(line))
                {
                    Debug.WriteLine(line);
                }
            }
        }

        private bool containsTrimmed(string line)
        {
            for (int i = 0; i < ParticleArr.Count(); i++)
            {
                string thisLine = ParticleArr[i];
                if (thisLine.Trim() == line)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
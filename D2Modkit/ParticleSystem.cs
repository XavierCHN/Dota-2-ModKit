using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace D2ModKit
{
    public class ParticleSystem
    {
        private Particle[] particles;

        public Particle[] Particles
        {
            get { return particles; }
            set { particles = value; }
        }

        private string[] paths;

        public string[] Paths
        {
            get
            {
                string[] pa = new string[Particles.Count()];
                for (int i = 0; i < Particles.Count(); i++)
                {
                    pa[i] = Particles.ElementAt(i).Path;
                }
                return pa;
            }
            set { paths = value; }
        }

        private string baseName;

        public string BaseName
        {
            get { return baseName; }
            set { baseName = value; }
        }

        public ParticleSystem(Particle[] particles)
        {
            Particles = particles;
        }

        public ParticleSystem(List<Particle> particles)
        {
            Particle[] ps = new Particle[particles.Count()];
            string[] paths = new string[particles.Count];
            for (int i = 0; i < particles.Count(); i++)
            {
                ps[i] = particles.ElementAt(i);
                paths[i] = particles.ElementAt(i).Path;
            }
            Particles = ps;
            Paths = paths;
        }

        public ParticleSystem(string[] paths)
        {
            Particle[] particles = new Particle[paths.Count()];
            for (int i = 0; i < paths.Count(); i++)
            {
                particles[i] = new Particle(paths.ElementAt(i));
            }
            Particles = particles;
            Paths = paths;
        }

        public void fixChildRefs(string newFolder)
        {
            for (int i = 0; i < Particles.Count(); i++)
            {
                Particle p = Particles.ElementAt(i);
                p.fixChildRefs(newFolder);
            }
        }

        public void changeColor(string[] rgb)
        {
            for (int i = 0; i < Particles.Count(); i++)
            {
                Particle p = Particles.ElementAt(i);
                p.changeColor(rgb);
            }
        }

        public void resize(int percentage)
        {
            for (int i = 0; i < Particles.Count(); i++)
            {
                Particle p = Particles.ElementAt(i);
                p.resize(percentage);
            }
        }


        public void rename(string newBase)
        {
            // Gets the current old base.
            int ptr = 0;
            string reference = Particles[0].Name;
            string oldBase = "";
            bool found = false;
            while (!found)
            {
                for (int i = 0; i < Particles.Length; i++)
                {
                    string currStr = Particles[i].Name;
                    if (ptr >= currStr.Length || currStr.ElementAt(ptr) != reference[ptr])
                    {
                        found = true;
                        break;
                    }
                    oldBase += reference[ptr];
                    ptr++;
                }
            }

            // ensure we don't include stuff after the period. (.vpcf)
            oldBase = oldBase.Substring(0, oldBase.LastIndexOf('.'));
            Debug.WriteLine("Base: " + oldBase);

            for (int i = 0; i < Particles.Count(); i++)
            {
                string p = Particles.ElementAt(i).Path;
                string currName = p.Substring(p.LastIndexOf('\\') + 1);
                string newName = currName.Replace(oldBase, newBase);
                string newPath = Path.Combine(p.Substring(0, p.LastIndexOf('\\')), newName);
                File.Move(p, newPath);
                Particles[i].Path = newPath;
                Particles[i].fixChildRefs(newPath.Substring(0, newPath.LastIndexOf('\\')), oldBase, newBase);
            }
        }
    }
}
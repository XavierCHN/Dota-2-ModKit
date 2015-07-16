using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VPKExtract;

namespace VPKExtract {
	public static class VPKUtil {

		public static Stream GetInputStream(string vpkDirFileName, VpkNode node) {
			if (node.EntryLength == 0 && node.PreloadBytes > 0) {
				return new MemoryStream(node.PreloadData);
			} else if (node.PreloadBytes == 0) {
				var prefix = new string(Enumerable.Repeat('0', 3 - node.ArchiveIndex.ToString().Length).ToArray());
				var dataPakFilename = vpkDirFileName.Replace("_dir.vpk", "_" + prefix + node.ArchiveIndex + ".vpk");

				var fsin = new FileStream(dataPakFilename, FileMode.Open);
				fsin.Seek(node.EntryOffset, SeekOrigin.Begin);
				return fsin;
			} else {
				throw new NotSupportedException("Unable to get entry data: Both EntryLength and PreloadBytes specified.");
			}
		}

	}
}

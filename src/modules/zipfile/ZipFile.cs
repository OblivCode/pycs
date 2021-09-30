using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static pycs.pycs;

namespace pycs.modules.zipfile
{
    public class ZipFile
    {
        private ZipArchive archive;
        /// <exception cref="OSError" />
        /// <exception cref="FileNotFoundError" />
        /// <exception cref="BadZipFile" />
        public ZipFile(string filename, char mode = 'w')
        {
            if (!os.path.exists(filename))
                throw new FileNotFoundError($"Zip file: '{filename}' not found.");
            else if (Path.GetExtension(filename).ToLower() != ".zip")
                throw new OSError($"Invalid file type: {Path.GetExtension(filename)}.");
            ZipArchiveMode archive_mode;
            switch (mode)
            {
                case 'r':
                    archive_mode = ZipArchiveMode.Read;
                    break;
                case 'w':
                    archive_mode = ZipArchiveMode.Update;
                    break;
                default:
                    archive_mode = ZipArchiveMode.Update;
                    break;
            }

            try { archive = System.IO.Compression.ZipFile.Open(filename, archive_mode); }
            catch (Exception e) { throw new BadZipFile(e.Message, e); }
        }

        public int CRC { get { return (int)archive.Entries[0].Crc32; } }
        public long compress_size
        {
            get
            {
                long size = 0;
                foreach (var entry in archive.Entries)
                    size += entry.CompressedLength;
                return size;
            }
        }
        public long file_size
        {
            get
            {
                long size = 0;
                foreach (var entry in archive.Entries)
                    size += entry.Length;
                return size;
            }
        }

        public string[] namelist() => archive.Entries.Select(x => x.Name).ToArray();
        public void printdir()
        {
            foreach (var entry in archive.Entries)
                print(entry.FullName);
        }

        /// <exception cref="OSError" />
        public FileStream extract(string entry)
        {
            var archive_entry = archive.GetEntry(entry);
            string filename = os.getcwd() + "/" + archive_entry.FullName;
            try { archive_entry.ExtractToFile(filename); }
            catch (Exception e) { throw new OSError(e.Message, e); }
            return File.Open(filename, FileMode.Open, FileAccess.ReadWrite);
        }

        /// <exception cref="OSError" />
        public DirectoryInfo extractall()
        {
            string dir = os.getcwd();
            try { archive.ExtractToDirectory(dir); }
            catch (Exception e) { throw new OSError(e.Message, e); }
            return new DirectoryInfo(dir);
        }

        public ZipInfo getinfo(string entry)
        {
            var archive_entry = archive.GetEntry(entry);
            return new(archive_entry.FullName, not(Path.HasExtension(archive_entry.FullName)), archive_entry.LastWriteTime.DateTime);
        }

        public ZipInfo[] infolist()
        {
            ZipInfo[] arr = new ZipInfo[archive.Entries.Count];
            foreach (var entry in archive.Entries)
                arr.Append(getinfo(entry.FullName));
            return arr;
        }
        /// <exception cref="FileNotFoundError" />
        public void write(string filename)
        {
            if (!os.path.exists(filename))
                throw new FileNotFoundError($"File: '{filename}' does not exist.");
            archive.CreateEntryFromFile(filename, Path.GetFileName(filename));
        }

        /// <exception cref="BufferError" />
        public ZipFile writestr(string name, string data)
        {
            try
            {
                using (Stream stream = archive.CreateEntry(name).Open())
                {

                    stream.Write(bytearray(data), 0, data.Length);
                    //stream.Close();
                    stream.Dispose();

                }
            }
            catch (Exception e) { throw new BufferError(e.Message, e); }
            return this;
        }

        /// <exception cref="BufferError" />
        public async Task<ZipFile> writestr_async(string name, string data)
        {
            try
            {
                using (Stream stream = archive.CreateEntry(name).Open())
                {

                    await stream.WriteAsync(bytearray(data), 0, data.Length);
                    //stream.Close();
                    await stream.DisposeAsync();

                }
            }
            catch (Exception e) { throw new BufferError(e.Message, e); }
            return this;
        }
        public void close() => archive.Dispose();
    }

    public class ZipInfo
    {
        private bool dir;
        public string filename;
        public (string, string, string, string, string, string) date_time;
        private string name { get { return Path.GetFileName(filename); } }

        public ZipInfo(string name, bool dir)
        {
            DateTime LM;
            if (!os.path.isdir(name))
            {
                LM = File.GetLastWriteTime(name);
            }
            else
                LM = Directory.GetLastWriteTime(name);
            date_time = (LM.ToString("yyyy"), LM.ToString("M"), LM.ToString("d"), LM.ToString("H"), LM.ToString("m"), LM.ToString("s"));

            this.filename = name;
            this.dir = dir;
        }

        public ZipInfo(string name, bool dir, DateTime LM)
        {
            date_time = (LM.ToString("yyyy"), LM.ToString("M"), LM.ToString("d"), LM.ToString("H"), LM.ToString("m"), LM.ToString("s"));

            this.filename = name;
            this.dir = dir;
        }
        public static ZipInfo from_file(string filename)
        {
            return new ZipInfo(filename, os.path.isdir(filename));
        }

        public bool is_dir() => dir;
    }
}

using DiscUtils.Vhd;
using DiscUtils;
using DiscUtils.Partitions;
using DiscUtils.Ntfs;
using DiscUtils.Fat;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PManager
{
    public class HDManager
    {
        /// <summary>
        /// This method creates a .vhd hard drive formatted in FAT.
        /// </summary>
        /// <param name="diskSize">The size of the disk in bytes.</param>
        /// <param name="location">Defines the physical location and file name. .vhd extension required.</param>
        /// <param name="dynamic">Determines whether or not the disk will be of fixed size or dynamic size.</param>
        /// <returns>A bool determining if the method succeeded.</returns>
        public bool CreateFatDrive(long diskSize, string location, bool dynamic)
        {
            try {
                using (Stream vhdStream = File.Create(@location))
                {
                    Disk disk = null;

                    if (dynamic)
                    {
                        disk = Disk.InitializeDynamic(vhdStream, Ownership.None, diskSize);
                    }
                    else
                    {
                        disk = Disk.InitializeFixed(vhdStream, Ownership.None, diskSize);
                    }

                    BiosPartitionTable.Initialize(disk, WellKnownPartitionType.WindowsFat);
                    FatFileSystem.FormatPartition(disk, 0, null);

                    return true;
                }
            } 
            catch (IOException e) 
            {
                Console.WriteLine(e.StackTrace);
                return false;
            }
        }

        /// <summary>
        /// This method creates a .vhd hard drive formatted in NTFS.
        /// </summary>
        /// <param name="diskSize">The size of the disk in bytes.</param>
        /// <param name="location">Defines the physical location and file name. .vhd extension required.</param>
        /// <param name="label">The label to be given to the disk.</param>
        /// <param name="dynamic">Determines whether or not the disk will be of fixed size or dynamic size.</param>
        /// <returns>A bool determining if the method succeeded.</returns>
        public bool CreateNtfsDrive(long diskSize, string location, string label, bool dynamic)
        {
            try
            {
                using(Stream vhdStream = File.Create(@location))
                {
                    Disk disk = null;

                    if(dynamic)
                    {
                        disk = Disk.InitializeDynamic(vhdStream, Ownership.None, diskSize);
                    }
                    else
                    {
                        disk = Disk.InitializeFixed(vhdStream, Ownership.None, diskSize);
                    }

                    BiosPartitionTable.Initialize(disk, WellKnownPartitionType.WindowsNtfs);
                    NtfsFileSystem.Format(new VolumeManager(disk).GetLogicalVolumes()[0], label);

                    return true;
                }
            }
            catch (IOException e)
            {
                Console.WriteLine(e.StackTrace);
                return false;
            }
        }
    }
}

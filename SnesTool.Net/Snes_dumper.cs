using System;
using System.ComponentModel;
using System.Text;

namespace SnesTool.Net
{
    public enum BankTypeEnum { Lo, Hi };
    public class SnesRomDump
    {
        /// <summary>
        /// Rom đã thêm Header hay chưa?
        /// </summary>
        public bool SmcHeader512 { get; private set; }

        // Indica la localización del header de SNES
        int HeaderLocation;

        // Array con los datos de la ROM
        public byte[] Data;

        public byte[] Data_NoHeader;

        /// <summary>
        /// Tên bên trong rom.
        /// </summary>
        public string RomName { get; private set; }
        /// <summary>
        /// The bitmask to use is 001A0BCD, the basic value is $20:
        /// <para>- A == 0 means SlowROM (+ $0), A == 1 means FastROM (+ $10).</para>
        /// <para>- B == 1 means ExHiROM (+ $4)</para>
        /// <para>- C == 1 means ExLoROM (+ $2)</para>
        /// <para>- D == 0 means LoROM (+ $0), D == 1 means HiROM (+ $1), is used with B and C in case of extended ROMs.</para>
        /// <para>Keep in mind that some people sometimes use "Mode 20" to refer to the LoROM mapping model, and "Mode 21" to refer to HiROM, although it's technically wrong. As the table shows, there are two LoROM and two HiROM mappings which have a different markup byte than the name suggests.</para>
        /// </summary>
        public byte Layout { get; private set; }
        /// <summary>
        /// <para></para>
        /// ROM Type (also called ROM Type). 
        ///It is a byte that determines if the ROM uses RAM, SRAM or Enhancement Chips. We have to evaluate this byte in hexadecimal format and according to its value, we know what type of ROM it is in terms of the use of RAM, SRAM or chip.
        ///    For cases in which no Enhancement Chip is used we can have the following hexadecimal values:
        ///0x00 : ROM only
        ///0x01 : ROM + RAM
        ///0x02 : ROM + RAM + SRAM
        ///    For cases where the ROM has an Enhancement Chip , we must look at both characters of the hexadecimal value. The first character indicates the type of Enhancement Chip used by the Super Nintendo cartridge, the different possibilities being the following:
        ///0x0* : DSP
        ///0x1* : SuperFX
        ///0x2* : OBC1
        ///0x3* : SA-1
        ///0x4* : S-DD1
        ///0xE* : Other
        ///0xF* : Custom Chip
        ///The second character tells us whether to use ROM, RAM or SRAM. The possibilities in this case are:
        ///0x*0 : ROM
        ///0x*1 : ROM + RAM
        ///0x*2 : ROM + RAM + SRAM
        ///0x*3 : ROM + Enhancement Chip
        ///0x*4 : ROM + Enhancement Chip + RAM
        ///0x*5 : ROM + Enhancement Chip + RAM + SRAM
        ///0x*6 : ROM + Enhancement Chip + SRAM
        ///    For example if the type of ROM is 0x14 , we would be facing: ROM + Enhancement Chip (SuperFX) + RAM.
        ///    Another example for 0x42 , we would be facing: ROM + Enhancement Chip (S-DD1) + RAM + SRAM.
        /// </summary>
        [Description("")]
        public byte CartridgeType { get; private set; }
        public byte RomSize { get; private set; }
        public string RomSizeDescription {
            get
            {
                string result = "(" + RomSize + ") = ";
                if (RomSize == 8) result += "2 Mbit";
                if (RomSize == 9) result += "4 Mbit";
                if (RomSize == 10) result += "8 Mbit";
                if (RomSize == 11) result += "16 Mbit";
                if (RomSize == 12) result += "32 Mbit";
                if (RomSize == 13) result += "48 Mbit";
                return result + " !!!";
            }
        }
        public byte RamSize { get; private set; }
        public string RamSizeDescription
        {
            get
            {
                string result = "(" + RamSize + ") = ";
                if (RamSize == 0) result += "0 Kbit";
                if (RamSize == 1) result += "16 Kbit = 2Kbyte";
                if (RamSize == 2) result += "32 Kbit = 4Kbyte";
                if (RamSize == 3) result += "64 Kbit = 8Kbyte";
                if (RamSize == 4) result += "128 Kbit = 16k";
                if (RamSize == 5) result += "256 Kbit = 32k";
                return result;
            }
        }

        public byte CountryCode { get; private set; }

        public string CountryCode_
        {
            get
            {
                switch (CountryCode)
                {
                    case 0x00:
                        return "Japan NTSC";
                    case 0x01:
                        return "North America NTSC";
                    case 0x02:
                        return "Europe PAL";
                    case 0x03:
                        return "Sweden/Scandinavia PAL";
                    case 0x04:
                        return "Finland PAL";
                    case 0x05:
                        return "Denmark	PAL";
                    case 0x06:
                        return "France SECAM (PAL-like, 50 Hz)";
                    case 0x07:
                        return "Netherlands PAL";
                    case 0x08:
                        return "Spain PAL";
                    case 0x09:
                        return "Germany PAL";
                    case 0x0A:
                        return "Italy PAL";
                    case 0x0B:
                        return "China PAL";
                    case 0x0C:
                        return "Indonesia PAL";
                    case 0x0D:
                        return "Korea NTSC";
                    case 0x0E:
                        return "Global (?)";
                    case 0x0F:
                        return "Canada NTSC";
                    case 0x10:
                        return "Brazil PAL-M (NTSC-like, 60 Hz)";
                    case 0x11:
                        return "Australia PAL";
                    case 0x12:
                        return "Other (1) ?";
                    case 0x13:
                        return "Other (2) ?";
                    case 0x14:
                        return "Other (3) ?";
                }

                return "?";
            }
        }

        public byte LicenseCode { get; private set; }
        public byte VersionNumber { get; private set; }
        ushort Checksum { get; set; }
        ushort ChecksumCompliment { get; set; }
        /// <summary>
        /// Lo or Hi
        /// </summary>
        public BankTypeEnum BankType { get; private set; }

        public decimal Size_Megabyte
        {
            get
            {
                decimal size = 0;
                size = (decimal)Data.Length / 0x100000;
                return size;
            }
        }

        // Chức năng này cho phép phân tích SNES ROMS với phần mở rộng SMC và SFC
        public SnesRomDump(byte[] rom)
        {
            this.Data = rom;
            // kiểm tra xem tiêu đề smc có tồn tại không
            if (this.Data.Length % 1024 == 512)
                SmcHeader512 = true;
            else// if (this.Data.Length % 1024 == 0)
                SmcHeader512 = false;
            //else
            //    throw new Exception("Rom không hợp lệ?.");

            if (SmcHeader512)
            {
                Data_NoHeader = new byte[Data.Length - 512];
                Buffer.BlockCopy(Data, 512, Data_NoHeader, 0, Data_NoHeader.Length);
            }
            else
            {
                Data_NoHeader = new byte[Data.Length];
                Buffer.BlockCopy(Data, 0, Data_NoHeader, 0, Data_NoHeader.Length);
            }

            this.HeaderLocation = 0x81C0;

            if (HeaderIsAt(0x07FC0)) // Rom là LoROM
            {
                this.BankType = BankTypeEnum.Lo;
            }
            else if (HeaderIsAt(0x0FFC0))
            {
                this.BankType = BankTypeEnum.Hi;
            }

            // Leemos el Header
            ReadHeader();

        }
        // Chức năng kiểm tra xem tiêu đề có đúng hướng không
        private bool HeaderIsAt(ushort addr)
        {
            this.HeaderLocation = addr;
            return VerifyChecksum();
        }

        // Offset 0x07FC0 in a headerless LoROM image (LoROM rom sin smc header)
        // Offset 0x0FFC0 in a headerless HiROM image (HiROM rom sin smc header)
        // verifica el checksum
        private bool VerifyChecksum()
        {
            // La rom tiene header smc
            if (SmcHeader512)
                this.HeaderLocation += 512;

            this.ChecksumCompliment = BitConverter.ToUInt16(this.Get(0x1C, 0x1D), 0);
            this.Checksum = BitConverter.ToUInt16(this.Get(0x1E, 0x1F), 0);
            ushort ver = (ushort)(this.Checksum ^ this.ChecksumCompliment);
            return (ver == 0xFFFF);
        }

        private void ReadHeader()
        {
            this.RomName = Encoding.ASCII.GetString(this.Get(0x00, 0x14)); // 21 chars
            this.Layout = this.At(0x15);
            this.CartridgeType = this.At(0x16);
            this.RomSize = this.At(0x17);
            this.RamSize = this.At(0x18);
            this.CountryCode = this.At(0x19);
            this.LicenseCode = this.At(0x1A);
            this.VersionNumber = this.At(0x1B);
        }

        private string GetROmB()
        {
            return String.Format("{0}", this.RomSize);
        }
        private byte[] Get(int from, int to)
        {
            byte[] result = new byte[to - from + 1];
            Buffer.BlockCopy(Data, this.HeaderLocation + from, result, 0, result.Length);
            return result;
            //return this.Data.Skip(this.HeaderLocation + from).Take(to - from + 1).ToArray();
        }

        private byte At(int addr)
        {
            return this.Data[this.HeaderLocation + addr];
        }
    }
}
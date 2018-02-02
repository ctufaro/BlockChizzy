using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BlockChizzy.CL
{
    //https://medium.com/@lhartikk/a-blockchain-in-200-lines-of-code-963cc1cc0e54
    public class Block:IEquatable<Block>
    {
        public int Index { get; set; }
        public DateTime TimeStamp { get; set; }
        public string Data { get; set; }
        public string Hash { get; set; }
        public string PrevHash { get; set; }

        public Block(int Index, DateTime TimeStamp, string Data, string Hash, string PrevHash)
        {
            this.Index = Index;
            this.TimeStamp = TimeStamp;
            this.Data = Data;
            this.Hash = Hash;
            this.PrevHash = PrevHash;
        }

        public Block() { }

        public static string HashBlock(int Index, string PreviousHash, DateTime TimeStamp, string Data)
        {            
            string Value = string.Format("{0}{1}{2}{3}", Index, PreviousHash, TimeStamp, Data);
            return RawHash(Value);
        }

        public static string RawHash(string Value)
        {
            StringBuilder Sb = new StringBuilder();

            using (SHA256 hash = SHA256Managed.Create())
            {
                Encoding enc = Encoding.UTF8;
                Byte[] result = hash.ComputeHash(enc.GetBytes(Value));

                foreach (Byte b in result)
                    Sb.Append(b.ToString("x2"));
            }

            return Sb.ToString();
        }

        public bool Equals(Block other)
        {
            return this.Index.Equals(other.Index) && this.TimeStamp.Equals(other.TimeStamp) && this.Data.Equals(other.Data) && this.Hash.Equals(other.Hash) &&
                this.PrevHash.Equals(other.PrevHash);
        }
    }
}

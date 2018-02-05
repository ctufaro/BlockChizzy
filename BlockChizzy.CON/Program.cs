using BlockChizzy.CL;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockChizzy.CON
{
    class Program
    {
        static void Main(string[] args)
        {
            BlockChain master = new BlockChain();
            master.Init();
            Block newBlock = master.GenerateNextBlock("ass");
            bool isValidBlock = master.IsValidNewBlock(newBlock, master.GetLatestBlock());
            master.MasterChain.Add(newBlock);
            bool isValidChain = master.IsValidChain(master);
            Console.ReadLine();
        }
    }
}

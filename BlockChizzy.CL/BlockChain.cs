using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockChizzy.CL
{

    public class BlockChain
    {
        public List<Block> MasterChain { get; set; }

        public void Init()
        {
            MasterChain = new List<Block>();            
            MasterChain.Add(GenerateGenesisBlock());
        }

        public Block GenerateGenesisBlock()
        {
            return new Block(0, new DateTime(1900,1,1), "Genesis Block", "816534932c2b7154836da6afc367695e6337db8a921823784c14378abed4f7d7", null);
        }

        public Block GenerateNextBlock(string BlockData)
        {
            Block previousBlock = GetLatestBlock();
            int nextIndex = previousBlock.Index + 1;
            DateTime nextTimeStamp = DateTime.Now;
            string nextHash = Block.HashBlock(nextIndex, previousBlock.Hash, nextTimeStamp, BlockData);
            return new Block(nextIndex, nextTimeStamp, BlockData, nextHash, previousBlock.Hash);
        }

        public string CalcHashBlock(Block block)
        {
            return Block.HashBlock(block.Index, block.PrevHash, block.TimeStamp, block.Data);
        }

        public Block GetLatestBlock()
        {
            return MasterChain[MasterChain.Count - 1];
        }

        public bool IsValidNewBlock(Block newBlock, Block prevBlock)
        {
            if(prevBlock.Index + 1 != newBlock.Index)
            {
                System.Console.WriteLine("Invalid Index");
                return false;
            }
            else if(!prevBlock.Hash.Equals(newBlock.PrevHash))
            {
                System.Console.WriteLine("Invalid Previous Hash");
                return false;
            }
            else if (!CalcHashBlock(newBlock).Equals(newBlock.Hash))
            {
                System.Console.WriteLine("Invalid Hash");
                return false;
            }
            return true;
        }

        public bool IsValidChain(BlockChain newChain)
        {
            Block genesis = GenerateGenesisBlock();
            if (!newChain.MasterChain[0].Equals(genesis))
            {
                return false;
            }
            if (newChain.MasterChain.Count > 1)
            {
                for(int i = 0; i<newChain.MasterChain.Count; i++)
                {
                    if (i == newChain.MasterChain.Count - 1)
                        break;

                    var prev = newChain.MasterChain[i];
                    var newb = newChain.MasterChain[i+1];

                    if (!IsValidNewBlock(newb, prev))
                        return false;
                }
            }
            return true;
        }

        public void Replace(BlockChain newChain)
        {
            if (IsValidChain(newChain) && newChain.MasterChain.Count > this.MasterChain.Count)
            {
                this.MasterChain = newChain.MasterChain;
            }
        }
}

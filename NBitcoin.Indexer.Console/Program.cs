﻿using CommandLine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBitcoin.Indexer.Console
{
	class Program
	{
		static void Main(string[] args)
		{
			var options = new IndexerOptions();
			if(args.Length == 0)
				System.Console.WriteLine(options.GetUsage());
			if(Parser.Default.ParseArguments(args, options))
			{
				var indexer = AzureIndexer.CreateIndexer();
				indexer.NoSave = options.NoSave;
				indexer.FromBlk = options.FromBlk;
				indexer.BlkCount = options.BlkCount;
				indexer.TaskCount = options.ThreadCount;
				if(options.IndexBlocks)
				{
					indexer.IndexBlocks();
				}
				if(options.IndexChain)
				{
					indexer.IndexMainChain();
				}
				if(options.IndexTransactions)
				{
					indexer.IndexTransactions();
				}
				if(options.IndexAddresses)
				{
					indexer.IndexAddresses();
				}
				if(options.CountBlkFiles)
				{
					var dir = new DirectoryInfo(indexer.Configuration.BlockDirectory);
					if(!dir.Exists)
					{
						System.Console.WriteLine(dir.FullName + " does not exists");
						return;
					}
					System.Console.WriteLine("Blk files count : " +
						dir
						.GetFiles()
						.Where(f => f.Name.EndsWith(".dat"))
						.Where(f => f.Name.StartsWith("blk")).Count());
				}
			}
		}
	}
}

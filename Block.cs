using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BlockchainAssignment
{
    class Block
    {
        public DateTime timestamp;
        public int index;
        public int nonce = 0;
        public int eNonce = -1;
        public String Hash = String.Empty;
        public String previousHash;
        public float difficulty;
        public int threadNumber = 2;
        public string minerAddress = String.Empty;
        public float reward = 7;
        public float cum_fees = 0;
        public String merkleRoot;

        
        public List<Transaction> transactionList = new List<Transaction>();
        
        private CancellationToken _cancellationToken;

        public Block(Block lastBlock, List<Transaction> transList, string minerAddress, bool threaded, int diff, int threads = 1)
        {
            // Set block properties
            this.timestamp = DateTime.Now;
            this.previousHash = lastBlock.Hash;
            this.index = lastBlock.index + 1;
            this.transactionList = transList;
            this.minerAddress = minerAddress;

            // Reward Logic - calculate total fee from all transactions in the block and add to reward
            transactionList.ForEach(t => cum_fees += t.fee);
            transactionList.Add(new Transaction("Mine Rewards", "", minerAddress, this.reward + cum_fees, 0));
            this.merkleRoot = MerkleRoot(transactionList);

            // Set block difficulty and thread number for mining
            this.threadNumber = threads;
            this.difficulty = diff;

            // Start stopwatch to time block mining
            var watch = new System.Diagnostics.Stopwatch();
            if (threaded)
            {
                // If threaded, run ThreadedMine() to mine the block
                watch.Start();
                ThreadedMine();
                watch.Stop();
            }
            else
            {
                // If not threaded, run Mine() to mine the block
                watch.Start();
                Mine();
                watch.Stop();
            }

            double timeTaken = (double)watch.ElapsedTicks / System.Diagnostics.Stopwatch.Frequency;

            string timeElapsed = "Time taken: " + timeTaken.ToString("0.###") + " seconds";

            Console.WriteLine(timeElapsed);

        }

        // This is the constructor for the Block class
        public Block()
        {
            // Set the timestamp to the current time
            timestamp = DateTime.Now;

            // Set the previous hash to an empty string, as this is the first block in the chain
            previousHash = String.Empty;

            // Set the index to 0, as this is the first block in the chain
            index = 0;

            // Set the Merkle root to an empty string
            this.merkleRoot = String.Empty;

            // Call the CreateHash method to create the hash for this block with eNonce = 1
            // Set the resulting hash to the Hash property of this block
            Hash = CreateHash();

            // Set the value of eNonce to 1
            eNonce = 1;
        }

        // This is a static method for calculating the Merkle root of a list of transactions
        public static String MerkleRoot(List<Transaction> transactionList)
        {
            // Create a list of hashes by selecting the hash of each transaction in the list
            List<String> hashes = transactionList.Select(t => t.Hash).ToList();

            // If the list is empty, return an empty string
            if (hashes.Count == 0)
            {
                return String.Empty;
            }

            // If the list has only one hash, return the combined hash of that hash with itself
            if (hashes.Count == 1)
            {
                return HashCode.HashTools.combineHash(hashes[0], hashes[0]);
            }

            // If the list has more than one hash, repeatedly combine hashes until only one hash remains
            while (hashes.Count != 1)
            {
                List<String> merkleLeaves = new List<string>();
                for (int i = 0; i < hashes.Count; i += 2)
                {
                    // If this is the last hash in the list, combine it with itself
                    if (i == hashes.Count - 1)
                    {
                        merkleLeaves.Add(HashCode.HashTools.combineHash(hashes[i], hashes[i]));
                    }
                    // Otherwise, combine this hash with the next hash in the list
                    else
                    {
                        merkleLeaves.Add(HashCode.HashTools.combineHash(hashes[i], hashes[i + 1]));
                    }
                }
                hashes = merkleLeaves;
            }

            // Return the final hash, which is the Merkle root of the original list of transactions
            return hashes[0];
        }
        // This is a method for mining a block by finding a hash that meets a certain difficulty target
        public void Mine()
        {
            // Create a string of zeros with length equal to the difficulty target
            String target_string = "";
            for (int i = 0; i < difficulty; i++)
            {
                target_string += "0";
            }

            // While the current hash does not meet the difficulty target, increment the nonce and recalculate the hash
            while (!Hash.StartsWith(target_string))
            {
                this.nonce++;
                this.Hash = CreateHash();
            }

            // Once a hash meeting the difficulty target is found, reset the eNonce to 1
            this.eNonce = 1;
        }

        // This is a method for mining a block using multiple threads to speed up the process
        public void ThreadedMine()
        {
            // Create a cancellation token to allow stopping threads when a viable token has been found
            var cancellationSource = new CancellationTokenSource();
            this._cancellationToken = cancellationSource.Token;

            // Define thread-local variables for the hash and nonce, to make the method thread-safe
            ThreadLocal<String> localHash = new ThreadLocal<String>(() => {
                return "";
            });

            ThreadLocal<int> localNonce = new ThreadLocal<int>(() => {
                return 0;
            });

            // Define variables to store the results from the 'successful thread'
            object result = null;
            object threadNum = null;
            object threadNonce = null;

            // Set the number of threads to use to the value of the 'threadNumber' property
            int no_of_threads = this.threadNumber;

            // Create a target string consisting of zeros with length equal to the difficulty target
            String target_string = "";
            for (int i = 0; i < difficulty; i++)
            {
                target_string += "0";
            }

            // Create an array of tasks, one for each thread, and start each task
            Task[] ts = new Task[no_of_threads];
            for (int i = 0; i < no_of_threads; i++)
            {
                ts[i] = Task.Run(() => {
                    // Keep calculating hashes until the cancellation token is requested
                    while (!_cancellationToken.IsCancellationRequested)
                    {
                        // Increment the nonce and calculate the hash for the current thread
                        localNonce.Value++;
                        localHash.Value = CreateHash(Thread.CurrentThread.ManagedThreadId, localNonce.Value);

                        // If the hash meets the difficulty target, cancel the cancellation token and set the result variables
                        if (localHash.Value.StartsWith(target_string))
                        {
                            cancellationSource.Cancel();
                            result = localHash.Value;
                            threadNonce = localNonce.Value;
                            threadNum = Thread.CurrentThread.ManagedThreadId;
                        }
                    }
                });
            }

            // Wait for all threads to complete before setting the final hash, nonce, and eNonce values
            Task.WaitAll(ts);
            Hash = result.ToString();
            this.nonce = (int)threadNonce;
            eNonce = (int)threadNum;
        }

        // Default parameter eNonce is so we can use the same function for the threaded mine and the non threaded mine.
        public String CreateHash(int eNonce = 1, int nonce = -1)
        {
            // This is to ensure validation works (as we're passing CreateHash with no arguments with validation)
            if (this.eNonce != -1)
            {
                eNonce = this.eNonce;
            }
            if (nonce == -1)
            {
                nonce = this.nonce;
            }

            // Create a SHA256 hasher
            SHA256 hasher;
            hasher = SHA256Managed.Create();

            // Concatenate all the necessary information for the hash
            String input = index.ToString() + timestamp.ToString() + previousHash + nonce.ToString() + eNonce.ToString() + difficulty.ToString() + reward.ToString() + merkleRoot;

            // Compute the hash value for the input
            Byte[] hashByte = hasher.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Convert the hash value to a hexadecimal string
            String hash = string.Empty;
            foreach (byte x in hashByte)
            {
                hash += String.Format("{0:x2}", x);
            }

            return hash;
        }

        public String ReturnInfo()
        {
            string str = "== BLOCK START ==" +
              "\nBlock index: " + index +
              " \t\tTimestamp: " + timestamp +
              "\nPrevious Hash: " + previousHash +
              "\nHash: " + this.Hash +
              "\nMerkleroot: " + merkleRoot +
              "\nNonce: " + nonce +
              "\nE-Nonce: " + this.eNonce +
              "\nDifficulty: " + difficulty +
              "\nMiner Address: " + minerAddress +
              "\nReward: " + reward +
              "\t\tCumulative Fees: " + cum_fees +
              "\n\n= TRANSACTIONS =";

            foreach (Transaction t in transactionList)
            {
                str += "\n\n" + t.ReturnInfo();
            }

            str += "\n== BLOCK END ==";

            return str;
        }

    }
}
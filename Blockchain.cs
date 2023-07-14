using System;
using System.Collections.Generic;
using System.Linq;


namespace BlockchainAssignment
{
    class Blockchain
    {
        public List<Block> Blocks = new List<Block>();
        public List<Transaction> transactionPool = new List<Transaction>();

        // Constructor for Blockchain
        public Blockchain()
        {
            Blocks.Add(new Block());
        }

        // This method returns a list of transactions based on the given criteria
        public List<Transaction> GetTransactionList(int mode, int no_of_trans, String minerAddress)
        {
            // Initialize an empty list to store the returned transactions
            List<Transaction> returnList = new List<Transaction>();

            // Check if the number of requested transactions is the same as the number of transactions in the pool
            if (no_of_trans == transactionPool.Count)
            {
                // If they are the same, return the entire transaction pool
                return transactionPool;
            }
            else
            {
                // If they are different, filter the transaction pool based on the selected mode

                // Greedy: Sort the transaction pool by fee, highest to lowest, and take the top no_of_trans transactions
                if (mode == 0)
                {
                    // Sort the transaction pool by fee, highest to lowest
                    returnList = transactionPool.OrderBy(t => t.fee)
                      .Reverse()
                      .ToList();

                    // Remove any transactions after the top no_of_trans transactions
                    returnList.RemoveRange(no_of_trans, returnList.Count - (no_of_trans));
                }
                // Random: Randomly select no_of_trans transactions from the transaction pool
                else if (mode == 1)
                {
                    // Create a new random number generator
                    var random = new Random();

                    // Copy the transaction pool to a temporary list
                    List<Transaction> tempList = transactionPool.ToList();

                    // Loop through no_of_trans times
                    for (int i = 0; i < no_of_trans; i++)
                    {
                        // Generate a random index to select a transaction from the temporary list
                        int rndIndex = random.Next(0, tempList.Count);

                        // Add the selected transaction to the return list
                        returnList.Add(tempList[rndIndex]);

                        // Remove the selected transaction from the temporary list so it is not selected again
                        tempList.RemoveAt(rndIndex);
                    }
                }
                // Check if the selected mode is 2
                else if (mode == 2)
                {
                    // Altruistic: Sort the transaction pool by timestamp (oldest to newest) and take the top no_of_trans transactions

                    // Sort the transaction pool by timestamp (oldest to newest)
                    returnList = transactionPool.OrderBy(t => t.timestamp)
                      .ToList();

                    // Remove any transactions after the top no_of_trans transactions
                    returnList.RemoveRange(no_of_trans, returnList.Count - (no_of_trans));
                }
                
                // Check if the selected mode is 3
                else if (mode == 3)
                {
                    // Mode 3: Select transactions involving the miner (either as sender or recipient) until no_of_trans transactions are selected

                    // Loop through each transaction in the transaction pool
                    foreach (Transaction t in transactionPool)
                    {
                        // Check if the transaction involves the miner (either as sender or recipient)
                        if (t.RecipientAddress.Equals(minerAddress) || t.SenderAddress.Equals(minerAddress))
                        {
                            // Add the transaction to the return list
                            returnList.Add(t);
                        }

                        // Check if the desired number of transactions have been selected
                        if (returnList.Count == no_of_trans)
                        {
                            // If enough transactions have been selected, exit the loop
                            break;
                        }
                    }
                }

                // If we don't have the right number of transactions (i.e. too  little)
                if (returnList.Count != no_of_trans)
                {
                    //  Find the transactions in Transaction pool that aren't in returnList
                    List<Transaction> resultList = transactionPool.Except(returnList).ToList();

                    // Add the remaining transactions we need
                    for (int i = 0; i < (no_of_trans - returnList.Count); i++)
                    {
                        returnList.Add(resultList[i]);
                    }
                }

            }
            foreach (Transaction t in returnList)
            {
                Console.WriteLine("\nAmount: " + t.amount.ToString() +
                  "\nFee: " + t.fee.ToString() +
                  "\nTimestamp: " + t.timestamp.ToString() +
                  "\nSender: " + t.SenderAddress +
                  "\nRecipient: " + t.RecipientAddress);
            }
            return returnList;
        }

        // This method returns information about a block at a given index
        public String GetBlockOutput(int blockIndex)
        {
            try
            {
                // Try to get the block at the given index and return its information
                return Blocks[blockIndex].ReturnInfo();
            }
            catch (ArgumentOutOfRangeException)
            {
                // If the index is out of range, return an error message
                return "Block doesn't exist";
            }
        }

        // This method returns the last block in the chain
        public Block GetLastBlock()
        {
            return Blocks[Blocks.Count - 1];
        }

        // This method returns information about the entire blockchain
        public String ReturnInfo()
        {
            string str = String.Empty;
            // Iterate through each block in the chain and add its information to the output string
            foreach (Block curBlock in Blocks)
            {
                str += curBlock.ReturnInfo();
                str += "\n\n";
            }
            return str;
        }

        // This method validates the hash of a given block
        public bool ValidateHash(Block b)
        {
            // Calculate the hash of the block
            String rehash = b.CreateHash();

            // Print whether the calculated hash matches the block's stored hash
            Console.WriteLine("Validate Hash: " + rehash.Equals(b.Hash).ToString());

            // If the calculated hash matches the block's stored hash, return true
            return rehash.Equals(b.Hash);
        }

        // This method validates the merkle root of a given block
        public bool ValidateMerkelRoot(Block b)
        {
            // Calculate the merkle root of the block's transaction list
            String reMerkle = Block.MerkleRoot(b.transactionList);

            // Print whether the calculated merkle root matches the block's merkle root
            Console.WriteLine("Validate MerkleRoot: " + reMerkle.Equals(b.merkleRoot).ToString());

            // If the calculated merkle root matches the block's merkle root, return true
            return reMerkle.Equals(b.merkleRoot);
        }

        // This method validates each transaction in a given block
        public bool ValidateTransactions(Block b)
        {
            // Iterate through each transaction in the block's transaction list
            foreach (Transaction t in b.transactionList)
            {
                // If the transaction's signature is null or invalid, return false
                if (t.Signature == "null" || !Wallet.Wallet.ValidateSignature(t.SenderAddress, t.Hash, t.Signature))
                {
                    return false;
                }
            }

            // If all transactions are valid, return true
            return true;
        }

        // This method calculates the balance for a given wallet address
        public double GetBalance(String address)
        {
            // Initialize the balance to zero
            double balance = 0;

            // Iterate through each block in the blockchain to find transactions that involve the given address
            foreach (Block b in Blocks)
            {
                // Iterate through each transaction in the current block
                foreach (Transaction t in b.transactionList)
                {
                    // If the transaction is a payment to the address, add the payment amount to the balance
                    if (t.RecipientAddress.Equals(address))
                    {
                        balance += t.amount;
                    }

                    // If the transaction is a payment from the address, subtract the payment amount and fee from the balance
                    if (t.SenderAddress.Equals(address))
                    {
                        balance -= (t.amount + t.fee);
                    }
                }
            }

            // Iterate through each transaction in the transaction pool to find pending transactions that involve the given address
            foreach (Transaction t in this.transactionPool)
            {
                // If the pending transaction is a payment to the address, add the payment amount to the balance
                if (t.RecipientAddress.Equals(address))
                {
                    balance += t.amount;
                }

                // If the pending transaction is a payment from the address, subtract the payment amount and fee from the balance
                if (t.SenderAddress.Equals(address))
                {
                    balance -= (t.amount + t.fee);
                }
            }

            // Return the calculated balance
            return balance;
        }

        // This method returns a string containing information about a list of transactions called "transactionPool"
        public String ReturnTransPoolInfo()
        {
            // Initialize an empty string to store the transaction information
            string str = String.Empty;

            // Iterate through each transaction in the transactionPool list using a foreach loop
            foreach (Transaction t in transactionPool)
            {
                // Add the index of the transaction in the list and the transaction information to the str string, separated by a newline character
                str += "\n\nIndex:" + transactionPool.IndexOf(t) + "\n" + t.ReturnInfo();
            }

            // If there are no transactions in the list, add "No Pending Transactions" to the str string
            if (str == String.Empty)
            {
                str += "No Pending Transactions";
            }

            // Return the str string
            return str;
        }

    }
}
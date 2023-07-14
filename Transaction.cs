using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;


namespace BlockchainAssignment
{
    class Transaction
    {
        public String Hash, Signature, SenderAddress, RecipientAddress;
        public DateTime timestamp;
        public float amount, fee;

// This constructor creates a new transaction with the specified sender and recipient addresses, amount, and fee
public Transaction(String SenderAddress, String sPrivKey, String RecipientAddress, float amount, float fee)
{
    // Set the sender and recipient addresses, amount, and fee for the transaction
    this.SenderAddress = SenderAddress;
    this.RecipientAddress = RecipientAddress;
    this.amount = amount;
    this.fee = fee;

    // Set the timestamp for the transaction to the current date and time
    timestamp = DateTime.Now;

    // Calculate the hash value for the transaction using the CreateHash() method
    Hash = CreateHash();

    // Create a digital signature for the transaction using the sender's private key and the hash value
    Signature = Wallet.Wallet.CreateSignature(SenderAddress, sPrivKey, Hash);
}


        // This method calculates and returns the SHA256 hash of a transaction
        public String CreateHash()
        {
            // Create a new SHA256 hasher object
            SHA256 hasher;
            hasher = SHA256Managed.Create();

            // Concatenate the transaction data (sender address, recipient address, timestamp, amount, and fee) into a single string
            String input = SenderAddress + RecipientAddress + timestamp.ToString() + amount.ToString() + fee.ToString();

            // Compute the hash value of the concatenated string using the SHA256 algorithm
            Byte[] hashByte = hasher.ComputeHash(Encoding.UTF8.GetBytes((input)));

            // Convert the hash value from a byte array to a hexadecimal string
            String hash = string.Empty;
            foreach (byte x in hashByte)
            {
                hash += String.Format("{0:x2}", x);
            }

            // Return the hexadecimal string representation of the hash value
            return hash;
        }

        public String ReturnInfo()
        {
            return "[TRANSACTION START]" +
                "\nTransaction Hash: " + Hash +
                "\nDigital Signature: " + Signature +
                "\nTimestamp: " + timestamp +
                "\nTransferred: " + amount.ToString() + " Assignment Coin" +
                "\nFees: " + fee.ToString() +
                "\nSender Address: " + SenderAddress +
                "\nReciever Address: " + RecipientAddress +
                "\n  [TRANSACTION END]";

        }
    }
}

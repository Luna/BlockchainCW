using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BlockchainAssignment
{
    public partial class BlockchainApp : Form
    {
        Blockchain blockchain;
        bool enableAutominer = false;
        bool enableAPOW = true;
        float tarBlockTime = 10;
        int sleepTimer = 500;
        int threadNum = 2;
        int difficulty = 5;

        public BlockchainApp()
        {
            InitializeComponent();
            blockchain = new Blockchain();
            richTextBox1.Text = "New Blockchain Initialised!";
        }

        // This method is called when the button1 is clicked
        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                // Get the block output of the block specified by the input in textBox1
                setRTB1Text(blockchain.GetBlockOutput(Int32.Parse(textBox1.Text)));
            }
            catch (FormatException)
            {
                // If the input is not valid (not an integer), show an error message
                setRTB1Text("Invalid Input");
            }
        }

        // This method sets the text of the richTextBox1 control with the given string 'str'
        private void setRTB1Text(string str)
        {
            // If the call to this method is not on the UI thread, then it invokes itself on the UI thread and returns immediately
            if (InvokeRequired)
            {
                this.Invoke(new Action<string>(setRTB1Text), new object[] {
          str
        });
                return;
            }
            // If the call to this method is on the UI thread, then it sets the text of richTextBox1 to the given string 'str'
            richTextBox1.Text = str;
        }

        // This method is used to set the text of a label (aPow) on the UI thread
        // If this method is called from a different thread, it will invoke itself on the UI thread using the Invoke method
        // This ensures that changes to UI elements are made on the UI thread and not on a different thread
        private void setAPoWText(string str)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<string>(setAPoWText), new object[] {
          str
        });
                return;
            }
            aPow.Text = str;
        }
        // This method is executed when the "Create Wallet" button is clicked in the UI
        private void button2_Click_1(object sender, EventArgs e)
        {
            String privKey, pubKey;

            // Create a new wallet and get its private key
            Wallet.Wallet wallet = new Wallet.Wallet(out privKey);

            // Get the wallet's public key and display it in the GUI
            pubKey = wallet.publicID;
            txtPubKey.Text = pubKey;

            // Display the private key in the GUI
            txtPrivKey.Text = privKey;

            // Display a message in the GUI confirming that a new wallet was created
            setRTB1Text("Created new Wallet");
        }

        // This function is called when button3 is clicked
        private void button3_Click(object sender, EventArgs e)
        {
            // Check the validity of the private key and public key and print result to RTB1Text
            if (Wallet.Wallet.ValidatePrivateKey(txtPrivKey.Text, txtPubKey.Text))
            {
                setRTB1Text("Keys are Valid");
            }
            else
            {
                setRTB1Text("Keys are INVALID");
            }
        }
        // This function is triggered when button4 is clicked
        private void button4_Click(object sender, EventArgs e)
        {
            // Get the balance of the wallet with the public key provided in the text box
            double walletBalance = blockchain.GetBalance(txtPubKey.Text);

            // Check if the wallet has enough balance to make the transaction
            if (walletBalance < float.Parse(txtAmnt.Text))
            {
                setRTB1Text("ERROR: Too low balance to make transaction");
                return;
            }

            // Validate the private key of the wallet with the public key provided
            if (!Wallet.Wallet.ValidatePrivateKey(txtPrivKey.Text, txtPubKey.Text))
            {
                setRTB1Text("ERROR: Keys provided were invalid");
                return;
            }

            // Create a new transaction object with the information provided in the text boxes
            Transaction trans = new Transaction(txtPubKey.Text, txtPrivKey.Text, txtRKey.Text, float.Parse(txtAmnt.Text), float.Parse(txtFee.Text));

            // Display the transaction information in the rich text box
            String newTransaction = trans.ReturnInfo();
            setRTB1Text(newTransaction);
            
            // Add the transaction to the transaction pool of the blockchain
            blockchain.transactionPool.Add(trans);
            transactionLog(newTransaction);
        }
        private void transactionLog(String trans) {
            
            // Set the file path for the transaction log
            string filePath = "transaction_log.txt";

            // Create a new StreamWriter object to write to the file
            StreamWriter writer = new StreamWriter(filePath, true);

            // Write the transaction information to the file
            writer.WriteLine(trans);

            // Close the StreamWriter object
            writer.Close();

        }

        // This method is used to generate a block using a single thread.
        private void button5_Click(object sender, EventArgs e)
        {
            // Check if the private and public key pair is valid
            if (!Wallet.Wallet.ValidatePrivateKey(txtPrivKey.Text, txtPubKey.Text))
            {
                setRTB1Text("ERROR: Can't Mine blocks with invalid Keys");
                return;
            }

            // Run a new task to generate a block using a single thread
            Task mine = Task.Run(() => {
                Miner(txtPubKey.Text, false);
            });
        }
        // End of code.

        // Button click event handler to display information about all blocks in the blockchain
        private void button6_Click(object sender, EventArgs e)
        {
            // Call the ReturnInfo() method of the blockchain object to get information about all blocks
            string blockchainInfo = blockchain.ReturnInfo();
            // Set the information as the text of the rich text box control
            setRTB1Text(blockchainInfo);
        }

        // Button click event handler to display information about pending transactions in the transaction pool
        private void button7_Click(object sender, EventArgs e)
        {
            // Call the ReturnTransPoolInfo() method of the blockchain object to get information about pending transactions
            string transPoolInfo = blockchain.ReturnTransPoolInfo();
            // If there are no pending transactions, set a message to display instead
            if (transPoolInfo == String.Empty)
            {
                transPoolInfo = "No more pending transactions in the pool";
            }
            // Set the information as the text of the rich text box control
            setRTB1Text(transPoolInfo);
        }

        // This button click event handler is used to display the balance of a wallet on the blockchain
        // It uses the GetBalance method of the Blockchain class to retrieve the balance of a wallet, and then displays it in the rich text box
        private void button9_Click(object sender, EventArgs e)
        {
            setRTB1Text(blockchain.GetBalance(txtPubKey.Text).ToString() + " AssignmentCoin");
        }

        // This function checks the validity of the blockchain
        private void button10_Click(object sender, EventArgs e)
        {
            if (blockchain.Blocks.Count == 1)
            {
                if (blockchain.ValidateMerkelRoot(blockchain.Blocks[0])) // validate the Merkle root of the only block in the blockchain
                {
                    setRTB1Text("Valid Blockchain"); // if the Merkle root is valid, print out a message saying the blockchain is valid
                }
                else
                {
                    setRTB1Text("Invalid Blockchain: Merkleroot"); // if the Merkle root is invalid, print out a message saying the blockchain is invalid
                }
                return;
            }
            else
            {
                for (int i = 1; i < blockchain.Blocks.Count; i++) // iterate through all the blocks in the blockchain except the first one
                {
                    if (
                      blockchain.Blocks[i].previousHash != blockchain.Blocks[i - 1].Hash || // check if the previous hash of the current block matches the hash of the previous block
                      !blockchain.ValidateMerkelRoot(blockchain.Blocks[i]) || // check if the Merkle root of the current block is valid
                      !blockchain.ValidateHash(blockchain.Blocks[i]) // check if the hash of the current block is valid
                    )
                    {
                        setRTB1Text("Invalid Hash History in Block " + i.ToString()); // if any of the above checks fail, print out a message saying the blockchain is invalid
                        return;
                    }
                    if (!blockchain.ValidateTransactions(blockchain.Blocks[i])) // check if the transactions in the current block are valid
                    {
                        setRTB1Text("Invalid Blockchain: Invalid Transaction in Block " + i.ToString()); // if the transactions in the current block are invalid, print out a message saying the blockchain is invalid
                        return;
                    }
                }
            }
            setRTB1Text("Valid Blockchain"); // if all the above checks pass, print out a message saying the blockchain is valid
        }

        // This button generates a block using threads
        private void button8_Click(object sender, EventArgs e)
        {
            // Validate private and public keys before mining
            if (!Wallet.Wallet.ValidatePrivateKey(txtPrivKey.Text, txtPubKey.Text))
            {
                setRTB1Text("ERROR: Can't Mine blocks with invalid Keys");
                return;
            }
            // Start a new task to run the Miner method with multi-threading enabled
            Task mine = Task.Run(() => {
                Miner(txtPubKey.Text, true);
            });
        }

        // This button starts the Autominer
        private void button11_Click(object sender, EventArgs e)
        {
            // Enable the Autominer flag
            this.enableAutominer = true;
            // Start a new task to run the Miner method with multi-threading enabled in a loop until Autominer is disabled
            Task ts = Task.Run(() => {
                while (enableAutominer)
                {
                    Miner("AUTOMINER", true);
                }
            });
        }
        // Multi-Functional miner, that allows for multi / single threaded execution, and can be used by the Autominer
        private void Miner(String recieverAddress, bool threaded)
        {
            // Sleep for the specified duration
            Thread.Sleep(this.sleepTimer);

            // Get the selected mining method from the combo box
            int comboInput = 1;
            String comboSetting = String.Empty;
            this.Invoke((MethodInvoker)delegate () {
                comboInput = cmbo_MineMethod.SelectedIndex;
                comboSetting = cmbo_MineMethod.SelectedItem.ToString();
            });

            // Set the number of transactions to be included in the block
            int numOfTrans = 3;
            String str = String.Empty;
            int transPoolCount = blockchain.transactionPool.Count();
            if (transPoolCount < numOfTrans)
            {
                str += "WARNING: Only " + transPoolCount + " transactions being added to block!\n";
                numOfTrans = transPoolCount;
            }

            // Get the selected transaction selection method and create a block with the selected transactions
            Console.WriteLine("Transaction Selection Setting: " + comboSetting);
            List<Transaction> blockTransactions = blockchain.GetTransactionList(comboInput, numOfTrans, txtPubKey.Text);
            blockchain.transactionPool = blockchain.transactionPool.Except(blockTransactions).ToList();
            Block newBlock = new Block(blockchain.GetLastBlock(), blockTransactions, recieverAddress, threaded, this.difficulty, this.threadNum);

            // Add the new block to the blockchain and display information about it
            blockchain.Blocks.Add(newBlock);
            str += "\n" + newBlock.ReturnInfo();

            setRTB1Text(str);
        }

        // This is for doing stuff at runtime.
        // This method is called when the form loads
        private void BlockchainApp_Load(object sender, EventArgs e)
        {
            // Set the default mining method to the first option in the combo box
            cmbo_MineMethod.SelectedIndex = 0;

            // Disable the mine time text box
            this.txtMineTime.Enabled = false;
        }

        // This method is called when the "Stop AutoMining" button is clicked
        private void button12_Click(object sender, EventArgs e)
        {
            // Set the flag to stop the automatic mining process
            this.enableAutominer = false;
        }

        // This function is called when the "Start" button is clicked. It starts a background task that continuously checks the current block time
        // and adjusts the difficulty level and sleep timer accordingly.
        private void button13_Click(object sender, EventArgs e)
        {
            // Enable the mine time text box so the user can enter a new target block time if desired.
            this.txtMineTime.Enabled = true;

            // Start a new background task using the Task.Run method.
            Task ts = Task.Run(() => {
                // This loop runs continuously until the enableAPOW variable is set to false.
                while (enableAPOW)
                {
                    // Create a list of the most recent block times and a list of the time differences between them.
                    List<DateTime> recentTimes = new List<DateTime>();
                    List<int> diffSeconds = new List<int>();
                    double averageSeconds = 0;
                    String str = "";

                    // Get the timestamp of the 10 most recent blocks.
                    for (int i = 0; i < 10; i++)
                    {
                        try
                        {
                            recentTimes.Add(blockchain.Blocks[blockchain.Blocks.Count - 1 - i].timestamp);
                        }
                        // If we run out of blocks, exit the loop.
                        catch (ArgumentOutOfRangeException)
                        {
                            break;
                        }
                    }

                    // If we have less than 2 block times, we can't calculate an average, so display an error message.
                    if (recentTimes.Count <= 1)
                    {
                        str += "Not enough Blocks";
                    }
                    // Otherwise, calculate the average block time.
                    else
                    {
                        str += "Actual Block Time: ";
                        // Iterate through all the times we have, except the last one
                        for (int i = 0; i < recentTimes.Count - 1; i++)
                        {
                            diffSeconds.Add((int)(recentTimes[i] - recentTimes[i + 1]).TotalSeconds);
                        }
                        averageSeconds = diffSeconds.Average();
                        str += averageSeconds.ToString("n2");
                    }

                    // Add the target block time and difficulty level to the output string.
                    str += "\nTarget Block time: " + this.tarBlockTime.ToString("n2");
                    adaptDifficulty(averageSeconds);
                    str += "\nDifficulty : " + this.difficulty.ToString();
                    str += "\nArtificial Sleep (ms): " + this.sleepTimer.ToString();

                    // Set the output string as the text of the A-PoW text box.
                    setAPoWText(str);

                    // Wait for 1 second before checking the block time again.
                    Thread.Sleep(1000);
                }
            });
        }

        // This function adjusts the difficulty level and sleep timer based on the current block time and target block time.
        private void adaptDifficulty(double CurrentBlockTime)
        {
            // Get the target block time from the tarBlockTime variable.
            float t = this.tarBlockTime;

            // Set the difficulty level based on the target block time.
            if (t < 0.5)
            {
                this.difficulty = 3;
            }
            else if (t >= 0.5 && t < 9.5)
            {
                this.difficulty = 4;
            }
            else if (t >= 9.5 && t < 200)
            {
                this.difficulty = 5;
            }
            else if (t >= 200)
            {
                this.difficulty = 6;
            }

            // Calculate the sleep timer based on the difference between the target block time and the current block time.
            this.sleepTimer = (int)((t - CurrentBlockTime) * 1000);

            // If the sleep timer is negative, set it to 0 and check if the current block time is more than 1.5 times the target block time.
            if (this.sleepTimer < 0)
            {
                this.sleepTimer = 0;

                // If the current block time is more than 1.5 times the target block time, decrease the difficulty level.
                if (CurrentBlockTime / t > 1.5)
                {
                    this.difficulty--;
                }
            }
        }

        // This function is an event handler for the button14_Click event.
        // It is executed whenever button14 is clicked.
        private void button14_Click(object sender, EventArgs e)
        {
            // Disable the APOW feature by setting enableAPOW to false.
            this.enableAPOW = false;

            // Reset the sleep timer to 0.
            this.sleepTimer = 0;

            // Disable the txtMineTime text box.
            this.txtMineTime.Enabled = false;

            // Set the text of the APoW label to "Disabled".
            setAPoWText("Disabled");
        }

        // This function is an event handler for the txtMineTime_TextChanged event.
        // It is executed whenever the text in the txtMineTime text box changes.
        private void txtMineTime_TextChanged(object sender, EventArgs e)
        {
            try
            {
                // Parse the text in the txtMineTime text box as a float value and assign it to the tarBlockTime variable.
                this.tarBlockTime = float.Parse(txtMineTime.Text);

                // Set the background color of the txtMineTime text box to green to indicate that the input was valid.
                txtMineTime.BackColor = Color.Green;
            }
            catch (FormatException)
            {
                // If the parsing fails, set tarBlockTime to a default value of 30f.
                this.tarBlockTime = 30;

                // Set the background color of the txtMineTime text box to red to indicate that the input was invalid.
                txtMineTime.BackColor = Color.Red;

                // Return from the function to prevent further execution.
                return;
            }
        }

        // This method is executed when button15 is clicked
        private void button15_Click(object sender, EventArgs e)
        {
            // The private and public keys of a wallet are set
            String privKey = "TStAcmYqy10Vp&I$/91|JS@UE+AUtMaW1b$RVoUf1\"95%DPrUe12;nt59rLPu^j";
            String pubKey = "<#,`WjEH%6k&]%X;!GF1xHAF'7MK'1]=x0d(Y:%W%39>;/ayjkVq3vl{q5f&Yc7";

            // A new Random object is created
            Random rnd = new Random();
            // The loop iterates 10 times
            for (int i = 0; i < 10; i++)
            {
                // A random amount between 0 and 1000 is generated
                float amount = (float)(rnd.NextDouble() * 1000);
                // A random fee between 0 and 100 is generated
                float fee = (float)(rnd.NextDouble() * 100);
                // A new transaction is created with the generated values and added to the transaction pool
                Transaction trans = new Transaction(pubKey, privKey, "TEST", amount, fee);
                // The thread sleeps for a random amount of time between 0 and 10000 milliseconds
                Thread.Sleep((int)(rnd.NextDouble() * 10000));
                blockchain.transactionPool.Add(trans);
            }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

    }
}
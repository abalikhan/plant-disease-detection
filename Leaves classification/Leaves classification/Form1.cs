using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using weka;
using weka.classifiers;

namespace Leaves_classification
{
    public partial class Form1 : Form
    {
        const int percentSplit = 66;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string fname="";
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter =
               "Weka Files (*.arff)|*.arff|All files (*.*)|*.*";
            dialog.InitialDirectory = Application.StartupPath;
            dialog.Title = "Select a .arff file";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                fname = dialog.FileName;
                //label5.Text = System.IO.Directory.;
            }
            if (fname == "")
                return;
            try
            {
                weka.core.Instances insts = new weka.core.Instances(new java.io.FileReader(fname.ToString()));
                insts.setClassIndex(insts.numAttributes() - 1);


                Classifier cl = new weka.classifiers.functions.SMO();
                //label1.Text = "Performing " + percentSplit + "% split evaluation.";


                //randomize the order of the instances in the dataset.
                weka.filters.Filter myRandom = new weka.filters.unsupervised.instance.Randomize();
                myRandom.setInputFormat(insts);
                insts = weka.filters.Filter.useFilter(insts, myRandom);

                int trainSize = insts.numInstances() * percentSplit / 100;
                int testSize = insts.numInstances() - trainSize;
                weka.core.Instances train = new weka.core.Instances(insts, 0, trainSize);

                cl.buildClassifier(train);
                int numCorrect = 0;
                for (int i = trainSize; i < insts.numInstances(); i++)
                {
                    weka.core.Instance currentInst = insts.instance(i);
                    double predictedClass = cl.classifyInstance(currentInst);
                    if (predictedClass == insts.instance(i).classValue())
                        numCorrect++;
                }
                //label1.Text = numCorrect + " out of " + testSize + " correct (" +
                           //(double)((double)numCorrect / (double)testSize * 100.0) + "%)";

                label6.Text = testSize.ToString();
                label7.Text = numCorrect.ToString();
                label8.Text = (double)((double)numCorrect / (double)testSize * 100.0) + "%";
                double result_perc = (double)((double)numCorrect / (double)testSize * 100.0);

                result_perc = Math.Truncate(result_perc);

                  try
                    {
                        // Send Data On Serial port
                        SerialPort serialPort = new SerialPort("COM" + textBox1.Text + "", Int32.Parse(textBox2.Text), Parity.None, 8);
                        serialPort.Open();

                        if (result_perc <= 75 )
                        {
                            serialPort.WriteLine("1");
                        }


                        serialPort.WriteLine("a");


                        serialPort.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
            
            }
            catch (java.lang.Exception ex)
            {
                MessageBox.Show(ex.getMessage().ToString(),"");
            }
        }
    }
}

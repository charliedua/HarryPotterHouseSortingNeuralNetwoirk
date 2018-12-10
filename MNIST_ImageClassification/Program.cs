using HarryPotterHouseSortingNeuralNetwoirk;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MNIST_ImageClassification
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("\nBegin\n");
            FileStream ifsLabels =
             new FileStream(@"C:\Users\duaan\source\repos\HarryPotterHouseSortingNeuralNetwoirk\Resources\train-labels.idx1-ubyte",
             FileMode.Open); // train labels  train-labels.idx1-ubyte
            FileStream ifsImages =
             new FileStream(@"C:\Users\duaan\source\repos\HarryPotterHouseSortingNeuralNetwoirk\Resources\train-images.idx3-ubyte",
             FileMode.Open); // train images

            BinaryReader brLabels =
             new BinaryReader(ifsLabels);
            BinaryReader brImages =
             new BinaryReader(ifsImages);

            // Input Layer
            var InputNeurons = new List<Neuron>();
            for (int i = 0; i < Math.Pow(28, 2); i++)
            {
                InputNeurons.Add(new InputNeuron());
            }
            InputNeurons.Add(new InputNeuron() { IsBiased = true });
            Layer input = new Layer(InputNeurons);

            // Hidden Layer 1
            var HiddenNeurons = new List<Neuron>();
            for (int i = 0; i < 25; i++)
            {
                HiddenNeurons.Add(new HiddenNeuron());
            }
            HiddenNeurons.Add(new HiddenNeuron() { IsBiased = true });
            Layer hidden = new Layer(HiddenNeurons);

            // Hidden Layer 2
            var Hidden2Neurons = new List<Neuron>();
            for (int i = 0; i < 25; i++)
            {
                Hidden2Neurons.Add(new HiddenNeuron());
            }
            Hidden2Neurons.Add(new HiddenNeuron() { IsBiased = true });
            Layer hidden2 = new Layer(Hidden2Neurons);

            // Output Layer
            var OutputNeurons = new List<Neuron>
                {
                    new OutputNeuron("0"),
                    new OutputNeuron("1"),
                    new OutputNeuron("2"),
                    new OutputNeuron("3"),
                    new OutputNeuron("4"),
                    new OutputNeuron("5"),
                    new OutputNeuron("6"),
                    new OutputNeuron("7"),
                    new OutputNeuron("8"),
                    new OutputNeuron("9"),
                };
            Layer output = new Layer(OutputNeurons);

            NeuralNetwork network = new NeuralNetwork(new List<Layer>() { input, hidden, hidden2, output });

            network.Init();

            int magic1 = brImages.ReadInt32(); // discard
            int numImages = brImages.ReadInt32();
            int numRows = brImages.ReadInt32();
            int numCols = brImages.ReadInt32();

            int magic2 = brLabels.ReadInt32();
            int numLabels = brLabels.ReadInt32();

            byte[][] pixels = new byte[28][];
            for (int i = 0; i < pixels.Length; ++i)
                pixels[i] = new byte[28];

            Console.Write("Training");

            // each test image
            for (int di = 0; di < 9999; ++di)
            {
                for (int i = 0; i < 28; ++i)
                {
                    for (int j = 0; j < 28; ++j)
                    {
                        byte b = brImages.ReadByte();
                        pixels[i][j] = b;
                    }
                }

                byte lbl = brLabels.ReadByte();

                Image dImage =
                  new Image(pixels, lbl);

                float[] data = new float[(int)Math.Pow(28, 2)];
                for (int i = 0; i < dImage.Pixels.Count(); i++)
                {
                    for (int j = 0; j < dImage.Pixels[i].Count(); j++)
                    {
                        data[i] = dImage.Pixels[i][j];
                    }
                }
                // Convert.ToInt32(dImage.Label);
                List<float> targets = new List<float>();
                for (int i = 0; i < 10; i++)
                {
                    if (i == Convert.ToInt32(dImage.Label))
                        targets.Add(1);
                    else
                        targets.Add(0);
                }
                network.Train(data, targets.ToArray());
                Console.WriteLine("Now Training {0}", di);
            } // each image

            ifsImages.Close();
            brImages.Close();
            ifsLabels.Close();
            brLabels.Close();

            FileStream testingLabels =
                new FileStream(@"C:\Users\duaan\source\repos\HarryPotterHouseSortingNeuralNetwoirk\Resources\t10k-labels.idx1-ubyte.bin", FileMode.Open); // test labels
            FileStream testingImages =
                new FileStream(@"C:\Users\duaan\source\repos\HarryPotterHouseSortingNeuralNetwoirk\Resources\t10k-images.idx3-ubyte.bin", FileMode.Open); // test images
            brLabels =
             new BinaryReader(testingLabels);
            brImages =
             new BinaryReader(testingImages);

            magic1 = brImages.ReadInt32(); // discard
            numImages = brImages.ReadInt32();
            numRows = brImages.ReadInt32();
            numCols = brImages.ReadInt32();

            magic2 = brLabels.ReadInt32();
            numLabels = brLabels.ReadInt32();

            pixels = new byte[28][];
            for (int i = 0; i < pixels.Length; ++i)
                pixels[i] = new byte[28];

            int corrects = 0;
            int incorrects = 0;

            for (int di = 0; di < 9999; di++)
            {
                for (int i = 0; i < 28; ++i)
                {
                    for (int j = 0; j < 28; ++j)
                    {
                        byte b = brImages.ReadByte();
                        pixels[i][j] = b;
                    }
                }

                byte lbl = brLabels.ReadByte();

                Image dImage =
                  new Image(pixels, lbl);

                float[] data = new float[(int)Math.Pow(28, 2)];
                for (int i = 0; i < dImage.Pixels.Count(); i++)
                {
                    for (int j = 0; j < dImage.Pixels[i].Count(); j++)
                    {
                        data[i] = dImage.Pixels[i][j];
                    }
                }

                var PredictedID = network.Predict(data);

                if (Convert.ToInt32(dImage.Label) == int.Parse(PredictedID))
                {
                    corrects++;
                }
                else
                {
                    incorrects++;
                }

                Console.WriteLine("Fitness = {0}", corrects / (corrects + incorrects));
            }

            Console.WriteLine("\nEnd\n");
            Console.ReadLine();
        } // Main
    } // Program

    internal class Image
    {
        public byte[][] Pixels { get; set; }
        public byte Label;

        public Image(byte[][] pixels,
          byte label)
        {
            Pixels = new byte[28][];
            for (int i = 0; i < Pixels.Length; ++i)
                Pixels[i] = new byte[28];

            for (int i = 0; i < 28; ++i)
                for (int j = 0; j < 28; ++j)
                    Pixels[i][j] = pixels[i][j];

            Label = label;
        }

        public override string ToString()
        {
            string s = "";
            for (int i = 0; i < 28; ++i)
            {
                for (int j = 0; j < 28; ++j)
                {
                    if (Pixels[i][j] == 0)
                        s += " "; // white
                    else if (Pixels[i][j] == 255)
                        s += "O"; // black
                    else
                        s += "."; // gray
                }
                s += "\n";
            }
            s += Label.ToString();
            return s;
        } // ToString
    } // Image
} // Namespace
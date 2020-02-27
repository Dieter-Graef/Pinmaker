using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace STM32_Pinmaker
{
    public partial class Form1 : Form
    {
        string Zieldatei;
        string GPIOSuchPfad;
        string GPIOPfad;
        string chipSuchPfad;
        string chipPfad;
        string Dateiname;
        byte[] fileBytes;
        int filezeiger = 0;
        char[] magicString;
        string[] funkliste;
        string[]altliste;
        string[] csvline = new string[20];
        public struct adcport
        {
            public string Port;
            public string ADCs;
        }
        adcport[] adcports;
        public Form1()
        {
            InitializeComponent();
            gpioladen.Enabled = false;
            csvPfad.Enabled = false;
            chipload.Enabled = false;
         }

        private string searchadc(string srch)
        {
            for (int i = 0;i< adcports.Length;i++)
            {
                if (adcports[i].Port == srch)
                    return adcports[i].ADCs;
            }
            return "";
        }

        private static bool IsMatch(byte[] array, int position, byte[] candidate)
        {
            if (candidate.Length > (array.Length - position))
                return false;

            for (int i = 0; i < candidate.Length; i++)
                if (array[position + i] != candidate[i])
                    return false;

            return true;
        }

        private static int[] Locate( byte[] self, byte[] candidate)
        {
            var list = new List<int>();
            int[] Empty = new int[0];
            for (int i = 0; i < self.Length; i++)
            {
                if (!IsMatch(self, i, candidate))
                    continue;

                list.Add(i);
            }

            return list.Count == 0 ? Empty : list.ToArray();
        }

        private static byte[] StringToByteArray(string str)
        {
            System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();
            return enc.GetBytes(str);
        }

        private void gpioladen_Click(object sender, EventArgs e)
        {
            string such = "    <About>";
            string ersetz1 = "<?xml version=\"1.0\" encoding =\"UTF - 8\" ?>;  <IP>";
            string ersetz2 = ersetz1.Replace(";", Environment.NewLine);
            string csvl1 = "Port,,AF0,AF1,AF2,AF3,AF4,AF5,AF6,AF7,AF8,AF9,AF10,AF11,AF12,AF13,AF14,AF15,";
            string csvl2 = ",,AF0,AF1,AF2,AF3,AF4,AF5,AF6,AF7,AF8,AF9,AF10,AF11,AF12,AF13,AF14,AF15,ADC";
            byte[] suchb = StringToByteArray(such);
            byte[] ersetzb = StringToByteArray(ersetz2);
            int[] position;
            string[] line;
            line = new string[20];
            DialogResult result;
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.InitialDirectory = cubepfad.Text+"\\STM32CubeMX\\db\\mcu\\IP\\";
            openFile.Filter = "CubeMCU GPIO files (GPIO-STM32*_gpio_v1_0_Modes.xml)|GPIO-STM32*_gpio_v1_0_Modes.xml";
            openFile.FilterIndex = 2;
            openFile.RestoreDirectory = true;
            result = openFile.ShowDialog();
            if (result == DialogResult.OK)
                GPIODatei.Text = openFile.FileName;
            if (result == DialogResult.Cancel)
            { GPIODatei.Text = ""; return; }
            if (GPIODatei.Text != "")
            {
                if (File.Exists(GPIODatei.Text))
                {
                    GPIOPfad = System.IO.Path.GetDirectoryName(GPIODatei.Text);
                    GPIOSuchPfad = System.IO.Path.GetFullPath(GPIOPfad);
                    Dateiname = System.IO.Path.GetFileNameWithoutExtension(GPIODatei.Text);
                    Dateiname = Dateiname + ".csv";
                    Datei.Text = Dateiname;
                    Zieldatei = csv.Text + "\\" + Dateiname;
                    using (System.IO.StreamWriter file = new System.IO.StreamWriter(@Zieldatei, false))
                    {
                        file.WriteLine(csvl1);
                        file.WriteLine(csvl2);
                    }

                }
                else
                {
                    MessageBox.Show("Datei existiert nicht");
                    return;
                }
                fileBytes = File.ReadAllBytes(GPIODatei.Text);
                filezeiger = 0;
                magicString = new char[5];
                int i;
                for (i = 0; i < 5; i++)
                {
                    magicString[i] = (char)fileBytes[i];
                }
                filezeiger = filezeiger + i;
                string tmp1 = new string(magicString);
                string tmp2 = "<?xml";
                if (!(tmp1 == tmp2))
                {
                    MessageBox.Show("Kein xml!");
                    return;
                }
                else
                {
                    // patch original to work
                    position = Locate(fileBytes, suchb);
                    int p = position[0];
                    for(i=0;i<ersetzb.Length;i++)
                    {
                        fileBytes[i] = ersetzb[i];
                    }
                    for(i=ersetzb.Length; i<(p-1); i++)
                    {
                        fileBytes[i] = (byte)' ';
                    }
                    // patch done
                    string XMLfile = System.Text.Encoding.UTF8.GetString(fileBytes);
                    XmlDocument doc = new XmlDocument();
                    doc.Load(new StringReader(XMLfile));
                    i = 0;
                    XmlNode r1 = doc.SelectSingleNode("/IP");
                    XmlNodeList listOfItems = doc.SelectNodes("/IP/GPIO_Pin");
                    foreach (XmlNode singleItem in listOfItems)
                    {
                        string port = singleItem.Attributes["PortName"].InnerText;
                        string pin  = singleItem.Attributes["Name"].InnerText;
                        var Funklist = new List<string>();
                        var Altlist = new List<string>();
                        foreach (XmlNode child1 in singleItem.ChildNodes)
                        {
                          if (child1.Name == "PinSignal")
                            {
                                i = 0;
                                Funklist.Add(child1.Attributes["Name"].InnerText);
                                foreach (XmlNode child2 in child1.ChildNodes)
                                {
                                    
                                    if (child2.Name == "SpecificParameter")
                                    {
                                        foreach (XmlNode child3 in child2.ChildNodes)
                                        {
                                            if (child3.Name == "PossibleValue")
                                            {
                                                Altlist.Add(child3.InnerText);
                                                i = 1;
                                            }
                                        }
                                    }
                                }
                                if (i==0)
                                {
                                    Altlist.Add("");
                                }
                            }
                        }
                        funkliste = Funklist.ToArray();
                        altliste = Altlist.ToArray();
                        for(i=0;i<20;i++)
                        {
                            csvline[i] = "";
                        }
                        csvline[0] = "Port"+port.Remove(0,1);
                        string srch = pin;
                        if (srch.IndexOf("_") > 0)
                        {
                            srch = srch.Remove(srch.IndexOf("_"));
                        }
                        if (srch.IndexOf("-") > 0)
                        {
                            srch = srch.Remove(srch.IndexOf("-"));
                        }
                        string srchresult = searchadc(srch);
                        csvline[1] = pin;
                        csvline[17] = "EVENTOUT";
                        csvline[18] = srchresult;
                        for (i=0;i<funkliste.Count();i++)
                        {
                            string[] spl = altliste[i].Split('_');
                            int af = -1;
                            if (spl.Count()>0)
                            {
                                af = int.Parse(spl[1].Remove(0,2));
                                af = af + 2;
                                if (csvline[af]=="")
                                {
                                    csvline[af] = funkliste[i];
                                }
                                else
                                {
                                    csvline[af] = csvline[af] + "/" + funkliste[i];
                                }
                            }


                        }
                        //csvline speichern
                        string sline;
                        sline = "";
                        for (i = 0; i < 19; i++)
                        {
                            sline = sline + csvline[i] + ",";
                        }
                        using (System.IO.StreamWriter file = new System.IO.StreamWriter(@Zieldatei, true))
                        {
                            file.WriteLine(sline);
                        }
                    }

                }
            }
            csvPfad.Enabled = false;
            gpioladen.Enabled = false;
            chipload.Enabled = false;
            MessageBox.Show("Done");
        }

        private void csvPfad_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog objDialog = new FolderBrowserDialog();
            objDialog.Description = "csv Pfad";
            objDialog.SelectedPath = @"C:\";       // Vorgabe Pfad (und danach der gewählte Pfad)
            DialogResult objResult = objDialog.ShowDialog(this);
            if (objResult == DialogResult.OK)
            {
                csv.Text = objDialog.SelectedPath;
                chipload.Enabled = true;
            }
            else
                MessageBox.Show("Abbruch gewählt!");

        }

        private void pfadcube_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog objDialog = new FolderBrowserDialog();
            objDialog.Description = "STM32Cube Pfad";
            objDialog.SelectedPath = @"C:\";       // Vorgabe Pfad (und danach der gewählte Pfad)
            DialogResult objResult = objDialog.ShowDialog(this);
            if (objResult == DialogResult.OK)
            {
                cubepfad.Text = objDialog.SelectedPath;
                csvPfad.Enabled = true;
            }
            else
                MessageBox.Show("Abbruch gewählt!");
        }

        private void chipload_Click(object sender, EventArgs e)
        {
            string such = "<Core>";
            string ersetz1 = "<?xml version=\"1.0\" encoding =\"UTF - 8\" ?>;  <Mcu>";
            string ersetz2 = ersetz1.Replace(";", Environment.NewLine);
            byte[] suchb = StringToByteArray(such);
            byte[] ersetzb = StringToByteArray(ersetz2);
            int[] position;
            string[] adcliste;
            DialogResult result;
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.InitialDirectory = cubepfad.Text + "\\STM32CubeMX\\db\\mcu\\";
            openFile.Filter = "CubeMCU CPU files (STM32*.xml)|STM32*.xml";
            openFile.FilterIndex = 2;
            openFile.RestoreDirectory = true;
            result = openFile.ShowDialog();
            if (result == DialogResult.OK)
                chip.Text = openFile.FileName;
            if (result == DialogResult.Cancel)
            { chip.Text = ""; return; }
            if (chip.Text != "")
            {
                if (File.Exists(chip.Text))
                {
                    chipPfad = System.IO.Path.GetDirectoryName(chip.Text);
                    chipSuchPfad = System.IO.Path.GetFullPath(chipPfad);
                }
                else
                {
                    MessageBox.Show("Datei existiert nicht");
                    return;
                }
                fileBytes = File.ReadAllBytes(chip.Text);
                filezeiger = 0;
                magicString = new char[5];
                int i;
                for (i = 0; i < 5; i++)
                {
                    magicString[i] = (char)fileBytes[i];
                }
                filezeiger = filezeiger + i;
                string tmp1 = new string(magicString);
                string tmp2 = "<?xml";
                if (!(tmp1 == tmp2))
                {
                    MessageBox.Show("Kein xml!");
                    return;
                }
                else
                {
                    // patch original to work
                    position = Locate(fileBytes, suchb);
                    int p = position[0];
                    for (i = 0; i < ersetzb.Length; i++)
                    {
                        fileBytes[i] = ersetzb[i];
                    }
                    for (i = ersetzb.Length; i < (p - 1); i++)
                    {
                        fileBytes[i] = (byte)' ';
                    }
                    // patch done
                    string XMLfile = System.Text.Encoding.UTF8.GetString(fileBytes);
                    XmlDocument doc = new XmlDocument();
                    doc.Load(new StringReader(XMLfile));
                    i = 0;
                    var ADClist = new List<adcport>();
                    XmlNode r1 = doc.SelectSingleNode("/Mcu");
                    XmlNodeList listOfItems = doc.SelectNodes("/Mcu/Pin");
                    foreach (XmlNode singleItem in listOfItems)
                    {
                        string port = singleItem.Attributes["Name"].InnerText;
                        var Alist = new List<string>();
                        foreach (XmlNode child1 in singleItem.ChildNodes)
                        {
                            if (child1.Name == "Signal")
                            {
                                string signal = child1.Attributes["Name"].InnerText;
                                if (signal.Length > 3)
                                {
                                    string test = signal.Remove(3);
                                    if (test == "ADC")
                                    {
                                        if(signal.IndexOf("EXTI")==-1)
                                        Alist.Add(signal);
                                    }
                                }
                            }
                        }
                        adcliste = Alist.ToArray();
                        if(Alist.Count>0)
                        {
                            adcport adcport;
                            if (port.IndexOf("_")>0)
                            {
                                port = port.Remove(port.IndexOf("_"));
                            }
                            if (port.IndexOf("-") > 0)
                            {
                                port = port.Remove(port.IndexOf("-"));
                            }
                            adcport.Port = port;
                            string adsc="";
                            for (i = 0; i < Alist.Count(); i++)
                            {
                                adsc = adsc + adcliste[i];
                                if (i < (Alist.Count - 1))
                                    adsc = adsc + "/";
                            }
                            adcport.ADCs = adsc;
                            ADClist.Add(adcport);
                        }
                    }
                    adcports = ADClist.ToArray();
                    gpioladen.Enabled = true;
                }
            }
        }
    }
}

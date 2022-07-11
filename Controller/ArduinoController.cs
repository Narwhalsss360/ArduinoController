using System;
using System.IO.Ports;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NStreamCom;
using System.Reflection;

namespace Controller
{
    public partial class ArduinoController : Form
    {
        public delegate void CrossThreadSendToOutputDelegate(string Text);
        CrossThreadSendToOutputDelegate CrossThreadSendToOutput;

        enum DoComboBoxItemIndices
        {
            UPTIME_REQUEST,
            PIN_CONTROL
        }

        enum PinControlModeComboBoxIndices
        {
            PINMODE,
            DIGITALREAD,
            DIGITALWRITE,
            ANALOGREAD,
            ANALOGWRITE
        }

        enum ComIDs
        {
            UPTIME = 1,
            PIN_CONTROL
        }

        SerialPort ArduinoPort;
        FileStream DebugFile;

        public ArduinoController()
        {
            InitializeComponent();
            Load += OnFormLoad;
            FormClosing += onFormClosing;
            FormClosed += Closed;
            CrossThreadSendToOutput = new CrossThreadSendToOutputDelegate(SendToOutput);
            UseWaitCursor = true;
        }

        void SendToOutput(string Text)
        {
            OutputTextBox.AppendText(Text + Environment.NewLine);
        }

        private void OnFormLoad(object Sender, EventArgs Args)
        {
            ArduinoPort = new SerialPort();
            ArduinoPort.BaudRate = 1000000;
            ArduinoPort.Parity = Parity.None;
            ArduinoPort.StopBits = StopBits.One;
            ArduinoPort.DataReceived += DataReceived;
            UseWaitCursor = false;
        }

        private void PortSelectionComboBox_DropDown(object sender, EventArgs e)
        {
            UseWaitCursor = true;

            PortSelectionComboBox.Items.Clear();
            foreach (string PortName in SerialPort.GetPortNames())
            {
                PortSelectionComboBox.Items.Add(PortName);
            }

            UseWaitCursor = false;
        }

        private void PortConnectionButton_Click(object sender, EventArgs e)
        {
            if (ArduinoPort.IsOpen)
            {
                ArduinoPort.Close();
                PortConnectionButton.Text = "Connect";
                DoComboBox.Enabled = false;
                PinControlModeComboBox.Enabled = false;
                PinControlValueComboBox.Enabled = false;
                DoButton.Enabled = false;
                PinSelectTextBox.Enabled = false;
                SendToOutput("Disconnected from port.");
                PortSelectionComboBox.Enabled = true;
            }
            else
            {
                UseWaitCursor = true;
                ArduinoPort.PortName = PortSelectionComboBox.Text;
                try
                {
                    ArduinoPort.Open();
                    goto OpenSuccess;
                }
                catch (IOException Caught)
                {
                    MessageBox.Show(Caught.Message, "Caught Exception trying to open port.");
                }

                SendToOutput($"Unsuccessfuly tried to connect to port {PortSelectionComboBox.Text}");
                UseWaitCursor = false;
                return;
            OpenSuccess:
                PortSelectionComboBox.Enabled = false;
                DoComboBox.Enabled = true;
                PortConnectionButton.Text = "Disconnect";
                UseWaitCursor = false;
                SendToOutput($"Successfuly connected to port {PortSelectionComboBox.Text}");
            }
        }
        
        private void DoComboBox_DropDownClosed(object sender, EventArgs e)
        {
            switch ((DoComboBoxItemIndices)DoComboBox.SelectedIndex)
            {
                case DoComboBoxItemIndices.UPTIME_REQUEST:
                    PinControlModeComboBox.Enabled = false;
                    PinControlValueComboBox.Enabled = false;
                    PinSelectTextBox.Enabled = false;
                    DoButton.Enabled = true;
                    PinControlModeComboBox.Items.Clear();
                    PinControlValueComboBox.Items.Clear();
                    break;
                case DoComboBoxItemIndices.PIN_CONTROL:
                    PinControlModeComboBox.Enabled = true;
                    PinControlValueComboBox.Enabled = true;
                    PinSelectTextBox.Enabled = true;
                    DoButton.Enabled = false;
                    PinControlModeComboBox.Items.Clear();
                    PinControlValueComboBox.Items.Clear();
                    PinControlModeComboBox.Items.Add("pinMode");
                    PinControlModeComboBox.Items.Add("digitalRead");
                    PinControlModeComboBox.Items.Add("digitalWrite");
                    PinControlModeComboBox.Items.Add("analogRead");
                    PinControlModeComboBox.Items.Add("analogWrite");
                    break;
                default:
                    break;
            }
        }

        private void DoButton_Click(object sender, EventArgs e)
        {
            UseWaitCursor = true;
            switch ((DoComboBoxItemIndices)DoComboBox.SelectedIndex)
            {
                case DoComboBoxItemIndices.UPTIME_REQUEST:
                    byte[] UptimeRequestBytes = new byte[] { 1, 1, 1, 0, 0, 13, 10};
                    ArduinoPort.Write(UptimeRequestBytes, 0, UptimeRequestBytes.Length);
                    SendToOutput("Requested board's uptime.");
                    break;
                case DoComboBoxItemIndices.PIN_CONTROL:
                    byte SelectedPin;
                    if (!byte.TryParse(PinSelectTextBox.Text, out SelectedPin))
                    {
                        MessageBox.Show("Must enter a valid pin number from 0 -> 255.");
                    }
                    byte PinAction = (byte)PinControlModeComboBox.SelectedIndex;

                    ushort PinValue = 0;

                    if (PinControlModeComboBox.SelectedIndex == (int)PinControlModeComboBoxIndices.ANALOGWRITE)
                    {
                        if (!ushort.TryParse(PinControlValueComboBox.Text, out PinValue))
                        {
                            MessageBox.Show("Must enter a valid pin number from 0 -> 65535. (Or use 0/1 or 0 -> 1023)");
                        }
                    }
                    else
                    {
                        PinValue = (ushort)PinControlValueComboBox.SelectedIndex;
                    }

                    byte[] PinValueBytes = BitConverter.GetBytes(PinValue);

                    byte[] OutBytes = new byte[10] { 1, 4, 2, 0, PinAction, SelectedPin, 0, 0, 13, 10 };
                    Array.Copy(PinValueBytes, 0, OutBytes, 6, 2);

                    ArduinoPort.Write(OutBytes, 0, 10);
                    SendToOutput($"Did { PinControlModeComboBox.Text }, with { PinControlValueComboBox.Text } on pin { PinSelectTextBox.Text }.");
                    break;
                default:
                    break;
            }
            UseWaitCursor = false;
        }

        private void PinControlComboBox_DropDownClosed(object sender, EventArgs e)
        {
            DoButton.Enabled = false;
            PinControlValueComboBox.Items.Clear();
            switch ((PinControlModeComboBoxIndices)PinControlModeComboBox.SelectedIndex)
            {
                case PinControlModeComboBoxIndices.PINMODE:
                    PinControlValueComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
                    PinControlValueComboBox.Items.Add("INPUT");
                    PinControlValueComboBox.Items.Add("OUTPUT");
                    PinControlValueComboBox.Items.Add("INPUT_PULLUP");
                    break;
                case PinControlModeComboBoxIndices.DIGITALREAD:
                    DoButton.Enabled = true;
                    PinControlValueComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
                    PinControlValueComboBox.Items.Clear();
                    break;
                case PinControlModeComboBoxIndices.DIGITALWRITE:
                    PinControlValueComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
                    PinControlValueComboBox.Items.Add("LOW");
                    PinControlValueComboBox.Items.Add("HIGH");
                    break;
                case PinControlModeComboBoxIndices.ANALOGREAD:
                    DoButton.Enabled = true;
                    PinControlValueComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
                    PinControlValueComboBox.Items.Clear();
                    break;
                case PinControlModeComboBoxIndices.ANALOGWRITE:
                    DoButton.Enabled = true;
                    PinControlValueComboBox.DropDownStyle = ComboBoxStyle.DropDown;
                    PinControlValueComboBox.Items.Clear();
                    break;
                default:
                    break;
            }
        }

        private void PinControlValueComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            DoButton.Enabled = true;
        }

        private void ClearTextBoxButton_Click(object sender, EventArgs e)
        {
            OutputTextBox.Clear();
        }

        private void SaveTextButton_Click(object sender, EventArgs e)
        {
            UseWaitCursor = true;
            string Location = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            using (DebugFile = new FileStream($"{Location}\\Output.txt", FileMode.Create, FileAccess.Write))
            {
                byte[] OutputTextBoxBytes = Encoding.ASCII.GetBytes(OutputTextBox.Text);
                DebugFile.Write(OutputTextBoxBytes, 0, OutputTextBoxBytes.Length);
            }
            UseWaitCursor = false;
        }

        private void onFormClosing(object Sender, FormClosingEventArgs Args)
        {
            if (ArduinoPort.IsOpen)
            {
                var result = MessageBox.Show("Are you sure you want to close the program? Serial Port is still open, it will close automagically.", "Are you sure you want to close?", MessageBoxButtons.YesNo);
                if (result == DialogResult.No)
                {
                    Args.Cancel = true;
                    return;
                }
                else
                {
                    ArduinoPort.Close();
                }
            }
        }

        private void Closed(object Sender, FormClosedEventArgs Args)
        {
            if (ArduinoPort.IsOpen)
            {
                ArduinoPort.Close();
            }
        }

        private void DataReceived(object Sender, EventArgs Args)
        {
            UseWaitCursor = true;
            byte[] ReceivedBytes = new byte[((SerialPort)Sender).BytesToRead];
            ((SerialPort)Sender).Read(ReceivedBytes, 0, ((SerialPort)Sender).BytesToRead);

            NStreamData? ReceivedDataNullable = NStreamComParser.Parse(ReceivedBytes);
            if (ReceivedDataNullable == null)
            {
                this.Invoke(CrossThreadSendToOutput, "Received bad data.");
                return;
            }

            NStreamData ReceivedData = (NStreamData)ReceivedDataNullable;
            switch ((ComIDs)ReceivedData.ID)
            {
                case ComIDs.UPTIME:
                    uint BoardTime = BitConverter.ToUInt32((byte[])ReceivedData.Data, 0);
                    this.Invoke(CrossThreadSendToOutput, $"Received Board Time at {BoardTime}.");
                    break;
                case ComIDs.PIN_CONTROL:
                    byte Pin = ((byte[])ReceivedData.Data)[0];
                    ushort PinValue = BitConverter.ToUInt16((byte[])ReceivedData.Data, 1);
                    this.Invoke(CrossThreadSendToOutput, $"Received Pin {Pin} with value {PinValue}.");
                    break;
                default:
                    this.Invoke(CrossThreadSendToOutput, $"Received unkown ID {ReceivedData.ID}.");
                    break;
            }
            UseWaitCursor = false;
        }
    }
}
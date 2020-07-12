using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace uHUE_Tester
{
    public partial class Form1 : Form
    {
        const char COLOUR_TYPE_SOLID = 'c';
        const char COLOUR_TYPE_FADE = 'f';
        const char COLOUR_TYPE_BLINK = 'b';
        SerialPort port;
        MessageBuilder builder;

        readonly Color White33 = Color.FromArgb( 255 / 3, 255 / 3, 255 / 3 );

        public Form1()
        {
            InitializeComponent();

            btnOpenPort.Click += ( o, e ) => openPort();
            btnVersion.Click += ( o, e ) => requestVersion();
            btnRed.Click += ( o, e ) => setColour( Color.Red );
            btnGreen.Click += ( o, e ) => setColour( Color.Lime );
            btnBlue.Click += ( o, e ) => setColour( Color.Blue );
            btnWhite.Click += ( o, e ) => setColour( White33 );
            btnOff.Click += ( o, e ) => setColour( Color.Black );
            btnPretty.Click += ( o, e ) => startPretty();

            FormClosing += ( o, e ) => stopPretty();
        }

        protected override void OnLoad( EventArgs e )
        {
            base.OnLoad( e );
            btnRefreshPorts_Click( null, null );
        }
        private void btnRefreshPorts_Click( object sender, EventArgs e )
        {
            comboPorts.DataSource = SerialPort.GetPortNames();
        }

        void openPort()
        {
            builder = new MessageBuilder();
            txtLog.Clear();

            var portName = comboPorts.SelectedItem as string;
            if( string.IsNullOrWhiteSpace( portName ) ) {
                return;
            }

            try {
                if( port != null ) {
                    port.DataReceived -= Port_DataReceived;
                    port.Close();
                    port.Dispose();
                    port = null;
                }

                port = new SerialPort( portName ) {
                    BaudRate = 115200,
                    StopBits = StopBits.One,
                    Parity = Parity.None
                };
                port.DataReceived += Port_DataReceived;
                port.Open();

                AddLog( "Port open" );
                grpCommands.Enabled = true;
            }
            catch( Exception ex ) {
                MessageBox.Show( ex.ToString() );
            }
        }

        void AddLog( string s )
        {
            Invoke( (MethodInvoker)delegate {
                txtLog.AppendText( s );
                txtLog.AppendText( Environment.NewLine );
            } );
        }

        private void Port_DataReceived( object sender, SerialDataReceivedEventArgs e )
        {
            while( true ) {
                byte[] recv = new byte[1];

                // Try to get a byte.
                try {
                    if( port.Read( recv, 0, 1 ) == 0 ) {
                        return;
                    }
                }
                catch { }

                // Have a byte!
                if( builder.AddByte( recv[0] ) ) {
                    // Have a message!
                    handleMessage( builder.Message );
                }
            }
        }

        void requestVersion()
        {
            if( port == null ) {
                return;
            }

            var msg = new MessageByteList {
                (byte)'v'
            };

            writeMessage( msg );
        }

        void setColour( Color colour )
        {
            stopPretty();
            sendColour( colour );
        }

        void sendColour( Color colour )
        {
            if( port == null ) {
                return;
            }

            char colourType = COLOUR_TYPE_SOLID;
            if( radFade.Checked ) colourType = COLOUR_TYPE_FADE;
            else if( radBlink.Checked ) colourType = COLOUR_TYPE_BLINK;

            int cycleTimeDeciseconds = (int)numCycleTimeMs.Value / 100;

            var msg = new MessageByteList {
                (byte)colourType,
                colour.R,
                colour.G,
                colour.B,
                (byte)cycleTimeDeciseconds
            };

            writeMessage( msg );
        }

        void writeMessage( MessageByteList msg )
        {
            var bytes = msg.GetBytes();
            
            StringBuilder sb = new StringBuilder( "Send message: " );
            sb.Append( BitConverter.ToString( bytes ) );
            AddLog( sb.ToString() );

            port.Write( bytes, 0, bytes.Length );
        }

        void handleMessage( byte [] msg )
        {
                       
            StringBuilder sb = new StringBuilder( "New message: " );
            
            foreach( var b in msg ) {
                char c = (char)b;
                if( char.IsControl( c ) ) {
                    c = '.';
                }
                sb.Append( c );
            }

            sb.Append( "   " );

            sb.Append( BitConverter.ToString( msg ) );

            AddLog( sb.ToString() );
        }

        
        System.Threading.Timer prettyTimer;
        int prettyStep;
        const int PrettyModeStepIntervalMs = 50;

        void stopPretty()
        {
            if( prettyTimer != null ) {
                prettyTimer.Dispose();
                prettyTimer = null;
            }

            sendColour( Color.Black );
        }

        void startPretty()
        {
            stopPretty();

            prettyStep = 0;
            prettyTimer = new System.Threading.Timer( doPretty, (int)numCycleTimeMs.Value, 0, PrettyModeStepIntervalMs );
        }

        void doPretty( object state )
        {            
            int cycleTimeMs = (int)state;
            int totalCycleSteps = cycleTimeMs / PrettyModeStepIntervalMs;

            double hue = 360.0 * prettyStep / totalCycleSteps;

            sendColour( HSV.ToColor( hue, 1.0, 1.0 ) );

            prettyStep++;
            if( prettyStep == totalCycleSteps ) {
                prettyStep = 0;
            }
        }
    }
}

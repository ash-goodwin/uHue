using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uHUE_Tester
{
    class MessageBuilder
    {
        public const byte FRAME_START = 0xfe;
        public const byte FRAME_END = 0xff;
        public const byte FRAME_ESCAPE = 0xfd;
        public const byte FRAME_XOR = 0x20;


        byte[] buffer = new byte[300];
        int count = 0;
        bool started = false;
        bool escapeNext = false;


        public bool AddByte( byte rx )
        {
            if( rx == FRAME_START ) {
                started = true;
                count = 0;
                escapeNext = false;
            }
            else if( rx == FRAME_END ) {
                Message = new byte[count];
                Array.Copy( buffer, Message, count );

                started = false;
                count = 0;

                return true;
            }
            else if( rx == FRAME_ESCAPE ) {
                escapeNext = true;
            }
            else if( started ) {

                if( count == buffer.Length ) {
                    // Already filled buffer and next char wasn't the END flag.
                    // :(
                    started = false;
                    return false;
                }

                if( escapeNext ) {
                    rx ^= FRAME_XOR;
                    escapeNext = false;
                }

                buffer[count++] = rx;
            }

            return false;
        }

        public byte[] Message {get; private set; }
    }
}

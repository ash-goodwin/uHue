using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uHUE_Tester
{
    class MessageByteList : IEnumerable<byte>
    {
        public const byte FRAME_START = MessageBuilder.FRAME_START;
        public const byte FRAME_END = MessageBuilder.FRAME_END;
        public const byte FRAME_ESCAPE = MessageBuilder.FRAME_ESCAPE;
        public const byte FRAME_XOR = MessageBuilder.FRAME_XOR;

        List<byte> msg = new List<byte>();

        public IEnumerator<byte> GetEnumerator()
        {
            return msg.GetEnumerator();
        }

        public void Add( byte b )
        {
            if( b >= FRAME_ESCAPE ) {
                msg.Add( FRAME_ESCAPE );
                b ^= FRAME_XOR;
            }
            msg.Add( b );
        }

        public byte[] GetBytes()
        {
            byte[] bytes = new byte[2 + msg.Count];
            var mb = msg.ToArray();

            bytes[0] = FRAME_START;
            mb.CopyTo( bytes, 1 );
            bytes[bytes.Length - 1] = FRAME_END;

            return bytes;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}

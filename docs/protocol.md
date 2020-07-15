# Communication Protocol

## Framing
A very simple framed protocol is used to control the uHUE. All messages start and end with a special marker, as shown below.

Start |  Payload  |  End  
:----:|:---------:|:-----:
0xFE  |  message  |  0xFF

In addition to the 'start' and 'end' markers, there is also an 'escape' marker, whose value is 0xFD. Any time one of the marker values appears as a byte in the message, it is substituted with a two-byte "escape sequence" consisting of the 'escape' marker followed by the original byte value XORed with 0x20.

The three possible two-byte sequences are shown below.

Marker        |    Escaped Value    | Sequence
:------------:|:-------------------:|:---------:
Escape (0xFD) |  0xFD ^ 0x20 = 0xDD | 0xFD 0xDD
Start (0xFE)  |  0xFE ^ 0x20 = 0xDE | 0xFD 0xDE
End (0xFF)    |  0xFF ^ 0x20 = 0xDF | 0xFD 0xDF

## Message Format
The messages and their format are shown below.

### Get Version
To have the uHUE send back its firmware version information, send a lower-case 'v' ASCII character.

Start |  Payload   |  End  
:----:|:----------:|:-----:
0xFE  | 'v' (0x76) | 0xFF

### Set Colour
This message controls the colour and pattern being shown by the uHUE. The pattern shown is controlled byt sending:
- ASCII 'c' (0x63) to show a 'solid' colour (no blinking or fading).
- ASCII 'f' (0x66) to show a colour that fades from OFF to the specified colour and back to OFF over the cycle time.
- ASCII 'b' (0x62) to show a colour that blinks from OFF to the specified colour and OFF again over the cycle time.

The colour is specified using three bytes - one each for the red, green and blue components.

The cycle time, if applicable, is specified in the last byte. The unit of this field is 'deci-seconds' (i.e., the value 10 represents 1 second). When sending a 'solid' colour message, the cycle time field is optional; if present, it is ignored.

Start |  Pattern        | Red     | Green   | Blue    | Cycle Time |  End  
:----:|:---------------:|:-------:|:-------:|:-------:|:----------:|:-----:
0xFE  | 'c', 'f' or 'b' | 0 - 255 | 0 - 255 | 0 - 255 | 0 - 255 | 0xFF

#include "main.h"
#include "led.h"
#include "mcc_generated_files/mcc.h"

#define FRAME_FLAG_START            0xFE
#define FRAME_FLAG_END              0xFF
#define FRAME_FLAG_ESCAPE           0xFD
#define FRAME_ESCAPE_XOR            0x20

#define ACTIVITY_LED_BLINK_COUNT    (10)

static struct {
    uint8_t buffer[10];
    int     count;
    
    uint8_t started : 1;
    uint8_t escapeNext : 1;
} receive;

static uint8_t activityLedCount = 0;

struct s_Timing timing;

void doReceive( void );
void processMessage( void );
void sendMessage( uint8_t *data, int length );
void blinkActivityLed( void );
void doActivityLed( void );

void main(void)
{
    // initialize the device
    SYSTEM_Initialize();

    Led_Initialise();
    
    // Lock PPS
    PPSLOCK = 0x55;
    PPSLOCK = 0xAA;
    PPSLOCKbits.PPSLOCKED = 1;
    
    // Enable the Global Interrupts
    INTERRUPT_GlobalInterruptEnable();

    // Enable the Peripheral Interrupts
    INTERRUPT_PeripheralInterruptEnable();

    while( true ) {
        doReceive();
        
        if( timing.tenMsTick ) {
            timing.tenMsTick = false;
            
            Led_Process();
            doActivityLed();
            
            CLRWDT();
        }
    }
}


void doReceive( void )
{
    while( EUSART_is_rx_ready() ) {
        uint8_t rx = EUSART_Read();
        
        blinkActivityLed();
                
        if( rx == FRAME_FLAG_START ) {
            receive.started = true;
            receive.count = 0;
            receive.escapeNext = false;
        }
        else if( rx == FRAME_FLAG_END ) {
            processMessage();
            receive.started = false;
        }
        else if( rx == FRAME_FLAG_ESCAPE ) {
            receive.escapeNext = true;
        }
        else if( receive.started ) {
            
            if( receive.count == sizeof( receive.buffer ) ){
                // Already filled buffer and next char wasn't the END flag.
                // :(
                receive.started = false;
                continue;
            }
            
            if( receive.escapeNext ) {
                rx ^= FRAME_ESCAPE_XOR;
                receive.escapeNext = false;
            }            
            
            receive.buffer[ receive.count++ ] = rx;            
        }
        
    }
}

void processMessage( void )
{
    uint8_t * data = receive.buffer;
    int dataLength = receive.count;
    struct s_LedColour colour = {
        .red = data[1],
        .green = data[2],
        .blue = data[3]
    };
    
    //sendMessage( data, dataLength );
    
    // Request for firmware version.
    // Format:  'v'
    if( dataLength == 1 && data[0] == 'v' ) {
        uint8_t version[] = "uHUE by Ash, v1";
        sendMessage( version, sizeof( version ) - 1 );
    }
    // Set solid colour.
    // Format:  'c', {red}, {green}, {blue}, {optional: cycle period, which is ignored}
    else if( dataLength >= 4 && data[0] == 'c' ) {
        Led_SetColour( PATTERN_SOLID, 0, colour );
    }
    // Set fading colour.
    // Format:  'f', {red}, {green}, {blue}, {cycle period in deci-seconds}
    else if( dataLength == 5 && data[0] == 'f' ) {
        Led_SetColour( PATTERN_FADE, data[4], colour );
    }
    // Set blinking colour.
    // Format:  'b', {red}, {green}, {blue}, {cycle period in deci-seconds}
    else if( dataLength == 5 && data[0] == 'b' ) {
        Led_SetColour( PATTERN_BLINK, data[4], colour );
    }    
}

void sendMessage( uint8_t *data, int length )
{
    EUSART_Write( FRAME_FLAG_START );
    
    while( length-- ) {
        if( *data >= FRAME_FLAG_ESCAPE ) {
            EUSART_Write( FRAME_FLAG_ESCAPE );
            EUSART_Write( *data ^ FRAME_ESCAPE_XOR );
        }
        else {
            EUSART_Write( *data );
        }
        data++;
    }
    
    EUSART_Write( FRAME_FLAG_END );
}

void blinkActivityLed( void )
{
    activityLedCount = ACTIVITY_LED_BLINK_COUNT;
    IO_LED_SetLow();    
}

void doActivityLed( void )
{
    if( activityLedCount ) {
        if( --activityLedCount == 0 ) {
            IO_LED_SetHigh();
        }
    }
}
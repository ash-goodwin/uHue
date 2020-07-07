#include "main.h"
#include "led.h"
#include "mcc_generated_files/mcc.h"

#define TICKS_PER_DECISECOND        (TICKS_PER_SECOND / 10)
#define NUM_LEDS                    (4)


static struct {
    struct {
        enum e_LedPattern   pattern;
        uint16_t            patternTime;
        struct s_LedColour  colour;
    } Setting;
        
    uint16_t                 cycleTimer;    
} led;

static const struct s_LedColour ColourOff = { 0, 0, 0 };

static void _WriteColour( struct s_LedColour colour );
static struct s_LedColour _MixColours( struct s_LedColour colourA, struct s_LedColour colourB, uint16_t dividend, uint16_t divisor );



void Led_Initialise( void )
{
    SPI_Open( SPI_DEFAULT );
    _WriteColour( ColourOff );
}
        
void Led_SetColour( enum e_LedPattern pattern, uint8_t patternTime, struct s_LedColour colour )
{
    led.Setting.pattern = pattern;
    led.Setting.patternTime = (uint16_t)patternTime * TICKS_PER_DECISECOND;
    led.Setting.colour = colour;    
    
    // Reset fade/blink timer
    led.cycleTimer = 0;
    
    switch( led.Setting.pattern ){
        case PATTERN_SOLID:
            // Just set the colour and done!
            _WriteColour( led.Setting.colour );
            break;

        case PATTERN_FADE:
            // Start with light off.
            _WriteColour( ColourOff );
            break;

        case PATTERN_BLINK:
            // Start with selected colour.
            _WriteColour( led.Setting.colour );
            break;
    } 
}


void Led_Process( void )
{       
    // Increase cycle timer.
    led.cycleTimer += 1;
    if( led.cycleTimer >= led.Setting.patternTime ) {
        led.cycleTimer = 0;
    }
    
    // Update light with new colour based on new position in cycle.v
    switch( led.Setting.pattern ){
        case PATTERN_SOLID:
            // Do nothing, colour doesn't change over time.
            break;

        case PATTERN_FADE: {
            // At time 0, the colour is 'off'.
            // Up to time (period / 2), the colour slowly becomes 'setting'.
            // Up to time (period), the colour slowly becomes 'off'.
            uint16_t halfPeriod = led.Setting.patternTime / 2;
            if( led.cycleTimer < halfPeriod ) {
                _WriteColour( _MixColours( ColourOff, led.Setting.colour, led.cycleTimer, halfPeriod ) );
            }
            else {
                uint16_t timeInSecondHalf = led.cycleTimer - halfPeriod;
                _WriteColour( _MixColours( led.Setting.colour, ColourOff, timeInSecondHalf, halfPeriod ) );
            }
            break;
        }

        case PATTERN_BLINK:
            // Whilst time < (period / 2), the colour is 'setting'.
            // For (period / 2) < time < period, the colour is 'off'.
            
            if( led.cycleTimer == 0 ) {
                _WriteColour( led.Setting.colour );
            }
            else if( led.cycleTimer == (led.Setting.patternTime / 2) ) {
                _WriteColour( ColourOff );
            }
            break;
    } 
}

static void _WriteColour( struct s_LedColour colour )
{
    int numLeds = NUM_LEDS;
    while( numLeds-- ) {
        (void) SPI_ExchangeByte( colour.green );
        (void) SPI_ExchangeByte( colour.red );
        (void) SPI_ExchangeByte( colour.blue );
    }
}

static uint8_t _Mix( uint8_t valueA, uint8_t valueB, uint16_t dividend, uint16_t divisor )
{
    if( dividend == 0 ) {
        return valueA;
    }
    else if( dividend >= divisor ) {
        return valueB;
    }
    
    uint8_t t = (((uint32_t) valueB) * dividend) / divisor;
    
    dividend = divisor - dividend;
    uint8_t u = (((uint32_t) valueA) * dividend) / divisor;
    
    return t + u;
}

static struct s_LedColour _MixColours( struct s_LedColour colourA, struct s_LedColour colourB, uint16_t dividend, uint16_t divisor )
{
    struct s_LedColour res = {
        .red = _Mix( colourA.red, colourB.red, dividend, divisor ),
        .green = _Mix( colourA.green, colourB.green, dividend, divisor ),
        .blue = _Mix( colourA.blue, colourB.blue, dividend, divisor ),
    };
    
    return res;
}
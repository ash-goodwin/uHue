#ifndef LED_H
#define	LED_H

#include <stdint.h>

enum e_LedPattern {
    PATTERN_SOLID,
    PATTERN_BLINK,
    PATTERN_FADE
};

struct s_LedColour {
    uint8_t red, green, blue;
};

void Led_Initialise( void );
void Led_Process( void );
void Led_SetColour( enum e_LedPattern pattern, uint8_t patternTime, struct s_LedColour colour );


#endif	/* LED_H */


/* 
 * File:   main.h
 * Author: ash
 *
 * Created on June 25, 2020, 6:47 PM
 */

#ifndef MAIN_H
#define	MAIN_H

#include <stdint.h>

#define TICKS_PER_SECOND        (100)

struct s_Timing {
    uint8_t    tenMsTick : 1;
};

extern struct s_Timing timing;


#endif	/* MAIN_H */

